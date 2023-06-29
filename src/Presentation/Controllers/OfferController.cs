using apollo.Application.Agents.Commands.SavePropertyAgent;
using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;
using apollo.Application.OfferGeneration.Queries.DTOs;
using apollo.Application.Properties.Queries.GetProperties;
using apollo.Domain.Enums;
using apollo.Presentation.Controllers.Base;
using apollo.Presentation.Utils;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace apollo.Presentation.Controllers
{
    public class OfferController : APIBaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IEmailService _emailService;

        public OfferController(IWebHostEnvironment hostingEnvironment, IEmailService emailService)
        {
            _hostingEnvironment = hostingEnvironment;
            _emailService = emailService;
        }
        [Authorize(Roles = "Superadmin")]
        [HttpGet("options")]
        [ProducesResponseType(typeof(List<OfferOptionListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBuildingsWithPagination([FromQuery] GetOfferOptionsListQuery query)
        {
            var response = new Response<List<OfferOptionListDTO>>();

            try
            {
                var result = await Mediator.Send(query);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [AllowAnonymous]
        [HttpGet("export-offers")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportOffer([FromQuery] ExportOfferOptionsRequest request)
        {
            try
            {
                var unitsData = await Mediator.Send(new GetSelectedUnitsForExportQuery
                {
                    AgentID = request.AgentID,
                    ContactID = request.ContactID,
                    UnitIds = request.UnitIds,
                });

                if (unitsData != null)
                {
                    if (unitsData.Units != null && unitsData.Units.Any() && request.TemplateType == TemplateType.Powerpoint)
                    {
                        MemoryStream stream = GetOfferFileStream(unitsData, request.SubMarketIds != null && request.SubMarketIds.Any());
                        string fileName = String.Format("{0}-StackingPlan-{1}.pptx", unitsData.ContactName, DateTime.Now.ToString("MM/dd/yyyy"));
                        string fileType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                        stream.Position = 0;
                        return File(stream, fileType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }


        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpPost("send-offer-in-email")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendOffer([FromBody] SendOfferOptionsInEmailRequest request)
        {
            var response = new Response<bool>();

            try
            {

                var unitsData = await Mediator.Send(new GetSelectedUnitsForExportQuery
                {
                    AgentID = request.AgentID,
                    ContactID = request.ContactID,
                    UnitIds = request.UnitIds,
                });

                if (unitsData != null)
                {
                    if (unitsData.Units != null && unitsData.Units.Any() && request.TemplateType == TemplateType.Powerpoint)
                    {
                        MemoryStream stream = GetOfferFileStream(unitsData, request.SubMarketIds != null && request.SubMarketIds.Any());
                        string fileName = String.Format("{0}-StackingPlan-{1}.pptx", unitsData.ContactName, DateTime.Now.ToString("MM/dd/yyyy"));
                        var emailResponse = await _emailService.SendEmail(new EmailModel
                        {
                            To = request.ToEmail,
                            Body = request.Message,
                            Subject = request.Subject,
                            Copy = request.CcEmail
                        }, fileName, stream.ToArray());

                        if (emailResponse)
                        {
                            var result = await Mediator.Send(request);
                            response.Data = result;
                            response.Message = "Offer sent successfully.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while sending the offer.";
            }

            return HandleResponse(response);
        }


        #region Helpers

        private MemoryStream GetOfferFileStream(SelectedOfferUnitsDTO unitsData, bool isGroupedBySubmarket)
        {
            var contentRootPath = Path.Combine(_hostingEnvironment.WebRootPath, "Resources");
            var stream = new MemoryStream();
            using (Stream source = System.IO.File.OpenRead(contentRootPath + @"/TestTemplate.pptx"))
            {
                byte[] buffer = new byte[2048];
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, bytesRead);
                }
            }

            using (PresentationDocument doc = PresentationDocument.Open(stream, true))
            {
                OpenXmlValidator validator = new OpenXmlValidator();
                // Get the presentation part of the document.
                PresentationPart presentationPart = doc.PresentationPart;

                ViewProperties viewProperties = presentationPart.ViewPropertiesPart.ViewProperties;
                viewProperties.LastView = ViewValues.SlideView;

                if (!string.IsNullOrWhiteSpace(unitsData.ContactName))
                {
                    var name = unitsData.ContactName.Split(" ");
                    if (name.Length > 1)
                    {
                        OfferSlideHelper.UpdateClientNameInFirstSlide(presentationPart, name[0], name[1]);
                    }
                }

                if (unitsData.Agent != null)
                {
                    OfferSlideHelper.InsertAgentDetails(presentationPart, unitsData.Agent);
                }

                var groupedUnits = unitsData.Units.GroupBy(x => new
                {
                    Name = isGroupedBySubmarket ? x.Address.SubMarketName : x.Address.CityName
                });
                int index = 3;
                foreach (var unit in groupedUnits)
                {
                    var buildingNames = unit.Select(x => x.BuildingName).Distinct();
                    var coordinates = unit.Select(x => new { x.Address.Longitude, x.Address.Latitude }).ToArray().Distinct();
                    if (coordinates != null && coordinates.Any())
                    {
                        var coordinatesValues = coordinates.Select(o => new[] { o.Longitude, o.Latitude }).ToArray();
                        var address = unit.FirstOrDefault().Address;
                        if (address != null && buildingNames.Any())
                        {
                            SlidePart citySlide = OfferSlideHelper.GetCitySlide(doc, unit.Key.Name, isGroupedBySubmarket ? "" : "City");
                            if (citySlide != null)
                            {
                                PowerPointUtils.SetSlideID(doc.PresentationPart, citySlide, index);
                                index++;
                            }

                            SlidePart optionsSlide = OfferSlideHelper.GetOptionsSlideByCity(doc, unit.Key.Name, isGroupedBySubmarket ? "" : "City", buildingNames, address.Latitude, address.Longitude, coordinatesValues);
                            if (optionsSlide != null)
                            {
                                PowerPointUtils.SetSlideID(doc.PresentationPart, optionsSlide, index);
                                index++;
                            }
                        }

                        var groupUnitByBuilding = unit.GroupBy(x => x.BuildingName);

                        foreach (var buildingUnit in groupUnitByBuilding)
                        {
                            if (buildingUnit != null && buildingUnit.Any())
                            {
                                var unitNames = string.Join(",", buildingUnit.Select(x => string.IsNullOrWhiteSpace(x.Name) ? x.UnitNumber : x.Name).ToList());

                                var slideBuildingAndLeaseData = GetSlideBuildingAndLeaseData(buildingUnit.FirstOrDefault(), unitNames);

                                if (slideBuildingAndLeaseData != null && slideBuildingAndLeaseData.Any())
                                {
                                    var mapUrl = GenerateGoogleMapsLink(buildingUnit.FirstOrDefault().Address.Line1, buildingUnit.FirstOrDefault().Address.Latitude, buildingUnit.FirstOrDefault().Address.Longitude);
                                    SlidePart unitSlide = OfferSlideHelper.GetUnitSlide(doc, isGroupedBySubmarket ? unit.Key.Name : unit.Key.Name + " City", buildingUnit.Key, buildingUnit.FirstOrDefault().MainImage, buildingUnit.FirstOrDefault().FloorPlanImage, slideBuildingAndLeaseData, buildingUnit.FirstOrDefault().Address.Line1, mapUrl);
                                    if (unitSlide != null)
                                    {
                                        PowerPointUtils.SetSlideID(doc.PresentationPart, unitSlide, index);
                                        index++;
                                    }
                                }
                            }
                        }
                    }

                    var errors = validator.Validate(doc);
                }
            }

            return stream;
        }

        public static string GenerateGoogleMapsLink(string address, double latitude, double longitude)
        {
            string encodedPlaceName = Uri.EscapeDataString(address);
            string mapLink = "https://www.google.com/maps/place/" + encodedPlaceName + "/@" + latitude + "," + longitude + "," + 17 + "z";
            return mapLink;
        }

        private static List<Dictionary<string, string>> GetSlideBuildingAndLeaseData(UnitDetailForOfferExportDTO data, string unitNames)
        {
            var response = new List<Dictionary<string, string>>
            {
               new Dictionary<string, string>
               {
                   {"OWNER / DEVELOPER",data.DeveloperName },
                   {"AVAILABLE DATE",data.AvailabilityName }
               },

               new Dictionary<string, string>
               {
                   {"YEAR COMPLETED / BUILDING CLASS",$"{data.YearBuilt} / {data.GradeName}" },
                   {"UNIT / FLOOR", unitNames }
               },

               new Dictionary<string, string>
               {
                   {"PEZA STATUS",data.PEZAStatusName },
                   {"FLOOR AREA (SQM)",string.Format("{0:N}", data.LeaseFloorArea) }
               },

               new Dictionary<string, string>
               {
                   {"TOTAL BUILDING SIZE (SQM)",string.Format("{0:N}", data.GrossBuildingSize) },
                   {"BASE RENT / SQM (Php)",string.Format("{0:N}", data.BasePrice) }
               },

               new Dictionary<string, string>
               {
                   {"TYPICAL FLOOR PLATE (SQM)",string.Format("{0:N}", data.TypicalFloorPlateSize) },
                   {"ANNUAL ESCALATION",data.EscalationRate+"%" }
               },

               new Dictionary<string, string>
               {
                   {"EFFICIENCY RATE",data.EfficiencyRatio+"%" },
                   {"MONTHLY DUES / SQM (Php)",string.Format("{0:N}", data.BasePrice) }
               },

               new Dictionary<string, string>
               {
                   {"CEILING HEIGHT (m)",data.CeilingHeight },
                   {"AC CHARGES / SQM (Php)",data.ACCharges }
               },

               new Dictionary<string, string>
               {
                   {"BACK-UP POWER",data.Power+"%" },
                   {"HANDOVER CONDITION",data.HandOverConditionName }
               },

               new Dictionary<string, string>
               {
                   {"AC SYSTEM",data.ACSystem },
                   {"MINIMUM LEASE TERM",data.MinimumLeaseTerm.ToString() + " Months" }
               },

               new Dictionary<string, string>
               {
                   {"TELECOMMUNICATION",data.Telcos },
                   {"AMENITIES",data.Amenities }
               },

               new Dictionary<string, string>
               {
                   {"FLOORS / ELEVATORS",data.TotalFloors +" / "+ data.Elevators },
                   {"TRANSPORTATION","N/A" }
               },

               new Dictionary<string, string>
               {
                   {"DENSITY RATIO",data.DensityRatio.ToString() },
                   {"PARKING RENT / SLOT (Php)",data.ParkingRent.ToString() }
               },
            };
            return response;
        }
        #endregion

    }
}

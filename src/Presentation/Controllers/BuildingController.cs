using apollo.Application.Buildings.Queries.DTOs;
using apollo.Application.Buildings.Queries.GetBuildingByID;
using apollo.Application.Buildings.Queries.GetBuildingList;
using apollo.Application.Common.Models;
using apollo.Application.Floors.Commands.CreateFloor;
using apollo.Application.Floors.Queries.DTOs;
using apollo.Application.Floors.Queries.GetFloorsByBuildingID;
using apollo.Application.Units.Commands.CreateUnit;
using apollo.Application.Units.Queries.DTOs;
using apollo.Application.Units.Queries.GetUnitByID;
using apollo.Presentation.Controllers.Base;
using apollo.Presentation.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Shared.DTO.Response;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace apollo.Presentation.Controllers
{
    public class BuildingController : APIBaseController
    {
        [Authorize(Roles = "Superadmin")]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<SimpleBuildingDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBuildingsWithPagination([FromQuery] GetBuildingListQuery query)
        {
            var response = new Response<PaginatedList<SimpleBuildingDTO>>();

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
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BuildingDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBuildingByID(int id)
        {
            var response = new Response<BuildingDTO>();

            try
            {
                var result = await Mediator.Send(new GetBuildingByIDQuery { ID = id });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("{Id:int}/floors")]
        [ProducesResponseType(typeof(List<FloorShortDetailDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFloorsByBuildingID([FromQuery] GetFloorsByBuildingIDQuery query)
        {
            var response = new Response<List<FloorShortDetailDTO>>();

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
        [HttpGet("{Id:int}/units")]
        [ProducesResponseType(typeof(PaginatedList<UnitShortDetailDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitsByBuildingID([FromQuery] GetUnitsByBuildingIDQuery query)
        {
            var response = new Response<PaginatedList<UnitShortDetailDTO>>();

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
        [HttpGet("floor/{id}")]
        [ProducesResponseType(typeof(FloorDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFloorByID(int id)
        {
            var response = new Response<FloorDTO>();

            try
            {
                var result = await Mediator.Send(new GetFloorByIDQuery { Id = id });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("unit/{id}")]
        [ProducesResponseType(typeof(UnitDetailDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitByID(int id)
        {
            var response = new Response<UnitDetailDTO>();

            try
            {
                var result = await Mediator.Send(new GetUnitByIDQuery { Id = id });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }


        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpPost("floor/save")]
        [ProducesResponseType(typeof(SaveEntityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveFloor([FromBody] SaveFloorRequest request)
        {
            var response = new Response<SaveEntityResult>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Floor detail saved successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the floor details.";
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpPost("unit/save")]
        [ProducesResponseType(typeof(SaveEntityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveUnit([FromBody] SaveUnitRequest request)
        {
            var response = new Response<SaveEntityResult>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Unit detail saved successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the unit details.";
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpPost("units/save")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveUnits([FromBody] UnitBulkSaveRequest request)
        {
            var response = new Response<bool>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Units detail saved successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the unit details.";
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("stackingplan/summary/{id}")]
        [ProducesResponseType(typeof(IEnumerable<BuildingSummaryDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBuildingSummary(int id)
        {
            var response = new Response<IEnumerable<BuildingSummaryDTO>>();

            try
            {
                var result = await Mediator.Send(new GetBuildingSummaryQuery { Id = id });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("stackingplan/export/{id}")]
        public async Task<IActionResult> ExportBuildingStackingplan(int id)
        {
            try
            {
                var stackingplanData = await Mediator.Send(new GetStackingPlanDataFroExportQuery { Id = id });
                if (stackingplanData.Count() > 0)
                {
                    var stream = new MemoryStream();
                    // Creating an instance 
                    // of ExcelPackage 
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage package = new ExcelPackage(stream))
                    {

                        // name of the sheet 
                        var workSheet = package.Workbook.Worksheets.Add("Stacking Plan");

                        // setting the properties 
                        // of the work sheet  
                        workSheet.TabColor = Color.DarkBlue;
                        workSheet.DefaultRowHeight = 12;

                        // Setting the properties 
                        // of the first row 
                        workSheet.Row(1).Height = 20;
                        workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Row(1).Style.Font.Bold = true;

                        // Header of the Excel sheet 
                        workSheet.Cells[1, 1].Value = "Unit ID";
                        workSheet.Cells[1, 2].Value = "Building ID";
                        workSheet.Cells[1, 3].Value = "Unit Name";
                        workSheet.Cells[1, 4].Value = "Building Name";
                        workSheet.Cells[1, 5].Value = "Status";
                        workSheet.Cells[1, 6].Value = "Unit Number";
                        workSheet.Cells[1, 7].Value = "Floor Location";
                        workSheet.Cells[1, 8].Value = "Leasable Floor Area \n (in SQM)";
                        workSheet.Cells[1, 9].Value = "Listing Type";
                        workSheet.Cells[1, 10].Value = "Base Price \n per SQM (PHP)";
                        workSheet.Cells[1, 11].Value = "CUSA per SQM (PHP)";
                        workSheet.Cells[1, 12].Value = "Handover Condition";
                        workSheet.Cells[1, 13].Value = "AC Charges (PHP)";
                        workSheet.Cells[1, 14].Value = "AC Extension Charges\n (PHP)";
                        workSheet.Cells[1, 15].Value = "Escalation Rate";
                        workSheet.Cells[1, 16].Value = "Minimum Lease Term";
                        workSheet.Cells[1, 17].Value = "Parking Rent per slot \n (PHP)";
                        workSheet.Cells[1, 18].Value = "Handover Date";
                        workSheet.Cells[1, 19].Value = "Notes";
                        workSheet.Cells[1, 20].Value = "Contact Person";
                        workSheet.Cells[1, 21].Value = "Contact Phone";
                        workSheet.Cells[1, 22].Value = "Contact Email";
                        workSheet.Cells[1, 23].Value = "Company Name";
                        workSheet.Cells[1, 24].Value = "Broker Name";
                        workSheet.Cells[1, 25].Value = "Tenant Name";
                        workSheet.Cells[1, 26].Value = "Tenant Start Date";
                        workSheet.Cells[1, 27].Value = "Tenant End Date";
                        workSheet.Cells[1, 28].Value = "Closing Rate";
                        workSheet.Cells[1, 29].Value = "Tenant Classification";
                        workSheet.Cells[1, 30].Value = "Estimated Area";
                        workSheet.Cells[1, 31].Value = "Lease Term";
                        workSheet.Cells[1, 32].Value = "Is Historical?";

                        //Header Title style
                        using (ExcelRange headings = workSheet.Cells[1, 1, 1, 32])
                        {
                            var fill = headings.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(Color.FromArgb(0, 24, 68));
                            headings.Style.Font.Color.SetColor(Color.White);
                        }


                        // Inserting the data into excel 
                        // sheet by using the for each loop 
                        // As we have values to the first row  
                        // we will start with second row 
                        int recordIndex = 2;
                        stackingplanData = stackingplanData.OrderBy(u => u.FloorName, new SemiNumericComparer()).OrderBy(x => (x.FloorName.ToLower().Contains("ph") || x.FloorName.ToLower().Contains("penthouse")) ? 1 : 2).ToList();

                        foreach (var stackingPlan in stackingplanData)
                        {
                            workSheet.Cells[recordIndex, 1].Value = stackingPlan.ID;
                            using (ExcelRange rng = workSheet.Cells[recordIndex, 1])
                            {
                                rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }
                            workSheet.Cells[recordIndex, 2].Value = stackingPlan.BuildingID;
                            using (ExcelRange rng = workSheet.Cells[recordIndex, 2])
                            {
                                rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }

                            workSheet.Cells[recordIndex, 3].Value = stackingPlan.Name;
                            workSheet.Cells[recordIndex, 4].Value = stackingPlan.BuildingName;
                            workSheet.Cells[recordIndex, 5].Value = stackingPlan.UnitStatusName;
                            workSheet.Cells[recordIndex, 6].Value = stackingPlan.UnitNumber;
                            workSheet.Cells[recordIndex, 7].Value = stackingPlan.FloorName;
                            workSheet.Cells[recordIndex, 8].Value = stackingPlan.LeaseFloorArea;
                            workSheet.Cells[recordIndex, 9].Value = stackingPlan.ListingTypeName;
                            workSheet.Cells[recordIndex, 10].Value = stackingPlan.BasePrice;
                            workSheet.Cells[recordIndex, 11].Value = stackingPlan.CUSA;
                            workSheet.Cells[recordIndex, 12].Value = stackingPlan.HandOverConditionName;
                            workSheet.Cells[recordIndex, 13].Value = stackingPlan.ACCharges;
                            workSheet.Cells[recordIndex, 14].Value = stackingPlan.ACExtensionCharges;
                            workSheet.Cells[recordIndex, 15].Value = stackingPlan.EscalationRate;
                            workSheet.Cells[recordIndex, 16].Value = stackingPlan.MinimumLeaseTerm;
                            workSheet.Cells[recordIndex, 17].Value = stackingPlan.ParkingRent;
                            workSheet.Cells[recordIndex, 18].Value = stackingPlan.HandOverDate != null ? stackingPlan.HandOverDate.Value.ToString("MM/dd/yyyy") : "";
                            workSheet.Cells[recordIndex, 19].Value = stackingPlan.Notes;
                            workSheet.Cells[recordIndex, 20].Value = stackingPlan.ContactName;
                            workSheet.Cells[recordIndex, 21].Value = stackingPlan.ContactPhoneNumber;
                            workSheet.Cells[recordIndex, 22].Value = stackingPlan.ContactEmail;
                            workSheet.Cells[recordIndex, 23].Value = stackingPlan.CompanyName;
                            workSheet.Cells[recordIndex, 24].Value = stackingPlan.BrokerName;
                            workSheet.Cells[recordIndex, 25].Value = stackingPlan.TenantName;
                            workSheet.Cells[recordIndex, 26].Value = stackingPlan.StartDate != null ? stackingPlan.StartDate.Value.ToString("MM/dd/yyyy") : "";
                            workSheet.Cells[recordIndex, 27].Value = stackingPlan.EndDate != null ? stackingPlan.EndDate.Value.ToString("MM/dd/yyyy") : "";
                            workSheet.Cells[recordIndex, 28].Value = stackingPlan.ClosingRate;
                            workSheet.Cells[recordIndex, 29].Value = stackingPlan.TenantClassification;
                            workSheet.Cells[recordIndex, 30].Value = stackingPlan.EstimatedArea;
                            workSheet.Cells[recordIndex, 31].Value = stackingPlan.LeaseTerm;
                            workSheet.Cells[recordIndex, 32].Value = stackingPlan.IsHistorical;

                            //To Wrap text for description
                            using (ExcelRange rng = workSheet.Cells[recordIndex, 32])
                            {
                                rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                rng.Style.WrapText = true;
                            }
                            recordIndex++;
                        }
                        // By default, the column width is not  
                        // set to auto fit for the content 
                        // of the range, so we are using 
                        // AutoFit() method here.  
                        int[] excludeCols = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 27, 28, 29, 30, 31, 32 };
                        for (int i = 1; i <= 32; i++)
                        {
                            if (!excludeCols.Contains(i)) //
                                workSheet.Column(i).AutoFit();
                            else
                            {
                                workSheet.Column(i).Width = 20;
                            }
                        }

                        workSheet.Column(21).Width = 35;
                        workSheet.Column(22).Width = 35;

                        package.Save();
                    }

                    string fileName = String.Format("{0}-StackingPlan-{1}.xlsx", stackingplanData.FirstOrDefault().BuildingName, DateTime.Now.ToString("MM/dd/yyyy"));
                    string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    stream.Position = 0;
                    return File(stream, fileType, fileName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();

        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpPost("stackingplan/import")]
        [ProducesResponseType(typeof(IEnumerable<BuildingSummaryDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportBuildingStackingPlan(IFormFile file)
        {
            var response = new Response<ImportStackingPlanDetailDTO>();

            try
            {
                IEnumerable<ExportStackingPlanDTO> records = new List<ExportStackingPlanDTO>();

                if (file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".xlsx"))
                {
                    var excelReader = new ExcelHelper();
                    records = await excelReader.GetExcelData(file);
                }
                else if (file.FileName.EndsWith(".csv"))
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = true,
                        HeaderValidated = null,
                        MissingFieldFound = null,
                        IgnoreBlankLines = false
                    };

                    using var reader = new StreamReader(file.OpenReadStream());
                    using var csv = new CsvReader(reader, config);
                    csv.Configuration.RegisterClassMap<ExportStackingPlanDTOMap>();
                    records = csv.GetRecords<ExportStackingPlanDTO>();
                }

                if (records.Any())
                {
                    var result = await Mediator.Send(new ImportUnitRequest { Units = records.ToList() });
                    response.Message = $"File with {result.Total} record processed successfully, Imported {result.ImportedCount} records and failed count is {result.FailedCount}";
                }
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }
    }
}

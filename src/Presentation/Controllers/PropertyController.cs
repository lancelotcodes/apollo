using apollo.Application.Agents.Queries.DTOs;
using apollo.Application.Agents.Queries.GetAgents;
using apollo.Application.Buildings.Commands.SavePropertyBuilding;
using apollo.Application.Buildings.Queries.DTOs;
using apollo.Application.Buildings.Queries.GetPropertyBuilding;
using apollo.Application.Common.Models;
using apollo.Application.Contracts.Commands;
using apollo.Application.Contracts.Queries;
using apollo.Application.Contracts.Queries.DTOs;
using apollo.Application.Documents.Commands.SavePropertyDocuments;
using apollo.Application.Documents.Commands.SavePropertyVideo;
using apollo.Application.Documents.Queries;
using apollo.Application.Documents.Queries.DTO;
using apollo.Application.Documents.Queries.GetPropertyVideo;
using apollo.Application.Properties.Commands.ConfirmProperty;
using apollo.Application.Properties.Commands.DeleteProperty;
using apollo.Application.Properties.Commands.SaveProperty;
using apollo.Application.Properties.Queries.DTOs;
using apollo.Application.Properties.Queries.GetProperties;
using apollo.Application.Properties.Queries.GetProperty;
using apollo.Application.PropertyAddress.Commands.SavePropertyAddress;
using apollo.Application.PropertyAddress.Queries;
using apollo.Application.PropertyAddress.Queries.DTOs;
using apollo.Application.PropertyMandate.Commands.SavePropertyMandate;
using apollo.Application.PropertyMandate.Queries;
using apollo.Application.PropertyMandate.Queries.DTOs;
using apollo.Application.PropertySEO.Commands.SavePropertySEO;
using apollo.Application.PropertySEO.Queries;
using apollo.Application.PropertySEO.Queries.DTOs;
using apollo.Application.PropertyTypes.Queries.DTOs;
using apollo.Application.PropertyTypes.Queries.GetPropertyTypes;
using apollo.Presentation.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apollo.Presentation.Controllers
{
    public class PropertyController : APIBaseController
    {
        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("map")]
        [ProducesResponseType(typeof(IEnumerable<MapPropertyListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProperties([FromQuery] GetPropertiesQuery query)
        {
            var response = new Response<IEnumerable<MapPropertyListDTO>>();

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
        [HttpPost("save")]
        [ProducesResponseType(typeof(SaveEntityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveProperty([FromBody] SavePropertyRequest request)
        {
            var response = new Response<SaveEntityResult>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Property details saved successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the property details.";
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("map/{Id}")]
        [ProducesResponseType(typeof(MapPropertyDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMapProperty([FromRoute] GetMapPropertyByIdQuery query)
        {
            var response = new Response<MapPropertyDTO>();

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
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(PropertyDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProperty([FromRoute] GetPropertyByIdQuery query)
        {
            var response = new Response<PropertyDTO>();

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
        [HttpGet("building/{Id}")]
        [ProducesResponseType(typeof(PropertyBuildingDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBuilding([FromRoute] GetBuildingByPropertyIdQuery query)
        {
            var response = new Response<PropertyBuildingDTO>();

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
        [HttpPost("building/save")]
        [ProducesResponseType(typeof(SaveEntityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SavePropertyBuilding([FromBody] SavePropertyBuildingRequest request)
        {
            var response = new Response<SaveEntityResult>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Building details saved successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the building details.";
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpPost("mandate/save")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> SavePropertyMandate([FromBody] List<PropertyMandateRequest> request)
        {
            var response = new Response<bool>();

            try
            {
                var result = await Mediator.Send(new SavePropertyMandateRequest { Mandates = request });
                response.Data = result;
                response.Message = "Mandate details saved successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the mandate details.";
            }

            return HandleResponse(response);
        }


        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpPost("documents/save")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> SavePropertyDocuments([FromBody] SavePropertyDocumentRequest request)
        {
            var response = new Response<bool>();

            try
            {
                request.ValidateActive();
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Media info saved successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the media.";
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpPost("video/save")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> SavePropertyVideo([FromBody] SavePropertyVideoRequest request)
        {
            var response = new Response<bool>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Video saved successfully.";

            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the video.";
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("video/{Id}")]
        [ProducesResponseType(typeof(PropertyVideoDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPropertyVideo([FromRoute] GetPropertyVideoByPropertyIdQuery query)
        {
            var response = new Response<PropertyVideoDTO>();

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
        [HttpPost("confirm")]
        [ProducesResponseType(typeof(SaveEntityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmProperty([FromBody] ConfirmPropertyRequest request)
        {
            var response = new Response<SaveEntityResult>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Property saved successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the property.";
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("list")]
        [ProducesResponseType(typeof(PaginatedList<PropertyListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPropertiesList([FromQuery] GetPropertiesListQuery model)
        {
            var response = new Response<PaginatedList<PropertyListDTO>>();

            try
            {
                var result = await Mediator.Send(model);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("short/list")]
        [ProducesResponseType(typeof(IEnumerable<ShortPropertyListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShortPropertiesList([FromQuery] GetShortPropertiesQuery model)
        {
            var response = new Response<IEnumerable<ShortPropertyListDTO>>();

            try
            {
                var result = await Mediator.Send(model);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("address/{Id}")]
        [ProducesResponseType(typeof(AddressDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPropertyAddressById([FromRoute] GetPropertyAddressByIdQuery query)
        {
            var response = new Response<AddressDTO>();

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
        [HttpPost("address/save")]
        [ProducesResponseType(typeof(SaveEntityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SavePropertyAddress([FromBody] SavePropertyAddressRequest request)
        {
            var response = new Response<SaveEntityResult>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Location saved successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the location.";
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpPost("seo/save")]
        [ProducesResponseType(typeof(SaveEntityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SavePropertySEO([FromBody] SavePropertySEORequest request)
        {
            var response = new Response<SaveEntityResult>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "SEO details saved successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the SEO.";
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("seo/{Id}")]
        [ProducesResponseType(typeof(SEODTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPropertySEOById([FromRoute] GetPropertySEOByIdQuery query)
        {
            var response = new Response<SEODTO>();

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
        [HttpGet("contracts/{Id}")]
        [ProducesResponseType(typeof(IEnumerable<ContractListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPropertyContractsById([FromRoute] GetPropertyContractsByIdQuery query)
        {
            var response = new Response<IEnumerable<ContractListDTO>>();

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
        [HttpPost("contract/save")]
        [ProducesResponseType(typeof(SaveEntityResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SavePropertyContract([FromBody] SaveContractRequest request)
        {
            var response = new Response<SaveEntityResult>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Contract details saved successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while saving the contract.";
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("agents/{Id}")]
        [ProducesResponseType(typeof(IEnumerable<PropertyAgentDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPropertyAgentsById([FromRoute] GetPropertyAgentsByIdQuery query)
        {
            var response = new Response<IEnumerable<PropertyAgentDTO>>();

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
        [HttpGet("mandates/{Id}")]
        [ProducesResponseType(typeof(IEnumerable<MandateDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPropertyMandatesById([FromRoute] GetPropertyMandatesByIdQuery query)
        {
            var response = new Response<IEnumerable<MandateDTO>>();

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
        [HttpGet("documents/{Id}")]
        [ProducesResponseType(typeof(IEnumerable<PropertyDocumentListDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPropertyDocumentsById([FromRoute] GetPropertyDocumentsByIdQuery query)
        {
            var response = new Response<IEnumerable<PropertyDocumentListDTO>>();

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
        [HttpGet("property-types/list")]
        [ProducesResponseType(typeof(IEnumerable<PropertyTypeDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPropertyTypes([FromRoute] GetPropertyTypesQuery query)
        {
            var response = new Response<IEnumerable<PropertyTypeDTO>>();

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
        [HttpDelete("delete")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProperty([FromBody] DeletePropertyRequest request)
        {
            var response = new Response<bool>();

            try
            {
                var result = await Mediator.Send(request);
                response.Data = result;
                response.Message = "Property deleted successfully.";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
                response.Message = "Error occured while deleting the agents.";
            }

            return HandleResponse(response);
        }
    }
}

using apollo.Application.Lookups.Availabilities;
using apollo.Application.Lookups.DTOs;
using apollo.Application.Lookups.GetCities;
using apollo.Application.Lookups.GetMicroDistricts;
using apollo.Application.Lookups.GetOwnershipTypes;
using apollo.Application.Lookups.GetProjectStatuses;
using apollo.Application.Lookups.GetSubMarkets;
using apollo.Application.Lookups.HandOverConditions;
using apollo.Application.Lookups.ListingTypes;
using apollo.Application.Lookups.TenantClassifications;
using apollo.Application.Lookups.UnitStatuses;
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
    public class LookupController : APIBaseController
    {

        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("province/list")]
        [ProducesResponseType(typeof(IEnumerable<ProvinceDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCities([FromRoute] GetProvincesQuery query)
        {
            var response = new Response<IEnumerable<ProvinceDTO>>();

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
        [HttpGet("city/list")]
        [ProducesResponseType(typeof(IEnumerable<CityDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCities(int? provinceID = null)
        {
            var response = new Response<IEnumerable<CityDTO>>();

            try
            {
                var result = await Mediator.Send(new GetCitiesQuery { ProvinceID = provinceID });
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }


        [Authorize(Roles = "Superadmin,Property Manager")]
        [HttpGet("city/get-cities-by-provinces")]
        [ProducesResponseType(typeof(IEnumerable<CityDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCitiesByProvinceIds([FromQuery] GetCitiesByProvinceIDsQuery query)
        {
            var response = new Response<IEnumerable<CityDTO>>();

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
        [HttpGet("city/microdistricts/list/{CityID}")]
        [ProducesResponseType(typeof(IEnumerable<MicroDistrictDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMicroDistricts([FromRoute] GetMicroDistrictsQuery query)
        {
            var response = new Response<IEnumerable<MicroDistrictDTO>>();

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
        [HttpGet("city/microdistricts/list")]
        [ProducesResponseType(typeof(IEnumerable<MicroDistrictDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMicroDistrictsByCityIds([FromQuery] GetMicroDistrictsByCityIdsQuery query)
        {
            var response = new Response<IEnumerable<MicroDistrictDTO>>();

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
        [HttpGet("city/submarkets/list/{CityID}")]
        [ProducesResponseType(typeof(IEnumerable<SubMarketDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubMarkets([FromRoute] GetSubMarketsQuery query)
        {
            var response = new Response<IEnumerable<SubMarketDTO>>();

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
        [HttpGet("city/submarkets/list")]
        [ProducesResponseType(typeof(IEnumerable<SubMarketDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubMarketsByCityIds([FromQuery] GetSubMarketsByCityIdsQuery query)
        {
            var response = new Response<IEnumerable<SubMarketDTO>>();

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
        [HttpGet("ownershiptype/list")]
        [ProducesResponseType(typeof(IEnumerable<OwnershipTypeDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOwnershipTypes([FromRoute] GetOwnershipTypesQuery query)
        {
            var response = new Response<IEnumerable<OwnershipTypeDTO>>();

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
        [HttpGet("projectstatus/list")]
        [ProducesResponseType(typeof(IEnumerable<ProjectStatusDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectStatuses([FromRoute] GetProjectStatusesQuery query)
        {
            var response = new Response<IEnumerable<ProjectStatusDTO>>();

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
        [HttpGet("availability/list")]
        [ProducesResponseType(typeof(IEnumerable<AvailabilityDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailabilities([FromRoute] GetAvailabilitiesQuery query)
        {
            var response = new Response<IEnumerable<AvailabilityDTO>>();

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
        [HttpGet("tenantclassification/list")]
        [ProducesResponseType(typeof(IEnumerable<TenantClassificationDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectStatuses([FromRoute] GetTenantClassificationsQuery query)
        {
            var response = new Response<IEnumerable<TenantClassificationDTO>>();

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
        [HttpGet("unitstatus/list")]
        [ProducesResponseType(typeof(IEnumerable<UnitStatusDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectStatuses([FromRoute] GetUnitStatusesQuery query)
        {
            var response = new Response<IEnumerable<UnitStatusDTO>>();

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
        [HttpGet("handovercondition/list")]
        [ProducesResponseType(typeof(IEnumerable<HandOverConditionDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectStatuses([FromRoute] GetHandOverConditionsQuery query)
        {
            var response = new Response<IEnumerable<HandOverConditionDTO>>();

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
        [HttpGet("listingtype/list")]
        [ProducesResponseType(typeof(IEnumerable<ListingTypeDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectStatuses([FromRoute] GetListingTypesQuery query)
        {
            var response = new Response<IEnumerable<ListingTypeDTO>>();

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
    }
}

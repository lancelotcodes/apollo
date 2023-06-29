using apollo.Application.Buildings.Commands.MigrateBuilding;
using apollo.Application.Buildings.Commands.MigrateContract;
using apollo.Application.Common.Models;
using apollo.Application.Floors.Commands.MigrateFloor;
using apollo.Application.References.Commands.MigrateReferences;
using apollo.Application.Units.Commands.MigrateUnit;
using apollo.Presentation.Controllers.Base;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO.Response;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace apollo.Presentation.Controllers
{
    public class CSVController : APIBaseController
    {

        [HttpPost("references")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var response = new Response<MigrationResultModel>();

            try
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
                var records = csv.GetRecords<MigrateReferencesDTO>();
                var upload = await Mediator.Send(new MigrateReferencesCommand
                {
                    Data = records.ToList()
                });

                response.Data = upload;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [HttpPost("buildings")]
        public async Task<IActionResult> UploadBuildingFile(IFormFile file)
        {
            var response = new Response<MigrationResultModel>();

            try
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
                var records = csv.GetRecords<MigrateBuildingsDTO>();
                var upload = await Mediator.Send(new MigrateBuildingCommand
                {
                    Data = records.ToList()
                });

                response.Data = upload;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [HttpPost("floors")]
        public async Task<IActionResult> UploadBuildingFloorsFile(IFormFile file)
        {
            var response = new Response<MigrationResultModel>();

            try
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
                var records = csv.GetRecords<MigrateFloorDTO>();
                var upload = await Mediator.Send(new MigrateFloorCommand
                {
                    Data = records.ToList()
                });

                response.Data = upload;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [HttpPost("unit")]
        public async Task<IActionResult> UploadBuildingUnitsFile(IFormFile file)
        {
            var response = new Response<MigrationResultModel>();

            try
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
                var records = csv.GetRecords<MigrateUnitDTO>();


                var upload = await Mediator.Send(new MigrateUnitCommand
                {
                    Data = records.ToList()
                });

                response.Data = upload;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [HttpPost("contract")]
        public async Task<IActionResult> UploadBuildingContractFile(IFormFile file)
        {
            var response = new Response<MigrationResultModel>();

            try
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
                var records = csv.GetRecords<MigrateContractDTO>();

                var upload = await Mediator.Send(new MigrateContractCommand
                {
                    Data = records.ToList()
                });

                response.Data = upload;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }
    }
}

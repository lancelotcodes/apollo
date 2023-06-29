using apollo.Application.Floors.Queries.DTOs;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace apollo.Presentation.Excel
{
    public class ExcelHelper
    {
        public async Task<DataTable> GetDataFromFile(IFormFile file)
        {

            //
            // We return the interface, so that
            IExcelDataReader reader = null;
            DataTable workSheet = new DataTable();

            try
            {
                // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                // to get started. This is how we avoid dependencies on ACE or Interop:
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    if (file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    if (file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    using (reader)
                    {
                        ExcelDataSetConfiguration conf = new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true
                            }
                        };
                        workSheet = reader.AsDataSet(conf).Tables[0];
                    }
                }
                return workSheet;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ExportStackingPlanDTO>> GetExcelData(IFormFile file)
        {
            var response = new List<ExportStackingPlanDTO>();
            DataTable workSheet = await GetDataFromFile(file);

            IEnumerable<DataRow> filteredRows = workSheet.Rows.Cast<DataRow>().Where(row => row.ItemArray.Any(field => !(field is DBNull)));

            foreach (DataRow a in filteredRows)
            {
                try
                {
                    ExportStackingPlanDTO data = new ExportStackingPlanDTO()
                    {
                        ID = workSheet.Columns.Contains("Unit ID") && a.Field<double?>("Unit ID").HasValue ? Convert.ToInt32(a.Field<double?>("Unit ID").Value) : 0,
                        Name = workSheet.Columns.Contains("Unit Name") ? a.Field<string>("Unit Name") : null,
                        BuildingID = workSheet.Columns.Contains("Building ID") && a.Field<double?>("Building ID").HasValue ? Convert.ToInt32(a.Field<double?>("Building ID").Value) : 0,
                        BuildingName = workSheet.Columns.Contains("Building Name") ? a.Field<string>("Building Name") : null,
                        UnitStatusName = workSheet.Columns.Contains("Status") ? a.Field<string>("Status") : null,
                        UnitNumber = workSheet.Columns.Contains("Unit Number") ? a.Field<string>("Unit Number") : null,
                        FloorName = workSheet.Columns.Contains("Floor Location") ? a.Field<string>("Floor Location") : null,
                        LeaseFloorArea = workSheet.Columns.Contains("Leasable Floor Area \n (in SQM)") && a.Field<double?>("Leasable Floor Area \n (in SQM)").HasValue ? Convert.ToDecimal(a.Field<double?>("Leasable Floor Area \n (in SQM)").Value) : 0,
                        ListingTypeName = workSheet.Columns.Contains("Listing Type") ? a.Field<string>("Listing Type") : null,
                        BasePrice = workSheet.Columns.Contains("Base Price \n per SQM (PHP)") && a.Field<double?>("Base Price \n per SQM (PHP)").HasValue ? Convert.ToDecimal(a.Field<double?>("Base Price \n per SQM (PHP)").Value) : 0,
                        CUSA = workSheet.Columns.Contains("CUSA per SQM (PHP)") && a.Field<double?>("CUSA per SQM (PHP)").HasValue ? Convert.ToDecimal(a.Field<double?>("CUSA per SQM (PHP)").Value) : 0,
                        HandOverConditionName = workSheet.Columns.Contains("Handover Condition") ? a.Field<string>("Handover Condition") : null,
                        ACCharges = workSheet.Columns.Contains("AC Charges (PHP)") ? a.Field<string>("AC Charges (PHP)") : null,
                        ACExtensionCharges = workSheet.Columns.Contains("AC Extension Charges\n (PHP)") ? a.Field<string>("AC Extension Charges\n (PHP)") : null,
                        EscalationRate = workSheet.Columns.Contains("Escalation Rate") && a.Field<double?>("Escalation Rate").HasValue ? Convert.ToDecimal(a.Field<double?>("Escalation Rate").Value) : 0,
                        MinimumLeaseTerm = workSheet.Columns.Contains("Minimum Lease Term") && a.Field<double?>("Minimum Lease Term").HasValue ? Convert.ToInt32(a.Field<double?>("Minimum Lease Term").Value) : 0,
                        ParkingRent = workSheet.Columns.Contains("Parking Rent per slot \n (PHP)") && a.Field<double?>("Parking Rent per slot \n (PHP)").HasValue ? Convert.ToDecimal(a.Field<double?>("Parking Rent per slot \n (PHP)").Value) : 0,
                        HandOverDate = workSheet.Columns.Contains("Handover Date") && !string.IsNullOrWhiteSpace(a.Field<string>("Handover Date")) ? DateTime.ParseExact(a.Field<string>("Handover Date"), "MM/dd/yyyy", null) : null,
                        Notes = workSheet.Columns.Contains("Notes") ? a.Field<string>("Notes") : null,
                        ContactName = workSheet.Columns.Contains("Contact Person") ? a.Field<string>("Contact Person") : null,
                        ContactPhoneNumber = workSheet.Columns.Contains("Contact Phone") ? a.Field<string>("Contact Phone") : null,
                        ContactEmail = workSheet.Columns.Contains("Contact Email") ? a.Field<string>("Contact Email") : null,
                        CompanyName = workSheet.Columns.Contains("Company Name") ? a.Field<string>("Company Name") : null,
                        BrokerName = workSheet.Columns.Contains("Broker Name") ? a.Field<string>("Broker Name") : null,
                        TenantName = workSheet.Columns.Contains("Tenant Name") ? a.Field<string>("Tenant Name") : null,
                        StartDate = workSheet.Columns.Contains("Tenant Start Date") && !string.IsNullOrWhiteSpace(a.Field<string>("Tenant Start Date")) ? DateTime.ParseExact(a.Field<string>("Tenant Start Date"), "MM/dd/yyyy", null) : null,
                        EndDate = workSheet.Columns.Contains("Tenant End Date") && !string.IsNullOrWhiteSpace(a.Field<string>("Tenant End Date")) ? DateTime.ParseExact(a.Field<string>("Tenant End Date"), "MM/dd/yyyy", null) : null,
                        ClosingRate = workSheet.Columns.Contains("Closing Rate") && a.Field<double?>("Closing Rate").HasValue ? Convert.ToInt32(a.Field<double?>("Closing Rate").Value) : 0,
                        TenantClassification = workSheet.Columns.Contains("Tenant Classification") ? a.Field<string>("Tenant Classification") : null,
                        LeaseTerm = workSheet.Columns.Contains("Lease Term") && a.Field<double?>("Lease Term").HasValue ? Convert.ToInt32(a.Field<double?>("Lease Term").Value) : 0,
                        EstimatedArea = workSheet.Columns.Contains("Estimated Area") && a.Field<double?>("Estimated Area").HasValue ? Convert.ToDecimal(a.Field<double?>("Estimated Area").Value) : 0,
                        IsHistorical = workSheet.Columns.Contains("Is Historical?") ? a.Field<bool>("Is Historical?") : false,
                    };
                    response.Add(data);
                }
                catch (Exception e)
                {
                }

            }

            return response;
        }
    }
}

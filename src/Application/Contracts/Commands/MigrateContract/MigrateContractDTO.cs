using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.Buildings.Commands.MigrateContract
{
    public class MigrateContractDTO
    {
        #region Contract
        public string Name { get; set; }
        public int PropertyID { get; set; }
        public int ContactID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TenantClassificationID { get; set; }
        public decimal EstimatedArea { get; set; }
        public int LeaseTerm { get; set; }
        public int BrokerID { get; set; }
        public int BrokerCompanyID { get; set; }
        public bool IsHistorical { get; set; }
        #endregion

        #region Contact
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        #endregion

        #region Company
        public string Tenant { get; set; }
        #endregion
    }
}

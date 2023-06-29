using apollo.Domain.Entities.References;
using Shared.Domain.Common;
using System;

namespace apollo.Domain.Entities.Core
{
    public class Appraisal : BaseEntityId
    {
        public int ID { get; set; }
        public int PropertyID { get; set; }
        public DateTime AppraisalDate { get; set; }
        public int ValuationTypeID { get; set; }
        public virtual ValuationType ValuationType { get; set; }
        public int ValuationApproachID { get; set; }
        public virtual ValuationApproach ValuationApproach { get; set; }
    }
}

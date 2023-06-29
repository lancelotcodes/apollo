using apollo.Domain.Enums;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace apollo.Application.Documents.Commands.SavePropertyDocuments
{
    public class SavePropertyDocumentRequest : IRequest<bool>
    {
        public int PropertyID { get; set; }
        public List<PropertyDocumentRequest> Images { get; set; }

        public void ValidateActive()
        {
            if (Images != null && Images.Count > 0)
            {
                if (Images.Where(x => x.IsPrimary && x.DocumentType == PropertyDocumentType.MainImage).Count() == 0)
                {
                    var mainImage = Images.FirstOrDefault(x => x.DocumentType == PropertyDocumentType.MainImage);
                    if (mainImage != null)
                        mainImage.IsPrimary = true;
                }
                else if (Images.Where(x => x.IsPrimary && x.DocumentType == PropertyDocumentType.MainImage).Count() > 1)
                {
                    var primary = Images.FirstOrDefault(x => x.IsPrimary && x.DocumentType == PropertyDocumentType.MainImage);
                    Images.Where(x => x.DocumentID != primary.DocumentID).Select(x => x.IsPrimary = false);
                }

                if (Images.Where(x => x.IsPrimary && x.DocumentType == PropertyDocumentType.FloorPlanImage).Count() == 0)
                {
                    var mainFloorImage = Images.FirstOrDefault(x => x.DocumentType == PropertyDocumentType.FloorPlanImage);
                    if (mainFloorImage != null)
                        mainFloorImage.IsPrimary = true;
                }
                else if (Images.Where(x => x.IsPrimary && x.DocumentType == PropertyDocumentType.FloorPlanImage).Count() > 1)
                {
                    var primary = Images.FirstOrDefault(x => x.IsPrimary && x.DocumentType == PropertyDocumentType.FloorPlanImage);
                    Images.Where(x => x.DocumentID != primary.DocumentID).Select(x => x.IsPrimary = false);
                }
            }
        }
    }

    public class PropertyDocumentRequest
    {
        public int ID { get; set; }
        public int DocumentID { get; set; }
        public bool IsPrimary { get; set; }
        public PropertyDocumentType? DocumentType { get; set; }
    }
}

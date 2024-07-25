using System;

namespace Document.Application.Domain.Document.Common.Dto
{
    public class OfferDocumentDto
    {
        public string DocumentId { get; set; }
        public int OfferId { get; set; }
        public string OwnerId { get; set; }
        public string RenterId { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public DateTime UploadDate { get; set; }
        public bool IsLatest { get; set; }
    }

}

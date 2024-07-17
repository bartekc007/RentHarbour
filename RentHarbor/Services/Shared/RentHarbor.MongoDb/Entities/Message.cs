namespace RentHarbor.MongoDb.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string ChatId { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }


}

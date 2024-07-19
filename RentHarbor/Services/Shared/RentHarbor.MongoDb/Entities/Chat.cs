namespace RentHarbor.MongoDb.Entities
{
    public class Chat
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string User1Id { get; set; }
        public string User2Id { get; set; }
        public string User1Name { get; set; }
        public string User2Name { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}

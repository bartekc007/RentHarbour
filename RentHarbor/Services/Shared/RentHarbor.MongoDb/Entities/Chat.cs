namespace RentHarbor.MongoDb.Entities
{
    public class Chat
    {
        public string Id { get; set; }
        public string User1Id { get; set; } // Id pierwszego użytkownika
        public string User2Id { get; set; } // Id drugiego użytkownika
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}

using MediatR;

namespace Communication.Application.Domain.Message.SendMessage
{
    public class SendMessageCommand : IRequest
    {
        public string ChatId { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }

}

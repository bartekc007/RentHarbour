using Communication.Persistance.Repositories.MongoDb;
using MediatR;

namespace Communication.Application.Domain.Message.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
    {
        private readonly IMessageRepository _messageRepository;

        public SendMessageCommandHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new RentHarbor.MongoDb.Entities.Message
            {
                Id = Guid.NewGuid().ToString(),
                ChatId = request.ChatId,
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                Content = request.Content,
                SentAt = DateTime.UtcNow
            };

            await _messageRepository.AddAsync(message);

            return Unit.Value;
        }
    }


}

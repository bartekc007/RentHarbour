using Communication.Application.Domain.Message.SendMessage;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Communication.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendMessage(string senderId, string receiverId, string content)
        {
            var command = new SendMessageCommand
            {
                SenderId = senderId,
                RecipientId = receiverId,
                Content = content
            };

            await _mediator.Send(command);
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, content);
        }
    }

}

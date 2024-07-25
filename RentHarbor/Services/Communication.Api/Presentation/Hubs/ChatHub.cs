using Communication.Persistance.Repositories.MongoDb;
using Microsoft.AspNetCore.SignalR;
using RentHarbor.AuthService.Services;
using RentHarbor.MongoDb.Entities;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Communication.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IAuthorizationService _authorizationService;

        public ChatHub(IMessageRepository chatService, IAuthorizationService authorizationService, IChatRepository chatRepository)
        {
            _messageRepository = chatService;
            _chatRepository = chatRepository;
            _authorizationService = authorizationService;
        }

        public async Task SendMessage(string chatId, string messageContent, string accessToken)
        {
            
            var userId = await GetUserIdFromToken(accessToken);
            var messages = await _messageRepository.GetMessagesAsync(userId, chatId);
            var chat = await _chatRepository.GetChatAsync(chatId, userId);

            var recipientId = userId == chat.User1Id ? chat.User2Id : chat.User1Id;
            var senderName = userId == chat.User1Id ? chat.User1Name : chat.User2Name;
            var recipientName = userId == chat.User1Id ? chat.User2Name : chat.User1Name;

            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                ChatId = chatId,
                RecipientId = recipientId,
                SenderId = userId,
                RecipientName = recipientName,
                SenderName = senderName,
                Content = messageContent,
                SentAt = DateTime.UtcNow
            };

            await _messageRepository.AddAsync(message);

            await Clients.Group(chatId).SendAsync("ReceiveMessage", chatId, userId, message.RecipientId, message.SenderName, message.RecipientName, messageContent);
        }

        private async Task<string> GetUserIdFromToken(string accessToekn)
        {
            var token = accessToekn.Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);
            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();
            return userId;
        }

        public override async Task OnConnectedAsync()
        {
            var chatId = Context.GetHttpContext().Request.Query["chatId"];
            if (!string.IsNullOrEmpty(chatId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var chatId = Context.GetHttpContext().Request.Query["chatId"];
            if (!string.IsNullOrEmpty(chatId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }

}

using Communication.Application.Domain.Message.GetMessage;
using Communication.Application.Domain.Message.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentHarbor.MongoDb.Entities;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Communication.Persistance.Repositories.MongoDb;
using System.Text.Json;
using Communication.Persistance.Repositories.Psql;
using Communication.Api.Request;

namespace Communication.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IChatRepository _chatRepository;
        private readonly IRentalRequestRepository _rentalRequestRepository;
        public readonly RentHarbor.AuthService.Services.IAuthorizationService _authorizationService;

        public ChatController(IMediator mediator, IChatRepository chatRepository, RentHarbor.AuthService.Services.IAuthorizationService authorizationService, IRentalRequestRepository rentalRequestRepository)
        {
            _mediator = mediator;
            _chatRepository = chatRepository;
            _rentalRequestRepository = rentalRequestRepository;
            _authorizationService = authorizationService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);

            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();

            command.SenderId = userId;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages([FromQuery] string chatId)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);

            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();

            var query = new GetMessagesQuery
            {
                UserId = userId,
                ChatId = chatId
            };

            var messages = await _mediator.Send(query);
            return Ok(messages);
        }

        [HttpGet("GetChatsForUser")]
        public async Task<ActionResult<List<Chat>>> GetChatsForUser()
        {
            try
            {
                string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
                JsonDocument doc = JsonDocument.Parse(userIdJson);

                JsonElement root = doc.RootElement;
                string userId = root.GetProperty("userId").GetString();

                var chats = await _chatRepository.GetChatsForUserAsync(userId);
                return Ok(chats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{chatId}")]
        public async Task<ActionResult<Chat>> GetChat(string chatId)
        {
            try
            {
                string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
                JsonDocument doc = JsonDocument.Parse(userIdJson);

                JsonElement root = doc.RootElement;
                string userId = root.GetProperty("userId").GetString();

                var chat = await _chatRepository.GetChatAsync(chatId, userId);
                if (chat == null)
                {
                    return NotFound();
                }
                return Ok(chat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CreateChat")]
        public async Task<ActionResult> CreateChat([FromBody] ChatRequest request)
        {
            try
            {
                string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
                JsonDocument doc = JsonDocument.Parse(userIdJson);

                JsonElement root = doc.RootElement;
                string userId = root.GetProperty("userId").GetString();
                var userName = await _authorizationService.GetUserNameById(userId);

                var offer = await _rentalRequestRepository.GetRentalRequestByOfferIdAsync(request.OfferId);

                Chat chat = new Chat
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = request.title
                };
                if (userId == offer.UserId)
                {
                    chat.User1Id = userId;
                    chat.User1Name = userName;
                    chat.User2Id = offer.TenantId;
                    var TenantName = await _authorizationService.GetUserNameById(offer.TenantId);
                    chat.User2Name = TenantName;
                }
                else
                {
                    chat.User1Id = userId;
                    chat.User1Name = userName;
                    chat.User2Id = offer.UserId;
                    var user2Name = await _authorizationService.GetUserNameById(offer.UserId);
                    chat.User2Name = user2Name;
                }

                await _chatRepository.AddChatAsync(chat);
                return CreatedAtAction(nameof(GetChat), new { chatId = chat.Id }, chat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}

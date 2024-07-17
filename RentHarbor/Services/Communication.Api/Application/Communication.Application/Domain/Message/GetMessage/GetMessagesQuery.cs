using Communication.Application.Domain.Message.Common.Dto;
using MediatR;

namespace Communication.Application.Domain.Message.GetMessage
{
    public class GetMessagesQuery : IRequest<IEnumerable<MessageDto>>
    {
        public string UserId { get; set; }
        public string ChatId { get; set; }
    }



}

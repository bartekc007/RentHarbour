using AutoMapper;
using Communication.Application.Domain.Message.Common.Dto;
using Communication.Persistance.Repositories.MongoDb;
using MediatR;

namespace Communication.Application.Domain.Message.GetMessage
{
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IEnumerable<MessageDto>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public GetMessagesQueryHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetMessagesAsync(request.UserId, request.ChatId);
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }
    }


}

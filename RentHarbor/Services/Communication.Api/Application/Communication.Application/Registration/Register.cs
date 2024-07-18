using Communication.Application.Domain.Message.Common.Dto;
using Communication.Application.Domain.Message.GetMessage;
using Communication.Application.Domain.Message.SendMessage;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Communication.Application.Registration
{
    public static class Register
    {
        public static void RegisterApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<SendMessageCommand,MediatR.Unit>, SendMessageCommandHandler>();
            services.AddTransient<IRequestHandler<GetMessagesQuery, IEnumerable<MessageDto>>, GetMessagesQueryHandler>();
        }
    }
}

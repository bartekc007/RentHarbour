﻿namespace Communication.Application.Domain.Message.Common.Dto
{
    public class MessageDto
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}

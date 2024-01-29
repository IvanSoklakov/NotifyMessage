using AutoMapper;
using NotifyMessage.BuisnessLogicLayer.Dto;
using NotifyMessage.BuisnessLogicLayer.Interfaces;
using NotifyMessage.DataAccesLayer.Models;

namespace NotifyMessage.BuisnessLogicLayer.Services
{
    public class MessageServices : IMessageServices
    {
        private readonly static Dictionary<int, Queue<MessageDto>> MessageQueue = new();
        private readonly IMapper _mapper;

        public MessageServices(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void SendMessage(Message message)
        {
            foreach (var recipient in message.Recipients)
            {
                if (MessageQueue.ContainsKey(recipient))
                {
                    MessageQueue[recipient].Enqueue(new MessageDto
                    {
                        Body = message.Body,
                        Subject = message.Subject,
                    });
                }
                else
                {
                    var messageQueue = new Queue<MessageDto>();
                    messageQueue.Enqueue(new MessageDto()
                    {
                        Body = message.Body,
                        Subject = message.Subject
                    });
                    MessageQueue.Add(recipient, messageQueue);
                }
            }
        }

        public MessageDto GetMessage(int rcpt)
        {
            if (MessageQueue.Count > 0 && MessageQueue.ContainsKey(rcpt) && MessageQueue[rcpt].Count > 0)
            {
                var message = MessageQueue[rcpt].Dequeue();
                return _mapper.Map<MessageDto>(source: message);
            }
            return null;
        }
    }
}

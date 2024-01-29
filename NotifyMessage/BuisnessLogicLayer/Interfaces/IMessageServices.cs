using NotifyMessage.BuisnessLogicLayer.Dto;
using NotifyMessage.DataAccesLayer.Models;

namespace NotifyMessage.BuisnessLogicLayer.Interfaces
{
    public interface IMessageServices
    {
        void SendMessage(Message message);
        MessageDto GetMessage(int rcpt);
    }
}

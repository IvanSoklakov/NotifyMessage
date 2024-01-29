using AutoMapper;
using NotifyMessage.BuisnessLogicLayer.Dto;
using NotifyMessage.DataAccesLayer.Models;


namespace NotifyMessage.BuisnessLogicLayer.Infrastucture
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessageDto>();
        }
    }
}

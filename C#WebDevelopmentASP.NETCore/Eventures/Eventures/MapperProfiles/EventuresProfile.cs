using AutoMapper;
using Eventures.Models;
using Eventures.ViewModels.Events;
using Eventures.ViewModels.Orders;
using Eventures.ViewModels.Users;

namespace Eventures.MapperProfiles
{
    public class EventuresProfile : Profile
    {
        public EventuresProfile()
        {
            CreateMap<Event, CreateEventViewModel>()
                .ReverseMap();

            CreateMap<EventuresUser, RegisterViewModel>()
                .ForMember(dest => dest.Username, from => from.MapFrom(src => src.UserName))
                .ReverseMap();

            CreateMap<Order, CreateOrderViewModel>()
                .ReverseMap();

            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.OrderedOn, from => from.MapFrom(src => src.OrderedOn))
                .ForMember(dest => dest.CustomerName, from => from.MapFrom(src => src.Customer.UserName))
                .ForMember(dest => dest.EventName, from => from.MapFrom(src => src.Event.Name))
                .ReverseMap();
        }
    }
}

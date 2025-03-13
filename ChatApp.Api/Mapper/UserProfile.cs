using AutoMapper;
using ChatApp.Api.Data.Identity;
using ChatApp.Api.Dtos;
using ChatApp.Api.Model;

namespace ChatApp.Api.Mapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<CustomUser, GetUserDto>().ForMember(dest=>dest.CurrentProfilePictureUrl,opt=>opt.MapFrom(src=>src.Photos.FirstOrDefault(x=>x.IsMain).Url));
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<UpdateUserDto,CustomUser>().ReverseMap();
            CreateMap<Message, MessageDto>().ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(x => x.Sender.Photos.FirstOrDefault(x => x.IsMain).Url)).ForMember(dest => dest.ReciverPhotoUrl, opt => opt.MapFrom(x => x.Reciver.Photos.FirstOrDefault(x => x.IsMain).Url));
            
        }
    }
}

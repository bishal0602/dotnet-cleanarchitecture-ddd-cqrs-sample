using AutoMapper;

namespace Books.API.Mappings
{
    public class AuthenticationMappings : Profile
    {
        public AuthenticationMappings()
        {
            CreateMap<Domain.UserAggregate.User, Models.Authentication.UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
            CreateMap<Application.Authentication.Common.AuthenticationResponse, Models.Authentication.AuthResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        }
    }
}

using AutoMapper;
using Shopi.Images.API.Commands;
using Shopi.Images.API.DTOs;

namespace Shopi.Images.API.Mappers;

public class ImageMappingProfile : Profile
{
    public ImageMappingProfile()
    {
        CreateMap<UploadImageDto, UploadImageCommand>();
        CreateMap<UploadImageCommand, UploadImageDto>();
    }
}
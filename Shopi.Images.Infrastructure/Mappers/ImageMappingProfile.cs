using AutoMapper;
using Shopi.Images.Application.Commands;
using Shopi.Images.Application.DTOs;
using Shopi.Images.Application.Queries;
using Shopi.Images.Domain.Queries;

namespace Shopi.Images.Infrastructure.Mappers;

public class ImageMappingProfile : Profile
{
    public ImageMappingProfile()
    {
        CreateMap<UploadImageDto, UploadImageCommand>();
        CreateMap<UploadImageCommand, UploadImageDto>();

        CreateMap<ListImagesQuery, QueryImages>();
    }
}
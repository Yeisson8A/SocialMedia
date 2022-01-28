using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //Crear mapeo de entidad Post a PostDto
            CreateMap<Post, PostDto>();
            //Crear mapeo de entidad PostDto a Post
            CreateMap<PostDto, Post>();
        }
    }
}

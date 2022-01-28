using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        //Método GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
        {
            //Obtener listado de posts
            var posts = await _postRepository.GetPosts();
            //Hacer mapeo de entidad Post a PostDto
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            return Ok(postsDto);
        }

        //Método GET con parámetro
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPost(int id)
        {
            //Obtener post especifico según Id
            var post = await _postRepository.GetPost(id);
            //Hacer mapeo de entidad Post a PostDto
            var postDto = _mapper.Map<PostDto>(post);
            return Ok(postDto);
        }

        //Método POST
        [HttpPost]
        public async Task<ActionResult<bool>> InsertPost(PostDto postDto)
        {
            //Hacer mapeo de entidad PostDto a Post
            var post = _mapper.Map<Post>(postDto);
            //Insertar nuevo post
            await _postRepository.InsertPost(post);
            return Ok(post);
        }
    }
}

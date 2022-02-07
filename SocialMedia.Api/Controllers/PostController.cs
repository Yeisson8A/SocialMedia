﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        //Método GET
        //Se marca el objeto recibido como parámetro con "FromQuery" para indicar que se va a mapear desde la Url
        //si no se marca al utilizar ApiController se interpretará que se va a mapear contra el body
        [HttpGet]
        public ActionResult<IEnumerable<PostDto>> GetPosts([FromQuery]PostQueryFilter filters)
        {
            //Obtener listado de posts
            var posts = _postService.GetPosts(filters);
            //Hacer mapeo de entidad Post a PostDto
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            //Adicionar respuesta a objeto a devolver
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto);

            //Objeto con los valores de paginación
            var metadata = new
            {
                posts.TotalCount,
                posts.PageSize,
                posts.CurrentPage,
                posts.TotalPages,
                posts.HasNextPage,
                posts.HasPreviousPage
            };

            //Agregar a los headers del response un tag con los valores de paginación
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            //Devolver respuesta
            return Ok(response);
        }

        //Método GET con parámetro
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPost(int id)
        {
            //Obtener post especifico según Id
            var post = await _postService.GetPost(id);
            //Hacer mapeo de entidad Post a PostDto
            var postDto = _mapper.Map<PostDto>(post);
            //Adicionar respuesta a objeto a devolver
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        //Método POST
        [HttpPost]
        public async Task<ActionResult<bool>> InsertPost(PostDto postDto)
        {
            //Hacer mapeo de entidad PostDto a Post
            var post = _mapper.Map<Post>(postDto);
            //Insertar nuevo post
            await _postService.InsertPost(post);
            //Hacer mapeo de entidad Post a PostDto
            postDto = _mapper.Map<PostDto>(post);
            //Adicionar respuesta a objeto a devolver
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        //Método PUT
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> EditPost(int id, PostDto postDto)
        {
            //Hacer mapeo de entidad PostDto a Post
            var post = _mapper.Map<Post>(postDto);
            //Asignar id del post a actualizar
            post.Id = id;
            //Actualizar un post existente
            var result = await _postService.EditPost(post);
            //Adicionar respuesta a objeto a devolver
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        //Método DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeletePost(int id)
        {
            //Eliminar un post existente
            var result = await _postService.DeletePost(id);
            //Adicionar respuesta a objeto a devolver
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}

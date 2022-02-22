using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Responses;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;
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
        private readonly IUriService _uriService;

        public PostController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
        }

        //Método GET
        //Se marca el objeto recibido como parámetro con "FromQuery" para indicar que se va a mapear desde la Url
        //si no se marca al utilizar ApiController se interpretará que se va a mapear contra el body
        //Se usa nameof para obtener como nombre del servicio, el nombre del método
        [HttpGet(Name = nameof(GetPosts))]
        public ActionResult<IEnumerable<PostDto>> GetPosts([FromQuery] PostQueryFilter filters)
        {
            //Obtener listado de posts
            var posts = _postService.GetPosts(filters);
            //Hacer mapeo de entidad Post a PostDto
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            //Objeto con los valores de paginación
            var metadata = new Metadata
            {
                TotalCount = posts.TotalCount,
                PageSize = posts.PageSize,
                CurrentPage = posts.CurrentPage,
                TotalPages = posts.TotalPages,
                HasNextPage = posts.HasNextPage,
                HasPreviousPage = posts.HasPreviousPage,
                //Se usa Url RouteUrl para que el framework derive la ruta del servicio
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString(),
                //Se usa Url RouteUrl para que el framework derive la ruta del servicio
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString()
            };

            //Adicionar respuesta a objeto a devolver,
            //así como objeto con la información de paginación
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto)
            {
                Meta = metadata
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

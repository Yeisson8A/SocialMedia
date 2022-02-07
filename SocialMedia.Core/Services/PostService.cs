using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        //private readonly IRepository<Post> _postRepository;
        //private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            //_postRepository = postRepository;
            //_userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeletePost(int id)
        {
            //Llamar al repositorio de post
            await _unitOfWork.PostRepository.Delete(id);
            //Llamar a UnitOfWork para guardar los cambios en la base de datos
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditPost(Post post)
        {
            //Llamar al repositorio de post
            _unitOfWork.PostRepository.Edit(post);
            //Llamar a UnitOfWork para guardar los cambios en la base de datos
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Post> GetPost(int id)
        {
            //Llamar al repositorio de post
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            //Llamar al repositorio de post
            var posts = _unitOfWork.PostRepository.GetAll();

            //Validar si se envió un filtro por usuario
            if (filters.UserId != null)
            {
                //Filtrar el listado de post obtenido por usuario
                posts = posts.Where(x => x.UserId == filters.UserId);
            }

            //Validar si se envió un filtro por fecha
            if (filters.Date != null)
            {
                //Filtrar el listado de post obtenido por fecha
                posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }

            //Validar si se envió un filtro por descripción
            if (filters.Description != null)
            {
                //Filtrar el listado de post obtenido por descripción
                posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            //Llamar a entidad PagedList para paginar el listado de post obtenido
            var pagedPost = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);

            //Retornar listado de post paginados
            return pagedPost;
        }

        public async Task<bool> InsertPost(Post post)
        {
            //Llamar al repositorio de usuario para obtener información de un usuario especifico
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);

            //Validar si encontró el usuario indicado
            if (user == null)
            {
                throw new BusinessException("El usuario no existe");
            }

            //Llamar al repositorio de post para obtener listado según un usuario especifico
            var userPosts = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);

            //Validar la cantidad de post del usuario
            if (userPosts.Count() < 10)
            {
                //Ordenar los posts del usuario por fecha de forma descendente y obtener el último post
                var lastPost = userPosts.OrderByDescending(x => x.Date).FirstOrDefault();

                //Validar que la diferencia en días desde la última publicación sea mayor o igual a siete días
                if ((DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("No puedes publicar");
                }
            }

            //Validar si la descripción del post tiene contenido no válido
            if (post.Description.Contains("Sexo"))
            {
                throw new BusinessException("Contenido no permitido");
            }

            //Llamar al repositorio de post
            await _unitOfWork.PostRepository.Add(post);
            //Llamar a UnitOfWork para guardar los cambios en la base de datos
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}

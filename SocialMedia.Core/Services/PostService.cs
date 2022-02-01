using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
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
            return await _unitOfWork.PostRepository.Delete(id);
        }

        public async Task<bool> EditPost(Post post)
        {
            //Llamar al repositorio de post
            return await _unitOfWork.PostRepository.Edit(post);
        }

        public async Task<Post> GetPost(int id)
        {
            //Llamar al repositorio de post
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            //Llamar al repositorio de post
            return await _unitOfWork.PostRepository.GetAll();
        }

        public async Task<bool> InsertPost(Post post)
        {
            //Llamar al repositorio de usuario para obtener información de un usuario especifico
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);

            //Validar si encontró el usuario indicado
            if (user == null)
            {
                throw new Exception("El usuario no existe");
            }

            //Validar si la descripción del post tiene contenido no válido
            if (post.Description.Contains("Sexo"))
            {
                throw new Exception("Contenido no permitido");
            }

            //Llamar al repositorio de post
            return await _unitOfWork.PostRepository.Add(post);
        }
    }
}

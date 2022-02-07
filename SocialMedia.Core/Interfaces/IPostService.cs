using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        //Método para obtener listado de todos los posts
        PagedList<Post> GetPosts(PostQueryFilter filters);
        //Método para obtener un post especifico por Id
        Task<Post> GetPost(int id);
        //Método para insertar un nuevo post
        Task<bool> InsertPost(Post post);
        //Método para actualizar un post existente
        Task<bool> EditPost(Post post);
        //Método para eliminar un post existente
        Task<bool> DeletePost(int id);
    }
}
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository
    {
        //Método para obtener listado de todos los posts
        Task<IEnumerable<Post>> GetPosts();
    }
}

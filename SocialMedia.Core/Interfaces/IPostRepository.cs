using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        //Método para obtener listado de todos los posts según un usuario
        Task<IEnumerable<Post>> GetPostsByUser(int userId);
    }
}

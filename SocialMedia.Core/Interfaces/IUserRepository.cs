using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUserRepository
    {
        //Método para obtener un usuario según el Id
        Task<User> GetUser(int id);
        //Método para obtener listado de usuarios
        Task<IEnumerable<User>> GetUsers();
    }
}

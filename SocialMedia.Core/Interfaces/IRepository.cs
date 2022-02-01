using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    //Se indica que la interfaz maneja generics pero dicho generic T solo acepta aquellas entidades que hereden de BaseEntity
    public interface IRepository<T> where T : BaseEntity
    {
        //Método para obtener listado de todos los objetos
        Task<IEnumerable<T>> GetAll();
        //Método para obtener un objeto especifico por Id
        Task<T> GetById(int id);
        //Método para insertar un nuevo post
        Task<bool> Add(T entity);
        //Método para actualizar un objeto existente
        Task<bool> Edit(T entity);
        //Método para eliminar un objeto existente
        Task<bool> Delete(int id);
    }
}

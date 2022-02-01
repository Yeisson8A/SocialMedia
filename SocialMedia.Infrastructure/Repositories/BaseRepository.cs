using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext _context;
        private DbSet<T> _entities;

        public BaseRepository(SocialMediaContext context)
        {
            _context = context;
            //Setear el tipo de la entidad recibida al repositorio generico
            _entities = _context.Set<T>();
        }

        public async Task<bool> Add(T entity)
        {
            //Agregar nuevo objeto de dicha entidad al contexto de la base de datos
            _entities.Add(entity);
            //Guardar cambios en la base de datos
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            //Obtener información del objeto a actualizar para dicha entidad
            var entity = await GetById(id);
            //Eliminar entidad del contexto de la base de datos
            _entities.Remove(entity);
            //Guardar cambios en la base de datos
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> Edit(T entity)
        {
            //Actualizar dicha entidad con la información recibida
            _entities.Update(entity);
            //Guardar cambios en la base de datos
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            //Obtener listado de todos los objetos de dicha entidad
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            //Obtener un objeto especifico según Id de dicha entidad
            return await _entities.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext _context;

        public PostRepository(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<Post> GetPost(int id)
        {
            //Obtener un post especifico según Id
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            return post;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            //Obtener listado de todas las publicaciones
            var posts = await _context.Posts.ToListAsync();
            //Devolver resultado obtenido
            return posts;
        }

        public async Task<bool> InsertPost(Post post)
        {
            //Agregar nuevo post al contexto de la base de datos
            _context.Posts.Add(post);
            //Guardar cambios en la base de datos
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }

            return false;
        }
    }
}

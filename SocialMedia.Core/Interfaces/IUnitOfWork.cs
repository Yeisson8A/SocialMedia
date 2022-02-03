using SocialMedia.Core.Entities;
using System;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Repositorio para la entidad post
        IPostRepository PostRepository { get; }
        //Repositorio para la entidad usuario
        IRepository<User> UserRepository { get; }
        //Repositorio para la entidad Comentario
        IRepository<Comment> CommentRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}

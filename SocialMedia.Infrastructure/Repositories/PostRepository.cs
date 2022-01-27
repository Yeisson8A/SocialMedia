using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        /*public IEnumerable<Post> GetPosts()
        {
            var posts = Enumerable.Range(1, 10).Select(x => new Post
            {
                PostId = x,
                UserId = x * 2,
                Date = DateTime.Now,
                Description = $"Description {x}",
                Image = ""
            });

            return posts;
        }*/
        public Task<IEnumerable<Post>> GetPosts()
        {
            throw new NotImplementedException();
        }
    }
}

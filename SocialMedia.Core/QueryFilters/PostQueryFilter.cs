using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.QueryFilters
{
    public class PostQueryFilter
    {
        //Se antepone ? a los tipos int y DateTime para indicar que aceptan nulos
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
    }
}

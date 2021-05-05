using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPhoto.Models
{
    public class HashTag
    {
        public HashTag()
        {
            Photos = new List<Photo>();
        }
        public int Id { get; set; }
        public string HashTagName { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
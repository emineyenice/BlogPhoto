using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogPhoto.Models
{
    public class Photo
    {
        public Photo()
        {
            HashTags = new List<HashTag>();
        }
        public int Id { get; set; }

        public string PhotoTitle { get; set; }

        public string Description { get; set; }

        public DateTime LoadTime { get; set; }
        public string HashTag { get; set; }
        public string ImagePath { get; set; }

        public int HashTagId { get; set; }
        
        public virtual ICollection<HashTag> HashTags { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogPhoto.Models
{
    public class PhotoContext : DbContext
    {
        public PhotoContext() : base("name=Baglantim")
        {

        }
        
        public DbSet<Photo> Photos { get; set; }
        public DbSet<HashTag> HashTags { get; set; }
    }
}
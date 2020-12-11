using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Coursework.Models
{
    public class CourseworkContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CourseworkContext() : base("name=CourseworkContext")
        {
        }

        public System.Data.Entity.DbSet<Coursework.Models.Album> Albums { get; set; }

        public System.Data.Entity.DbSet<Coursework.Models.AlbumType> AlbumTypes { get; set; }

        public System.Data.Entity.DbSet<Coursework.Models.Artist> Artists { get; set; }

        public System.Data.Entity.DbSet<Coursework.Models.AlbumArtist> AlbumArtists { get; set; }

        public System.Data.Entity.DbSet<Coursework.Models.AlbumCopy> AlbumCopies { get; set; }

        public System.Data.Entity.DbSet<Coursework.Models.Producer> Producers { get; set; }

        public System.Data.Entity.DbSet<Coursework.Models.AlbumProducer> AlbumProducers { get; set; }

        public System.Data.Entity.DbSet<Coursework.Models.UserRole> UserRoles { get; set; }

        public System.Data.Entity.DbSet<Coursework.Models.User> Users { get; set; }

       // public System.Data.Entity.DbSet<Coursework.Models.Loan> Loans { get; set; }

        public System.Data.Entity.DbSet<Coursework.Models.Member> Members { get; set; }

        public System.Data.Entity.DbSet<Coursework.Models.MemberCategory> MemberCategories { get; set; }

        public System.Data.Entity.DbSet<Coursework.Models.Loan> Loans { get; set; }
    }
}

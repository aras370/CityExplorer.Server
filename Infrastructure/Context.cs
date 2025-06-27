using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Context:DbContext
    {

        public Context(DbContextOptions<Context> options):base(options) 
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<PlaceCategory> PlaceCategories { get; set; }

    }
}

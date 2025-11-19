using Bootcamp.DataAccessLayer.Models;

using Clase9.DAL.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase9.DAL.Data
{
    public class BootcampDbContext: IdentityDbContext<User, IdentityRole<int>, int> //DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Data Source=DESKTOP-SV7GMDJ;Initial Catalog=bootcamp-database;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //}
        public BootcampDbContext(DbContextOptions<BootcampDbContext> options) : base(options)
        {
        }
        //public DbSet<Persona> Personas { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<Auto> Autos { get; set; }

    }
}

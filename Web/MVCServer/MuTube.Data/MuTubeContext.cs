using MeTube.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MuTube.Data
{
    public class MuTubeContext : DbContext
    {
        DbSet<User> Users { get; set; }

        DbSet<Tube> Tubes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MuTube_steff;Integrated Security=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}

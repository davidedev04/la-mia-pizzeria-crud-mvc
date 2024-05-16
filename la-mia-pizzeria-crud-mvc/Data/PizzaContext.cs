﻿using Microsoft.EntityFrameworkCore;


namespace la_mia_pizzeria_crud_mvc.Data
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza>Pizze { get; set; }
        public const string CONNECTION_STRING = "Data Source=localhost;Initial Catalog=miapizzeria;Integrated Security=True;TrustServerCertificate=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
        }
    }
}
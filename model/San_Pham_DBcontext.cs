using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace EF_02_X1{
    public class Context :DbContext{
        public DbSet<Product> products{set;get;}
        public DbSet<category> categories{set;get;}
        
        ILoggerFactory loggerFactory = LoggerFactory.Create((p)=>{
            p.AddFilter(DbLoggerCategory.Query.Name,LogLevel.Information);
            p.AddConsole();
        });
        private const string temp = "Data Source = localhost,1433;Database= EF-02;User ID = sa;Password = Password123 ";
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(temp);
            optionsBuilder.UseLoggerFactory(loggerFactory);   
        }

    }
}
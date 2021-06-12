using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using TPICAP.Persons.Persistence;

namespace TPICAP.Persons.Tests.Persistence
{
    public class InMemoryDbContext
    {
        private readonly IConfiguration _configuration;
        public DbContextOptionsBuilder<AppDbContext> DbContextOptionsBuilder { get; }
        public AppDbContext GetDbContext() =>
            (AppDbContext)Activator.CreateInstance(typeof(AppDbContext), DbContextOptionsBuilder.Options, _configuration);

        public InMemoryDbContext(string name)
        {
            DbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(name);
        }

        public void AddEntities<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            using var context = GetDbContext();
            context.Set<TEntity>().AddRange(entities);
            context.SaveChanges();
        }
    }
}

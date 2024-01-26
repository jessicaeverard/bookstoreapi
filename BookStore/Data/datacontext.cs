//responsible for interacting with the database and represents a session with said database. used to write and execute queries.
//materialize query results as Entity objects and track changes to those objects 

using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStore.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<BookModel> Books { get; set; }

        public virtual DbSet<AuthorModel> Authors { get; set; }

    }
}

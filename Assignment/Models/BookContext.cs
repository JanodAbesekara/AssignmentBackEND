using Microsoft.EntityFrameworkCore;

namespace Assignment.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

      
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id); 
                entity.Property(b => b.Title)
                      .HasMaxLength(100)
                      .IsRequired(); 
                entity.Property(b => b.Author)
                      .HasMaxLength(50)
                      .IsRequired(); 
                entity.Property(b => b.PublicationDate)
                      .IsRequired(); 
                entity.Property(b => b.ISBN)
                      .HasMaxLength(13);
            });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoEntity;

namespace AppContext;
public class AppDbContext : DbContext
{
    public DbSet<TodoItem> TodoTask{get;set;}
    public AppDbContext() {}
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<TodoItem>(entity =>
      {
        entity.HasKey(x => x.Id);
        entity.Property<string>(x => x.Description)
            .IsRequired()
            .HasMaxLength(200);
        entity.Property<DateTime>(x => x.CreatedAt);
        entity.Property<DateTime>(x => x.UpdatedAt);
        entity.Property<bool>(x => x.isCompleted);
            
      });
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=todolist.db");
        base.OnConfiguring(optionsBuilder);
    }

}

    

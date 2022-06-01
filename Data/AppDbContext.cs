using AluraFlix.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AluraFlix.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions opt) : base(opt)
        {

        }
        //configurando conexao Categorias  com Videos
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Video>()
                .HasOne(video => video.Categoria)
                .WithMany(categoria => categoria.Videos)
                .HasForeignKey(vidoe => vidoe.CategoriaId);

        }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }

    //Configurando banco de dados
    public class BloggingContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseMySql("Server=localhost;User ID=root;Password=ads.Microsoft2;Database=AluraFlix", ServerVersion.Parse("8.0.29"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
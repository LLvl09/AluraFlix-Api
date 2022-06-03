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
        public DbSet<Video> Videos { get; private set; }
        public DbSet<Categoria> Categorias { get; private set; }
    }
}
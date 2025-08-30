using System.Formats.Tar;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models;

namespace Pokedex.Data;

    public class AppDbContext: DbContext
    {
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  //ctor
      {
        
      } 

      public DbSet<Genero> Generos { get; set; }

      public DbSet<Pokemon> Pokemons { get; set; }

       public DbSet<PokemonTipo> PokemonTipos { get; set; }

       public DbSet<Regiao> Regioes { get; set; }

     public DbSet<Tipo> Tipos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); //continua fazendo oq ta programado na herença!

        // Fluente api que da pra mudar varias coisas

        #region  Muitos para Muitos do Pokemon Tipo

         // Configuração da chave primária.

             builder.Entity<PokemonTipo>().HasKey(
             pt => new { pt.PokemonNumero, pt.TipoId}
         );

         //Chave Estrangeira - PokemonTipo -> Pokemon

             builder.Entity<PokemonTipo>()
             .HasOne(pt => pt.Pokemon) // Relacionamento com Pokemon
             .WithMany(p => p.Tipos)   // Pokemon tem muitos Tipos
             .HasForeignKey(pt => pt.PokemonNumero);  // A chave estrangeira



              //Chave Estrangeira - PokemonTipo -> Tipo
              builder.Entity<PokemonTipo>()
             .HasOne(pt => pt.Tipo) 
             .WithMany(t => t.Pokemons)   
             .HasForeignKey(pt => pt.TipoId);  
             
        #endregion 
           

    }
}

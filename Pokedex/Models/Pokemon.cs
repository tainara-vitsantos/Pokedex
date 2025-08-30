 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Pokedex.Models;
    [Table("Pokemon")]
    public class Pokemon
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.None)]
       public uint  Numero { get; set; }

      [Required(ErrorMessage = "Por favor, informe a regiao")]
      public uint RegiaoId { get; set; }
      [ForeignKey("RegiaoId")]
      public Regiao Regiao { get; set; } //propriedade de navegação

     [Required(ErrorMessage = "Por favor, informe o Gênero")]
     public uint GeneroId { get; set; }
     [ForeignKey("GeneroId")]
     public Genero Genero { get; set; }

     [Required(ErrorMessage = "Por favor, informe o nome")]
     public string Nome { get; set; }

     [StringLength(1000)]
     public string Descricao { get; set; }

    [Required(ErrorMessage = "Informe a altura")]
    [Column(TypeName = "double(5,2)")] // especifica a altura
     public double Altura { get; set; } //doble é um numero grande

    [Required(ErrorMessage = "Informe o peso")]
    [Column(TypeName = "double(7,3)")] // especifica a altura
     public double Peso { get; set; } //doble é um numero grande

     [StringLength(200)]
     public string Imagem { get; set; }
      
    [StringLength(400)]

    public string  Animacao { get; set; }

    public ICollection<PokemonTipo> Tipos { get; set; }

  }
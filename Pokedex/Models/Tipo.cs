using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models;

[Table("Tipo")]
public class Tipo
{
    [Key]   // chave primaria da minha tabela
    public uint Id { get; set; }

    [StringLength(30)]
    [Required(ErrorMessage = "Por favor, informe o nome")]
    public string Nome { get; set; }  //unique so pra ter um nome deste tipo
 
    [StringLength(25)]
    public string Cor { get; set; } = "#000";

    public ICollection<PokemonTipo> Pokemons { get; set; }
}

   
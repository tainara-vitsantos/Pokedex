using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models;
[Table("Tipo")]

    public class Tipo
    {
        [Key] // chave primaria da minha tabela
        public uint Id { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome")]
        [StringLength(30)]
        public string Nome { get; set; } //unique so pra ter um nome deste tipo

        [StringLength(25)]
        public string Cor { get; set; }

        public ICollection<PokemonTipo> Pokemons { get; set; }
    }

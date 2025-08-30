
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models;

    [Table("PokemonTipo")]
    public class PokemonTipo
    {
        [Key, Column(Order = 1)] // Pq não reconhece duas chaves primarias, então tem que determinar a ordem.
        public int PokemonNumero { get; set; }
        [ForeignKey("PokemonNumero")]
        
        [Key, Column(Order  = 2)]
        public int TipoId { get; set; }
        [ForeignKey("TipoId")]

        public Pokemon Pokemon { get; set; }

        public Tipo Tipo { get; set; }
    }

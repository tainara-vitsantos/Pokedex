
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models;

    [Table("PokemonTipo")]
    public class PokemonTipo
    {
        [Key, Column(Order = 1)] // Pq não reconhece duas chaves primarias, então tem que determinar a ordem.
        public uint PokemonNumero { get; set; }
        [ForeignKey("PokemonNumero")]

        public Pokemon Pokemon {get; set;}
        public virtual Pokemon Pokemon {get; set;}
        
        [Key, Column(Order  = 2)]
        public uint TiposId { get; set; }
        public uint TipoId { get; set; }
        [ForeignKey("TipoId")]
        public Tipo Tipo {get; set;}

        public ICollection<PokemonTipo> Pokemons { get; set; }
        public virtual Tipo Tipo {get; set;}
    }

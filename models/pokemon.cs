using System.Collections.Generic;

namespace PokemonAPI.Models
{
    public class Pokemon
    {
        public string? Nombre { get; set; }
        public string? Tipo { get; set; }
        public List<int>? Habilidades { get; set; } 
        public double? Defensa { get; set; }
}
}
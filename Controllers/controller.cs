using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokemonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private static List<Pokemon> pokemones = new List<Pokemon>();
        private static Mochila mochila = new Mochila();
        private static Pokedex pokedex = new Pokedex();

        // Crear un Pokémon
        [HttpPost]
        public IActionResult CrearPokemon([FromBody] Pokemon pokemon)
        {
            pokemones.Add(pokemon);
            return Ok();
        }

        // Crear múltiples Pokémon
        [HttpPost("multiples")]
        public IActionResult CrearMultiplesPokemones([FromBody] List<Pokemon> nuevosPokemones)
        {
            pokemones.AddRange(nuevosPokemones);
            return Ok();
        }

        // Editar un Pokémon
        [HttpPut("{nombre}")]
        public IActionResult EditarPokemon(string nombre, [FromBody] Pokemon pokemon)
        {
            var pokemonExistente = pokemones.Find(p => p.Nombre == nombre);
            if (pokemonExistente != null)
            {
                pokemonExistente.Nombre = pokemon.Nombre;
                pokemonExistente.Tipo = pokemon.Tipo;
                pokemonExistente.Habilidades = pokemon.Habilidades;
                pokemonExistente.Defensa = pokemon.Defensa;
                return Ok();
            }
            return NotFound();
        }

        // Eliminar un Pokémon
        [HttpDelete("{nombre}")]
        public IActionResult EliminarPokemon(string nombre)
        {
            var pokemonExistente = pokemones.Find(p => p.Nombre == nombre);
            if (pokemonExistente != null)
            {
                pokemones.Remove(pokemonExistente);
                return Ok();
            }
            return NotFound();
        }

        // Obtener un Pokémon
        [HttpGet("{nombre}")]
        public IActionResult ObtenerPokemon(string nombre)
        {
            var pokemon = pokemones.Find(p => p.Nombre == nombre);
            if (pokemon != null)
            {
                return Ok(pokemon);
            }
            return NotFound();
        }

        // Obtener todos los Pokémon de un tipo
        [HttpGet("tipo/{tipo}")]
        public IActionResult ObtenerPokemonesPorTipo(string tipo)
        {
            var pokemonesPorTipo = pokemones.Where(p => p.Tipo == tipo).ToList();
            return Ok(pokemonesPorTipo);
        }

        // Agregar un objeto a la mochila
        [HttpPost("mochila")]
        public IActionResult AgregarObjetoAMochila([FromBody] string objeto)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            mochila.Objetos.Add(objeto);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return Ok();
        }

        // Eliminar un objeto de la mochila
        [HttpDelete("mochila/{objeto}")]
        public IActionResult EliminarObjetoDeMochila(string objeto)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (mochila.Objetos.Contains(objeto))
            {
                mochila.Objetos.Remove(objeto);
                return Ok();
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return NotFound();
        }

        // Registrar un Pokémon en la Pokédex
        [HttpPost("pokedex")]
        public IActionResult RegistrarPokemonEnPokedex([FromBody] string nombre)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (!pokedex.PokemonesRegistrados.Contains(nombre))
            {
                pokedex.PokemonesRegistrados.Add(nombre);
                return Ok();
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return BadRequest("El Pokémon ya está registrado en la Pokédex.");
        }

        // Mostrar Pokémon registrados en la Pokédex
        [HttpGet("pokedex")]
        public IActionResult MostrarPokemonesRegistradosEnPokedex()
        {
            return Ok(pokedex.PokemonesRegistrados);
        }
    }
}

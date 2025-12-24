using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Runtime.InteropServices;

namespace PokemonReviewApp.Kontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller // გვაძლევს საშუალებას HTTP მეთოდებს მივწვდეთ
    {
        private readonly IPokemonRepository _pokemonRepository; // ერთჯერადად ანიჭებ
        public PokemonController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;  // მეთოდი რომელსაც უნდა დაუძახოს კნოტროლერმა
        }

        [HttpGet] // ეუბნება სისტემას რომ ამ მეთოდმა უნდა დააბრუნოს Get request
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))] //
        public IActionResult GetPokemons() // გვაძლევვს საშუალებას დავაბრუნოთ valid valid response
        {
            var pokemons = _pokemonRepository.GetPokemons(); // ვიძახებთ ფუნქციას
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // თუ რამე პრობლემაა ვაბრუნებთ 400 type responses
            }
            return Ok(pokemons); // წინააღმდეგ შემთხვევაშივაბრუნებთ 200 type responses
        }
    }
}

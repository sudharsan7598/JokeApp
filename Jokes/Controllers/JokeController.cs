using Jokes.Common;
using Jokes.Interface;
using Jokes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jokes.Controllers
{
    [ApiController]
    public class JokeController : ControllerBase
    {
        private IJokeProvider _jokeProvider;
        private readonly ILogger<JokeController> _logger;

        public JokeController(IJokeProvider jokeProvider, ILogger<JokeController> logger)
        {
            _jokeProvider = jokeProvider;
            _logger = logger;
        }

        [HttpGet]
        [Route("/FetchRandomJoke")]
        public async Task<IActionResult> FetchRandomJoke_GET()
        {
            try
            {
                string response = await _jokeProvider.FetchRandomJoke();
                return Ok(response); // Return a 200 OK response with the fetched joke
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.FetchRandomJokeErrorMsg);
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.FetchRandomJokeErrorMsg);
            }
        }

        [HttpGet]
        [Route("/SearchJoke/{searchTerm}")]
        public async Task<IActionResult> SearchJoke_GET(string searchTerm)
        {
            try
            {
                List<SearchJoke> response = await _jokeProvider.SearchJokes(searchTerm);
                return Ok(response); // Return a 200 OK response with the search results
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.SearchJokeErrorMsg);
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.SearchJokeErrorMsg);
            }
        }

    }
}


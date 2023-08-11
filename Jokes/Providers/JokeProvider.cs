using System;
using Jokes.Common;
using Jokes.Controllers;
using Jokes.Interface;
using Jokes.Models;

namespace Jokes.Providers
{
	public class JokeProvider : IJokeProvider
	{
        private IJokeRepository _jokeRepository;
        private readonly ILogger<JokeProvider> _logger;

        public JokeProvider(IJokeRepository jokeRepository, ILogger<JokeProvider> logger)
		{
			_jokeRepository = jokeRepository;
			_logger = logger;
		}

        public async Task<string> FetchRandomJoke()
        {
            try
            {
                RandomJoke baseJoke = await _jokeRepository.FetchRandomJoke();
                if (baseJoke != null)
                {
                    return baseJoke.joke;
                }
                return "Error!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.FetchRandomJokeErrorMsg);
                return "Error!";
            }
        }

        public async Task<List<SearchJoke>> SearchJokes(string searchTerm)
        {
            try
            {
                List<SearchJoke> searchedJokes = new List<SearchJoke>();
                SearchJokeDTO response = await _jokeRepository.SearchJokes(searchTerm);

                foreach (var result in response.results)
                {
                    searchedJokes.Add(new SearchJoke()
                    {
                        joke = MarkSearchTermInJoke(result.joke, searchTerm),
                        category = MapJokeCategory(result.joke)
                    });
                }

                List<SearchJoke> groupedJokes = searchedJokes.GroupBy(x => x.category)
                                                             .OrderBy(group => group.Key)
                                                             .SelectMany(group => group.OrderBy(x => x.category))
                                                             .ToList();

                return groupedJokes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.SearchJokeErrorMsg);
                return new List<SearchJoke>();
            }
        }


        private JokeLengthCategory MapJokeCategory(string joke)
        {
            int count = Utility.CountWords(joke);
            if (count < 10)
            {
                return JokeLengthCategory.Short;
            }
            else if (count >= 10 && count < 20)
            {
                return JokeLengthCategory.Medium;
            }
            else
            {
                return JokeLengthCategory.Long;
            }
        }

        private string MarkSearchTermInJoke(string joke, string searchTerm)
        {
            return joke.Replace(searchTerm, searchTerm.ToUpper());
        }
    }
}


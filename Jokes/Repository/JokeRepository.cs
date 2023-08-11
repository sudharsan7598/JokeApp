using System;
using System.Net.Http;
using Jokes.Common;
using Jokes.Interface;
using Jokes.Models;

namespace Jokes.Repository
{
	public class JokeRepository : IJokeRepository
    {
        private readonly ConfigManager _configManager;
        private readonly IJokeHttpClientFactory _httpClient;
        private readonly ILogger<JokeRepository> _logger;

        public JokeRepository(ConfigManager configManager, IJokeHttpClientFactory httpClient, ILogger<JokeRepository> logger)
		{
            _configManager = configManager;
            _httpClient = httpClient;
            _logger = logger;
		}

        public async Task<RandomJoke> FetchRandomJoke()
        {
            try
            {
                var response = await _httpClient.MakeGetCall<RandomJoke>(Constants.DefaultAPIPath, _configManager.JokeURL);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.FetchRandomJokeErrorMsg);
                return null;
            }
        }

        public async Task<SearchJokeDTO> SearchJokes(string searchTerm)
        {
            string path = string.Format(Constants.SearchAPIPath, searchTerm, _configManager.SearchJokesLimit);
            try
            {
                var response = await _httpClient.MakeGetCall<SearchJokeDTO>(path, _configManager.JokeURL);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.SearchJokeErrorMsg);
                return null;
            }
        }
    }
}


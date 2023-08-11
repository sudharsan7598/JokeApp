using System;
using Jokes.Models;

namespace Jokes.Interface
{
	public interface IJokeProvider
	{
        Task<string> FetchRandomJoke();

        Task<List<SearchJoke>> SearchJokes(string searchTerm);
    }
}


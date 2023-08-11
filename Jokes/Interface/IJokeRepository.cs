using System;
using Jokes.Models;

namespace Jokes.Interface
{
	public interface IJokeRepository
	{
		Task<RandomJoke> FetchRandomJoke();

		Task<SearchJokeDTO> SearchJokes(string searchTerm);
	}
}


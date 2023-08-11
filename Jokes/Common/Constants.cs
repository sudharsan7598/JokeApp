using System;
namespace Jokes.Common
{
	public class Constants
	{
		public const string DefaultAPIPath = "/";

		public const string SearchAPIPath = "search?term={0}&limit={1}";

        public const string FetchRandomJokeErrorMsg = "An error occurred while fetching a random joke.";

		public const string SearchJokeErrorMsg = "An error occurred while searching for jokes.";

		public const string HttpClientErrorMsg = "An error occurred while trying to connect icanhazdadjoke.com.";

    }
}


using System;
namespace Jokes.Common
{
	public interface IJokeHttpClientFactory
	{
        Task<Response> MakeGetCall<Response>(string path, string serviceURL, Dictionary<string, string> headers = null);
    }
}


using System;
namespace Jokes.Models
{
	public class RandomJoke
	{
		public string id { get; set; }
		public string joke { get; set; }
		public int status { get; set; }
	}

	public class SearchJoke
	{
		public string joke { get; set; }
		public JokeLengthCategory category { get; set; }
	}

    public class BaseJoke
    {
        public string id { get; set; }
        public string joke { get; set; }
    }

    public class SearchJokeDTO
	{
		public int current_page { get; set; }
		public int limit { get; set; }
        public int next_page { get; set; }
        public int previous_page { get; set; }
        public List<BaseJoke> results { get; set; }
        public string search_term { get; set; }
        public int status { get; set; }
        public int total_jokes { get; set; }
        public int total_pages { get; set; }
    }

	public enum JokeLengthCategory
	{
		Short,
		Medium,
		Long
	}
}


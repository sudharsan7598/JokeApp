using System;
namespace Jokes.Common
{
	public class Utility
	{
        public static int CountWords(string text)
        {
            string[] words = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }
    }
}


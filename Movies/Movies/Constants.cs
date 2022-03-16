using System;
using System.Collections.Generic;
using System.Text;

namespace Movies
{
    internal static class Constants
    {
        public static String GetMoviesUri(String searchTerm)
        {
            return $"http://www.omdbapi.com/?apikey=63d6a3cc&s={searchTerm}";
        }
    }
}

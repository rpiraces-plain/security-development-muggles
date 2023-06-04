using ClickJacking.Models;
using System.Text.Json;

namespace ClickJacking.Classes
{
    public class MovieListHelper
    {
        public static List<MovieListItem> getList()
        {
            var movieList =
                StaticHttpContext.Current?.Session.GetString("MovieList");
            if (movieList == null)
            {
                return new List<MovieListItem>();
            }
            else
            {
                return JsonSerializer.Deserialize<List<MovieListItem>>(movieList) ?? new List<MovieListItem>();
            }
        }

        public static void addToList(MovieListItem item)
        {
            var movieList = getList();
            movieList.Add(item);
            StaticHttpContext.Current?.Session.SetString("MovieList", JsonSerializer.Serialize<List<MovieListItem>>(movieList));
        }

        public static void clearList()
        {
            StaticHttpContext.Current?.Session.SetString("MovieList", "[]");
        }
    }
}

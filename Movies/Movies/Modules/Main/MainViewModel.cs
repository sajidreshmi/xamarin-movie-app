using Movies.Common.Base;
using Movies.Common.Models;
using Movies.Common.Navigation;
using Movies.Modules.MovieDetails;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies
{
    public class MainViewModel : BaseViewModel
    {
        private INetworkService _networkService;
        private INavigationService _navigationService;
        public MainViewModel(INetworkService networkService, INavigationService navigationService)
        {
            _networkService = networkService;
            _navigationService = navigationService;
            OnPropertyChanged("Items");
            //GetMovieData();
        }

        private async Task GetMovieData()
        {
            var result = await _networkService.GetTask<ListOfMovies>(ApiConstants.GetMoviesUri(SearchTerm));
            Items = new ObservableCollection<MovieData>(result.Search
                .Select(x => new MovieData(x.Title, x.Poster.Replace("SX300", "SX600"), x.Year, x.imdbID)));
            OnPropertyChanged("Items");
        }

        public ObservableCollection<MovieData> Items { get; set; }


        public string SearchTerm { get; set; }

        public ICommand PerformSearchCommand
        {
            get => new Command(async () =>
            {
                await GetMovieData();
            });
        }

        private MovieData _selectedMovie;

        public MovieData SelectedMovie
        {
            get => _selectedMovie;
            set => SetProperty(ref _selectedMovie, value);
        }

        public ICommand MovieChangedCommand
        {
            get => new Command(async () => await GotoMovieDetails());
        }

        private async Task GotoMovieDetails()
        {
            if(SelectedMovie == null)
            {
                return;
            }
            await _navigationService.PushAsync<MovieDetailsViewModel>(SelectedMovie);
            SelectedMovie = null;
        }

        /*public ObservableCollection<String> Items => new ObservableCollection<string>
        {
            "Avengers",
            "Avengers: Age of Ultron",
            "Avengers: Infinity Wars"
        };*/
    }
}

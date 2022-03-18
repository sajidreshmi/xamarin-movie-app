using Movies.Common.Base;
using Movies.Common.Models;
using Movies.Common.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies.Modules.MovieDetails
{
    public class MovieDetailsViewModel : BaseViewModel
    {
        private INetworkService _networkService;
        private INavigationService _navigationService;
        private MovieData _movieData;

        public MovieDetailsViewModel(INetworkService networkService, INavigationService navigationService)
        {
            _networkService = networkService;
            _navigationService = navigationService;
        }

        public MovieData MovieData
        {
            get { return _movieData; }
            set => SetProperty(ref _movieData, value);
        }

        private FullMovieInformation movieInformation;

        public FullMovieInformation MovieInformation
        {
            get { return movieInformation; }
            set => SetProperty(ref movieInformation, value);
        }
        public override async Task InitializeAsync(object parameter)
        {
            MovieData = parameter as MovieData;
            MovieInformation = await _networkService.GetTask<FullMovieInformation>(ApiConstants.GetMovieById(MovieData.ImdbID));
            Console.WriteLine(MovieInformation);
            //return base.InitializeAsync(parameter);
        }

        public ICommand GoBackCommand => new Command(async () => await GoBack());

        private async Task GoBack()
        {
            await _navigationService.PopAsync();
        }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get { return _isFavorite; }
            set { SetProperty(ref _isFavorite, value); }
        }

        public ICommand FavoriteCommand => new Command(async () => await SetFavoriteMovie());

        private async Task SetFavoriteMovie()
        {
            IsFavorite = !_isFavorite;
        }
    }
}

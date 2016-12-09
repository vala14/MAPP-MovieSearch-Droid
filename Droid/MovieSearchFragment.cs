
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieDownload;
using Newtonsoft.Json;

using Fragment = Android.Support.V4.App.Fragment;

namespace MovieSearch.Droid
{
	public class MovieSearchFragment : Fragment
	{
		private MovieHelper _movieHelper;
		private IApiMovieRequest _movieApi;

		private List<Movie> _movies;
		private ProgressBar _progressBar;


		public MovieSearchFragment(MovieHelper movieHelper, IApiMovieRequest movieApi)
		{
			this._movieHelper = movieHelper;
			this._movieApi = movieApi;
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			_movies = new List<Movie>();
		}

		public override void OnPause()
		{
			base.OnPause();
			_progressBar.Visibility = Android.Views.ViewStates.Gone;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var rootView = inflater.Inflate(Resource.Layout.MovieSearch, container, false);

			var movieEditText = rootView.FindViewById<EditText>(Resource.Id.movieEditText);

			_progressBar = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar);
			_progressBar.Visibility = Android.Views.ViewStates.Gone;

			var getMovieButton = rootView.FindViewById<Button>(Resource.Id.getMovieButton);
			getMovieButton.Click += async (sender, args) =>
				{
					_progressBar.Visibility = Android.Views.ViewStates.Visible;
					getMovieButton.Enabled = false;
					var manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
					manager.HideSoftInputFromWindow(movieEditText.WindowToken, 0);

					ApiSearchResponse<MovieInfo> responseMovieInfos = await _movieApi.SearchByTitleAsync(movieEditText.Text == null ? "" : movieEditText.Text);
					await _movieHelper.GetMovies(responseMovieInfos);
					_movies = _movieHelper.MoviesList;

					var intent = new Intent(this.Context, typeof(MovieListActivity));
					intent.PutExtra("movieList", JsonConvert.SerializeObject(this._movies));
					this.StartActivity(intent);
					getMovieButton.Enabled = true;
				};

			return rootView;
		}
	}
}

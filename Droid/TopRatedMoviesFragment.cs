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
	public class TopRatedMoviesFragment : Fragment
	{
		private MovieHelper _movieHelper;
		private IApiMovieRequest _movieApi;

		private List<Movie> _movies;
		private ProgressBar _progressBar;
		private ListView _listView;
		private View _rootView;


		public TopRatedMoviesFragment(MovieHelper movieHelper, IApiMovieRequest movieApi)
		{
			this._movieHelper = movieHelper;
			this._movieApi = movieApi;
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			_movies = new List<Movie>();
			MovieDbFactory.RegisterSettings(new MovieDbSettings());
		}

		public override void OnPause()
		{
			base.OnPause();
			this._progressBar.Visibility = Android.Views.ViewStates.Gone;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			this._rootView = inflater.Inflate(Resource.Layout.TopRatedMovies, container, false);
			this._listView = _rootView.FindViewById<ListView>(Resource.Id.listview);
			this._progressBar = _rootView.FindViewById<ProgressBar>(Resource.Id.progressBar);

			this._movies = new List<Movie>();
			MovieListAdapter adapter = new MovieListAdapter(this.Activity, this._movies);
			this._listView.Adapter = adapter;
			adapter.NotifyDataSetChanged();

			_listView.ItemClick += listItemClick;

			return _rootView;
		}

		private void listItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			MovieListAdapter movieListAdapter = (MovieListAdapter)_listView.Adapter;

			Movie movie = (Movie)movieListAdapter.GetMovie(e.Position);

			var intent = new Intent(Application.Context, typeof(MovieInfoActivity));

			intent.PutExtra("Name", movie.Name);
			intent.PutExtra("Genres", movie.Genres);
			intent.PutExtra("YearReleased", movie.YearReleased);
			intent.PutExtra("Overview", movie.Overview);
			intent.PutExtra("RunningTime", movie.RunningTime.ToString());
			intent.PutExtra("ImagePath", movie.ImagePath);

			StartActivity(intent);
		}

		public async Task GetMovies()
		{
			this._progressBar.Visibility = Android.Views.ViewStates.Visible;

			ApiSearchResponse<MovieInfo> responseMovieInfos = await this._movieApi.GetTopRatedAsync();

			this._movies = new List<Movie>();
			MovieListAdapter adapter = new MovieListAdapter(this.Activity, this._movies);
			this._listView.Adapter = adapter;
			adapter.NotifyDataSetChanged();

			_movieHelper.ClearMoviesList();
			await _movieHelper.GetMovies(responseMovieInfos);
			this._movies = _movieHelper.MoviesList;

			adapter = new MovieListAdapter(this.Activity, this._movies);
			this._listView.Adapter = adapter;
			adapter.NotifyDataSetChanged();
			this._progressBar.Visibility = Android.Views.ViewStates.Gone;
		}
	}
}

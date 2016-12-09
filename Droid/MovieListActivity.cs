using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
	[Activity(Theme = "@style/MyTheme", Label = "Movie list")]
	public class MovieListActivity : Activity 
	{
		private ListView _listView; 

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			this.SetContentView(Resource.Layout.MovieList);

			var jsonStr = this.Intent.GetStringExtra("movieList");
			var movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonStr);
			this._listView = this.FindViewById<ListView>(Resource.Id.listview);
			_listView.Adapter = new MovieListAdapter(this, movieList);

			// Override onclick on list item
			_listView.ItemClick += listItemClick;

			setToolbar();
		}

		private void setToolbar()
		{
			var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
			this.SetActionBar(toolbar);
			this.ActionBar.Title = this.GetString(Resource.String.ToolbarTitleList);
		}

		private void listItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			MovieListAdapter movieListAdapter = (MovieListAdapter)this._listView.Adapter;
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
	}
}

using System;
using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Text.Method;
using Android.Widget;
using Square.Picasso;

namespace MovieSearch.Droid
{
	[Activity(Theme = "@style/MyTheme", Label = "Movie info")]
	public class MovieInfoActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			this.SetContentView(Resource.Layout.MovieInfo);

			Movie _movie = getMovie();

			this.FindViewById<TextView>(Resource.Id.Name).Text = _movie.Name + " (" + _movie.YearReleased + ")";
			this.FindViewById<TextView>(Resource.Id.Genres).Text = _movie.Genres;
			this.FindViewById<TextView>(Resource.Id.Runntime).Text = this.Intent.GetStringExtra("RunningTime") + " min  |";
			this.FindViewById<TextView>(Resource.Id.Overview).Text = _movie.Overview;
			// Add scroll to Overview TextView
			this.FindViewById<TextView>(Resource.Id.Overview).MovementMethod = new ScrollingMovementMethod();

			var imageView = this.FindViewById<ImageView>(Resource.Id.Image);

			if (_movie.ImagePath != null)
			{
				Picasso.With(this).Load("http://image.tmdb.org/t/p/w92" + _movie.ImagePath).Into(imageView);
			}

			setToolbar();
		}

		private void setToolbar()
		{
			var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
			this.SetActionBar(toolbar);
			this.ActionBar.Title = this.ActionBar.Title = this.GetString(Resource.String.ToolbarTitleInfo);
		}

		private Movie getMovie()
		{
			Movie movie = new Movie();
			movie.Name = this.Intent.GetStringExtra("Name");
			movie.Genres = this.Intent.GetStringExtra("Genres");
			movie.YearReleased = this.Intent.GetStringExtra("YearReleased");
			movie.Overview = this.Intent.GetStringExtra("Overview");
			movie.ImagePath = this.Intent.GetStringExtra("ImagePath");

			return movie;
		}
	}
}
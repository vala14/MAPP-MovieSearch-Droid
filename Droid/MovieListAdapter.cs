using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.IO;
using Square.Picasso;

namespace MovieSearch.Droid
{
	public class MovieListAdapter : BaseAdapter<Movie>
	{
		private Activity _context;
		private List<Movie> _movieList;

		public MovieListAdapter(Activity context, List<Movie> movieList)
		{
			this._context = context;
			this._movieList = movieList;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public Movie GetMovie(int position)
		{
			return _movieList[position];
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			if (view == null)
			{
				view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);
			}

			var movie = this._movieList[position];
			view.FindViewById<TextView>(Resource.Id.movieTitle).Text = movie.Name + " (" + movie.YearReleased + ")";
			view.FindViewById<TextView>(Resource.Id.actors).Text = movie.Actors;
			var imageView = view.FindViewById<ImageView>(Resource.Id.image);

			if (movie.ImagePath != null)
			{
				Picasso.With((Context)this._context).Load("http://image.tmdb.org/t/p/w92" + movie.ImagePath).Into(imageView);
			}

			//var myBitmap = BitmapFactory.DecodeFile(imgFile.AbsolutePath);
			//view.FindViewById<ImageView>(Resource.Id.image).SetImageBitmap(myBitmap);

			return view;
		}

		public override Movie this[int position]
		{
			get
			{
				return this._movieList[position];
			}
		}

		public override int Count
		{
			get
			{
				return this._movieList.Count;
			}
		}
	}
}

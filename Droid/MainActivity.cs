using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using DM.MovieApi;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;
using MovieDownload;
using System.Threading;
using System.IO;
using Android.Support.V4.App;
using Android.Runtime;
using Android.Support.V4.View;

using Fragment = Android.Support.V4.App.Fragment;
using Android.Support.Design.Widget;

namespace MovieSearch.Droid
{
	[Activity(Theme = "@style/MyTheme", Label = "Movie Search", Icon = "@drawable/roundicon")]
	public class MainActivity : FragmentActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			this.SetContentView(Resource.Layout.Main);

			var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
			ToolbarTabs.Construct(this, toolbar);
		}
	}
}


using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;

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


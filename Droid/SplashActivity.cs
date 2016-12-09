using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;

namespace MovieSearch.Droid
{
	[Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			// Create your application here

			//this.StartActivity(typeof(MainActivity));
			//this.Finish();
		}

		protected override void OnResume()
		{
			base.OnResume();
			Task startupWork = new Task(() =>
			{
				Task.Delay(4000);  // Simulate a bit of startup work.
			});

			startupWork.ContinueWith(t =>
			{
				//StartActivity(typeof(MainActivity));
				StartActivity(new Intent(Application.Context, typeof(MainActivity)));
			}, System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());

			startupWork.Start();
		}
	}
}

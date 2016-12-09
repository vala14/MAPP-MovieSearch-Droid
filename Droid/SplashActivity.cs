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
		protected override void OnResume()
		{
			base.OnResume();
			Task startupWork = new Task(() =>
			{
				Task.Delay(3000);
			});

			startupWork.ContinueWith(t =>
			{
				StartActivity(new Intent(Application.Context, typeof(MainActivity)));
			}, System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());

			startupWork.Start();
		}
	}
}

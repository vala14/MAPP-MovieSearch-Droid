using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MovieSearch.Droid
{
	using Android.Support.Design.Widget;
	using Android.Support.V4.App;
	using Android.Support.V4.View;
	using Android.Views.InputMethods;
	using DM.MovieApi;
	using DM.MovieApi.MovieDb.Movies;

	public static class ToolbarTabs
	{
		private static TopRatedMoviesFragment _topRatedFragment;
		private static MovieHelper _topRatedHelper;
		private static MovieHelper _searchHelper;
		private static IApiMovieRequest _movieApi;

		public static void Construct(FragmentActivity activity, Toolbar toolbar)
		{
			MovieDbFactory.RegisterSettings(new MovieDbSettings());
			_topRatedHelper = new MovieHelper();
			_searchHelper = new MovieHelper();
			_movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

			_topRatedFragment = new TopRatedMoviesFragment(_topRatedHelper, _movieApi);

			var fragments = new Fragment[]
								{
									new MovieSearchFragment(_searchHelper, _movieApi),
									_topRatedFragment
								};
			var titles = CharSequence.ArrayFromStringArray(new[]
								{
									activity.GetString(Resource.String.SearchTab),
									activity.GetString(Resource.String.TopRatedTab)
								});

			var viewPager = activity.FindViewById<ViewPager>(Resource.Id.viewpager);
			viewPager.Adapter = new TabsFragmentPagerAdapter(activity.SupportFragmentManager, fragments, titles);
			viewPager.OffscreenPageLimit = 0;

			// Give the TabLayout the ViewPager
			var tabLayout = activity.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
			tabLayout.SetupWithViewPager(viewPager);

			tabLayout.TabSelected += async (sender, args) =>
				{
					var tab = args.Tab;
					if (tab.Position == 1)
					{
						viewPager.SetCurrentItem(tab.Position, true);
						var manager = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
						manager.HideSoftInputFromWindow(activity.CurrentFocus.WindowToken, 0);
						await _topRatedFragment.GetMovies();
					}
					else
					{ 
						viewPager.SetCurrentItem(tab.Position, true);
					}
				};

			SetToolbar(activity, toolbar);
		}

		public static void SetToolbar(Activity activity, Toolbar toolbar)
		{
			activity.SetActionBar(toolbar);
			activity.ActionBar.Title = activity.GetString(Resource.String.ToolbarTitle);
		}
	}
}

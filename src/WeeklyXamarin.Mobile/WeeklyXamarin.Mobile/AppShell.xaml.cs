﻿using System;
using System.Collections.Generic;
using WeeklyXamarin.Core.Helpers;
using WeeklyXamarin.Mobile.Views;
using Xamarin.Forms;

namespace WeeklyXamarin.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
//            WeeklyXamarin.Core.Helpers.Constants.Navigation.PageMode.Bookmarks;

            InitializeComponent();
            Routing.RegisterRoute(Constants.Navigation.Paths.Articles,typeof(ArticlesListPage));
        }
    }
}

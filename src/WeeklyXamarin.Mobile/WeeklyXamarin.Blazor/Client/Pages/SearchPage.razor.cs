﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeeklyXamarin.Core.Helpers;
using WeeklyXamarin.Core.Models;
using WeeklyXamarin.Core.Services;
using WeeklyXamarin.Core.ViewModels;

namespace WeeklyXamarin.Blazor.Client.Pages
{
    public partial class SearchPage : ComponentBase
    {

        [Inject]
        public IDataStore dataStore { get; set; }

        public ListState CurrentState { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();

        public string SearchText { get; set; }
        public List<Category> Categories { get; private set; }
        public Category SearchCategory { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Categories = (await dataStore.GetCategories()).ToList();
            Categories.Insert(0, new Category());
        }
        private CancellationTokenSource source = new CancellationTokenSource();
        public async Task SearchArticles()
        {
            CurrentState = ListState.Loading;
            source.Cancel();
            source = new CancellationTokenSource();
            Articles.Clear();
            IAsyncEnumerable<Article> articlesAsync;

            articlesAsync = dataStore.GetArticleFromSearchAsync(SearchText, SearchCategory?.Name, source.Token);

            await foreach (Article article in articlesAsync)
            {
                Articles.Add(article);
                await Task.Delay(10);
                StateHasChanged();
            }
            CurrentState = ListState.None;

        }
    }
}

﻿using DnnSummit.Manager;
using DnnSummit.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DnnSummit
{
    public partial class App : PrismApplication
    {
        public const string OfflineLoading = "/" + Constants.Navigation.LoaddingOfflineModePage;
        public const string InternetLoading = "/" + Constants.Navigation.LoadingPage;
        public const string EntryPoint = "/" + Constants.Navigation.NavigationPage + "/" + Constants.Navigation.TabbedPage;

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            var secrets = new SecretsManager();
            AppCenter.Start(
                $"android={secrets["AppCenter:iOS"]};" +
                $"ios={secrets["AppCenter:Android"]};",
                typeof(Analytics), typeof(Crashes));

            Data.Startup.Initialize();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(FindViewModel);

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                NavigationService.NavigateAsync(InternetLoading);
            }
            else
            {
                NavigationService.NavigateAsync(OfflineLoading);
            }
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterNavigation(containerRegistry);
            RegisterDependencies(containerRegistry);
        }

        private void RegisterNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoadingPage>(Constants.Navigation.LoadingPage);
            containerRegistry.RegisterForNavigation<LoadingOfflineModePage>(Constants.Navigation.LoaddingOfflineModePage);
            containerRegistry.RegisterForNavigation<DnnSummitNavigationPage>(Constants.Navigation.NavigationPage);
            containerRegistry.RegisterForNavigation<DnnSummitTabbedPage>(Constants.Navigation.TabbedPage);
            containerRegistry.RegisterForNavigation<LocationPage>(Constants.Navigation.LocationPage);
            containerRegistry.RegisterForNavigation<ScheduleDetailsPage>(Constants.Navigation.ScheduleDetailsPage);
            containerRegistry.RegisterForNavigation<SessionDetailsPage>(Constants.Navigation.SessionDetailsPage);
            containerRegistry.RegisterForNavigation<SponsorsPage>(Constants.Navigation.SponsorsPage);
        }

        private void RegisterDependencies(IContainerRegistry containerRegistry)
        {
            Data.Startup.RegisterDependencies(containerRegistry);
        }

        // Page/ViewModel Wireup Logic
        //                Page - FooPage
        //                ViewModel - FooViewModel
        private Type FindViewModel(Type viewType)
        {
            var viewName = viewType.FullName
                .Replace("Page", string.Empty)
                .Replace("Views", "ViewModels");

            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);

            return Type.GetType(viewModelName);
        }
    }
}

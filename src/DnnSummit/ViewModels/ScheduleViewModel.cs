﻿using DnnSummit.Data.Services.Interfaces;
using DnnSummit.Manager.Interfaces;
using DnnSummit.Models;
using DnnSummit.ViewModels.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DnnSummit.ViewModels
{
    public class ScheduleViewModel : BindableBase, INavigatingAware, IHasDataRetrieval
    {
        protected IItineraryService ItineraryService { get; }
        protected ISettingsService SettingsService { get; }
        protected IErrorRetryManager ErrorRetryManager { get; }
        public string Title => "Schedule";
        public ObservableCollection<Day> Days { get; }
        public ICommand ToggleOfflineNotice { get; }
        public ScheduleViewModel(
            IItineraryService itineraryService,
            ISettingsService settingsService,
            IErrorRetryManager errorRetryManager)
        {
            ItineraryService = itineraryService;
            SettingsService = settingsService;
            ErrorRetryManager = errorRetryManager;
            Days = new ObservableCollection<Day>();
            DisplayOfflineNotice = true;
            ToggleOfflineNotice = new DelegateCommand(OnToggleOfflineNotice);
        }

        private bool _displayOfflineNotice;
        public bool DisplayOfflineNotice
        {
            get { return _displayOfflineNotice; }
            set
            {
                SetProperty(ref _displayOfflineNotice, value);
                RaisePropertyChanged(nameof(DisplayOfflineNotice));
            }
        }

        private DateTime _contentRetrieved;
        public DateTime ContentRetrieved
        {
            get { return _contentRetrieved; }
            set
            {
                SetProperty(ref _contentRetrieved, value);
                RaisePropertyChanged(nameof(ContentRetrieved));
            }
        }

        private void OnToggleOfflineNotice()
        {
            DisplayOfflineNotice = !DisplayOfflineNotice;
        }

        public async Task OnLoadAsync(INavigationParameters parameters, int attempt = 0)
        {
            try
            {
                var data = await ItineraryService.GetAsync();
                Days.Clear();

                foreach (var item in data)
                {
                    Days.Add(new Day
                    {
                        Title = item.Title,
                        Messages = item.Messages.Select(x => new DayMessage
                        {
                            Title = x.Title,
                            Heading = x.Heading,
                            SubHeading = x.SubHeading,
                            Events = x.Events.Select(e => new DayEvent
                            {
                                Title = e.Title,
                                TimeSlot = e.TimeSlot,
                                Description = e.Description,
                                Room = e.Room
                            })
                        })
                    });
                }

                ContentRetrieved = SettingsService.Get().LastUpdated;
            }
            catch (Exception)
            {
                await ErrorRetryManager.HandleRetryAsync(this, parameters, attempt);
            }
        }

        public async void OnNavigatingTo(INavigationParameters parameters)
        {
            await OnLoadAsync(parameters);
        }
    }
}

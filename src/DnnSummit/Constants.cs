﻿namespace DnnSummit
{
    public static class Constants
    {
        public static class Navigation
        {
            public const string LoadingPage = "LoadingPage";
            public const string LoaddingOfflineModePage = "LoadingOfflineModePage";
            public const string NavigationPage = "NavigationPage";
            public const string TabbedPage = "TabbedPage";
            public const string VenuePage = "VenuePage";
            public const string SchedulePage = "SchedulePage";
            public const string ScheduleDetailsPage = "ScheduleDetailsPage";
            public const string SessionDetailsPage = "SessionDetailsPage";
            public const string SponsorsPage = "SponsorsPage";
            public const string CreditsPage = "CreditsPage";
            public const string FeedbackPage = "FeedbackPage";

            public static class Parameters
            {
                public const string LastUpdated = "LastUpdated";
            }
        }

        public static class ErrorHandling
        {
            public const int RetryAttempts = 5;
            public const int RetryWait = 500;
        }

        public static class Messages
        {
            public const string LastRetrieved = "Last Retrieved: {0}";
        }
    }
}

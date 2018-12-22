﻿using DnnSummit.Manager.Interfaces;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace DnnSummit.Manager
{
    public class AppCenterManager : IAppCenterManager
    {
        protected ISecretsManager SecretsManager { get; }
        public AppCenterManager(ISecretsManager secretsManager)
        {
            SecretsManager = secretsManager;
        }

        public void Initialize()
        {
            AppCenter.Start(
                $"android={SecretsManager["AppCenter:iOS"]};" +
                $"ios={SecretsManager["AppCenter:Android"]};",
                typeof(Analytics), typeof(Crashes));
        }
    }
}

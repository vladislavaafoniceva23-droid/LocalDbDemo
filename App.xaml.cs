using System;
using System.IO;
using System.Windows;

namespace WpfLocalDbDemo
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "WpfLocalDbDemo");

            if (!Directory.Exists(appDataPath))
                Directory.CreateDirectory(appDataPath);

            AppDomain.CurrentDomain.SetData("DataDirectory", appDataPath);
        }
    }
}

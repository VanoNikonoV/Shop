using ShopWpf.View.Base;
using ShopWpf.ViewModels;
using ShopWpfCore.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace ShopWpfCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindowViewModel viewModel;
        protected override void OnStartup(StartupEventArgs e)
        {

            var cultureInfo = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);

            MainWindow window = new MainWindow();

            viewModel = new();

            window.DataContext = viewModel;

            window.Show();

        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await viewModel.ShopContext.DisposeAsync();

            viewModel.Dispose();

            base.OnExit(e);
        }


    }
}

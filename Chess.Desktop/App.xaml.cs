using Chess.Desktop.ViewModels;
using Chess.Desktop.ViewModels.WindowService;
using Chess.Desktop.Views.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Desktop
{
    public partial class App : Application
    {
        MainViewModel mainVM;
        public App()
        {
            DisplayRootRegistry.RegisterWindowType<MainViewModel, MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RunProgramLogic();
        }

        private void RunProgramLogic()
        {
            mainVM = new MainViewModel();

            DisplayRootRegistry.ShowPresentation(mainVM);
        }
    }
}

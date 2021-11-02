using Chess.Desktop.ViewModels.Navigation;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows.Controls;

namespace Chess.Desktop.ViewModels.Windows
{
    public class MainViewModel : NavigateViewModel
    {
        private Frame _mainFrame = new Frame();
        public Frame MainFrame
        {
            get => _mainFrame;
            set
            {
                _mainFrame = value;
                RaisePropertyChanged(nameof(MainFrame));
            }
        }

        public MainViewModel()
        {
            NavigationSetup();
            Navigate("Views/Pages/Game.xaml");
        }

        private void NavigationSetup()
        {
            Messenger.Default.Register<NavigateArgs>(this, (x) =>
            {
                MainFrame.Navigate(new Uri(x.Url, UriKind.Relative));
            });
        }
    }
}

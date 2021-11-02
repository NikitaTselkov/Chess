using Chess.Desktop.ViewModels.Pages;
using Chess.Desktop.ViewModels.Windows;
using Chess.Desktop.ViewModels.WindowService;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Desktop.ViewModels.Navigation
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
 
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<GameViewModel>();
        }
        public MainViewModel MainVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public GameViewModel GameVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GameViewModel>();
            }
        }
    }
}

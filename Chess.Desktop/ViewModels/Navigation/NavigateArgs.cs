using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Desktop.ViewModels.Navigation
{
    public class NavigateArgs : EventArgs
    {
        public NavigateArgs()
        {

        }
        public NavigateArgs(string url)
        {
            Url = url;
        }
        public string Url { get; }

        public override string ToString()
        {
            return $"Url: {Url}.";
        }
    }
}

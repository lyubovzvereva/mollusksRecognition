using MolluskRecognition.Presenters;
using MolluskRecognition.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolluskRecognition
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            IStartView mainView = new StartWindow();
            MainPresenter mainPresenter = new MainPresenter(mainView);
            mainPresenter.Activate();
        }
    }
}

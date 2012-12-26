using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TombstoneHelper;

namespace TurboBooks
{
    public partial class AddNewBookPage : PhoneApplicationPage
    {
        public AddNewBookPage()
        {
            InitializeComponent();

            DataContext = App.AddNewPageViewModel;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            this.SaveState(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RestoreState();
            if (!App.AddNewPageViewModel.IsDataLoaded)
            {
                App.AddNewPageViewModel.LoadData();
            }
        }

    }
}
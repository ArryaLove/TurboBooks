using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using TombstoneHelper;
using TurboBooks.Data;
using System.Windows.Input;
using Coding4Fun.Phone.Controls;

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

        private void StackPanel_ManipulationStarted_1(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            ((UIElement)sender).RenderTransform = new System.Windows.Media.TranslateTransform() { X = 2, Y = 2 };
        }

        private void StackPanel_ManipulationCompleted_1(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            ((UIElement)sender).RenderTransform = null;
        }

        public ICommand AddPlaylistCommand { get; private set; }

        private void CreateAddPlaylistCommand()
        {
        }

        private void RoundButton_Click_1(object sender, RoutedEventArgs e)
        {
            var playlist = (PlaylistViewModel)((RoundButton)e.OriginalSource).CommandParameter;
            App.ViewModel.AddBook(new Book
            {
                BookName = playlist.PlaylistName,
                BookType = BookType.SystemPlaylist
            });
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

    }
}
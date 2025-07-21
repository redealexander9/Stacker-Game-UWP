using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Game_App
{
    
    public sealed partial class HowToPage : Page
    {
        public HowToPage()
        {
            this.InitializeComponent();
            FocusState Focus = FocusState;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;


        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            Debug.WriteLine("jello");
        }

        private void Page_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            Debug.WriteLine("jello");

        }

       private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                e.Handled = true;
                this.Frame.GoBack();
            }
        }

    }
}

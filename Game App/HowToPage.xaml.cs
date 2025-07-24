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

       private void OnBackRequested(object sender, BackRequestedEventArgs e)    // User pressed back button
        {
            if (this.Frame.CanGoBack)   // Checks if navigation stack has a previous page to return to
            {
                e.Handled = true;
                this.Frame.GoBack();
            }
        }

    }
}

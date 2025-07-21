using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Game_App
{
    
    public sealed partial class StartPage : Page
    {
        public StartPage()
        {
            this.InitializeComponent();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(MainPage));
        }

        private void HowToPlayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HowToPage));
        }

        private void PrintNavigationStack()
        {
            var frame = Frame;
            if (frame == null)
            {
                Debug.WriteLine("Frame is null.");
                return;
            }

            var backStack = frame.BackStack;
            if (backStack == null || backStack.Count == 0)
            {
                Debug.WriteLine("BackStack is empty.");
                return;
            }

            Debug.WriteLine("Navigation Stack:");
            foreach (var entry in backStack)
            {
                Debug.WriteLine(entry.SourcePageType.ToString());
            }
        }
    }


}

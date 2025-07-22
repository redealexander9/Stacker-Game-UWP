using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;



namespace Game_App
{
    public sealed partial class MainPage : Page
    {
        private StackerGame Game;
        private DispatcherTimer timer;
        private const int Speed = 100; // Speed in milliseconds
        private bool isCountdownComplete = false;
        private int NextRow;
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, Speed)
            };
            NextRow = 8;
            Game = new StackerGame();
            timer.Tick += Game.StartAnimation;
            timer.Tick += RedrawGrid;
            CreateGrid();
            this.Loaded += MainPage_Loaded;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
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

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            PrintNavigationStack();
            RestartGame();  
            this.Focus(FocusState.Programmatic);
        }
        private void CreateGrid()
        {
            int RectSize = (int)GameBoard.ActualWidth / Game.GridSize;

            for (int r = 0; r < Game.GridSize; r++)
            {
                for (int c = 0; c < Game.GridSize; c++)
                {
                    Rectangle GridSquare = new Rectangle();
                    GridSquare.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
                    GridSquare.Width = RectSize + 1;
                    GridSquare.Height = GridSquare.Width + 1;
                    GridSquare.Stroke = new SolidColorBrush(Windows.UI.Colors.White);

                    GridSquare.Tag = new Point(r, c);

                    Canvas.SetTop(GridSquare, r * RectSize);
                    Canvas.SetLeft(GridSquare, c * RectSize);

                    GameBoard.Children.Add(GridSquare);
                }
            }
            DrawGrid();
        }

        public void DrawGrid()
        {
            int index = 0;
            Color BackgroundColor = Color.FromArgb(255, 0, 31, 63); // Navy blue
            Color GridLines = Color.FromArgb(80, 255, 255, 255);    // White
            Color BlockColor = Color.FromArgb(255, 127, 219, 255);  // Sky Blue
            for (int r = 0; r < Game.GridSize; r++)
            {
                for (int c = 0; c < Game.GridSize; c++)
                {
                    Rectangle Rect = GameBoard.Children[index] as Rectangle;
                    if (Game.IsOn(r, c)) // Checks if specific space on Game Board is an empty space or a block
                    {
                        Rect.Fill = new SolidColorBrush(BlockColor);
                        Rect.Stroke = new SolidColorBrush(GridLines);
                    }
                    else    // Background space
                    {
                        Rect.Fill = new SolidColorBrush(BackgroundColor);
                        Rect.Stroke = new SolidColorBrush(GridLines);
                    }
                    index++;
                }
            }
        }
        public void RedrawGrid(object sender, object e)
        {
            if(Game.IsGameOver)
            {
                GameOver();
            }
            else
            {
                DrawGrid();
            }

        }
       
        private void RestartGame()
        {
            GameBoard.Children.Clear();     
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, Speed)
            };
            NextRow = 8;
            Game = new StackerGame();
            timer.Tick += Game.StartAnimation;
            timer.Tick += RedrawGrid;
            CreateGrid();
            //StopBlocksButton.IsEnabled = false;
            StartCountdownGame();
            
        }

        private async void GameOver() {
            timer.Stop();
            ContentDialog GameOverDialog = new ContentDialog()
            {
                Title = "Game Over",
                Content = "Sorry, You lost. Would you like to try again or return to main menu?",
                PrimaryButtonText = "Play Again",
                SecondaryButtonText = "Return"
            };

            ContentDialogResult Result = await GameOverDialog.ShowAsync();   // Tell the User the game is over, returns response from user, either play again or return to main menu

            if (Result == ContentDialogResult.Primary)
            {
                RestartGame();
            }
            else
            {
                this.Frame.Navigate(typeof(MainMenuPage));

            }

        }
        
        private async void GameWon()
        {
            timer.Stop();
            ContentDialog WonDialog = new ContentDialog()
            {
                Title = "You Won!",
                Content = "Congrats! You won, Do you want to play again or return to main menu?",
                PrimaryButtonText = "Play Again",
                SecondaryButtonText = "Return"             
            };

            ContentDialogResult Result = await WonDialog.ShowAsync();

            if(Result == ContentDialogResult.Primary)
            {
                RestartGame();
            }
            else
            {
                this.Frame.Navigate(typeof(MainMenuPage));
            }
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)   // User pressed back button in top left corner
        {
            if (this.Frame.CanGoBack)
            {
                RestartGame();
                e.Handled = true;
                this.Frame.GoBack();
            }
        }

        private void StartGame()
        {
            timer.Start();
            //StopBlocksButton.IsEnabled = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //StopBlocksButton.IsEnabled = false;
            StartCountdownGame();
            
        }

        private async void StartCountdownGame()
        {
            CountdownText.Text = "3";
            CountdownText.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            CountdownText.Text = "2";
            await Task.Delay(1000);
            CountdownText.Text = "1";
            await Task.Delay(1000);
            CountdownText.Text = "Go!";
            StartGame();
            await Task.Delay(1000);
            CountdownText.Visibility = Visibility.Collapsed;
        }

        private void Page_StopBlocks(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Space && CountdownText.Text == "Go!")   // If user pressed spacebar and countdown timer is done
            {
                GoToNextRow();
            }
        }

       

        private void GoToNextRow()
        {
            if (NextRow < 8) // Game is on second row 
            {
                Game.SubtractBlocks(Game.CurrentPosition);
            }
            timer.Stop();
            Game.XPosition = NextRow;
            Game.YPosition = 0;
            Game.Direction = 1;
            Game.IsLayerChanging = true;
            if(NextRow == -1)   // User reached the top
            {
                GameWon();
            }
            else
            {
                NextRow--;
                timer.Start();
                //StopBlocksButton.IsEnabled = true;
            }
        }

        
    }
}

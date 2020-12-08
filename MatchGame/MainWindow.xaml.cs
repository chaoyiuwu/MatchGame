using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Threading;

namespace MatchGame
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DispatcherTimer dispatcherTimer;
		int ElapsedSeconds;
		
		public MainWindow() {
			InitializeComponent();
			
			SetupGame();
			SetupDispatcherTimer();

			Thread thread = new Thread(CheckGameEndThread);
			thread.Start();
		}

        #region timer setup
        private void SetupDispatcherTimer()
		{
			dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
			dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
			ElapsedSeconds = 0;
			dispatcherTimer.Start();
		}
		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
			ElapsedSeconds++;
			lblTimer.Content = $"{ElapsedSeconds} s";
			CommandManager.InvalidateRequerySuggested();
		}
        #endregion
        private void SetupGame() {
			List<string> foodEmoji = EmojiList.GetEmojiList(8);

			Random random = new Random();

			foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) {
				int index = random.Next(foodEmoji.Count);
				string nextEmoji = foodEmoji[index];
				textBlock.Text = nextEmoji;
				foodEmoji.RemoveAt(index);
			}
		}


		TextBlock lastTextBlockClicked;
		bool findingMatch = false;
		private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
			/* If it's the first in the
			* pair being clicked, keep
			* track of which TextBlock
			* was clicked and make the
			* animal disappear. If
			* it's the second one,
			* either make it disappear
			* (if it's a match) or
			* bring back the first one
			* (if it's not).
			*/
			TextBlock textBlock = sender as TextBlock;
			if (findingMatch == false) {
				textBlock.Visibility = Visibility.Hidden;
				lastTextBlockClicked = textBlock;
				findingMatch = true;
			}
			else if (textBlock.Text == lastTextBlockClicked.Text) {
				textBlock.Visibility = Visibility.Hidden;
				findingMatch = false;
			}
			else {
				lastTextBlockClicked.Visibility = Visibility.Visible;
				findingMatch = false;
			}
		}

		private void CheckGameEndThread() {
			
			bool continueGame = true;
			while (continueGame) {
				try {
					Dispatcher.Invoke(() =>
					{
						continueGame = mainGrid.Children.OfType<TextBlock>().Where(t => t.Visibility == Visibility.Visible).Count() != 0;
					});
				}
                catch {
					break;
                }
			}
			dispatcherTimer.Stop();
		}
	}
}

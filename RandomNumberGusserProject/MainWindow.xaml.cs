using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Timers;
using System.Threading;
using System.Windows.Threading;

namespace RandomNumberGusserProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        DispatcherTimer StartedTimer;
        int randomNumber;
        int timerCounter = 0;
        int enteredValue = 0;
        string theEnteredValue = "THE ENTERED VALUE ";
        string tooHigh = " IS TOO HIGH";
        string tooLow = " IS TWO LOW";
        string equal = " IS EQUAL";
        
        public MainWindow()
        {
            InitializeComponent();
            
            


        }



        private void GameStartButtonClickEvent(object sender, RoutedEventArgs e)
        {
            string btntext = "";
            string newgame = "NEW GAME";
            string endgame = "END GAME";
            var btn = sender as Button;
            var sp = btn.Content as StackPanel;
            foreach (var item in sp.Children)
            {
                if (item is TextBlock)
                {
                    btntext = (item as TextBlock).Text;
                }
            }
            if (btntext.Equals(newgame))
            {
                Debug.WriteLine(btntext);

                foreach (var item in sp.Children)
                {
                    if (item is TextBlock)
                    {
                        var currbtn = (item as TextBlock);
                        currbtn.Text = endgame;
                        currbtn.Foreground = new SolidColorBrush(Colors.Red);

                    }
                }
                //when new game btn is clicked generate a random number
                randomNumber = random.Next(0, 100);
                //set the timer started
                SetTimer();


                //set visibility to all the controls
                enterNumberLabel.Visibility = Visibility.Visible;
                inputValueBox.Visibility = Visibility.Visible;
                displayBlock.Visibility = Visibility.Visible;
                Grid.SetRowSpan(displayBlock, 1);

               /* //initially display font size is 12
                displayBlock.FontSize = 12;*/

               

                //remove previous game data
                inputValueBox.Text = String.Empty;
                displayBlock.Text = String.Empty;

                //HIDE display game completed block
                finalResultDisplay.Visibility = Visibility.Hidden;




            }
            if (btntext.Equals(endgame))
            {
                Debug.WriteLine(btntext);
                foreach (var item in sp.Children)
                {
                    if (item is TextBlock)
                    {
                        var currbtn = (item as TextBlock);
                        currbtn.Text = newgame;
                        currbtn.Foreground = new SolidColorBrush(Colors.Green);

                    }
                }
                //end the timer
                EndTimer();
                //close visibility to controls
                //set visibility to all the controls
                enterNumberLabel.Visibility = Visibility.Hidden;
                inputValueBox.Visibility = Visibility.Hidden;
                displayBlock.Visibility = Visibility.Hidden;
                Debug.WriteLine(btntext);
                //HIDE display game completed block
                finalResultDisplay.Visibility = Visibility.Hidden;
            }
        }

       

        private void InputNumberChanged(object sender, TextChangedEventArgs e)
        {
            //edge case where text can be empty
            string textEmpty = inputValueBox.Text;
            if(textEmpty != "")
            {
                enteredValue = Convert.ToInt32(inputValueBox.Text);

                if (enteredValue == randomNumber)
                {
                    displayBlock.Text = theEnteredValue + enteredValue.ToString() + equal;
                    enterNumberLabel.Visibility = Visibility.Hidden;
                    inputValueBox.Visibility = Visibility.Hidden;
                    Grid.SetRowSpan(displayBlock, 3);

                    /* //when the entered value is equal the the font size of display box increase
                     displayBlock.FontSize = 30;*/

                    //display game completed block
                    finalResultDisplay.Visibility = Visibility.Visible;
                    finalResultDisplay.Text = "YOU WON TOOK " + timerBlock.Text + " TO COMPLETE.";

                        //when ever the enterted value is equal stop the timer
                        EndTimer();
                    
                }
                else if (enteredValue >= randomNumber)
                {
                    displayBlock.Text = theEnteredValue + enteredValue.ToString() + tooHigh;
                }
                else
                {
                    displayBlock.Text = theEnteredValue + enteredValue.ToString() + tooLow;
                }

            }
           
           
        }
        private  void SetTimer()
        {
            StartedTimer = new DispatcherTimer();
            StartedTimer.Interval = TimeSpan.FromMilliseconds(1000);
            StartedTimer.IsEnabled = true;
            StartedTimer.Tick += StartedTimer_Elapsed;
            
        }
        private void EndTimer()
        {
            StartedTimer.Stop();
            timerCounter = 0;
        }

        private void StartedTimer_Elapsed(object sender, EventArgs e)
        {
            timerCounter += 1;
            timerBlock.Text = timerCounter.ToString() + " " + "SECONDS";
            Debug.WriteLine(timerCounter.ToString());
        }
    }
}

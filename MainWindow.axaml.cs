using System;
// using System.Media;
// using System.Security.Permissions;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace Timer;

public partial class MainWindow : Window
{
    bool timerRunning = false;
    string resetTo = "0:0:0";
    int seconds;
    DispatcherTimer timer;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnButtonClick(object sender, RoutedEventArgs EventArgs) {
        var source = EventArgs.Source as Control;
        if (source != null) {
            Console.WriteLine("Button: " + source.Name);
        
            switch(source.Name) {
                case "StartStop":
                    toggleTimer(this.FindControl<TextBox>("MainTextBox").Text);
                    break;
                case "Reset":
                    timer.Stop();
                    this.FindControl<TextBox>("MainTextBox").Text = resetTo;
                    timerRunning = false;
                    break;
                default:
                    Console.WriteLine("Not a button(?)");
                    break;
            }
        }
    }
    public void toggleTimer(string time) {
        if (timerRunning == false) {
            string[] timeSplit = time.Split(":");
            Console.WriteLine(timeSplit.Length);
            seconds = 0;
            if (timeSplit.Length == 2) {
                seconds = (int.Parse(timeSplit[0])*60)+int.Parse(timeSplit[1]);
            }else if (timeSplit.Length == 3) {
                seconds = ((int.Parse(timeSplit[0])*60)*60)+(int.Parse(timeSplit[1])*60)+int.Parse(timeSplit[2]);
            }else {
                seconds = int.Parse(timeSplit[0]);
            }
            Console.WriteLine(seconds);
            int minutes = seconds / 60;
            int tempSeconds = seconds % 60;
            int hours = minutes / 60;
            minutes = minutes % 60;
            resetTo = (hours+":"+minutes+":"+tempSeconds);
            seconds -= 1;
            timer = new DispatcherTimer(new TimeSpan(00,00,01), DispatcherPriority.Normal, (x, y) => {
                int minutes = seconds / 60;
                int tempSeconds = seconds % 60;
                int hours = minutes / 60;
                minutes = minutes % 60;
                string setTo = (hours+":"+minutes+":"+tempSeconds);
                seconds -= 1;
                Console.WriteLine(setTo);
                this.FindControl<TextBox>("MainTextBox").Text = setTo;
                if (seconds < 0) {
                    timer.Stop();
                }
            });
            timer.Start();
            
            timerRunning = true;
        } else if (timerRunning == true) {
            timer.Stop();
            timerRunning = false;
        }
    }
}
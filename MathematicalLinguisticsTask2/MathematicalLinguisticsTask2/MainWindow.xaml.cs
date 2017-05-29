using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MathematicalLinguisticsTask2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Automat Automat
        {
            get { return DataContext as Automat; }
        }

        private bool _started;
        private CancellationTokenSource _tokenSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnStartStop_Click(object sender, RoutedEventArgs e)
        {
            _started = !_started;

            if (_started)
            {
                
                btnStartStop.Content = "Stop";
                _tokenSource = new CancellationTokenSource();

                await Task.Factory.StartNew(() =>
                {
                    var _currentPosition = Dispatcher.Invoke(() => Automat.CurrentPosition);
                    var currentWordLength = Dispatcher.Invoke(() => Automat.Word.Length);
                    while (_currentPosition < currentWordLength && !_tokenSource.IsCancellationRequested)
                    {
                        Dispatcher.Invoke(() => Automat.PerformStep());
                        Thread.Sleep(1000);
                        _currentPosition = Dispatcher.Invoke(() => Automat.CurrentPosition);
                    }
                }, _tokenSource.Token);
            }
            else
            {
                btnStartStop.Content = "Start";
                _tokenSource.Cancel();
            }
        }

        private void BtnStep_Click(object sender, RoutedEventArgs e)
        {
            if(Automat.CurrentPosition < Automat.Word.Length)
                Automat.PerformStep();
            else
            {
                btnStartStop.IsEnabled = false;
                btnStep.IsEnabled = false;

                if (Automat.WordIsAcceptable)
                   Automat.AcceptedWords.Add(Automat.Word);

                Automat.CurrentPosition = 0;
            }
        }

        private void BtnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();

            if (!string.IsNullOrWhiteSpace(dialog.FileName))
            {
                using (StreamReader sr = new StreamReader(dialog.FileName))
                {
                    foreach (var word in sr.ReadToEnd().Split('#'))
                    {
                        if(word.IsMatchingAlphabet(Automat.Alphabet))
                            Automat.ReadedWords.Add(word);
                    }
                }
            }
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var wordAlreadyAccepted = Automat.AcceptedWords.Contains(e.AddedItems[0]);
            btnStartStop.IsEnabled = !wordAlreadyAccepted;
            btnStep.IsEnabled = !wordAlreadyAccepted;

            Automat.Word = e.AddedItems[0].ToString();
            Automat.ProcessedWord = string.Empty;
            Automat.CurrentPosition = 0;
            Automat.CurrentStates.Clear();
            Automat.CurrentStates.Add(Automat.States.Single(s => s.Name.Equals("Q0")));
            Automat.StateTraces.Clear();
            Automat.StateTraces.Add(new StateTrace() { Automat.States.Single(s => s.Name.Equals("Q0")) });
        }
    }
}

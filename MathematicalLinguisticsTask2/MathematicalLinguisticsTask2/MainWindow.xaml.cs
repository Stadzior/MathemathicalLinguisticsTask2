using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
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

        public Dictionary<string, bool> ReadedWords { get; set; }

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
                    while (_currentPosition < Automat.Word.Length && !Automat.CurrentState.Equals("Q11") && !_tokenSource.IsCancellationRequested)
                    {
                        Dispatcher.Invoke(() => TuringMachine.PerformStep());
                        Thread.Sleep(2000);
                        _currentPosition = Dispatcher.Invoke(() => TuringMachine.HeadPosition);
                    }
                    Dispatcher.Invoke(() => false);
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
            TuringMachine.PerformStep();
            btnStep.IsEnabled = TuringMachine.HeadPosition > 0;
        }

        private void BtnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            var filePath = new OpenFileDialog().FileName;
            ReadedWords = new Dictionary<string, bool>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                foreach (var word in sr.ReadToEnd().Split('#'))
                    ReadedWords.Add(word, false);
            }
        }
    }
}

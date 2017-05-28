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
                    while (_currentPosition < Automat.Word.Length && !_tokenSource.IsCancellationRequested)
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
            Automat.PerformStep();
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
    }
}

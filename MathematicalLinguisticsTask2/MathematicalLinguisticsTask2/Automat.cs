using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalLinguisticsTask2
{
    public class Automat : NotifyPropertyChangedBase
    {

        private string _word;
        public string Word
        {
            get { return _word; }
            set
            {
                SetField(ref _word, value);
            }
        }

        public ObservableCollection<State> States { get; set; }

        private State _currentState;
        public State CurrentState
        {
            get { return _currentState; }
            set
            {
                SetField(ref _currentState, value);
            }
        }

        public int MyProperty { get; set; }

        private int _currentPosition;
        public int CurrentPosition
        {
            get { return _currentPosition; }
            set
            {
                SetField(ref _currentPosition, value);
            }
        }

        public readonly HashSet<char> Alphabet = new HashSet<char>() { '0','1','2','3','4','5','6','7','8','9' }; 

        public Automat()
        {
        }
    }
}

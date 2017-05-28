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

        private string _processedWord;
        public string ProcessedWord
        {
            get { return _processedWord; }
            set
            {
                SetField(ref _processedWord, value);
            }
        }

        public bool WordIsAcceptable
            => StateTraces.Any(trace => trace.HasAcceptableState);

        private string _currentStates;
        public string CurrentStates
        {
            get { return _currentStates; }
            set
            {
                SetField(ref _currentStates, value);
            }
        }

        public ObservableCollection<State> States { get; set; }

        public ObservableCollection<StateTrack> StateTraces { get; set; }

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
            States = new ObservableCollection<State>()
            {
                new State() { Name = "Q0"},
                new State() { Name = "Q1"},
                new State() { Name = "Q2"},
                new State() { Name = "Q3"},
                new State() { Name = "Q4"},
                new State() { Name = "Q5"},
                new State() { Name = "Q6"},
                new State() { Name = "Q7"},
                new State() { Name = "Q8"},
                new State() { Name = "Q9"},
                new State() { Name = "Q10"},
                new State() { Name = "Q11", IsAcceptable = true},
            };

            InitializeStateTree();
        }

        private void InitializeStateTree()
        {
            var q0 = States.Single(s => s.Name.Equals("Q0"));
            var q1 = States.Single(s => s.Name.Equals("Q1"));
            var q2 = States.Single(s => s.Name.Equals("Q2"));
            var q3 = States.Single(s => s.Name.Equals("Q3"));
            var q4 = States.Single(s => s.Name.Equals("Q4"));
            var q5 = States.Single(s => s.Name.Equals("Q5"));
            var q6 = States.Single(s => s.Name.Equals("Q6"));
            var q7 = States.Single(s => s.Name.Equals("Q7"));
            var q8 = States.Single(s => s.Name.Equals("Q8"));
            var q9 = States.Single(s => s.Name.Equals("Q9"));
            var q10 = States.Single(s => s.Name.Equals("Q10"));
            var q11 = States.Single(s => s.Name.Equals("Q11"));

            q0.PossibleNextStates.Add(q0, value => value > -1 && value < 10);
            q0.PossibleNextStates.Add(q1, value => value == 0);
            q0.PossibleNextStates.Add(q2, value => value == 1);
            q0.PossibleNextStates.Add(q3, value => value == 2);
            q0.PossibleNextStates.Add(q4, value => value == 3);
            q0.PossibleNextStates.Add(q5, value => value == 4);
            q0.PossibleNextStates.Add(q6, value => value == 5);
            q0.PossibleNextStates.Add(q7, value => value == 6);
            q0.PossibleNextStates.Add(q8, value => value == 7);
            q0.PossibleNextStates.Add(q9, value => value == 8);
            q0.PossibleNextStates.Add(q10, value => value == 9);

            q1.PossibleNextStates.Add(q11, value => value == 0);
            q2.PossibleNextStates.Add(q11, value => value == 1);
            q3.PossibleNextStates.Add(q11, value => value == 2);
            q4.PossibleNextStates.Add(q11, value => value == 3);
            q5.PossibleNextStates.Add(q11, value => value == 4);
            q6.PossibleNextStates.Add(q11, value => value == 5);
            q7.PossibleNextStates.Add(q11, value => value == 6);
            q8.PossibleNextStates.Add(q11, value => value == 7);
            q9.PossibleNextStates.Add(q11, value => value == 8);
            q10.PossibleNextStates.Add(q11, value => value == 9);

            q11.PossibleNextStates.Add(q11, value => value > -1 && value < 10);
        }

        public void PerformStep()
        {

        }
    }
}

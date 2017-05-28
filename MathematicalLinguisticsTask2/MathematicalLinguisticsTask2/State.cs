using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalLinguisticsTask2
{
    public class State : NotifyPropertyChangedBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetField(ref _name, value);
            }
        }

        public IList<State> PossibleNextStates { get; set; }


    }
}

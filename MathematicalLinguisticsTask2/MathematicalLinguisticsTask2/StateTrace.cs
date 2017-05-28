using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalLinguisticsTask2
{
    public class StateTrace : ObservableCollection<State>
    {
        public bool IsWrong { get; set; }
        public bool HasAcceptableState
        {
            get { return this.Any(s => s.IsAcceptable); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetField(ref _description, value); }
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler OrdinaryPropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            OrdinaryPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public new void Add(State item)
        {
            base.Add(item);
            Description += $"->{item.Name}";
        }
    }
}

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDo.Annotations;
using Xamarin.Forms;

namespace ToDo.Model
{
    public enum State
    {
        TODO,
        DONE
    }

    public class ToDo:INotifyPropertyChanged
    {
        public Color StateColor => State.Equals(State.DONE) ? Color.DarkSeaGreen : Color.PaleVioletRed;

        public DateTime DateTime { get; set; }

        public string Title { get; set; }

        private State state;

        public State State
        {
            get => state;
            set
            {
                state = value;
                OnPropertyChanged(nameof(State));
                OnPropertyChanged(nameof(StateColor));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

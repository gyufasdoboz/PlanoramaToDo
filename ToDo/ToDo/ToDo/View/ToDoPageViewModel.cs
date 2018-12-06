using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDo.Annotations;
using ToDo.Model;

namespace ToDo
{
    public class ToDoPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Connects the ToDo ListView to the actual entity
        /// </summary>
        public ObservableCollection<Model.ToDo> ToDoList => ToDoHandler.GetCurrentList();

        /// <summary>
        /// Makes available the options from the `Order` enum in the picker
        /// </summary>
        private ObservableCollection<string> orderBy;
        public ObservableCollection<string> OrderBy
        {
            get
            {
                if (orderBy == null)
                {
                    orderBy = new ObservableCollection<string>(Enum.GetNames(typeof(Order)));
                    OnPropertyChanged(nameof(OrderBy));

                }
                return orderBy;
            }
            set
            {
                orderBy = value;
                OnPropertyChanged(nameof(OrderBy));
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

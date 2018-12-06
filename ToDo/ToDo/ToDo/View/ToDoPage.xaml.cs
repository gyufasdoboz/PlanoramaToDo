using System;
using System.Threading.Tasks;
using ToDo.Model;
using Xamarin.Forms;

namespace ToDo
{
    /// <summary>
    /// This page is responsible for the UI interactions
    /// </summary>
    public partial class ToDoPage
    {
        private readonly ToDoPageViewModel viewModel;
        public ToDoPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ToDoPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // To refresh the UI when on reappearing
            ToDoListView.ItemsSource = viewModel.ToDoList;

            // -it's just handy
            AddButton.Focus();
        }

        /// <summary>
        /// When the button is pressed a new modal window appears,
        /// in which we can add a new ToDo to the list.
        /// </summary>
        private async Task AddButton_OnClicked(object sender, EventArgs e)
        {
            AddButton.IsEnabled = false;
            await Navigation.PushModalAsync(new AddTaskPage());
            AddButton.IsEnabled = true;
        }

        /// <summary>
        /// When modifying the sorting mode, the ToDoHandler reorder the ToDo list
        /// And we make sure that the UI gets updated
        /// </summary>
        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ToDoHandler.Ordering = ConvertStringToEnum(((Picker)sender).SelectedItem.ToString());
            ToDoHandler.Order();
            ToDoListView.ItemsSource = null;
            ToDoListView.ItemsSource = viewModel.ToDoList;
        }

        /// <summary>
        /// When a ToDo's state (TODO/DONE) is touched,
        /// the state will change to the opposite
        /// </summary>
        private void StateBoxView_OnClicked(object sender, EventArgs e)
        {
            ((Model.ToDo)((Button)sender).BindingContext).State = ((Model.ToDo)((Button)sender).BindingContext).State == State.DONE ? State.TODO : State.DONE;
        }

        /// <summary>
        /// When the save is hit, the ToDoHandler takes care of the saving process
        /// </summary>
        private async Task SaveButton_OnClicked(object sender, EventArgs e)
        {
            await ToDoHandler.SaveToDoList();
        }


        /// <summary>
        /// Helper method
        /// </summary>
        public static Order ConvertStringToEnum(string value)
        {
            return (Order)Enum.Parse(typeof(Order), value);
        }
    }
}

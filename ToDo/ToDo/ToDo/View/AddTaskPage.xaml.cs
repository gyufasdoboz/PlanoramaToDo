using System;
using ToDo.Model;
using Xamarin.Forms.Xaml;

namespace ToDo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTaskPage
    {
        public AddTaskPage()
        {
            InitializeComponent();
            ToDoTitle.Completed += (s, e) => {
                DoneButton.Focus();
            };
        }

        /// <summary>
        /// If adding is cancelled, we return to the ToDo list page
        /// </summary>
        private void CancelButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        /// <summary>
        /// We make sure, that the entry field is in focus,
        /// just because its handy
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ToDoTitle.Focus();
        }

        /// <summary>
        /// If the User hits the done button, the new ToDo will be added
        /// to the list via the `ToDoHandler`
        /// (Warning: the input is not validated, the user is allowed to enter empty string, or anything else)
        /// </summary>
        private void DoneButton_OnClicked(object sender, EventArgs e)
        {
            var toDo = new Model.ToDo { DateTime = DateTime.Now, State = State.TODO, Title = ToDoTitle.Text };
            ToDoHandler.Add(toDo);
            Navigation.PopModalAsync();
        }
    }
}
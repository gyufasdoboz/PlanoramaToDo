using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Model
{
    public enum Order
    {
        ByName,
        ByDate
    }

    public static class ToDoHandler
    {
        private static ObservableCollection<ToDo> toDoList = new ObservableCollection<ToDo>();
        public static Order Ordering { get; set; } = Model.Order.ByDate;
        
        /// <summary>
        /// When we add new item, we order the list again
        /// </summary>
        public static void Add(ToDo toDo)
        {
            toDoList.Add(toDo);
            Order();
        }

        /// <summary>
        /// We use the WebService to load the user data
        /// </summary>
        /// <returns></returns>
        public static async Task LoadToDoListAsync()
        {
            var loadedToDoList = await App.WebService.GetToDoListAsync();
            toDoList =new ObservableCollection<ToDo>(loadedToDoList);
            Order();
        }

        public static void Order()
        {
            if (Ordering == Model.Order.ByDate)
            {
                toDoList = new ObservableCollection<ToDo>(toDoList.OrderBy(x => x.DateTime).ToList());
            }
            else
            {
                toDoList= new ObservableCollection<ToDo>(toDoList.OrderBy(x => x.Title).ToList());
            }
        }

        public static ObservableCollection<ToDo> GetCurrentList()
        {
            return toDoList;
        }

        /// <summary>
        /// When we save the ToDo list, the Webservice gets called
        /// </summary>
        /// <returns></returns>
        public static async Task SaveToDoList()
        {
            await App.WebService.SaveToDoListAsync(toDoList.ToList());
        }
    }
}

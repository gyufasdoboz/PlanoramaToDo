using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace ToDo.DB
{
    /// <summary>
    /// This is an SQLite DB, just for storing the user data
    /// on the local storage. This can be changed to anything else
    /// if it's needed
    /// </summary>
    public class ToDoDatabase : IWebService
    {
        readonly SQLiteAsyncConnection database;

        public ToDoDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Model.ToDo>().Wait();
        }

        public Task<List<Model.ToDo>> GetToDoListAsync()
        {
            return database.Table<Model.ToDo>().ToListAsync();
        }

        /// <summary>
        /// We save the whole ToDo list at once,
        /// so before doing it, we delete the content of the DB
        /// </summary>
        public async Task SaveToDoListAsync(List<Model.ToDo> toDoList)
        {
            await DeleteToDoListAsync();
            await database.InsertAllAsync(toDoList);
        }

        public async Task DeleteToDoListAsync()
        {
            if (database.Table<Model.ToDo>() != null)
            {
                await database.DropTableAsync<Model.ToDo>();
                database.CreateTableAsync<Model.ToDo>().Wait();
            }
        }
    }
}

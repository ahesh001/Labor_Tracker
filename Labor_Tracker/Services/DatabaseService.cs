using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Labor_Tracker.Models;

// This service handles interactions with the local SQLite database, including adding and retrieving users.
namespace Labor_Tracker.Services
{
    public class DatabaseService
    {
        private static SQLiteAsyncConnection _database;
        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "userdb.db3"); //Creates or connects to userdb.db3 in the app's local data directory
            _database = new SQLiteAsyncConnection(dbPath); //Ensures the User table exists.
            _database.CreateTableAsync<User>().Wait();
        }

        //Insterting new users into the database
        public Task<int> AddUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        //Retrieve a user by username
        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _database.Table<User>()
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}

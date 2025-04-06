using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This model represents the user entity in the database. It includes fields for the username, password hash, and salt.

namespace Labor_Tracker.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } //The primary key for the SQLite database table.

        [Unique]
        public string Username { get; set; } //The user's username. Marked as [Unique] to prevent duplicate usernames.

        public string PasswordHash { get; set; }

        public string Salt { get; set; }
    }
}

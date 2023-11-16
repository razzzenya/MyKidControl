using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Configuration;

namespace MainLibrary
{
    internal class TrackMethodsClass
    {
        public static double method() => 5;

    }

    class User
    {
        public Dictionary<string, string> proccesses 
        {
            get => proccesses;
            set { proccesses = value; }
        } //name of proccess:time
        public string name
        {
            get => name; 
            set {  name = value; } 
        }
        public string password
        {
            get => password;
            set { password = value; }
        }
        public User()
        {
            name = "Иван";
            password = "1234";
        }
        public User(Dictionary<string, string> proccesses, string name, string password)
        {
            this.proccesses = proccesses;
            this.name = name;
            this.password = password;
        }
        ~User()
        {
            proccesses.Clear();
            proccesses = null;
        }
    }

    class UsersCollection
    {
        private List<User> users;
        public UsersCollection()
        {
            users = new List<User>();
        }
        public UsersCollection(List<User> users)
        {
            this.users = users;
        }

        public void AddUser(User user) => users.Add(user);

        public Dictionary<string, string> GetUserProccesses(string username)
        {
            foreach(User user in users) 
            {
                if (user.name == username)
                {
                    return user.proccesses;
                }
            }
            return null;
        }

        ~UsersCollection()
        {
            users.Clear();
            users = null;
        }

    }

}

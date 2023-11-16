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
        private Dictionary<string, string> proccesses 
        {
            get { return proccesses; }
            set { proccesses = value; }
        } //name of proccess:time
        private string name
        {   
            get { return name; } 
            set {  name = value; } 
        }
        private string password;
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

    public class UsersCollection
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
        public void AddUser(User user)
        {
           users.Add(user);
        }

        public Dictionary<string, string> GetUserProccesses(string username)
        {
            foreach(User user in users) 
            {
                if (user.Name() == username)
                {
                    return user.Proccesses();
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

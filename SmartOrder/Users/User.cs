using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOrder
{
    class User
    {
        private int user_id;
        private string name;
        private string password;
        private Boolean online;

        #region setters/getters
        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string HiddenPassword
        {
            get { return "******"; }
        }

        public Boolean Online
        {
            get { return online; }
            set { online = value; }
        }
        public String State
        {
            get { return Online ? "Συνδεδεμένος" : "Αποσυνδεδεμένος"; }
        }
        #endregion

        public User(int user_id, string name, string password)
        {
            User_id = user_id;
            Name = name;
            Password = password;
        }

        public override string ToString()
        {
            return User_id + " " + Name + " " + Password;
        }

    }
}

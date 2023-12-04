using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viktoryna_Pytymko_V.L
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string BirthDate { get; set; }

        public User(string login, string password, string birthdate)
        {
            Login = login;
            Password = password;
            BirthDate = birthdate;
        }

        public static User FromString(string data)
        {
            string[] parts = data.Split(',');
            return new User(parts[0], parts[1], parts[2]);
        }
        public override string ToString()
        {
            return $"{Login},{Password},{BirthDate}";
        }
    }
}

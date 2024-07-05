using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanPing
{
    public class Users
    {
        private string id,login, ip, hostname;
        public string Id { get { return id; } set { id = value; } }
        public string Login { get { return login; } set { login = value; } }
        public string Ip { get { return ip; } set { ip = value; } }
        public string Hostname { get { return hostname; } set { hostname = value; } }

        static List<Users> listagem = new List<Users>();
        public List<Users> Lista { get { return listagem; } set { listagem = value; } }

        public Users()
        {
        }

        public Users(string id, string login, string ip, string hostname)
        {
            this.id = id;
            this.login = login;
            this.ip = ip;
            this.hostname = hostname;
        }
    }
}

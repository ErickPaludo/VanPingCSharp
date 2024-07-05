using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace VanPing
{
    public class PingV
    {
        Dados dados = new Dados();
        public PingV()
        {
        }
        public void TestPing()
        {
            Users user = new Users();
            foreach (Users obj in user.Lista)
            {
                if (new Ping().Send(obj.Ip).Status == IPStatus.Success)
                {
                    Console.WriteLine($"{DateTime.Now} - IP {obj.Ip} | User: {obj.Login} / {obj.Id} - Conexão: OK");
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now} - IP {obj.Ip} | User: {obj.Login} / {obj.Id} - Conexão: Falha / Usuário desconectado");
                    dados.Deslog(Convert.ToInt32(obj.Id));
                }
            }

        }
    }
}

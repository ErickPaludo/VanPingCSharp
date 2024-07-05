using System;

namespace VanPing
{
    internal class Program
    {
        static void  Main(string[] args)
        {
            Dados dados = new Dados();
            PingV ping = new PingV();
            dados.ColetaUsuarios();

            while (true)
            {
                ping.TestPing();
                Thread.Sleep(5000);
                dados.ColetaUsuarios();
            }

        }
    }
}
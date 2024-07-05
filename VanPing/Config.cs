using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanilla
{
    internal class Config
    {
        public Config()
        {
        }

        public void GravarLog(string info) //Grava o endereço do banco em um .tex
        {
            using (StreamWriter sw = File.AppendText("Logs.txt"))
            {
                sw.WriteLine(info);
                sw.Close();
            }
        }
        public string Lerdados() //lê o endereço do banco
        {
            string endereco = "";

            try
            {
                StreamReader sr = new StreamReader("settings.txt");
                endereco = sr.ReadLine();
                sr.Close();
            }
            catch
            {
                StreamWriter sw = File.CreateText("settings.txt");
            }
           
            return endereco;
        }
    }
}

using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla;
using static System.Net.Mime.MediaTypeNames;

namespace VanPing
{
    public class Dados
    {
        Config config = new Config();
        Users users = new Users();
        public void ColetaUsuarios()
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(config.Lerdados()))
                {
                    connection.Open();
                    using (OracleCommand cmd = new OracleCommand($"select * from view_users_logados", connection))
                    {
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bool id = users.Lista.Exists(item => item.Id.Equals(reader["id"].ToString(), StringComparison.OrdinalIgnoreCase));

                                if (!id)
                                {
                                    users.Lista.Add(new Users(reader["id"].ToString(), reader["login"].ToString(), reader["ip"].ToString(), reader["hostname"].ToString()));
                                }
                            }
                            foreach(Users obj in users.Lista)
                            {
                                VerificaAtividade(Convert.ToInt32(Convert.ToInt32(obj.Id)));
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} - {ex.ToString()}");
                config.GravarLog($"{DateTime.Now} - {ex.ToString()}");
            }
        }
        public void Deslog(int id)
        {
            Config config = new Config();
            using (OracleConnection connection = new OracleConnection(config.Lerdados()))
            {
                try
                {
                    connection.Open();
                    using (OracleCommand cmd = new OracleCommand("vnl_pkg_users.vnl_deslog", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("v_id", OracleDbType.Int16).Value = id;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    //  MessageBox.Show(ex.Message, "Houve um erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void VerificaAtividade(int id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(config.Lerdados()))
                {
                    connection.Open();

                    using (OracleCommand cmd = new OracleCommand("vnl_prc_verificar_existencia_usuario", connection))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new OracleParameter("v_valor", OracleDbType.NVarchar2, ParameterDirection.Input)).Value = id;
                            cmd.Parameters.Add(new OracleParameter("r_retorno", OracleDbType.Boolean, ParameterDirection.Output));

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {

                        }

                        bool v_retorno = Convert.ToBoolean(cmd.Parameters["r_retorno"].Value.ToString());

                        if (v_retorno != true)
                        {
                            Users us = new Users();
                            foreach (Users obj in us.Lista)
                            {
                                if (obj.Id == id.ToString())
                                {
                                    us.Lista.Remove(obj);
                                    break;
                                }
                            }
                            Deslog(id);
                        }
                        connection.Close();
            
                    }
                }
            }
            catch (Exception ex)
            {
        
            }
        }
    }
}

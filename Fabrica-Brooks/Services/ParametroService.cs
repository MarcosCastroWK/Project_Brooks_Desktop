using ConfigurationSilc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibSILC.Services
{
    public class ParametroService 
    {

        public ParametroService()
        {
            _silcConfig = new SilcConfigurationManager();
            connectionString = _silcConfig.GetConnectionString() ?? "server=localhost;user id=root;password=12345;database=test";
        }

        private SilcConfigurationManager _silcConfig;
        private string connectionString;

        public Dictionary<string, string> Chaves()
        {
            Dictionary<string, string> parametros = new Dictionary<string, string>();

            string query = "SELECT chave, valor FROM chaves";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string chave = reader["chave"].ToString();
                        string valor = reader["valor"].ToString();
                        parametros[chave] = valor;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }

            return parametros;
        }
    }
}

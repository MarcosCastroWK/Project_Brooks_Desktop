using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using PdfSharp.Charting;

namespace formSILC
{
    public partial class frmChaves : Form
    {

        private string connectionString = "server=localhost;user id=root;password=12345;database=test";
        private DataTable dataTable = new DataTable();

        public frmChaves()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.frmChaves_Load);
            //gridChaves.UserAddedRow += gridChaves_UserAddedRow;
            gridChaves.CellValueChanged += gridChaves_CellValueChanged;

        }
        private void frmChaves_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string query = "SELECT chave, valor FROM chaves";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    gridChaves.Rows.Clear();
                    while (reader.Read())
                    {
                        gridChaves.Rows.Add(reader["chave"].ToString(), reader["valor"].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }



        private void SaveChanges()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM chaves", connection);
                    MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(adapter);

                    adapter.Update(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar alterações: " + ex.Message);
                }
            }
        }


        private void btnAddRow_Click(object sender, EventArgs e)
        {
            gridChaves.Rows.Add();
        }

        private void gridChaves_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || gridChaves.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }

            string chave = gridChaves.Rows[e.RowIndex].Cells["chave"].Value?.ToString();
            string valor = gridChaves.Rows[e.RowIndex].Cells["valor"].Value?.ToString();

            if (!string.IsNullOrEmpty(chave) && !string.IsNullOrEmpty(valor))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Verificar se a chave já existe no banco de dados
                    string selectQuery = "SELECT COUNT(*) FROM chaves WHERE chave = @chave";
                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@chave", chave);
                        int count = Convert.ToInt32(selectCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            // Atualizar linha existente
                            string updateQuery = "UPDATE chaves SET valor = @valor WHERE chave = @chave";
                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@chave", chave);
                                updateCommand.Parameters.AddWithValue("@valor", valor);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Inserir nova linha
                            string insertQuery = "INSERT INTO chaves (chave, valor) VALUES (@chave, @valor)";
                            try
                            {
                                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@chave", chave);
                                    insertCommand.Parameters.AddWithValue("@valor", valor);
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                            catch (MySqlException ex)
                            {
                                if (ex.Number == 1062) // Código de erro para duplicidade de chave
                                {
                                    MessageBox.Show("Chave duplicada. A chave deve ser única.");
                                    gridChaves.Rows[e.RowIndex].Cells["chave"].Value = null; // Limpar a célula da chave duplicada
                                }
                                else
                                {
                                    MessageBox.Show("Erro: " + ex.Message);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void gridChaves_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string chave = e.Row.Cells["chave"].Value?.ToString();
            if (!string.IsNullOrEmpty(chave))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM chaves WHERE chave = @chave";
                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@chave", chave);
                        deleteCommand.ExecuteNonQuery();
                    }
                }
            }
        }


    }
}

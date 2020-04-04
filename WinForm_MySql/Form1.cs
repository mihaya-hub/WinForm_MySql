using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_MySql
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;Database=student;Uid=root;Pwd=password");

        public Form1()
        {
            InitializeComponent();
        }

        private void Read_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();

                string query = "SELECT * FROM student";

                connection.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];

                    dataGridView1.Rows.Add(row.ItemArray);
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Create_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count != 1) return;

                string query = "INSERT INTO student (grade, cclass, no, name, score) VALUES (";
                DataGridViewRow row = dataGridView1.SelectedCells[0].OwningRow;

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    query += "'" + row.Cells[i].Value.ToString();
                    if (i + 1 != row.Cells.Count) query += "', ";
                    else query += "')";
                }

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                Read.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count != 1) return;

                string query = "UPDATE student SET ";
                DataGridViewRow row = dataGridView1.SelectedCells[0].OwningRow;

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    query += row.Cells[i].OwningColumn.Name + " = '" + row.Cells[i].Value.ToString();
                    if (i + 1 != row.Cells.Count) query += "', ";
                    else query += "' WHERE name = '" + row.Cells["name"].Value + "'";
                }

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                Read.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedCells.Count != 1) return;

                DataGridViewRow row = dataGridView1.SelectedCells[0].OwningRow;
                string query = "DELETE FROM student WHERE name = '" + row.Cells["name"].Value + "'";

                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                connection.Close();

                Read.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

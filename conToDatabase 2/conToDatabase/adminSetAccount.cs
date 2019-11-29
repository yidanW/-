using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace conToDatabase
{
    public partial class adminSetAccount : Form
    {
        public MySqlConnection conn;
        MySqlDataAdapter mySqlDataAdapter;
        DataSet dataSet = new DataSet();
        public adminSetAccount(MySqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
            update();
        }
        public void update()
        {
            try
            {
                conn.Open();
                string SqlStr = "select account.accountNum,teamName," +
                     "pwd from account,team where Accounttype='team' and account.accountnum=team.accountnum;";
                mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                dataSet.Clear();
                mySqlDataAdapter.Fill(dataSet);

                //将数据绑定并显示
                this.dataGridView1.DataSource = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void addClick(object sender, EventArgs e)
        {
            try
            {
                AddEditAccount addEditAccount = new AddEditAccount(conn);
                addEditAccount.ShowDialog();
                update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteClick(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                var rows = dataGridView1.SelectedRows;
                string accountNum = rows[0].Cells[0].Value.ToString();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"delete from account where accountnum ={accountNum};";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            update();
        }

        private void editClick(object sender, EventArgs e)
        {
            try
            {
                var rows = dataGridView1.SelectedRows;
                string accountNum = rows[0].Cells[0].Value.ToString();
                AddEditAccount addEditAccount = new AddEditAccount(conn, accountNum);
                addEditAccount.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                update();
            }
        }
    }
}

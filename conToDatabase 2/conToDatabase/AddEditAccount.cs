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
    public partial class AddEditAccount : Form
    {
        private string AccountNum;
        MySqlConnection conn;
        public AddEditAccount(MySqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }
        public AddEditAccount(MySqlConnection conn,string AccountNum)
        {
            this.AccountNum = AccountNum;
            InitializeComponent();
            this.conn = conn;
            try
            {
                conn.Open();
                string SqlStr = "select account.accountNum,teamName," +
                      $"pwd from account,team where Accounttype='team'and account.AccountNum={AccountNum}" +
                      " and account.accountnum=team.accountnum;";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                DataSet dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet);
                textBox1.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[0]].ToString();
                textBox2.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[1]].ToString();
                textBox3.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[2]].ToString();
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

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                if (AccountNum != null)
                {
                    command.CommandText = $"delete from account where accountNum = {AccountNum};";
                    command.ExecuteNonQuery();
                }
                command.CommandText = $"insert into account values('{textBox1.Text}','{textBox2.Text}','team');";
                command.ExecuteNonQuery();
                command.CommandText = $"insert into team(accountNum,teamName) values('{textBox1.Text}','{textBox3.Text}');";
                command.ExecuteNonQuery();
                this.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

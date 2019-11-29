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
    public partial class TeamInfo : Form
    {
        private MySqlConnection conn;
        MySqlDataAdapter mySqlDataAdapter;
        string teamAccountNum;
        DataSet dataSetAth = new DataSet();
        DataSet dataSetOth = new DataSet();

        //构造函数
        public TeamInfo(MySqlConnection conn,string teamAccountNum)
        {
            InitializeComponent();
            this.conn = conn;
            this.teamAccountNum = teamAccountNum;
            update();
        }
        public void update()
        {
            try
            {
                conn.Open();
                string SqlStr = "select Person.Idcard,Name,age,eduScore,gender " +
                    $"from Person,Athlete where Person.Idcard=Athlete.idcard and" +
                    $" teamAccount ={teamAccountNum};";
                mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                dataSetAth.Clear();
                mySqlDataAdapter.Fill(dataSetAth);

                //将数据绑定dgv1 并显示
                this.dataGridView1.DataSource = dataSetAth.Tables[0];

                //其他人员的显示
                dataSetOth.Clear();
                //leader
                SqlStr = "select person.idcard,name,tel,type from person,leader where " +
                    $"teamaccount='{teamAccountNum}' and person.idcard=leader.idcard;";
                mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                mySqlDataAdapter.Fill(dataSetOth);

                //doctor
                SqlStr = "select person.idcard,name,tel,type from person,doctor where " +
                    $"teamaccount='{teamAccountNum}' and person.idcard=doctor.idcard;";
                mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                mySqlDataAdapter.Fill(dataSetOth);
                //coach
                SqlStr = "select person.idcard,name,tel,type from person,coach where " +
                    $"teamaccount='{teamAccountNum}' and person.idcard=coach.idcard;";
                mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                mySqlDataAdapter.Fill(dataSetOth);
                //referee
                SqlStr = "select person.idcard,name,tel,type from person,referee where " +
                    $"teamaccount='{teamAccountNum}' and person.idcard=referee.idcard;";
                mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                mySqlDataAdapter.Fill(dataSetOth);

                this.dataGridView2.DataSource = dataSetOth.Tables[0];
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

        private void addAthClick(object sender, EventArgs e)
        {
            try
            {
                TeamAddAth teamAddAth = new TeamAddAth(conn,teamAccountNum);
                teamAddAth.ShowDialog();
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
                string IdCard = rows[0].Cells[0].Value.ToString();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"delete from person where idcard='{IdCard}';";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                update();
            }
        }

        private void editClick(object sender, EventArgs e)
        {
            try
            {
                var rows = dataGridView1.SelectedRows;
                int IdCard = Int32.Parse(rows[0].Cells[0].Value.ToString());
                TeamAddAth teamAddAth = new TeamAddAth(conn, teamAccountNum, IdCard);
                teamAddAth.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                update();
            }
        }
        private void addOthClick(object sender, EventArgs e)
        {
            try
            {
                TeamAddOth teamAddOth = new TeamAddOth(conn,teamAccountNum);
                teamAddOth.ShowDialog();
                update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteOthClick(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                var rows = dataGridView2.SelectedRows;
                string IdCard = rows[0].Cells[0].Value.ToString();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"delete from person where idcard='{IdCard}';";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                update();
            }
        }

        private void editOthClick(object sender, EventArgs e)
        {
            try
            {
                var rows = dataGridView2.SelectedRows;
                int IdCard = Int32.Parse(rows[0].Cells[0].Value.ToString());
                TeamAddOth teamAddOth = new TeamAddOth(conn,teamAccountNum,IdCard);
                teamAddOth.ShowDialog();
            }
            catch (Exception ex)
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

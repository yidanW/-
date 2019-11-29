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
    public partial class TeamAddOth : Form
    {
        private string teamAccountNum;
        MySqlConnection conn;
        Dictionary<string, string> typePairs = new Dictionary<string, string>();
        private int IdCard = 0;
        public TeamAddOth(MySqlConnection conn, string teamAccountNum)
        {
            InitializeComponent();
            //初始化
            this.conn = conn;
            this.teamAccountNum = teamAccountNum;
            initailDic();
            comboBox1.SelectedIndex = 0;
        }
        public TeamAddOth(MySqlConnection conn, string teamAccountNum,int IdCard)
        {
            InitializeComponent();
            //初始化
            this.conn = conn;
            this.teamAccountNum = teamAccountNum;
            this.IdCard = IdCard;
            initailDic();
            comboBox1.SelectedIndex = 0;

            try
            {
                conn.Open();
                string SqlStr = $"select name,idcard,type from person where idcard='{IdCard}';";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                DataSet dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet);
                textBox1.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[0]].ToString();
                textBox2.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[1]].ToString();
                //获取类型
                string type = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[2]].ToString();
                //类型分类
                if(type == "leader")
                {
                    SqlStr = $"select tel from leader where idcard='{IdCard}';";
                    mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                    dataSet = new DataSet();
                    mySqlDataAdapter.Fill(dataSet);
                    textBox3.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[0]].ToString();
                    comboBox1.SelectedIndex = 0;
                }
                if (type == "doctor")
                {
                    SqlStr = $"select tel from doctor where idcard='{IdCard}';";
                    mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                    dataSet = new DataSet();
                    mySqlDataAdapter.Fill(dataSet);
                    textBox3.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[0]].ToString();
                    comboBox1.SelectedIndex = 1;
                }
                if (type == "coach")
                {
                    SqlStr = $"select tel,gender from coach where idcard='{IdCard}';";
                    mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                    dataSet = new DataSet();
                    mySqlDataAdapter.Fill(dataSet);
                    textBox3.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[0]].ToString();
                    string gender = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[1]].ToString();
                    comboBox1.SelectedIndex = 2;
                    label5.Visible = true;
                    checkBox1.Visible = true;
                    checkBox2.Visible = true;
                    if (gender == "male")
                        checkBox1.Checked = true;
                    else checkBox2.Checked = true;

                }
                if (type == "referee")
                {
                    SqlStr = $"select tel from referee where idcard='{IdCard}';";
                    mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                    dataSet = new DataSet();
                    mySqlDataAdapter.Fill(dataSet);
                    textBox3.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[0]].ToString();
                    comboBox1.SelectedIndex = 3;
                }
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
        private void initailDic()
        {
            typePairs.Add("领队", "leader");
            typePairs.Add("队医", "doctor");
            typePairs.Add("裁判员", "referee");
            typePairs.Add("教练员", "coach");
        }
        private void click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                //如果是编辑
                if(IdCard != 0)
                {
                    MySqlCommand command1 = conn.CreateCommand();
                    command1.CommandText = $"delete from person where idcard = '{IdCard}';";
                    command1.ExecuteNonQuery();
                }
                MySqlCommand command = conn.CreateCommand();
                string type = typePairs[comboBox1.SelectedItem.ToString()];
                command.CommandText = $"insert into Person values('{textBox2.Text}'" +
                    $",'{textBox1.Text}','{type}','{teamAccountNum}');";
                command.ExecuteNonQuery();
                if (type == "leader")
                {
                    command.CommandText = $"insert into leader(idcard,tel) values('{textBox2.Text}'" +
                        $",'{textBox3.Text}');";
                    command.ExecuteNonQuery();
                }
                else if(type == "referee")
                {
                    command.CommandText = $"insert into referee(idcard,tel) values('{textBox2.Text}'" +
                        $",'{textBox3.Text}');";
                    command.ExecuteNonQuery();
                }
                else if(type == "doctor")
                {
                    command.CommandText = $"insert into doctor values('{textBox2.Text}'" +
                        $",'{textBox3.Text}');";
                    command.ExecuteNonQuery();
                }
                else if (type == "coach")
                {
                    if(checkBox1.Checked == true)
                    {
                        command.CommandText = $"insert into coach(idcard,tel,gender) values('{textBox2.Text}'" +
                            $",'{textBox3.Text}','male');";
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandText = $"insert into coach(idcard,tel,gender) values('{textBox2.Text}'" +
                            $",'{textBox3.Text}','female');";
                        command.ExecuteNonQuery();
                    }
                }
                this.Dispose();
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

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString() != "教练员")
            {
                label5.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
            }
            else
            {
                label5.Visible = true;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                checkBox2.Checked = false;
            else checkBox2.Checked = true;
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
                checkBox1.Checked = false;
            else checkBox1.Checked = true;
        }
    }
}

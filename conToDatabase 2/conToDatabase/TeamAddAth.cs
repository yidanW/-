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
    public partial class TeamAddAth : Form
    {
        private string teamAccountNum;
        MySqlConnection conn;
        private int IdCard = 0;
        Dictionary<int, CheckBox> matchId_checkbox_dic = new Dictionary<int, CheckBox>();
        public TeamAddAth(MySqlConnection conn,string teamAccountNum)
        {
            InitializeComponent();
            //初始化
            checkBoxMale.Checked = true;
            this.conn = conn;
            this.teamAccountNum = teamAccountNum;
        }
        private void initialDic()
        {
            matchId_checkbox_dic.Add(101, checkBox3);
            matchId_checkbox_dic.Add(102, checkBox4);
            matchId_checkbox_dic.Add(103, checkBox6);
            matchId_checkbox_dic.Add(104, checkBox5);
            matchId_checkbox_dic.Add(105, checkBox8);
            matchId_checkbox_dic.Add(106, checkBox7);
            matchId_checkbox_dic.Add(107, checkBox10);
            matchId_checkbox_dic.Add(201, checkBox16);
            matchId_checkbox_dic.Add(202, checkBox15);
            matchId_checkbox_dic.Add(203, checkBox14);
            matchId_checkbox_dic.Add(204, checkBox13);
            matchId_checkbox_dic.Add(205, checkBox12);
        }
        public TeamAddAth(MySqlConnection conn,string AccountNum , int IdCard)
        {
            InitializeComponent();
            //初始化
            checkBoxMale.Checked = true;
            this.teamAccountNum = AccountNum;
            this.conn = conn;
            this.IdCard = IdCard;
            try
            {
                conn.Open();
                string SqlStr = $"select name,idcard from person where idcard='{IdCard}';";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                DataSet dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet);
                textBox1.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[0]].ToString();
                textBox2.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[1]].ToString();

                SqlStr = $"select gender,age,eduscore from athlete where idcard='{IdCard}';";
                mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                dataSet.Clear();
                mySqlDataAdapter.Fill(dataSet);
                string gender = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[2]].ToString();
                textBox3.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[3]].ToString();
                textBox4.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[4]].ToString();
                if (gender == "male") IsMale();
                else if (gender == "female") IsFemale();

                SqlStr = $"select matchId from ath_mat where idcard='{IdCard}';";
                mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet);
                initialDic();
                int matchId;
                foreach(DataRow row in dataSet.Tables[0].Rows){
                    matchId = Int32.Parse(row[0].ToString());
                    matchId_checkbox_dic[matchId].Checked = true;
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
        private void IsMale()
        {
            checkBoxMale.Checked = true;
            checkBoxFemale.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox10.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            checkBox3.Visible = true;
            checkBox4.Visible = true;
            checkBox5.Visible = true;
            checkBox6.Visible = true;
            checkBox7.Visible = true;
            checkBox8.Visible = true;
            checkBox10.Visible = true;
            checkBox12.Visible = false;
            checkBox13.Visible = false;
            checkBox14.Visible = false;
            checkBox15.Visible = false;
            checkBox16.Visible = false;
        }
        private void IsFemale()
        {

            checkBoxMale.Checked = false;
            checkBoxFemale.Checked = true;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox10.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            checkBox5.Visible = false;
            checkBox6.Visible = false;
            checkBox7.Visible = false;
            checkBox8.Visible = false;
            checkBox10.Visible = false;
            checkBox12.Visible = true;
            checkBox13.Visible = true;
            checkBox14.Visible = true;
            checkBox15.Visible = true;
            checkBox16.Visible = true;
        }
        private void CheckBoxMale_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMale.Checked == true)
                IsMale();
            else
                IsFemale();
        }
        private void CheckBoxFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFemale.Checked == true)
                IsFemale();
            else
                IsMale();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                //如果是编辑
                if (IdCard != 0 )
                {
                    MySqlCommand command1 = conn.CreateCommand();
                    command1.CommandText = $"delete from person where idcard='{IdCard}';";
                    command1.ExecuteNonQuery();
                }
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"insert into Person values('{textBox2.Text}','{textBox1.Text}','athlete','{teamAccountNum}');";
                command.ExecuteNonQuery();
                string gender = checkBoxMale.Checked == true ? "male" : "female";
                command.CommandText = $"insert into athlete (Idcard,age,eduscore,gender)" +
                    $" values('{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{gender}');";
                command.ExecuteNonQuery();
                if(checkBox16.Checked == true)//跳马
                {
                    command.CommandText = $"insert into ath_mat (Idcard,matchid) " +
                        $"values ('{textBox2.Text}',{201});";
                    command.ExecuteNonQuery();
                }
                if (checkBox15.Checked == true)//高低杠
                {
                    command.CommandText = $"insert into ath_mat (Idcard,matchid) " +
                        $"values ('{textBox2.Text}',{202});";
                    command.ExecuteNonQuery();
                }
                if (checkBox14.Checked == true)//平衡木
                {
                    command.CommandText = $"insert into ath_mat (Idcard,matchid) " +
                        $"values ('{textBox2.Text}',{203});";
                    command.ExecuteNonQuery();
                }
                if (checkBox13.Checked == true)//自由体操
                {
                    command.CommandText = $"insert into ath_mat (Idcard,matchid) " +
                        $"values ('{textBox2.Text}',{204});";
                    command.ExecuteNonQuery();
                }
                if (checkBox12.Checked == true)//蹦床
                {
                    command.CommandText = $"insert into ath_mat (Idcard,matchid) " +
                        $"values ('{textBox2.Text}',{205});";
                    command.ExecuteNonQuery();
                }
                if (checkBox3.Checked == true)//单杠
                {
                    command.CommandText = $"insert into ath_mat (Idcard,matchid) " +
                        $"values ('{textBox2.Text}',{101});";
                    command.ExecuteNonQuery();
                }
                if (checkBox4.Checked == true)//双杠
                {
                    command.CommandText = $"insert into ath_mat (Idcard,matchid) " +
                        $"values ('{textBox2.Text}',{102});";
                    command.ExecuteNonQuery();
                }
                if (checkBox6.Checked == true)//吊环
                {
                    command.CommandText = $"insert into ath_mat (Idcard,matchid) " +
                        $"values ('{textBox2.Text}',{103});";
                    command.ExecuteNonQuery();
                }
                if (checkBox5.Checked == true)//跳马
                {
                    command.CommandText = $"insert into ath_mat (Idcard,matchid) " +
                        $"values ('{textBox2.Text}',{104});";
                    command.ExecuteNonQuery();
                }
                if (checkBox8.Checked == true)//自由体操
                {
                    command.CommandText = $"insert into ath_mat (Idcard,matchid) " +
                        $"values ('{textBox2.Text}',{105});";
                    command.ExecuteNonQuery();
                }
                if (checkBox7.Checked == true)//鞍马
                {
                    command.CommandText = $"insert into ath_mat (Idcard,matchid) " +
                        $"values ('{textBox2.Text}',{106});";
                    command.ExecuteNonQuery();
                }
                if (checkBox10.Checked == true)//蹦床
                {
                    command.CommandText = $"insert into ath_mat (Idcard,matchid) " +
                        $"values ('{textBox2.Text}',{107});";
                    command.ExecuteNonQuery();
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
    }
}

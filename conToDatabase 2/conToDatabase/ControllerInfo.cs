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
    public partial class ControllerInfo : Form
    {
        private MySqlConnection conn;
        MySqlDataAdapter mySqlDataAdapter;
        int age;
        string gender;
        int matchId;
        DataSet dataSet;
        public ControllerInfo(MySqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            showAll();
        }
        private void showAll()
        {
            try
            {
                conn.Open();
                string SqlStr = $"select name,athnumber,matchname,athlete.gender,age" +
                    $",finalscore,person.idcard,matches.matchId " +
                    $"from athlete,matches,ath_mat,person " +
                    $"where matches.matchid = ath_mat.matchid and " +
                    $"person.idcard = athlete.idcard and " +
                    $"athlete.idcard = ath_mat.idcard; ";
                mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                DataSet dataSet = new DataSet();
                dataSet.Clear();
                mySqlDataAdapter.Fill(dataSet);
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
        //自动生成运动员号码
        private void formAthNumClick(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                //将运动员信息查询结果放入dataset中
                string SqlStr = $"select idcard,gender,athnumber from athlete;";
                mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                DataSet dataSet = new DataSet();
                dataSet.Clear();
                mySqlDataAdapter.Fill(dataSet);

                //初始化字典
                Dictionary<int, bool> athNumPais = new Dictionary<int, bool>();
                for (int i = 0; i < 1000; i++)
                {
                    athNumPais.Add(i, false);
                }
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    string athNumber = row[2].ToString();
                    if(athNumber != "")
                    {
                        //表示已经分配给这个运动员运动号码了
                        athNumPais[Int32.Parse(athNumber) ] = true;
                    }
                }
                //遍历每一个IdCard
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    int IdCard = Int32.Parse(row[0].ToString());
                    if (row[2].ToString() == "")
                    {
                        string gender = row[1].ToString();
                        //男运动员单号，女运动员双号
                        int p = 1;
                        if (gender == "female")
                            p = 0;
                        for (int i = 0; i < 500; i++)
                        {
                            //还未分配
                            if (athNumPais[i * 2 + p] == false)
                            {
                                athNumPais[i * 2 + p] = true;
                                //更新DBS
                                MySqlCommand command = conn.CreateCommand();
                                command.CommandText = $"update athlete set athnumber ='{i * 2 + p}' where idcard='{IdCard}';";
                                command.ExecuteNonQuery();
                                break;
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                showAll();
            }
        }

        private void gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            //男
            if(comboBox2.SelectedIndex == 0)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("单杠");
                comboBox3.Items.Add("双杠");
                comboBox3.Items.Add("吊环");
                comboBox3.Items.Add("跳马");
                comboBox3.Items.Add("自由体操");
                comboBox3.Items.Add("鞍马");
                comboBox3.Items.Add("蹦床");
                gender = "male";
            }//女
            else
            {
                comboBox3.Items.Clear();
                comboBox3.Items.Add("跳马");
                comboBox3.Items.Add("高低杠");
                comboBox3.Items.Add("平衡木");
                comboBox3.Items.Add("自由体操");
                comboBox3.Items.Add("蹦床");
                gender = "female";
            }
        }

        private void age_SelectedIndexChanged(object sender, EventArgs e)
        {
            age = comboBox1.SelectedIndex;
        }

        private void match_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(gender == "male")
            {
                switch (comboBox3.SelectedIndex)
                {
                    case 0:
                        matchId = 101;break;
                    case 1:
                        matchId = 102;break;
                    case 2:
                        matchId = 103; break;
                    case 3:
                        matchId = 104;break;
                    case 4:
                        matchId = 105;break;
                    case 5:
                        matchId = 106; break;
                    case 6:
                        matchId = 107; break;
                }
            }else if(gender == "female")
            {
                switch (comboBox3.SelectedIndex)
                {
                    case 0:
                        matchId = 201; break;
                    case 1:
                        matchId = 202; break;
                    case 2:
                        matchId = 203; break;
                    case 3:
                        matchId = 204; break;
                    case 4:
                        matchId = 205; break;
                }
            }
        }

        private void queryClick(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string SqlStr=null;
                //7-8
                if (age == 0)
                {
                    SqlStr = $"select name,athnumber,matchname,athlete.gender" +
                        $",age,finalscore,person.idcard,matches.matchId " +
                    $"from athlete,matches,ath_mat,person " +
                    $"where matches.matchid = ath_mat.matchid and " +
                    $"person.idcard = athlete.idcard and " +
                    $"athlete.idcard = ath_mat.idcard and" +
                    $"(age = 7 or age = 8) and matches.matchid={matchId}; ";
                }
                //9-10
                else if (age == 1)
                {
                    SqlStr = $"select name,athnumber,matchname,athlete.gender,age,finalscore,person.idcard,matches.matchId " +
                    $"from athlete,matches,ath_mat,person " +
                    $"where matches.matchid = ath_mat.matchid and " +
                    $"person.idcard = athlete.idcard and " +
                    $"athlete.idcard = ath_mat.idcard and" +
                    $"(age = 9 or age = 10) and matches.matchid={matchId}; ";
                }
                //11-12
                else if(age == 2)
                {
                    SqlStr = $"select name,athnumber,matchname,athlete.gender" +
                        $",age,finalscore,person.idcard,matches.matchId " +
                    $"from athlete,matches,ath_mat,person " +
                    $"where matches.matchid = ath_mat.matchid and " +
                    $"person.idcard = athlete.idcard and " +
                    $"athlete.idcard = ath_mat.idcard and" +
                    $"(age = 11 or age = 12) and matches.matchid={matchId}; ";
                }
                
                mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                dataSet = new DataSet();
                dataSet.Clear();
                mySqlDataAdapter.Fill(dataSet);
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

        private void Button2_Click(object sender, EventArgs e)
        {
            RefereeLeader refereeLeader = new RefereeLeader(conn,age,matchId);
            refereeLeader.ShowDialog();
        }
    }
}

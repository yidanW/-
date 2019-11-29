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
    public partial class RefereeLeader : Form
    {
        private MySqlConnection conn;
        MySqlDataAdapter mySqlDataAdapter;
        DataSet dataSet;
        int age;
        int idCard;
        int matchId;
        public RefereeLeader(MySqlConnection conn,int age,int matchId)
        {
            InitializeComponent();
            this.conn = conn;
            this.age = age;
            this.matchId = matchId;
            show();
        }
        private void show()
        {
            try
            {
                conn.Open();
                string SqlStr = null;
                if (age == 0)
                {
                    SqlStr = $"select name,matchName,score1,score2,score3,score4,score5,D,p,finalScore,person.idcard " +
                    $"from athlete,ath_mat,matches,person " +
                    $"where athlete.idcard = ath_mat.idcard and " +
                    $"person.idcard=athlete.idcard and " +
                    $"ath_mat.matchid = matches.matchid " +
                    $"and matches.matchid='{matchId}' " +
                    $"and (age=7 or age =8);";
                }else if(age == 1)
                {
                    SqlStr = $"select name,matchName,score1,score2,score3,score4,score5,D,p,finalScore,person.idcard " +
                    $"from athlete,ath_mat,matches,person " +
                    $"where athlete.idcard = ath_mat.idcard and " +
                    $"person.idcard=athlete.idcard and " +
                    $"ath_mat.matchid = matches.matchid " +
                    $"and matches.matchid='{matchId}' " +
                    $"and (age=9 or age =10);";
                }
                else if (age == 2)
                {
                    SqlStr = $"select name,matchName,score1,score2,score3,score4,score5,D,p,finalScore,person.idcard " +
                    $"from athlete,ath_mat,matches,person " +
                    $"where athlete.idcard = ath_mat.idcard and " +
                    $"person.idcard=athlete.idcard and " +
                    $"ath_mat.matchid = matches.matchid " +
                    $"and matches.matchid='{matchId}' " +
                    $"and (age=11 or age =12);";
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

        private void score_Click(object sender, EventArgs e)
        {
            try
            {
                var rows = dataGridView1.SelectedRows;
                idCard = Int32.Parse(rows[0].Cells[10].Value.ToString());
                for (int i = 0; i < 5; i++)
                {
                    Score score = new Score();
                    RefereeScore refereeScore = new RefereeScore(conn, idCard, matchId, score,i+1);
                    refereeScore.ShowDialog();
                    try
                    {
                        conn.Open();
                        MySqlCommand command = conn.CreateCommand();
                        command.CommandText = $"update ath_mat set score{i + 1}={score.num} " +
                            $"where idcard={idCard} and matchId={matchId};";
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                        show();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DP_click(object sender, EventArgs e)
        {
            try
            {
                var rows = dataGridView1.SelectedRows;
                idCard = Int32.Parse(rows[0].Cells[10].Value.ToString());
                Score score = new Score();

                conn.Open();
                RefereeDP refereeDP = new RefereeDP(score);
                refereeDP.ShowDialog();

                MySqlCommand command = conn.CreateCommand();
                command.CommandText = $"update ath_mat set D={score.D} " +
                    $"where idcard={idCard} and matchId={matchId};";
                command.ExecuteNonQuery();
                command.CommandText = $"update ath_mat set P={score.P} " +
                    $"where idcard={idCard} and matchId={matchId};";
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
                show();
            }
        }

        private void confirmScore_Click(object sender, EventArgs e)
        {
            //name,matchName,score1,score2,score3,score4,score5,D,p,finalScore,person.idcard
            try
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    string idCard = row[10].ToString();
                    string matchName = row[1].ToString();
                    int[] score = new int[5];
                    int D = Int32.Parse(row[7].ToString());
                    int P = Int32.Parse(row[8].ToString());
                    int max = score[0] = Int32.Parse(row[2].ToString());
                    int min = score[0] = Int32.Parse(row[2].ToString());
                    int sum = 0;
                    for (int i = 1; i < 5; i++)
                    {
                        score[i] = Int32.Parse(row[i + 2].ToString());
                        if (score[i] > max)
                            max = score[i];
                        if (score[i] < min)
                            min = score[i];
                        sum += score[i];
                    }
                    sum -= min;
                    sum -= max;
                    int finalScore = sum + D - P;
                    try
                    {
                        conn.Open();
                        MySqlCommand command = conn.CreateCommand();
                        command.CommandText = $"update ath_mat set finalScore={finalScore} " +
                            $"where idcard={idCard} and matchId={matchId};";
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                        show();
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
    public class Score
    {
        public int num;
        public int P;
        public int D;
    }
}

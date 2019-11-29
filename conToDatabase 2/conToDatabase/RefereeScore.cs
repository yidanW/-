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
    public partial class RefereeScore : Form
    {
        int idCard;
        int matchId;
        private MySqlConnection conn;
        MySqlDataAdapter mySqlDataAdapter;
        DataSet dataSet;
        public Score score;
        public RefereeScore(MySqlConnection conn, int idCard, int matchId,Score score,int order)
        {
            InitializeComponent();
            this.idCard = idCard;
            this.matchId = matchId;
            this.conn = conn;
            this.score = score;
            label4.Text = $"第{order}位裁判打分";
            try
            {
                conn.Open();
                string SqlStr = $"select name,athlete.gender,age,matchname " +
                    $"from person,athlete,matches,ath_mat " +
                    $"where person.idcard=athlete.idcard " +
                    $"and person.idcard={idCard} " +
                    $"and matches.matchid=ath_mat.matchid " +
                    $"and matches.matchid={matchId};";
                mySqlDataAdapter = new MySqlDataAdapter(SqlStr, conn);
                dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet);
                lableName.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[0]].ToString();
                labelGender.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[1]].ToString();
                labelAge.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[2]].ToString();
                labelMatch.Text = dataSet.Tables[0].Rows[0][dataSet.Tables[0].Columns[3]].ToString();
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

        private void Button5_Click(object sender, EventArgs e)
        {
            score.num = Int32.Parse(textBox1.Text);
            Confirm confirm = new Confirm(this);
            confirm.ShowDialog();
        }
    }
}

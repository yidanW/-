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
    public partial class TeamIni : Form
    {
        private MySqlConnection conn;
        string teamAccountNum;
        public TeamIni(MySqlConnection conn,string teamAccountNum)
        {
            InitializeComponent();
            this.conn = conn;
            this.teamAccountNum = teamAccountNum;
        }

        private void editClick(object sender, EventArgs e)
        {
            TeamInfo teamInfo = new TeamInfo(conn,teamAccountNum);
            teamInfo.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}

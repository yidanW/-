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
    public partial class AdminIni : Form
    {

        Parameter parameter = new Parameter();
        public MySqlConnection conn;
        public AdminIni(MySqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            adminSetAccount setAccount = new adminSetAccount(this.conn);
            setAccount.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            adminSetPara setPara = new adminSetPara(parameter);
            setPara.ShowDialog();
        }
    }
    public class Parameter
    {
        public int maxSignAth;
        public int maxSCAth;
        public int teamScoreAth;
    }
}

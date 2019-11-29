using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace conToDatabase
{
    public partial class Confirm : Form
    {
        RefereeScore refereeScore;
        public Confirm(RefereeScore refereeScore)
        {
            InitializeComponent();
            this.refereeScore = refereeScore;
            label1.Text = "裁判打分为" +refereeScore.score.num.ToString();
        }
        //通过
        private void Button4_Click(object sender, EventArgs e)
        {
            refereeScore.Dispose();
            this.Dispose();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}

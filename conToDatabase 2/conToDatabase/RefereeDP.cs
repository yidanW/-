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
    public partial class RefereeDP : Form
    {
        Score score;
        public RefereeDP(Score score)
        {
            InitializeComponent();
            this.score = score;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            score.D = Int32.Parse(textBox1.Text);
            score.P = Int32.Parse(textBox2.Text);
            this.Dispose();
        }
    }
}

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
    public partial class adminSetPara : Form
    {
        Parameter parameter;
        public adminSetPara(Parameter parameter)
        {
            InitializeComponent();
            this.parameter = parameter;
            textBox1.Text = parameter.maxSignAth.ToString();
            textBox2.Text = parameter.maxSCAth.ToString();
            textBox3.Text = parameter.teamScoreAth.ToString();
        }


        private void Button1_Click_1(object sender, EventArgs e)
        {
            parameter.maxSignAth = Int32.Parse(textBox1.Text);
            parameter.maxSCAth = Int32.Parse(textBox2.Text);
            parameter.teamScoreAth = Int32.Parse(textBox3.Text);
            this.Dispose();
        }
    }
}

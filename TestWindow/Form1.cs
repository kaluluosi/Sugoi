using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWindow {
    public partial class Form1 : Form {
        private List<CheckBox> cbxes = new List<CheckBox>();

        public bool AllChecked() {
            foreach (CheckBox cb in cbxes) {
                if (cb.Checked == false) return false;
            }
            return true;
        }

        public Form1() {
            InitializeComponent();
            cbxes.Add(this.checkBox1);
            cbxes.Add(this.checkBox2);
            cbxes.Add(this.checkBox3);
            cbxes.Add(this.checkBox4);
            cbxes.Add(this.checkBox5);
            cbxes.Add(this.checkBox6);

            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
        }

        void timer1_Tick(object sender, EventArgs e) {
            pictureBox1.Visible = !pictureBox1.Visible;
            ((Timer)sender).Stop();
        }

        private void button1_Click(object sender, EventArgs e) {
            pictureBox1.Visible = true;
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e) {
            pictureBox1.Visible = false;
            timer1.Start();
        }

        private void hide(object sender, EventArgs e) {
            pictureBox1.Visible = false;
        }

        private void mouseClick(object sender, MouseEventArgs e) {
            if(e.Button == System.Windows.Forms.MouseButtons.Right) {
                pictureBox1.Image = Properties.Resources.charactor;
            }
        }
    }
}

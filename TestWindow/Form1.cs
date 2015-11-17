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
        }
    }
}

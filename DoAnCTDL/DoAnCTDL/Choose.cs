﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DoAnCTDL
{
    public partial class Choose : Form
    {
        public Choose()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (LyThuyet a = new LyThuyet())
            {
                Hide();
                a.ShowDialog();
                a.Dispose();
                Show();
            }
            GC.Collect();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (UngDung a = new UngDung())
            {
                Hide();
                a.ShowDialog();
                a.Dispose();
                Show();
            }
            GC.Collect();
        }
    }
}

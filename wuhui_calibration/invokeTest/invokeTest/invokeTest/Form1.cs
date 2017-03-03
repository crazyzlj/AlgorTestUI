using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestDemo
{
    public partial class Form1 : Form
    {
        public TestDemo.FormWuhui_calibration child3;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (child3 == null || child3.IsDisposed)
            {
                child3 = new TestDemo.FormWuhui_calibration();
                child3.ParamCaliOutputProperty = "F:\\863Desktop\\TestDemo\\wuhui_calibration\\testData";
                child3.Show();
            }
            else
                child3.Activate();
        }
    }
}

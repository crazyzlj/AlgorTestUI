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
    public partial class SelectCoresFrm : Form
    {
        private string[] cores;
        public string[] Cores
        {
            get { return cores; }
            set { cores = value; }
        }
        public SelectCoresFrm()
        {
            InitializeComponent();
        }
        // 返回选择框中被选中的记录
        public string[] SelectedStrs(CheckedListBox CLB)
        {
            int SelectedNum = 0;
            for (int i = 0; i < CLB.Items.Count; i++)
                if (CLB.GetItemChecked(i))
                    SelectedNum++;
            //MessageBox.Show(SelectedNum.ToString());
            if (SelectedNum == 0)
            {
                return null;
            }
            else
            {
                string[] Cores = new string[SelectedNum];
                int j = 0;
                for (int i = 0; i < CLB.Items.Count; i++)
                {
                    if (CLB.GetItemChecked(i))
                    {
                        Cores[j] = CLB.GetItemText(CLB.Items[i]);
                        //MessageBox.Show(Cores[j]);
                        j++;
                        CLB.SetItemCheckState(i, CheckState.Unchecked);
                    }
                }
                return Cores;
            }
        }

        private void buttonSelectCores_Click(object sender, EventArgs e)
        {
            cores = SelectedStrs(checkedListBoxCores);
            this.DialogResult = DialogResult.OK;
            //this.Close();
            //string[] CoresSelected = SelectedStrs(checkedListBoxCores);
        }
        public void SelectAllorNone(CheckedListBox CL)
        {
            int SelectedNum = 0;
            for (int i = 0; i < CL.Items.Count; i++)
                if (CL.GetItemChecked(i))
                    SelectedNum++;
            if (SelectedNum == CL.Items.Count)
            {
                for (int i = 0; i < CL.Items.Count; i++)
                {
                    CL.SetItemChecked(i, false);
                }
            }
            else
            {
                for (int i = 0; i < CL.Items.Count; i++)
                {
                    CL.SetItemChecked(i, true);
                }
            }
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            SelectAllorNone(checkedListBoxCores);
        }
    }
}

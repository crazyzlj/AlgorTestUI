using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TestDemo
{
    public partial class FormWuhui_calibration : Form
    {
        public FormWuhui_calibration()
        {
            InitializeComponent();

            string[] ParamNames = { "Conductivity", "Depression", "FieldCape","Porosity", "Omega", "ccoe", "ChTcCo", "ChDetCo" };
            double[] InitialValue = { -99.0, -99.0, -99.0, -99.0, 0.01, 0.01, 0.01, 0.005};
            double[] MinValue = { 0.01, 0.1, 0.1, 0.1, 0.05, 0.05, 0.05, 0.05};
            double[] MaxValue = { 0.5, 1.0, 1.0, 1.0, 20.0, 20.0, 20.0, 10.0};
            int ParamNum = ParamNames.Length;
            for (int i = 0; i < ParamNum;i++ )
            {
                int RowIdx = this.dataGridViewParamsCali.Rows.Add();
                dataGridViewParamsCali.Rows[RowIdx].Cells[0].Value = ParamNames[i];
                dataGridViewParamsCali.Rows[RowIdx].Cells[1].Value = InitialValue[i];
                dataGridViewParamsCali.Rows[RowIdx].Cells[2].Value = MinValue[i];
                dataGridViewParamsCali.Rows[RowIdx].Cells[3].Value = MaxValue[i];
            }
            this.dataGridViewParamsCali.Columns[0].Frozen = true;
            //int ColIdx = this.dataGridViewParamsCali.Columns.Add(;
            string ParamCaliOutput = "F:\\863Results\\output32\\final_nondom_pop.out";
            StreamReader sr = new StreamReader(ParamCaliOutput,Encoding.Default);
            string line;
            int count = 0;
            string[] Lines = new string[100];
            while ((line = sr.ReadLine()) != null)
            {
                //MessageBox.Show(line.ToString());
                Lines[count] = line.ToString();
                count++;
            }
            string[] CaliResults = new string[count - 2];
            for (int i = 0; i < count - 2;i++ )
            {
                CaliResults[i] = Lines[i + 2];
                //MessageBox.Show(CaliResults[i].ToString());
            }
            double[,] OptCali = new double[CaliResults.Length, ParamNum];
            double[,] Objective = new double[CaliResults.Length, 2];
            for (int i = 0; i < CaliResults.Length; i++)
            {
                Objective[i,0] = Convert.ToDouble(CaliResults[i].Split('\t')[3].ToString().Trim());
                Objective[i,1] = Convert.ToDouble(CaliResults[i].Split('\t')[4].ToString().Trim());
                for (int j = 0; j < ParamNum - 1;j++ )
                {
                    OptCali[i, j] = Convert.ToDouble(CaliResults[i].Split('\t')[j + 7].ToString().Trim());
                }
            }
            int RowBlank = this.dataGridViewParamsCali.Rows.Add();
            int RowIdx2 = this.dataGridViewParamsCali.Rows.Add();
            int RowIdx3 = this.dataGridViewParamsCali.Rows.Add();
            this.dataGridViewParamsCali[0, RowIdx2].Value = "Objective1";
            this.dataGridViewParamsCali[0, RowIdx3].Value = "Objective2";
            for (int i = 0; i < CaliResults.Length;i++ )
            {
                int ColIdx = this.dataGridViewParamsCali.Columns.Add("ColumnCali" + (i + 1).ToString(), "参数率定结果"+(i + 1).ToString());
                for (int j=0;j<ParamNum-1;j++)
                {
                    this.dataGridViewParamsCali[ColIdx,j].Value = OptCali[i,j];
                }
                this.dataGridViewParamsCali[ColIdx, RowIdx2].Value = Objective[i, 0];
                this.dataGridViewParamsCali[ColIdx, RowIdx3].Value = Objective[i, 1];
            }
        }
    }
}

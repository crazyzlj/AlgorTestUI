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
        public struct QData
        {
            public DateTime[] Time;
            public double[] QValue;
            public int count;
        }
        public TestDemo.Hydrograph childHydroQ;
        public TestDemo.Hydrograph childHydroS;
        private string ParamCaliOutput;
        public string ParamCaliOutputProperty
        {
            get { return ParamCaliOutput; }
            set { ParamCaliOutput = value; }
        }
        public FormWuhui_calibration()
        {
            InitializeComponent();  
        }
        public void FillDataViewTable(string ParamCaliOutput)
        {
            // Read Pardef.txt
            string PardefFile = ParamCaliOutput + "\\input\\Pardef.txt";
            StreamReader pardef = new StreamReader(PardefFile, Encoding.Default);
            string parline;
            int ParamNum = 0;
            string[] parlines = new string[100];
            while ((parline = pardef.ReadLine()) != null)
            {
                //MessageBox.Show(line.ToString());
                parlines[ParamNum] = parline.ToString();
                ParamNum++;
            }
            ParamNum = ParamNum - 1;
            //MessageBox.Show(ParamNum.ToString());
            string[] paramValues = new string[ParamNum];
            for (int i = 0; i < ParamNum;i++ )
            {
                paramValues[i] = parlines[i + 1];
                int RowIdx = this.dataGridViewParamsCali.Rows.Add();
                dataGridViewParamsCali.Rows[RowIdx].Cells[0].Value = paramValues[i].Split('\t')[0].ToString().Trim();
                dataGridViewParamsCali.Rows[RowIdx].Cells[1].Value = paramValues[i].Split('\t')[1].ToString().Trim();
                dataGridViewParamsCali.Rows[RowIdx].Cells[2].Value = paramValues[i].Split('\t')[2].ToString().Trim();
                dataGridViewParamsCali.Rows[RowIdx].Cells[3].Value = paramValues[i].Split('\t')[3].ToString().Trim();
            }
            this.dataGridViewParamsCali.Columns[0].Frozen = true;
            string ParamCaliOutputFile = ParamCaliOutput + "\\output32\\final_nondom_pop.out";
            StreamReader sr = new StreamReader(ParamCaliOutputFile, Encoding.Default);
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
                Objective[i,0] = Convert.ToDouble(CaliResults[i].Split('\t')[3].ToString().Trim()) * -1;
                Objective[i,1] = Convert.ToDouble(CaliResults[i].Split('\t')[4].ToString().Trim()) * -1;
                for (int j = 0; j < ParamNum;j++ )
                {
                    OptCali[i, j] = Convert.ToDouble(CaliResults[i].Split('\t')[j + 7].ToString().Trim());
                }
            }
            int RowBlank = this.dataGridViewParamsCali.Rows.Add();
            int RowIdx2 = this.dataGridViewParamsCali.Rows.Add();
            int RowIdx3 = this.dataGridViewParamsCali.Rows.Add();
            this.dataGridViewParamsCali[0, RowIdx2].Value = "Discharge_Nash";
            this.dataGridViewParamsCali[0, RowIdx3].Value = "Sediment_Nash";

            for (int i = 0; i < CaliResults.Length; i++)
            {
                if (Objective[i, 0] > 0 || Objective[i, 1] > 0)
                {
                    int ColIdx = this.dataGridViewParamsCali.Columns.Add("ColumnCali" + (i + 1).ToString(), "率定结果" + (i + 1).ToString());
                    this.dataGridViewParamsCali.Columns[ColIdx].ContextMenuStrip = contextMenuStripDraw;
                    this.dataGridViewParamsCali.Columns[ColIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                    for (int j = 0; j < ParamNum; j++)
                    {
                        this.dataGridViewParamsCali[ColIdx, j].Value = OptCali[i, j];
                    }
                    this.dataGridViewParamsCali[ColIdx, RowIdx2].Value = Objective[i, 0];
                    this.dataGridViewParamsCali[ColIdx, RowIdx3].Value = Objective[i, 1];
                }
            }
            for (int i = 0; i < this.dataGridViewParamsCali.ColumnCount; i++)
            {
                this.dataGridViewParamsCali[i, RowBlank].Value = "=========";
            }
        }

        private void FormWuhui_calibration_Load(object sender, EventArgs e)
        {
            FillDataViewTable(ParamCaliOutput);
        }
        private double[,] ReadPopout()
        {
            string OutputFile = ParamCaliOutput + "\\output32\\final_pop.out";
            int ParamNum = this.dataGridViewParamsCali.RowCount - 3;
            StreamReader sr = new StreamReader(OutputFile, Encoding.Default);
            string line;
            int count = 0;
            string[] Lines = new string[100];
            while ((line = sr.ReadLine()) != null)
            {
                //MessageBox.Show(line.ToString());
                Lines[count] = line.ToString();
                count++;
            }
            double[,] CaliResults = new double[count - 2, ParamNum];
            for (int i = 0; i < count - 2; i++)
            {
                for (int j = 0; j < ParamNum; j++)
                {
                    CaliResults[i, j] = Convert.ToDouble(Lines[i + 2].Split('\t')[j + 7].ToString().Trim());
                }
            }
            return CaliResults;
        }


        private void toolStripMenuItemHydroQ_Click(object sender, EventArgs e)
        {
            int ColSelected = this.dataGridViewParamsCali.SelectedCells[0].ColumnIndex;
            int RowIdx = this.dataGridViewParamsCali.RowCount - 2;
            QorSHydrograh(ColSelected,RowIdx, "Q", childHydroQ);
        }
        public double calNash(string[] files)
        {
            string SimFile = files[2];
            string ObsFile = files[1];
            string PrecFile = files[0];
            QData ObsQOrig = ReadQData(ObsFile);
            QData SimQOrig = ReadQData(SimFile);
            QData SimQ = new QData();
            QData ObsQ = new QData();
            if (SimQOrig.count > ObsQOrig.count)
            {
                int CommonNum = 0;
                double[] QS = new double[ObsQOrig.count];
                DateTime[] TimeS = new DateTime[ObsQOrig.count];
                double[] QO = new double[ObsQOrig.count];
                DateTime[] TimeO = new DateTime[ObsQOrig.count];
                for (int i = 0; i < ObsQOrig.count; i++)
                {
                    for (int j = 0; j < SimQOrig.count; j++)
                    {
                        if (SimQOrig.Time[j] == ObsQOrig.Time[i])
                        {
                            QO[CommonNum] = ObsQOrig.QValue[i];
                            TimeO[CommonNum] = ObsQOrig.Time[i];
                            QS[CommonNum] = SimQOrig.QValue[j];
                            TimeS[CommonNum] = SimQOrig.Time[j];
                            CommonNum++;
                            continue;
                        }
                    }
                }
                //MessageBox.Show(CommonNum.ToString());
                double[] QS2 = new double[CommonNum];
                DateTime[] TimeS2 = new DateTime[CommonNum];
                double[] QO2 = new double[CommonNum];
                DateTime[] TimeO2 = new DateTime[CommonNum];
                for (int k = 0; k < CommonNum; k++)
                {
                    QS2[k] = QS[k];
                    TimeS2[k] = TimeS[k];
                    QO2[k] = QO[k];
                    TimeO2[k] = TimeO[k];
                }
                SimQ.Time = TimeS2;
                SimQ.QValue = QS2;
                SimQ.count = CommonNum;
                ObsQ.Time = TimeO2;
                ObsQ.QValue = QO2;
                ObsQ.count = CommonNum;
            }
            else
                SimQ = SimQOrig;
            double nash = NashCoef(ObsQ.QValue, SimQ.QValue);
            return nash;
        }
        public QData ReadQData(string QFile)
        {
            StreamReader sr = new StreamReader(QFile, Encoding.Default);
            string line;
            int count = 0;
            while ((line = sr.ReadLine()) != null)
            {
                //MessageBox.Show(line.ToString().Split('\t')[2]); 
                count++;
            }
            double[] Q = new double[count];
            DateTime[] Time = new DateTime[count];
            StreamReader sr2 = new StreamReader(QFile, Encoding.Default);
            int idx = 0;
            string line2;
            while ((line2 = sr2.ReadLine()) != null)
            {
                Time[idx] = DateTime.Parse(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[0] + " " + System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[1]);
                Q[idx] = Convert.ToDouble(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[2].ToString().Trim());
                //Time[idx] = DateTime.Parse(line2.Split('\t')[0].ToString() + " " + line2.Split('\t')[1].ToString());
                //Time[idx] = DateTime.Parse(line2.Split('\t')[0].ToString());
                //Q[idx] = Convert.ToDouble(line2.Split('\t')[1].ToString().Trim());
                //MessageBox.Show(Time[idx].ToString()); 
                idx++;
            }
            QData qdata = new QData();
            qdata.Time = Time;
            qdata.QValue = Q;
            qdata.count = count;
            return qdata;
        }
        public double NashCoef(double[] qObs, double[] qSimu)
        {
            int num = Math.Min(qObs.Length, qSimu.Length);
            double ave = qObs.Sum() / num;
            double a1 = 0.0;
            double a2 = 0.0;
            for (int i = 0; i < num; i++)
            {
                a1 += Math.Pow(qObs[i] - qSimu[i], 2);
                a2 += Math.Pow(qObs[i] - ave, 2);
            }
            return 1 - a1 / a2;
        }
        public void QorSHydrograh(int ColSelected,int RowIdx,string QorS,TestDemo.Hydrograph childHydro)
        {
            int ParamNum = this.dataGridViewParamsCali.RowCount - 3;
            string[] ResultFilePaths = new string[4];
            ResultFilePaths[0] = ParamCaliOutput + "\\input\\prec.txt";
            if (QorS == "SED")
            {
                ResultFilePaths[1] = ParamCaliOutput + "\\input\\obsS.txt";
                ResultFilePaths[3] = "Sediment / kg/m3";
            }
            else
            {
                ResultFilePaths[1] = ParamCaliOutput + "\\input\\obsQ.txt";
                ResultFilePaths[3] = "Discharge / m3/s";
            }
            for (int pro = 0; pro < 32;pro++ )
            {
                ResultFilePaths[2] = ParamCaliOutput + "\\process" + pro.ToString() + "\\1_" + QorS + "_OUTLET.txt";
                double Nash = calNash(ResultFilePaths);
                if (Math.Abs(Nash - Convert.ToDouble(this.dataGridViewParamsCali[ColSelected, RowIdx].Value.ToString())) < 0.0000001)
                {
                    //MessageBox.Show(pro.ToString());
                    if (childHydro == null || childHydro.IsDisposed)
                    {
                        childHydro = new TestDemo.Hydrograph();
                        childHydro.Files = ResultFilePaths;
                        childHydro.Show();
                    }
                    else
                    {
                        childHydro.Dispose();
                        //childHydro = new TestDemo.Hydrograph();
                        //childHydro.Files = ResultFilePaths;
                        //childHydro.Show();
                    }
                    break;
                }
            }
#region  old

            //if (ColSelected >= 4)
            //{
            //    // Read final_pop.out
            //    double[,] AllResults = ReadPopout();
            //    for (int i = 0; i < AllResults.Length; i++)
            //    {
            //        int count = 0;
            //        for (int j = 0; j < ParamNum;j++ )
            //        {
            //            if (AllResults[i, j] == Convert.ToDouble(this.dataGridViewParamsCali[ColSelected,j].Value.ToString()))
            //            {
            //                count++;
            //            }
            //        }
            //        if (count == ParamNum)
            //        {
            //            int idx;
            //            if (ColSelected == 4)
            //            {
            //                idx = 0;
            //            }
            //            else
            //                idx = 16;
            //            //MessageBox.Show(i.ToString());
            //            ResultFilePaths[0] = ParamCaliOutput + "\\input\\prec.txt";
            //            if (QorS == "Discharge")
            //            {
            //                ResultFilePaths[1] = ParamCaliOutput + "\\input\\obsQ.txt";
            //                ResultFilePaths[2] = ParamCaliOutput + "\\process" + idx.ToString() + "\\1_Q_OUTLET.txt";
            //                break;
            //            }
            //            else if (QorS == "Sediment")
            //            {
            //                ResultFilePaths[1] = ParamCaliOutput + "\\input\\obsS.txt";
            //                ResultFilePaths[2] = ParamCaliOutput + "\\process" + idx.ToString() + "\\1_SED_OUTLET.txt";
            //                break;
            //            }
            //        }   
            //    }
            //}
#endregion
            
        }

        private void toolStripMenuItemHydroS_Click(object sender, EventArgs e)
        {
            int ColSelected = this.dataGridViewParamsCali.SelectedCells[0].ColumnIndex;
            int RowIdx = this.dataGridViewParamsCali.RowCount - 1;
            QorSHydrograh(ColSelected,RowIdx, "SED", childHydroS);
        }

        private void dataGridViewParamsCali_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dataGridViewParamsCali.ClearSelection();
                this.dataGridViewParamsCali[e.ColumnIndex, e.RowIndex].Selected = true;
            }
        }
    }
}

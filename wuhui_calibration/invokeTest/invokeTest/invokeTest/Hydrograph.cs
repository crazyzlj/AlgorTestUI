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
    public partial class Hydrograph : Form
    {
        private string[] files;
        public string[] Files
        {
            get { return files; }
            set { files = value; }
        }
        public struct QData
        {
            public DateTime[] Time;
            public double[] QValue;
            public int count;
        }
        public Hydrograph()
        {
            InitializeComponent();
        }
        public void DrawHydrograph()
        {
            string SimFile = files[2];
            string ObsFile = files[1];
            string PrecFile = files[0];
            QData ObsQOrig = ReadQData(ObsFile);
            QData SimQOrig = ReadQData(SimFile);
            QData pData = ReadPrecData(PrecFile);
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
                    for (int j=0;j < SimQOrig.count;j++)
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
                for (int k = 0; k < CommonNum;k++ )
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
            //MessageBox.Show(nash.ToString());
            axTChartHydrograph.Axis.Left.Automatic = false;
            axTChartHydrograph.Axis.Left.Maximum = ObsQ.QValue.Max() * 1.5;
            axTChartHydrograph.Axis.Left.Minimum = Math.Min(0, Math.Min(ObsQ.QValue.Min(), SimQ.QValue.Min()) - 10);
            axTChartHydrograph.Axis.Right.Automatic = false;
            axTChartHydrograph.Axis.Right.Minimum = pData.QValue.Min();
            axTChartHydrograph.Axis.Right.Maximum = pData.QValue.Max() * 4.0;
            
            axTChartHydrograph.Series(0).AddArray(ObsQ.QValue.Length, ObsQ.QValue, ObsQ.Time);
            axTChartHydrograph.Series(1).AddArray(SimQ.QValue.Length, SimQ.QValue, SimQ.Time);
            axTChartHydrograph.Series(2).AddArray(pData.Time.Length, pData.QValue, pData.Time);
            axTChartHydrograph.Axis.Left.Title.Caption = files[3];
            labelChartTitle.Text = "Nash Coefficient: " + nash.ToString("f3");
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
        public QData ReadPrecData(string PrecFile)
        {
            StreamReader sr = new StreamReader(PrecFile, Encoding.Default);
            string line;
            int count = 0;
            while ((line = sr.ReadLine()) != null)
                count++;
            DateTime[] precTime = new DateTime[count * 4];
            double[] Prec = new double[count * 4];
            int idx = 0;
            StreamReader sr2 = new StreamReader(PrecFile, Encoding.Default);
            string line2;
            while ((line2 = sr2.ReadLine()) != null)
            {
                precTime[idx] = DateTime.Parse(line2.Split('\t')[0].ToString() + " " + line2.Split('\t')[1].ToString());
                precTime[idx + 1] = DateTime.Parse(line2.Split('\t')[0].ToString() + " " + line2.Split('\t')[1].ToString());
                precTime[idx + 2] = DateTime.Parse(line2.Split('\t')[0].ToString() + " " + line2.Split('\t')[2].ToString());
                precTime[idx + 3] = DateTime.Parse(line2.Split('\t')[0].ToString() + " " + line2.Split('\t')[2].ToString());
                Prec[idx] = 0.0;
                Prec[idx + 1] = Convert.ToDouble(line2.Split('\t')[3].ToString().Trim());
                Prec[idx + 2] = Convert.ToDouble(line2.Split('\t')[3].ToString().Trim());
                Prec[idx + 3] = 0.0;
                //MessageBox.Show(Prec[idx].ToString());
                idx = idx + 4;
            }
            QData precdata = new QData();
            precdata.Time = precTime;
            precdata.QValue = Prec;
            precdata.count = count;
            return precdata;
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

        private void Hydrograph_Load(object sender, EventArgs e)
        {
            DrawHydrograph();
        }
    }
}

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
        public struct QData
        {
            public DateTime[] Time;
            public double[] QValue;
        }
        //public struct PrecData
        //{
        //    public DateTime[] StartTime;
        //    public DateTime[] EndTime;
        //    public double[] Prec;
        //}
        public Hydrograph()
        {
            InitializeComponent();
            string HydroFolder = "F:\\863Results\\HydroGraph";
            string SimQFile = HydroFolder + "\\simuS.txt";
            string ObsQFile = HydroFolder + "\\obsS.txt";
            string PrecFile = HydroFolder + "\\prec.txt";
            QData ObsQ = ReadQData(ObsQFile);
            QData SimQ = ReadQData(SimQFile);
            QData pData = ReadPrecData(PrecFile);
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
                Time[idx] = DateTime.Parse(line2.Split('\t')[0].ToString() + " " + line2.Split('\t')[1].ToString());
                Q[idx] = Convert.ToDouble(line2.Split('\t')[2].ToString().Trim());
                //MessageBox.Show(Time[idx].ToString()); 
                idx++;
            }
            QData qdata = new QData();
            qdata.Time = Time;
            qdata.QValue = Q;
            return qdata;
        }
        public QData ReadPrecData(string PrecFile)
        {
            StreamReader sr = new StreamReader(PrecFile, Encoding.Default);
            string line;
            int count = 0;
            while ((line = sr.ReadLine()) != null)
                count++;
            //DateTime[] StartTime = new DateTime[count];
            //DateTime[] EndTime = new DateTime[count];
            DateTime[] precTime = new DateTime[count * 4];
            double[] Prec = new double[count * 4];
            int idx = 0;
            StreamReader sr2 = new StreamReader(PrecFile, Encoding.Default);
            string line2;
            while ((line2 = sr2.ReadLine()) != null)
            {
                //StartTime[idx] = DateTime.Parse(line2.Split('\t')[0].ToString() + " " + line2.Split('\t')[1].ToString());
                //EndTime[idx] = DateTime.Parse(line2.Split('\t')[0].ToString() + " " + line2.Split('\t')[2].ToString());
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
            //PrecData precdata = new PrecData();
            //precdata.StartTime = StartTime;
            //precdata.EndTime = EndTime;
            //precdata.Prec = Prec;
            QData precdata = new QData();
            precdata.Time = precTime;
            precdata.QValue = Prec;
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
    }
}

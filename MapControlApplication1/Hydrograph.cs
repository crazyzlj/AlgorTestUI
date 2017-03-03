using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

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
            {
                SimQ = SimQOrig;
                ObsQ = ObsQOrig;
            }
            double nash = NashCoef(ObsQ.QValue, SimQ.QValue);
            //MessageBox.Show(nash.ToString());
            axTChartHydrograph.Axis.Left.Automatic = false;
            axTChartHydrograph.Axis.Left.Maximum = Math.Max(ObsQ.QValue.Max(),SimQ.QValue.Max()) * 1.5;
            //axTChartHydrograph.Axis.Left.Minimum = Math.Min(0, Math.Min(ObsQ.QValue.Min(), SimQ.QValue.Min()) - 10);
            axTChartHydrograph.Axis.Left.Minimum = Math.Min(ObsQ.QValue.Min(), SimQ.QValue.Min()) - 10;
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
            sr.Close();
            double[] Q = new double[count];
            DateTime[] Time = new DateTime[count];
            StreamReader sr2 = new StreamReader(QFile, Encoding.Default);
            int idx = 0;
            string line2;
            while ((line2 = sr2.ReadLine()) != null)
            {
                //try
                //{
                //    Time[idx] = DateTime.Parse(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[0] + " " + System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[1]);
                //    Q[idx] = Convert.ToDouble(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[2].ToString().Trim());
                //    idx++;
                //}
                //catch (System.Exception ex)
                //{
                //    DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
                //    dtFormat.ShortDatePattern = "yyyy/MM/dd";
                //    Time[idx] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[0].ToString(), dtFormat);
                //    Q[idx] = Convert.ToDouble(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[1].ToString().Trim());
                //    idx++;
                //}

                Time[idx] = DateTime.Parse(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[0] + " " + System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[1]);
                //Time[idx] = DateTime.Parse(line2.Split('\t')[0].ToString());
                Q[idx] = Convert.ToDouble(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[2].ToString().Trim());
                //MessageBox.Show(Time[idx].ToString()); 
                idx++;
            }
            QData qdata = new QData();
            qdata.Time = Time;
            qdata.QValue = Q;
            qdata.count = count;
            sr2.Close();
            return qdata;
        }
        public QData ReadPrecData(string PrecFile)
        {
            StreamReader sr = new StreamReader(PrecFile, Encoding.Default);
            string line;
            int count = 0;
            string[] line2 = new string[500];
            while ((line = sr.ReadLine()) != null)
            {
                line2[count] = line.ToString();
                count++;
            }
            sr.Close();
            DateTime[] precTime = new DateTime[count * 4];
            double[] Prec = new double[count * 4];
            int idx = 0;
            StreamReader sr2 = new StreamReader(PrecFile, Encoding.Default);
            int identi = line2[0].Split('\t').Length;
            for (int i = 0; i < count;i++ )
            {
                DateTime temptime = DateTime.Parse(System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[0] + " " + System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[1]);
                
                if (identi ==4)
                {
                    DateTime temptimeend = DateTime.Parse(System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[0] + " " + System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[2]);
                    double tempvalue = Convert.ToDouble(System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[3].ToString().Trim());
                    precTime[idx] = temptime;
                    precTime[idx + 1] = temptime;
                    precTime[idx + 2] = temptimeend;
                    precTime[idx + 3] = temptimeend;
                    Prec[idx] = 0.0;
                    Prec[idx + 1] = tempvalue;
                    Prec[idx + 2] = tempvalue;
                    Prec[idx + 3] = 0.0;
                    //MessageBox.Show(Prec[idx].ToString());
                    idx = idx + 4;
                }
                else
                {
                    double tempvalue = Convert.ToDouble(System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[2].ToString().Trim());
                    precTime[idx] = temptime;
                    precTime[idx + 1] = temptime;
                    if (idx + 3 < count * 4 - 1)
                    {
                        DateTime temptime2 = DateTime.Parse(System.Text.RegularExpressions.Regex.Split(line2[i+1], @"\s+")[0] + " " + System.Text.RegularExpressions.Regex.Split(line2[i+1], @"\s+")[1]); 
                        precTime[idx + 2] = temptime2;
                        precTime[idx + 3] = temptime2;
                        //precTime[idx + 2] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2[i + 1], @"\s+")[0].ToString(), dtFormat);
                        //precTime[idx + 3] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2[i + 1], @"\s+")[0].ToString(), dtFormat);
                    }
                    else
                    {
                        precTime[idx + 2] = temptime;
                        precTime[idx + 3] = temptime;
                    }
                    Prec[idx] = 0.0;
                    Prec[idx + 1] = tempvalue;
                    Prec[idx + 2] = tempvalue;
                    Prec[idx + 3] = 0.0;
                    idx = idx + 4;

                    //DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
                    //dtFormat.ShortDatePattern = "yyyy/MM/dd";
                    //DateTime temptime = Convert.ToDateTime(line2[i].Split('\t')[0].ToString().Trim(), dtFormat);
                    //double tempvalue = Convert.ToDouble(System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[1].ToString().Trim());
                    //precTime[idx] = temptime;
                    //precTime[idx + 1] = temptime;
                    ////precTime[idx] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[0].ToString(), dtFormat);
                    ////precTime[idx + 1] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[0].ToString(), dtFormat);
                    //if (idx + 3 < count * 4 - 1)
                    //{
                    //    DateTime temptime2 = Convert.ToDateTime(line2[i + 1].Split('\t')[0].ToString().Trim(), dtFormat);
                    //    precTime[idx + 2] = temptime2;
                    //    precTime[idx + 3] = temptime2;
                    //    //precTime[idx + 2] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2[i + 1], @"\s+")[0].ToString(), dtFormat);
                    //    //precTime[idx + 3] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2[i + 1], @"\s+")[0].ToString(), dtFormat);
                    //}
                    //else
                    //{
                    //    precTime[idx + 2] = temptime;
                    //    precTime[idx + 3] = temptime;
                    //    //precTime[idx + 2] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[0].ToString(), dtFormat);
                    //    //precTime[idx + 3] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[0].ToString(), dtFormat);
                    //}
                    //Prec[idx] = 0.0;
                    //Prec[idx + 1] = tempvalue;
                    //Prec[idx + 2] = tempvalue;
                    ////Prec[idx + 1] = Convert.ToDouble(System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[1].ToString().Trim());
                    ////Prec[idx + 2] = Convert.ToDouble(System.Text.RegularExpressions.Regex.Split(line2[i], @"\s+")[1].ToString().Trim());
                    //Prec[idx + 3] = 0.0;
                    ////MessageBox.Show(Prec[idx].ToString());
                    //idx = idx + 4;
                }
                
            }
            sr2.Close();
            //while ((line2 = sr2.ReadLine()) != null)
            //{
            //    try
            //    {
            //        precTime[idx] = DateTime.Parse(line2.Split('\t')[0].ToString() + " " + line2.Split('\t')[1].ToString());
            //        precTime[idx + 1] = DateTime.Parse(line2.Split('\t')[0].ToString() + " " + line2.Split('\t')[1].ToString());
            //        precTime[idx + 2] = DateTime.Parse(line2.Split('\t')[0].ToString() + " " + line2.Split('\t')[2].ToString());
            //        precTime[idx + 3] = DateTime.Parse(line2.Split('\t')[0].ToString() + " " + line2.Split('\t')[2].ToString());
            //        Prec[idx] = 0.0;
            //        Prec[idx + 1] = Convert.ToDouble(line2.Split('\t')[3].ToString().Trim());
            //        Prec[idx + 2] = Convert.ToDouble(line2.Split('\t')[3].ToString().Trim());
            //        Prec[idx + 3] = 0.0;
            //        //MessageBox.Show(Prec[idx].ToString());
            //        idx = idx + 4;
            //    }
            //    catch (System.Exception ex)
            //    {
            //        DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            //        dtFormat.ShortDatePattern = "yyyy/MM/dd";
            //        precTime[idx] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[0].ToString(), dtFormat);
            //        precTime[idx + 1] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[0].ToString(), dtFormat);
            //        precTime[idx + 2] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[0].ToString(), dtFormat);
            //        precTime[idx + 3] = Convert.ToDateTime(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[0].ToString(), dtFormat);
            //        Prec[idx] = 0.0;
            //        Prec[idx + 1] = Convert.ToDouble(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[1].ToString().Trim());
            //        Prec[idx + 2] = Convert.ToDouble(System.Text.RegularExpressions.Regex.Split(line2, @"\s+")[1].ToString().Trim());
            //        Prec[idx + 3] = 0.0;
            //        //MessageBox.Show(Prec[idx].ToString());
            //        idx = idx + 4;
            //    }
                
            //}
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

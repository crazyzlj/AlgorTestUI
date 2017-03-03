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
    public partial class FormPareto : Form
    {
        public struct XYCoords
        {
            public double[] X;
            public double[] Y;
        }
        public FormPareto()
        {
            InitializeComponent();
        }

        private void FormPareto_Load(object sender, EventArgs e)
        {
            string testfile = "C:\\Users\\ZhuLJ\\Desktop\\BMP\\output16\\final_nondom_pop.out";
            string testfile2 = "C:\\Users\\ZhuLJ\\Desktop\\BMP\\output32\\final_nondom_pop.out";
            XYCoords xy = ReadXYData(testfile);
            XYCoords xy2 = ReadXYData(testfile2);
            //MessageBox.Show(xy.X[0].ToString() + "," + xy.Y[0].ToString());
            initializeChart();
            Plots("cores32", xy.X, xy.Y);
            Plots("cores16", xy2.X, xy2.Y);
        }
        public void initializeChart()
        {
            this.axTChartParetoPlots.Axis.Left.Title.Caption = "泥沙削减率 (%)";
            this.axTChartParetoPlots.Axis.Bottom.Title.Caption = "BMP成本 (元)";
        }
        public void Plots(string seriesName,double[] x,double[] y)
        {
            int seriesNum1 = this.axTChartParetoPlots.AddSeries(TeeChart.ESeriesClass.scPoint);
            this.axTChartParetoPlots.Series(seriesNum1).Name = seriesName;
            
            this.axTChartParetoPlots.Legend.ShadowSize = 0;
            this.axTChartParetoPlots.Legend.ColumnWidthAuto = true;
            this.axTChartParetoPlots.Series(seriesNum1).AddArray(x.Length, y, x);
        }
        public XYCoords ReadXYData(string dateFile)
        {
            StreamReader sr = new StreamReader(dateFile, Encoding.Default);
            string line;
            int count = 0;
            while ((line = sr.ReadLine()) != null)
            {
                //MessageBox.Show(line.ToString());
                //Lines[count] = line.ToString();
                count++;
            }
            string[] Lines = new string[count];
            StreamReader sr2 = new StreamReader(dateFile, Encoding.Default);
            int idx = 0;
            while ((line = sr2.ReadLine()) != null)
            {
                Lines[idx] = line.ToString();
                idx++;
            }
            string[] CaliResults = new string[count - 2];
            for (int i = 0; i < count - 2; i++)
            {
                CaliResults[i] = Lines[i + 2];
                //MessageBox.Show(CaliResults[i].ToString());
            }
            double[] X = new double[CaliResults.Length];
            double[] Y = new double[CaliResults.Length];
            for (int i = 0; i < CaliResults.Length; i++)
            {
                X[i] = Convert.ToDouble(CaliResults[i].Split('\t')[4].ToString().Trim());
                Y[i] = Convert.ToDouble(CaliResults[i].Split('\t')[3].ToString().Trim()) * -1;
            }
            XYCoords XY = new XYCoords();
            XY.X = X;
            XY.Y = Y;
            return XY;
        }
        
    }
}

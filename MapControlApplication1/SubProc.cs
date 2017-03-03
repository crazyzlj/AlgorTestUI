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
    public partial class SubProc : Form
    {
        private string RunTimefile;
        public string RunTimeFile
        {
            get { return RunTimefile; }
            set { RunTimefile = value; }
        }
        public SubProc()
        {
            InitializeComponent();
        }

        private void SubProc_Load(object sender, EventArgs e)
        {
            initializeChart();
            //string[] AllTime = File.ReadAllLines(@"F:\863ResultsDataNew\WatershedModeling\SubprocessTest\Daily\RunTime.txt");
            string[] AllTime = File.ReadAllLines(RunTimefile);
            double[] AllCORES = { 1, 2, 4, 8, 16};
            double[] tempcores = new double[AllTime[0].Split(' ').Length - 3];
            for (int i = 0; i < AllTime[0].Split(' ').Length - 3;i++ )
            {
                tempcores[i] = Convert.ToDouble(AllTime[0].Split(' ')[i+2]);
            }
            int tempcount = 0;
            double[] tempcores2 = new double[tempcores.Length];
            for (int i = 0; i < tempcores.Length; i++)
            {
                for (int j = 0; j < AllCORES.Length;j++ )
                {
                    if (AllCORES[j] == tempcores[i])
                    {
                        tempcores2[tempcount] = tempcores[i];
                        tempcount++;
                    }
                }
            }
            double[] cores = new double[tempcount];
            for (int i = 0; i < tempcount; i++)
            {
                cores[i] = tempcores2[i];
            }
            for (int i = 1; i < AllTime.Length; i++)
            {
                string[] item = AllTime[i].Split(' ');
                string SubProcName = item[1];
                double[] SubProcTime = new double[tempcount];
                for (int j = 0; j < tempcount; j++)
                {
                    SubProcTime[j] = Convert.ToDouble(item[2 + j]);
                }
                Plots(this.axTChartSubProcRuntime, SubProcName, cores, SubProcTime);
                if (tempcount > 1)
                {
                    double[] SubProcSpeed = new double[tempcount];
                    double[] SubProcEff = new double[tempcount];
                    SubProcEff[0] = 1;
                    SubProcSpeed[0] = 1;
                    for (int k = 1; k < tempcount; k++)
                    {
                        SubProcSpeed[k] = SubProcTime[0] / SubProcTime[k];
                        SubProcEff[k] = SubProcTime[0] / (SubProcTime[k] * cores[k]);
                    }
                    Plots(this.axTChartSubProcSpeedup, SubProcName, cores, SubProcSpeed);
                    Plots(this.axTChartSubProcEfficiency, SubProcName, cores, SubProcEff);
                }
                
            }
            //this.axTChartSubProcRuntime.Series
            
        }
        
        public void initializeChart()
        {
            this.axTChartSubProcRuntime.Axis.Left.Title.Caption = "时间/秒";
            this.axTChartSubProcRuntime.Axis.Bottom.Title.Caption = "核数";
            this.axTChartSubProcSpeedup.Axis.Left.Title.Caption = "加速比";
            this.axTChartSubProcSpeedup.Axis.Bottom.Title.Caption = "核数";
            this.axTChartSubProcEfficiency.Axis.Left.Title.Caption = "并行效率";
            this.axTChartSubProcEfficiency.Axis.Bottom.Title.Caption = "核数";
            this.axTChartSubProcEfficiency.Axis.Left.Increment = 0.2;
            //this.axTChartSubProcEfficiency.Axis.Left.SetMinMax(0.0, 1.4);
        }
        public void Plots(AxTeeChart.AxTChart chart ,string seriesName, double[] x, double[] y)
        {
            int seriesNum1 = chart.AddSeries(TeeChart.ESeriesClass.scLine);
            chart.Series(seriesNum1).asLine.LinePen.Width = 2;
            chart.Series(seriesNum1).asLine.Pointer.Visible = true;
            chart.Series(seriesNum1).asLine.Pointer.Style = TeeChart.EPointerStyle.psCircle;
            chart.Series(seriesNum1).asLine.Pointer.HorizontalSize = 3;
            chart.Series(seriesNum1).asLine.Pointer.VerticalSize = 3;
            chart.Series(seriesNum1).Name = seriesName;
            chart.Legend.ShadowSize = 0;
            chart.Legend.ColumnWidthAuto = true;
            chart.Series(seriesNum1).AddArray(x.Length, y, x);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InverseChecked(axTChartSubProcRuntime);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            InverseChecked(axTChartSubProcSpeedup);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InverseChecked(axTChartSubProcEfficiency);
        }
        public void InverseChecked(AxTeeChart.AxTChart chart)
        {
            int SeriesNum = chart.SeriesCount;
            for (int i = 0; i < SeriesNum; i++)
            {
                if (chart.Series(i).Active)
                {
                    chart.Series(i).Active = false;
                }
                else
                    chart.Series(i).Active = true;
            }
        }
    }
}

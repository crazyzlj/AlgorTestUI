using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Web.Services;
using System.Threading;
using System.Diagnostics;

namespace TestDemo
{
    public partial class MainForm : Form
    {
        //public System.Windows.Forms.MdiClient m_MdiClient;
        private TestDemo.SelectCoresFrm child;
        private TestDemo.HelpMessage child2;
        private TestDemo.FormWuhui_calibration child3;
        private TestDemo.Hydrograph childHydro;
        private TestDemo.FormPareto childPareto;
        private TestDemo.SubProc childSubProc;
        //protected BackgroundWorker worker = new BackgroundWorker();
        //protected int SelectedCoreNum = 0;
        bool downFlag = false;  // False : Download Spatial Analysis Files and HydroModeling Files on LINUX Server; True: Download HydroModeling Files on Windows Server.
        static bool long_seimWIN = false;
        static bool storm_seimLINUX = false;
        static bool long_seimLINUX = false;
        static bool storm_seimWIN = false;
        static bool OptBMPs = false;
        static bool Yanglin = false;
        static bool DownloadOrNot = true;
        string[] defaultCores = { "16", "32" };
        string[] CORE;
        bool downInputs = false;
        static bool WuhuiCalibration = false;
        public MainForm()
        {
            InitializeComponent();
            TestDemo.SelectCoresFrm child = new TestDemo.SelectCoresFrm();
            //child.MdiParent = this;
            TestDemo.HelpMessage child2 = new TestDemo.HelpMessage();
            //child2.MdiParent = this;
            //int iCnt = this.Controls.Count;
            //for (int i = 0; i < iCnt; i++)
            //{
            //    if (this.Controls[i].GetType().ToString() == "System.Windows.Forms.MdiClient")
            //    {
            //        this.m_MdiClient = (System.Windows.Forms.MdiClient)this.Controls[i];
            //        break;
            //    }
            //}
            //this.m_MdiClient.BackColor = System.Drawing.SystemColors.Control;
            InitializeCombobox();
            //DrawTimeLines();
            //string ResultStr = "[DEBUG][COMPUTING TIME] 0.56;[DEBUG][IO TIME] 12.34;[DEBUG][TOTAL TIME] 12.9";
            //string ResultStr2 = "123456 jjc1022 [DEBUG] [OPTIONS] input file directory: /data/wangy/test/data/sc_p_15w.shp [DEBUG] [OPTIONS][IO] 21.9667  output file directory: /data/wangy/test/result/sc_p_3w_dis_32.shp [DEBUG] [OPTIONS] input file size is : 150000*16*1 [DEBUG] time consuming: [DEBUG][TIMESPAN][DEBUG][TIMESPAN][COMPUTING] 64.5678 [DEBUG][TIMESPAN][TOTAL] 86.5344 hello world! end";
            //double[] TimeValues1 = ExtractTimeValue(ResultStr);
            //double[] TimeValues2 = ExtractTimeValue(ResultStr2);//依次为IO、COMPUTING、TOTAL时间
            //string[] IOFiles = IOFile(ResultStr2);//依次为输入输出文件
            //for (int i = 0; i < TimeValues2.Length; i++)
            //{
            //    MessageBox.Show(TimeValues2[i].ToString());
            //}
            //for (int i = 0; i < IOFiles.Length; i++)
            //{
            //    MessageBox.Show(IOFiles[i].ToString());
            //}
            //progressBarUtility.Style = ProgressBarStyle.Marquee;
            //Application.EnableVisualStyles();
            //worker.WorkerReportsProgress = true;
            //worker.DoWork +=new DoWorkEventHandler(worker_DoWork);
            //worker.ProgressChanged +=new ProgressChangedEventHandler(worker_ProgressChanged);
            //worker.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        //#region Code of BackgroundWorker
        //void worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    int start = (int)e.Argument;
        //    e.Result = DoWorkAsync(start, (BackgroundWorker)sender, e);
        //}
        //private int DoWorkAsync(int start, BackgroundWorker worker, DoWorkEventArgs e)
        //{
        //    string[] RunCores = SelectedStrs(checkedListBoxCores);
        //    int NUM = RunCores.Length;
        //    SelectedCoreNum = SelectedCoreNumFunc();
        //    string shellName;//  "dis_shell";
        //    bool downFlag = false;
        //    string state = "";
        //    labelProgramState.Text = "";
        //    if (((ComboxItem)this.comboBoxFirstClass.SelectedItem).Value == "SA" && ((ComboxItem)this.comboBoxThirdClass.SelectedItem).Value != null)
        //    {
        //        shellName = ((ComboxItem)this.comboBoxThirdClass.SelectedItem).Value;
        //        state = this.comboBoxThirdClass.SelectedItem.ToString();
        //        downFlag = false;
        //    }
        //    else if (((ComboxItem)comboBoxFirstClass.SelectedItem).Value == "HM" && ((ComboxItem)comboBoxSecondClass.SelectedItem).Value != null)
        //    {
        //        shellName = ((ComboxItem)comboBoxSecondClass.SelectedItem).Value;
        //        state = comboBoxSecondClass.SelectedItem.ToString();
        //        downFlag = true;
        //    }
        //    else
        //    {
        //        shellName = "";
        //        MessageBox.Show("请正确选择算法后点击执行！");
        //    }
        //    //MessageBox.Show(shellName);
        //    //shellName = "dis_shell";

        //    int[] Cores = new int[NUM];
        //    double[] CompTime = new double[NUM];
        //    double[] IOTime = new double[NUM];
        //    double[] TotalTime = new double[NUM];
        //    //MessageBox.Show(NUM.ToString());
        //    for (int i = 0; i < NUM; i++)
        //    {
        //        labelProgramState.Text = "正在计算" + state + "，计算核心数为：" + RunCores[i].ToString() + "个！";
        //        labelProgramState.Update();
        //        Cores[i] = Convert.ToInt16(RunCores[i].ToString().Trim());
        //        string resultStr = GetRunResult(shellName, RunCores[i]);
        //        double[] tempValues = ExtractTimeValue(resultStr);
        //        IOTime[i] = tempValues[0];
        //        CompTime[i] = tempValues[1];
        //        TotalTime[i] = tempValues[2];
        //        // Update processbar value
        //        worker.ReportProgress((i + 1) % SelectedCoreNum);
        //    }
        //    DrawTimeLines(Cores, CompTime, IOTime, TotalTime);
        //    labelProgramState.Text = state + "计算完毕！";
        //    InitializeCombox();
        //    buttonExecute.Enabled = false;
        //    if (downFlag)
        //    {
        //        buttonHydroDown.Enabled = true;
        //        buttonSpaAnDown.Enabled = false;
        //    }
        //    else
        //    {
        //        buttonSpaAnDown.Enabled = true;
        //        buttonHydroDown.Enabled = false;
        //    }
        //    return NUM;
        //}
        //void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    progressBarUtility.Value = SelectedCoreNum;
        //    if (e.Error != null)
        //    {
        //        MessageBox.Show("计算完成！");
        //    }
        //}
        //// Update processbar value
        //void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    progressBarUtility.Value = e.ProgressPercentage;
        //}
        //#endregion
        public string GetRunResult(string shellName, string CoreNum)
        {
            string requestUrl = null;
            if (long_seimWIN || storm_seimWIN)
            {
                requestUrl = "http://192.168.6.56:8080/invoke_linux_shell/services/invoke_windows?method=call_cmd&batName="+shellName+"&cores=" + CoreNum;
            }
            else
            {
                requestUrl = "http://192.168.6.55:8080/invoke_linux_shell/services/invoke_shell?method=call&shfile=" + shellName + "&cores=" + CoreNum;
            }
            //string requestUrl = "http://192.168.6.55:8080/invoke_linux_shell/services/invoke_shell?method=call&shfile=";//dis_shell&cores=1
            //requestUrl = requestUrl + shellName + "&cores=" + CoreNum;
            HttpWebRequest testRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
            testRequest.Method = "GET";
            //testRequest.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            //testRequest.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
            //testRequest.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6");
            //testRequest.Headers.Add("Connection", "keep-alive");
            //testRequest.Headers.Add("Host", "192.168.6.55:8080");
            //testRequest.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.137 Safari/537.36");
            testRequest.Timeout = 10000000;
            WebResponse res = testRequest.GetResponse();
            Stream respStream = res.GetResponseStream();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream))
            {
                //MessageBox.Show(reader.ReadToEnd());
                return reader.ReadToEnd().ToString();
            }
        }
        private static double[] ExtractResultsValues(string ResultStr, int num)
        {
            double[] values = new double[num];
            if (num == 1)
            {
                Regex ExtratValues = new Regex("\\[\\bR\\w{5}\\](.*)", RegexOptions.Singleline);
                Regex ExtractValues2 = new Regex(@"[+-]?\d+\.?\d*(e?)([+-]?)(\d+)$", RegexOptions.Multiline);
                string tempStr = ExtratValues.Matches(ResultStr)[0].ToString();
                Console.WriteLine(ExtractValues2.Matches(tempStr).Count);
                if (num == ExtractValues2.Matches(tempStr).Count)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = Convert.ToDouble(ExtractValues2.Matches(tempStr)[i].ToString().Trim());
                    }
                    return values;
                }
                else
                    return null;
            }
            else
            {
                Regex ExtractValues3 = new Regex("\\[\\bO\\w{5}\\](.*)$", RegexOptions.Multiline);
                Regex ExtractValues4 = new Regex(@"[+-]?\d+\.?\d*(e?)([+-]?)(\d+)$", RegexOptions.IgnoreCase);
                string[] tempStr = new string[ExtractValues3.Matches(ResultStr).Count];
                if (num == tempStr.Length)
                {
                    for (int i = 0; i < tempStr.Length; i++)
                    {
                        tempStr[i] = ExtractValues3.Matches(ResultStr)[i].ToString();
                        values[i] = Convert.ToDouble(ExtractValues4.Matches(tempStr[i])[0].ToString().Trim());
                    }
                    return values;
                }
                else
                    return null;
            }
        }
        private static double[] ExtractTimeValue(string ResultStr)
        {
            double[] TimeValues = new double[3];
            Regex ExtratTimeIO = new Regex(@"\[\bI\w{1}\]\s*(:?)\d*\.\d+");
            Regex ExtratTimeComp = new Regex(@"\[\bC\w*\]\s*(:?)\d*\.\d+");
            Regex ExtratTimeTotal = new Regex("\\[\\bT\\w{4}\\]\\s*(:?)\\d*\\.\\d+");
            Regex ExtratTimeReg2 = new Regex("\\d*\\.\\d+");
            //MessageBox.Show(ExtratTimeComp.Matches(ResultStr)[0].ToString());
            TimeValues[0] = Convert.ToDouble(ExtratTimeReg2.Matches(ExtratTimeIO.Matches(ResultStr)[0].ToString())[0].ToString().Trim());
            TimeValues[1] = Convert.ToDouble(ExtratTimeReg2.Matches(ExtratTimeComp.Matches(ResultStr)[0].ToString())[0].ToString().Trim());
            TimeValues[2] = Convert.ToDouble(ExtratTimeReg2.Matches(ExtratTimeTotal.Matches(ResultStr)[0].ToString())[0].ToString().Trim());
            return TimeValues;
        }
        private static double[] ExtractLongSEIMSubProcTime(string ResultStr)
        {
            double[] tempProcTime = new double[34];
            double[] SubProcTime = new double[23];
            Regex ExtratProcTime = new Regex(@"\[\bP\w{6}\].*$", RegexOptions.Multiline);
            Regex ExtratProcTime2 = new Regex(@"\d+(\.?)\d*");
            for (int i = 0; i < 34; i++)
            {
                string tempstr = ExtratProcTime.Matches(ResultStr)[i].ToString();
                tempProcTime[i] = Convert.ToDouble(ExtratProcTime2.Matches(tempstr)[0].ToString().Trim());
            }
            SubProcTime[0] = tempProcTime[5];
            for (int i = 6; i < 12; i++)
            {
                SubProcTime[1] += tempProcTime[i];
            }
            int temp = 2;
            for (int i = 12; i < 28; i++)
            {
                SubProcTime[temp] = tempProcTime[i];
                temp++;
            }
            for (int i = 29; i < 34; i++)
            {
                SubProcTime[temp] = tempProcTime[i];
                temp++;
            }
            return SubProcTime;
        }
        private static double[] ExtractStormSEIMSubProcTime(string ResultStr)
        {
            double[] SubProcTime = new double[12];
            Regex ExtratProcTime = new Regex(@"\[\bP\w{6}\].*$", RegexOptions.Multiline);
            Regex ExtratProcTime2 = new Regex(@"\d+(\.?)\d*");
            for (int i = 0; i < 12; i++)
            {
                string tempstr = ExtratProcTime.Matches(ResultStr)[i + 1].ToString();
                SubProcTime[i] = Convert.ToDouble(ExtratProcTime2.Matches(tempstr)[0].ToString().Trim());
            }
            return SubProcTime;
        }
#region old InputFiles and OutputFiles

        //private static string[] InputFiles(string ResultStr)
        //{
        //    int trueFileNum = 0;
        //    Regex ExtractInputFiles = new Regex("\\bi\\w{8}\\:(.*)\\bo\\w{9}\\:", RegexOptions.Singleline);
        //    Regex ExtractInputFile = new Regex("/(.*)$", RegexOptions.Multiline);
        //    try
        //    {
        //        string tempStr = ExtractInputFiles.Matches(ResultStr)[0].ToString();
        //        string[] InFiles = new string[ExtractInputFile.Matches(tempStr).Count];
        //        for (int i = 0; i < InFiles.Length; i++)
        //        {
        //            InFiles[i] = ExtractInputFile.Matches(tempStr)[i].ToString().Trim();
        //            InFiles[i] = Regex.Replace(InFiles[i], @"\s+", " ");
        //            for (int j = 0; j < InFiles[i].Split(' ').Length; j++)
        //                trueFileNum++;
        //        }
        //        if (trueFileNum == InFiles.Length)
        //        {
        //            return InFiles;
        //        }
        //        else
        //        {
        //            string[] InFiles2 = new string[trueFileNum];
        //            trueFileNum = 0;
        //            for (int i = 0; i < InFiles.Length; i++)
        //            {
        //                InFiles[i] = ExtractInputFile.Matches(tempStr)[i].ToString().Trim();
        //                InFiles[i] = Regex.Replace(InFiles[i], @"\s+", " ");
        //                for (int j = 0; j < InFiles[i].Split(' ').Length; j++)
        //                {
        //                    InFiles2[trueFileNum] = InFiles[i].Split(' ')[j];
        //                    trueFileNum++;
        //                }
        //            }
        //            return InFiles2;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //private static string[] OutputFiles(string ResultStr)
        //{
        //    int trueFileNum = 0;
        //    Regex ExtractOutputFiles = new Regex("\\bo\\w{9}\\:(.*)", RegexOptions.Singleline);
        //    Regex ExtractOutputFile = new Regex("/(.*)$", RegexOptions.Multiline);
        //    try
        //    {
        //        string tempStr = ExtractOutputFiles.Matches(ResultStr)[0].ToString();
        //        string[] OutFiles = new string[ExtractOutputFile.Matches(tempStr).Count - 1];
        //        //Console.WriteLine(ExtractOutputFile.Matches(tempStr).Count);
        //        for (int i = 0; i < OutFiles.Length; i++)
        //        {
        //            OutFiles[i] = ExtractOutputFile.Matches(tempStr)[i].ToString().Trim();
        //            OutFiles[i] = Regex.Replace(OutFiles[i], @"\s+", " ");
        //            for (int j = 0; j < OutFiles[i].Split(' ').Length; j++)
        //                trueFileNum++;
        //        }
        //        if (trueFileNum == OutFiles.Length)
        //        {
        //            return OutFiles;
        //        }
        //        else
        //        {
        //            string[] OutFiles2 = new string[trueFileNum];
        //            trueFileNum = 0;
        //            for (int i = 0; i < OutFiles.Length; i++)
        //            {
        //                OutFiles[i] = ExtractOutputFile.Matches(tempStr)[i].ToString().Trim();
        //                OutFiles[i] = Regex.Replace(OutFiles[i], @"\s+", " ");
        //                for (int j = 0; j < OutFiles[i].Split(' ').Length; j++)
        //                {
        //                    OutFiles2[trueFileNum] = OutFiles[i].Split(' ')[j];
        //                    trueFileNum++;
        //                }
        //            }
        //            return OutFiles2;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return null;
        //    }
        //}
#endregion
        public void InitialFlag()
        {
            downFlag = false;
            long_seimWIN = false;
            long_seimLINUX = false;
            storm_seimLINUX = false;
            storm_seimWIN = false;
            WuhuiCalibration = false;
            OptBMPs = false;
            Yanglin = false;
        }
        private static string[] InputFiles(string ResultStr)
        {
            int trueFileNum = 0;
            Regex ExtractInputFiles = new Regex("\\bi\\w{8}\\:(.*)\\bo\\w{9}\\:", RegexOptions.Singleline);
            Regex ExtractInputFile = new Regex("/(.*)$", RegexOptions.Multiline);
            Regex ExtractInputFile2 = new Regex(@"D:\\(.+)$", RegexOptions.Multiline);
            try
            {
                string tempStr = ExtractInputFiles.Matches(ResultStr)[0].ToString();
                string[] OutFiles = null;
                //Console.WriteLine(ExtractInputFile.Matches(tempStr)[0]);
                if (ExtractInputFile.Matches(tempStr).Count == 0)
                {
                    OutFiles = new string[ExtractInputFile2.Matches(tempStr).Count];
                    for (int i = 0; i < OutFiles.Length; i++)
                    {
                        //Console.WriteLine(ExtractInputFile2.Matches(tempStr)[i]);
                        OutFiles[i] = ExtractInputFile2.Matches(tempStr)[i].ToString().Trim();
                        OutFiles[i] = Regex.Replace(OutFiles[i], @"\s+", " ");
                        for (int j = 0; j < OutFiles[i].Split(' ').Length; j++)
                            trueFileNum++;
                    }
                }
                else
                {
                    OutFiles = new string[ExtractInputFile.Matches(tempStr).Count];
                    for (int i = 0; i < OutFiles.Length; i++)
                    {
                        //Console.WriteLine(ExtractInputFile.Matches(tempStr)[i]);
                        OutFiles[i] = ExtractInputFile.Matches(tempStr)[i].ToString().Trim();
                        OutFiles[i] = Regex.Replace(OutFiles[i], @"\s+", " ");
                        for (int j = 0; j < OutFiles[i].Split(' ').Length; j++)
                            trueFileNum++;
                    }
                }
                if (trueFileNum == OutFiles.Length)
                {
                    return OutFiles;
                }
                else
                {
                    string[] OutFiles2 = new string[trueFileNum];
                    trueFileNum = 0;
                    for (int i = 0; i < OutFiles.Length; i++)
                    {
                        //OutFiles[i] = ExtractInputFile.Matches(tempStr)[i].ToString().Trim();
                        OutFiles[i] = Regex.Replace(OutFiles[i], @"\s+", " ");
                        for (int j = 0; j < OutFiles[i].Split(' ').Length; j++)
                        {
                            OutFiles2[trueFileNum] = OutFiles[i].Split(' ')[j];
                            trueFileNum++;
                        }
                    }
                    return OutFiles2;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }

            //try
            //{
            //    string tempStr = ExtractInputFiles.Matches(ResultStr)[0].ToString();
            //    string[] InFiles = new string[ExtractInputFile.Matches(tempStr).Count];
            //    for (int i = 0; i < InFiles.Length; i++)
            //    {
            //        InFiles[i] = ExtractInputFile.Matches(tempStr)[i].ToString().Trim();
            //        InFiles[i] = Regex.Replace(InFiles[i], @"\s+", " ");
            //        for (int j = 0; j < InFiles[i].Split(' ').Length; j++)
            //            trueFileNum++;
            //    }
            //    if (trueFileNum == InFiles.Length)
            //    {
            //        return InFiles;
            //    }
            //    else
            //    {
            //        string[] InFiles2 = new string[trueFileNum];
            //        trueFileNum = 0;
            //        for (int i = 0; i < InFiles.Length; i++)
            //        {
            //            InFiles[i] = ExtractInputFile.Matches(tempStr)[i].ToString().Trim();
            //            InFiles[i] = Regex.Replace(InFiles[i], @"\s+", " ");
            //            for (int j = 0; j < InFiles[i].Split(' ').Length; j++)
            //            {
            //                InFiles2[trueFileNum] = InFiles[i].Split(' ')[j];
            //                trueFileNum++;
            //            }
            //        }
            //        return InFiles2;
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    return null;
            //}
        }
        private static string[] OutputFiles(string ResultStr)
        {
            int trueFileNum = 0;
            Regex ExtractOutputFiles = new Regex("\\bo\\w{9}\\:(.*)", RegexOptions.Singleline);
            Regex ExtractOutputFile = new Regex("/(.+)$", RegexOptions.Multiline);
            Regex ExtractOutputFile2 = new Regex(@"D:\\(.+)$", RegexOptions.Multiline);
            try
            {
                string tempStr = ExtractOutputFiles.Matches(ResultStr)[0].ToString();
                string[] OutFiles = null;
                //Console.WriteLine(ExtractOutputFile.Matches(tempStr)[0]);
                if (ExtractOutputFile.Matches(tempStr).Count == 1)
                {
                    OutFiles = new string[ExtractOutputFile2.Matches(tempStr).Count];
                    for (int i = 0; i < OutFiles.Length; i++)
                    {
                        //Console.WriteLine(ExtractOutputFile2.Matches(tempStr)[i]);
                        OutFiles[i] = ExtractOutputFile2.Matches(tempStr)[i].ToString().Trim();
                        OutFiles[i] = Regex.Replace(OutFiles[i], @"\s+", " ");
                        for (int j = 0; j < OutFiles[i].Split(' ').Length; j++)
                            trueFileNum++;
                    }
                }
                else
                {
                    OutFiles = new string[ExtractOutputFile.Matches(tempStr).Count - 1];
                    for (int i = 0; i < OutFiles.Length; i++)
                    {
                        //Console.WriteLine(ExtractOutputFile.Matches(tempStr)[i]);
                        OutFiles[i] = ExtractOutputFile.Matches(tempStr)[i].ToString().Trim();
                        OutFiles[i] = Regex.Replace(OutFiles[i], @"\s+", " ");
                        for (int j = 0; j < OutFiles[i].Split(' ').Length; j++)
                            trueFileNum++;
                    }
                }
                //string[] OutFiles = new string[tempStr.Split('\n').Length];
                //Console.WriteLine(ExtractOutputFile.Matches(tempStr).Count.ToString());


                //Console.WriteLine(ExtractOutputFile.Matches(tempStr).Count);
                //for (int i = 0; i < OutFiles.Length; i++)
                //{
                //    OutFiles[i] = ExtractOutputFile.Matches(tempStr)[i].ToString().Trim();
                //    OutFiles[i] = Regex.Replace(OutFiles[i], @"\s+", " ");
                //    for (int j = 0; j < OutFiles[i].Split(' ').Length; j++)
                //        trueFileNum++;
                //}
                if (trueFileNum == OutFiles.Length)
                {
                    return OutFiles;
                }
                else
                {
                    string[] OutFiles2 = new string[trueFileNum];
                    trueFileNum = 0;
                    for (int i = 0; i < OutFiles.Length; i++)
                    {
                        //OutFiles[i] = ExtractOutputFile.Matches(tempStr)[i].ToString().Trim();
                        OutFiles[i] = Regex.Replace(OutFiles[i], @"\s+", " ");
                        for (int j = 0; j < OutFiles[i].Split(' ').Length; j++)
                        {
                            OutFiles2[trueFileNum] = OutFiles[i].Split(' ')[j];
                            trueFileNum++;
                        }
                    }
                    return OutFiles2;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void ToolStripMenuItemMapViewer_Click(object sender, EventArgs e)
        {
            EasyMapViewer MapViewerIns = new EasyMapViewer();
            MapViewerIns.Show();
        }
        private void ToolStripMenuItemHelp_Click(object sender, EventArgs e)
        {
            if (child2 == null || child2.IsDisposed)
            {
                child2 = new TestDemo.HelpMessage();
                //child2.MdiParent = this;
                //child2.TopMost = true;
                child2.Show();
            }
            else
                child2.Activate();
            //HelpMessage hp = new HelpMessage();
            //hp.MdiParent = this;
            //hp.Show();
        }
        public void ClearElementsBeforeExecute()
        {
            labelProgramState.Text = "";
            labelProgramState.Update();
            labelProgramState.Refresh();
            
            for (int i = 0; i < axTChartComputingTime.SeriesCount; i++)
            {
                axTChartComputingTime.Series(i).Clear();
                axTChartComputingTime.Update();
            }
            //axTChartComputingTime.Repaint();
            
            for (int i = 0; i < axTChartParallelEfficiency.SeriesCount;i++ )
            {
                axTChartParallelEfficiency.Series(i).Clear();
                axTChartParallelEfficiency.Update();
            }
            for (int i = 0; i < axTChartSpeedup.SeriesCount; i++)
            {
                axTChartSpeedup.Series(i).Clear();
                axTChartSpeedup.Update();
            }
            checkedListBoxOutputFiles.Items.Clear();
            checkedListBoxOutputFiles.Update();
            checkedListBoxInputFiles.Items.Clear();
            checkedListBoxInputFiles.Update();
            textBoxAlgorithmResults.Text = "";
            textBoxAlgorithmResults.Update();
            progressBarUtility.Value = 0;
            progressBarUtility.Update();

        }
        private void DrawTimeLines(int[] kernel, double[] ComputingTime, double[] IOTime, double[] TotalTime)
        {
            //ClearElementsBeforeExecute();
            //int[] kernel = new int[] { 4, 8, 32 };
            //double[] ComputingTime = new double[] { 4.3, 3.2, 1.1 };
            //double[] IOTime = new double[] { 20, 19, 18 };
            //double[] TotalTime = new double[] { 24.3, 22.2, 19.1 };
            // Calculate the maximum of parallel efficiency
            double maxPE = 0.0;
            for (int i = 1; i < kernel.Length; i++)
            {
                double tempPE = (double)ComputingTime[0] / ((double)ComputingTime[i] * (double)kernel[i]);
                if (tempPE > maxPE)
                    maxPE = tempPE;
            }
            if (maxPE < 1.0)
            {
                maxPE = 1.0;
            } 
            else
            {
                maxPE = maxPE * 1.1;
            }
            axTChartSpeedup.Axis.Left.Minimum = 0.0;
            axTChartSpeedup.Axis.Left.Maximum = 32;
            axTChartSpeedup.Axis.Bottom.Minimum = 0.0;
            axTChartSpeedup.Axis.Bottom.Maximum = 32;
            axTChartParallelEfficiency.Axis.Left.SetMinMax(0.0, maxPE);
            axTChartParallelEfficiency.Axis.Left.Increment = 0.2;
            axTChartParallelEfficiency.Axis.Bottom.Minimum = 0.0;
            axTChartParallelEfficiency.Axis.Bottom.Maximum = 32;
            axTChartComputingTime.Axis.Left.Minimum = 0.0;
            axTChartComputingTime.Axis.Bottom.Minimum = 0.0;
            if (kernel.Length == ComputingTime.Length && ComputingTime.Length == TotalTime.Length)
            {
                //for (int i = 0; i < 2; i++)
                //    axTChartComputingTime.Series(i).Clear();
                //double[] ComputTime = ComputingTime.ToArray(typeof(double));

                for (int i = 0; i < kernel.Length; i++)
                {
                    axTChartComputingTime.Series(0).AddXY((double)kernel[i], (double)ComputingTime[i], "", 1);
                    axTChartComputingTime.Series(1).AddXY((double)kernel[i], (double)IOTime[i], "", 1);
                    axTChartComputingTime.Series(2).AddXY((double)kernel[i], (double)TotalTime[i], "", 1);
                    
                }
                axTChartSpeedup.Series(1).AddXY((double)kernel[0], (double)kernel[0], "", 1);
                for (int i = 1; i < kernel.Length;i++ )
                {
                    axTChartSpeedup.Series(0).AddXY((double)kernel[i], (double)ComputingTime[0] / (double)ComputingTime[i], "", 1);
                    axTChartSpeedup.Series(1).AddXY((double)kernel[i], (double)kernel[i], "", 1);
                    axTChartParallelEfficiency.Series(0).AddXY((double)kernel[i], (double)ComputingTime[0] / ((double)ComputingTime[i] * (double)kernel[i]), "", 1);
                    
                }
            }
            tabControlParallelMap.SelectTab(tabPageRuntime);
        }
        private void InitializeCombobox()
        {
            comboBoxFirstClass.Items.Clear();
            comboBoxSecondClass.Items.Clear();
            comboBoxThirdClass.Items.Clear();
            comboBoxFirstClass.Text = null;
            comboBoxSecondClass.Text = null;
            comboBoxThirdClass.Text = null;
            comboBoxFirstClass.Items.Add(new ComboxItem("空间统计","SA"));
            comboBoxFirstClass.Items.Add(new ComboxItem("过程模拟","HM"));
        }
        //private void ChangeDownButton()
        //{
        //    if (bothNotDown)
        //    {
        //        buttonHydroDown.Enabled = false;
        //        buttonDownFiles.Enabled = false;
        //    }
        //    else if (downFlag)
        //    {
        //        buttonHydroDown.Enabled = true;
        //        buttonDownFiles.Enabled = false;
        //    }
        //    else
        //    {
        //        buttonDownFiles.Enabled = true;
        //        buttonHydroDown.Enabled = false;
        //    }
        //    buttonExecute.Enabled = false;
        //}
        public class ComboxItem
        {
            public string Text = "";
            public string Value = "";
            public ComboxItem(string _Text, string _Value)
            {
                Text = _Text;
                Value = _Value;
            }
            public override string ToString()
            {
                return Text;
            }
            public string ItemValue()
            {
                return Value;
            }

        }
        private void comboBoxFirstClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboxItem FirstFunc = (ComboxItem)comboBoxFirstClass.SelectedItem;
            //MessageBox.Show(FirstFunc.Value);
            comboBoxSecondClass.Items.Clear();
            comboBoxSecondClass.ResetText();
            comboBoxThirdClass.Items.Clear();
            comboBoxThirdClass.ResetText();
            switch (FirstFunc.Value)
            {
                case "SA":
                    comboBoxSecondClass.Items.Add(new ComboxItem("空间数据预处理","SpaDataPre"));
                    comboBoxSecondClass.Items.Add(new ComboxItem("空间相关性探索", "SpaCorrExp"));
                    comboBoxSecondClass.Items.Add(new ComboxItem("空间插值", "SpaInter"));
                    comboBoxSecondClass.Items.Add(new ComboxItem("面域总体估计", "SurfEst"));
                    comboBoxSecondClass.Items.Add(new ComboxItem("统计分类预测", "StatEst"));
                    comboBoxSecondClass.Items.Add(new ComboxItem("时空模式识别", "STPat"));
                    break;
                case "HM":
                    comboBoxThirdClass.Items.Clear();
                    comboBoxThirdClass.ResetText();
                    comboBoxSecondClass.Items.Add(new ComboxItem("冲沟浅沟系数", "gully_erosion"));
                    comboBoxSecondClass.Items.Add(new ComboxItem("参数率定", "wuhui_calibration"));
                    comboBoxSecondClass.Items.Add(new ComboxItem("长时段流域过程模拟", "long_seim"));
                    comboBoxSecondClass.Items.Add(new ComboxItem("次降雨流域过程模拟", "storm_seim"));
                    comboBoxSecondClass.Items.Add(new ComboxItem("情景分析", "OptBMPs"));
                    //comboBoxSecondClass.Items.Add(new ComboxItem("", ""));
                    break;
            }
            //ExecuteOrNot();
        }
        //private void comboBoxSecondClass_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (((ComboxItem)comboBoxFirstClass.SelectedItem).Value == "HM")
        //    {
        //        buttonExecute.Enabled = true;
        //    }
            
        //    comboBoxThirdClass.Items.Clear();
        //    comboBoxThirdClass.ResetText();
        //    ComboxItem SecondFunc = (ComboxItem)comboBoxSecondClass.SelectedItem;
        //    switch (SecondFunc.Value)
        //    {
        //        case "SpaDataPre":
        //            comboBoxThirdClass.Items.Add(new ComboxItem("正则化", "hpgcT_nml"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("分布转换", "log"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("离散化", "dis"));
        //            break;
        //        case "SpaCorrExp":
        //            comboBoxThirdClass.Items.Add(new ComboxItem("Moran's I", "hpgcT_moran"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("Geary's C", "gearyC"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("空间权重矩阵", "swm"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("变异函数拟合", "hpgcT_semivar"));
        //            break;
        //        case "SpaInter":
        //            comboBoxThirdClass.Items.Add(new ComboxItem("Kriging", "kriging"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("HASM", "hasm"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("TIN", "tin"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("移动平均", "hpgcT_movingaverage"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("反距离加权", "idw"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("随意样点插值", "solim"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("典型样点设计", "yanglin"));
        //            break;
        //        case "SurfEst":
        //            comboBoxThirdClass.Items.Add(new ComboxItem("简单随机抽样", "srs"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("多单元三明治", "sandwich"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("MSN", "msn"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("B-Shade", "bshade"));
        //            break;
        //        case "StatEst":
        //            comboBoxThirdClass.Items.Add(new ComboxItem("朴素贝叶斯", "naive"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("贝叶斯EM算法", "em"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("贝叶斯爬山算法", "hillclimb"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("Gibbs算法", "gibbs"));
        //            break;
        //        case "STPat":
        //            comboBoxThirdClass.Items.Add(new ComboxItem("Getis G", "getisG"));
        //            comboBoxThirdClass.Items.Add(new ComboxItem("时空扫描统计", "popu"));
        //            break;
        //    }
        //    ///ExecuteOrNot();
        //}
        //public void ExecuteOrNot()
        //{
        //    // To decide the Execute button is enable or not.
        //    int CoreNum = SelectedCoreNumFunc();
        //    if (CoreNum >= 0)
        //    {
        //        ComboxItem tempCI = AlgorithmSelected(ref downFlag);
        //        if (tempCI.Value != "")
        //        {
        //            buttonExecute.Enabled = true;
        //        }
        //        else
        //            buttonExecute.Enabled = false;
        //    }
        //    else
        //        buttonExecute.Enabled = false;
        //}
        public bool DownloadHTTPFile(string FileUrl, string saveFullName)
        {
            bool flagDown = false;
            System.Net.HttpWebRequest httpWebRequest = null;
            try
            {
                httpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FileUrl);
                System.Net.HttpWebResponse httpWebResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                System.IO.Stream sr = httpWebResponse.GetResponseStream();
                System.IO.Stream sw = new System.IO.FileStream(saveFullName, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = sr.Read(by, 0, (int)by.Length);
                while (osize>0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    sw.Write(by, 0, osize);
                    osize = sr.Read(by, 0, (int)by.Length);
                }
                System.Threading.Thread.Sleep(100);
                flagDown = true;
                sw.Close();
                sr.Close();
            }
            catch (System.Exception)
            {
            	if (httpWebRequest!=null)
            	{
                    httpWebRequest.Abort();
            	}
            }
            return flagDown;
        }

        public static int DownloadFtp(string filePath, string AlgoriName, string fileName, string ftpServerIP, string ftpUserID, string ftpPassword,ProgressBar ProcBar)
        {
            FtpWebRequest reqFTP;
            try
            {
                //filePath = < <The full path where the file is to be created.>>, 
                //fileName = < <Name of the file to be created(Need not be the name of the file on FTP server).>> 
                FileStream outputStream;
                
                //string subFolder = fileName.Split('.')[0].Split('_')[1];
                //if (!Directory.Exists(filePath + "\\" + subFolder + "Cores"))
                //{
                //    Directory.CreateDirectory(filePath + "\\" + subFolder + "Cores\\");
                //}
                if (WuhuiCalibration)
                {
                    string filePathCali = filePath + "\\demo";
                    if (!Directory.Exists(filePathCali + "\\" + fileName.Split('/')[1]))
                    {
                        Directory.CreateDirectory(filePathCali + "\\" + fileName.Split('/')[1]);
                        for (int f = 0; f < 9;f++ )
                        {
                            if (!Directory.Exists(filePathCali + "\\" + fileName.Split('/')[1] + "\\final_pop" + (f + 1).ToString()))
                            {
                                Directory.CreateDirectory(filePathCali + "\\" + fileName.Split('/')[1] + "\\final_pop" + (f + 1).ToString());
                            }
                        }
                        if (!Directory.Exists(filePathCali + "\\" + fileName.Split('/')[1] + "\\input"))
                        {
                            Directory.CreateDirectory(filePathCali + "\\" + fileName.Split('/')[1] + "\\input");
                        }
                        if (!Directory.Exists(filePathCali + "\\" + fileName.Split('/')[1] + "\\output"))
                        {
                            Directory.CreateDirectory(filePathCali + "\\" + fileName.Split('/')[1] + "\\output");
                        }
                    }
                }
                if (OptBMPs)
                {
                    string filePathCali = filePath + "\\demo";
                    if (!Directory.Exists(filePathCali + "\\" + fileName.Split('/')[1]))
                    {
                        Directory.CreateDirectory(filePathCali + "\\" + fileName.Split('/')[1]);
                        //for (int f = 0; f < 30; f++)
                        //{
                        //    if (!Directory.Exists(filePathCali + "\\" + fileName.Split('/')[1] + "\\final_pop" + (f + 1).ToString()))
                        //    {
                        //        Directory.CreateDirectory(filePathCali + "\\" + fileName.Split('/')[1] + "\\final_pop" + (f + 1).ToString());
                        //    }
                        //}
                        if (!Directory.Exists(filePathCali + "\\" + fileName.Split('/')[1] + "\\input"))
                        {
                            Directory.CreateDirectory(filePathCali + "\\" + fileName.Split('/')[1] + "\\input");
                        }
                        if (!Directory.Exists(filePathCali + "\\" + fileName.Split('/')[1] + "\\output"))
                        {
                            Directory.CreateDirectory(filePathCali + "\\" + fileName.Split('/')[1] + "\\output");
                        }
                    }
                }
                if (storm_seimLINUX || long_seimLINUX || Yanglin)
                {
                    if (!Directory.Exists(filePath+"\\"+fileName.Split('/')[0]))
                    {
                        Directory.CreateDirectory(filePath+"\\"+fileName.Split('/')[0]);
                    }
                }
                

                //try
                //{
                //    outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                //}
                //catch
                //{
                //    outputStream = new FileStream(filePath + "\\" + fileName.Split('/')[1], FileMode.Create);
                //}
                if (fileName.Split('/')[0] != "input" && !WuhuiCalibration && !OptBMPs)
                {
                    
                    if (storm_seimLINUX || long_seimLINUX || Yanglin)
                    {
                        reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + AlgoriName + "/" + fileName));
                    }
                    else
                        reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + AlgoriName + "/" + fileName.Split('/')[1]));
                }
                else
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + AlgoriName + "/" + fileName));
               
                
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.KeepAlive = false;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                
                if (!string.IsNullOrEmpty(ftpStream.ToString()))
                {
                    outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                    long cl = response.ContentLength;  // Get file length to determine the processbar's value
                    int bufferSize = 2048;
                    //int pbMax = (int)cl%bufferSize;
                    ProcBar.Value = 0;
                    ProcBar.Maximum = 100;
                    int readCount;
                    byte[] buffer = new byte[bufferSize];

                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        outputStream.Write(buffer, 0, readCount);
                        ProcBar.Value = (int)(100 * outputStream.Length / cl) >= 100 ? 100 : (int)(100 * outputStream.Length / cl);
                        readCount = ftpStream.Read(buffer, 0, bufferSize);
                        if (ProcBar.Value < 100)
                        {
                            ProcBar.Value++;
                        }

                        //ProcBar.Update();
                    }
                    //MessageBox.Show(pbMax.ToString() + "," + ProcBar.Value.ToString());
                    ProcBar.Value = 0;
                    ftpStream.Close();
                    outputStream.Close();
                    response.Close();
                }
                return 0;
            }
            catch (Exception ex)
            {
                // Logging.WriteError(ex.Message + ex.StackTrace);
                // System.Windows.Forms.MessageBox.Show(ex.Message);
                return -2;
            }
        }

#region All Algorithms (Spatial statistics and Hydrology modeling)

        public void RunCommonAlgorithm(string[] RunCores,ComboxItem tempCI)
        {
            ClearElementsBeforeExecute();
            //ChangeDownButton();
            int NUM = RunCores.Length;
            progressBarUtility.Style = ProgressBarStyle.Blocks;
            progressBarUtility.Maximum = NUM;
            progressBarUtility.Value = 0;
            progressBarUtility.Step = 1;
            string shellName;
            string state = "";
            labelProgramState.Text = "";
            shellName = tempCI.Value;
            state = tempCI.ToString();

            int[] Cores = new int[NUM];
            double[] CompTime = new double[NUM];
            double[] IOTime = new double[NUM];
            double[] TotalTime = new double[NUM];
            //MessageBox.Show(NUM.ToString());
            string[,] SubProcNameStorm = { { "ITP_P", "ITP" }, { "PI_STORM", "PI" }, { "SUR_SGA","SUR"}, {"DEP_FS","DEP" }, {"PERCO_DARCY","PERCO" }, { "INTERFLOW_IKW","INTERFLOW"}, 
                                    { "IKW_OL","IKW_OL"}, { "GWATER_RESERVOIR","GWATER"}, {"IKW_CH","IKW_CH" },{"SPLASHERO_PARK","SPLASH"},{"KINWAVSED_OL","ERO_OL"},{"KINWAVSED_CH","ERO_CH"}};
            string[] StormStr2TXT = new string[SubProcNameStorm.Length / 2 +1];
            string[,] SubProcNameLong = { { "PET_H", "PET" }, { "ITP_P", "ITP" }, { "PI_MSM","PI"}, {"SNO_DD","SNO" }, {"STP_FP","STP" }, { "SUR_MR","SUR"}, { "DEP_LINSLEY","DEP"}, { "PER_STR","PERCO"}, {"SSR_DA","SSR" },
                                    {"SET_LM","SET"},{"GWA_RE","GWA"},{"MUSLE_AS","MUSLE"},{"ATMDEP","ATMDEP"},{"MINDEC","MINDEC"},{"NITVOL","NITVOL"},{"DENITRI","DENITRI"},{"SORPHO","SORPHO"},{"NITFIX","NITFIX"},
                                    {"POTENTIALBIOMASS","POTBI"},{"UPTAKEGROWTH","UPTAKE"},{"YIELD","YIELD"},{"IUH_OL","IUHOL"},{"MUSK_CH","MUSKCH"}};
            string[] LongStr2TXT = new string[SubProcNameLong.Length / 2+1];
            double[,] SubProcTimeLong = new double[NUM, SubProcNameLong.Length / 2];
            double[] tempTimeLong = new double[SubProcNameLong.Length / 2];
            double[,] SubProcTimeStorm = new double[NUM, SubProcNameStorm.Length / 2];
            double[] tempTimeStorm = new double[SubProcNameStorm.Length / 2];
            LongStr2TXT[0] = "Cores Cores ";
            StormStr2TXT[0] = "Cores Cores ";
            for (int j = 0; j < SubProcNameLong.Length / 2; j++)
                LongStr2TXT[j + 1] = SubProcNameLong[j, 0] + " " + SubProcNameLong[j, 1] + " ";
            for (int j = 0; j < SubProcNameStorm.Length / 2; j++)
                StormStr2TXT[j + 1] = SubProcNameStorm[j, 0] + " " + SubProcNameStorm[j, 1] + " ";
            for (int i = 0; i < NUM; i++)
            {
                LongStr2TXT[0] = LongStr2TXT[0] + RunCores[i].ToString() + " ";
                StormStr2TXT[0] = StormStr2TXT[0] + RunCores[i].ToString() + " ";
                progressBarUtility.Value++;
                progressBarUtility.Refresh();
                progressBarUtility.Update();
                labelProgramState.Text = "正在计算" + state + "，计算核心数为：" + RunCores[i].ToString() + "个！";
                labelProgramState.Update();
                Cores[i] = Convert.ToInt16(RunCores[i].ToString().Trim());
                string resultStr = GetRunResult(shellName, RunCores[i]);
                double[] tempValues = ExtractTimeValue(resultStr);
                string[] OutFiles = OutputFiles(resultStr);
                string[] InFiles = InputFiles(resultStr);
                if (InFiles != null)
                {
                    buttonDownFiles.Enabled = true;
                    if (checkedListBoxInputFiles.Items.Count == 0 || WuhuiCalibration ||OptBMPs)
                    {
                        for (int j = 0; j < InFiles.Length; j++)
                            checkedListBoxInputFiles.Items.Add(InFiles[j]);
                        //tabControlResultShow.SelectTab(tabPageFilesInput);
                    }
                }
                if (OutFiles != null)
                {
                    buttonDownFiles.Enabled = true;
                    for (int j = 0; j < OutFiles.Length; j++)
                        checkedListBoxOutputFiles.Items.Add(OutFiles[j]);
                    tabControlResultShow.SelectTab(tabPageFilesResults);
                }
                if (shellName == "hpgcT_moran" || shellName == "hpgcT_moran2" || shellName == "gearyC" || shellName == "gearyC2" || shellName == "getisG" || shellName == "getisG2")
                {
                    double[] MoranResult = new double[1];
                    MoranResult = ExtractResultsValues(resultStr, 1);
                    textBoxAlgorithmResults.Text += RunCores[i].ToString() + " Cores :" + Environment.NewLine + "    " + state + " : " + MoranResult[0].ToString() + Environment.NewLine;
                    textBoxAlgorithmResults.Update();
                    tabControlResultShow.SelectTab(tabPageNumericResults);
                    buttonDownFiles.Enabled = true;
                }
                else if (shellName == "msn" || shellName == "bshade")
                {
                    Regex ExtractValues = new Regex(@"\:\s*[+-]?\d+\.\d*(e?)([+-]?)(\d+)", RegexOptions.Singleline);
                    string[] test = new string[ExtractValues.Matches(resultStr).Count];
                    for (int j = 0; j < test.Length; j++)
                    {
                        test[j] = ExtractValues.Matches(resultStr)[j].ToString().Trim();
                    }
                    textBoxAlgorithmResults.Text += RunCores[i].ToString() + " Cores :" + Environment.NewLine;
                    textBoxAlgorithmResults.Text += "总体均值" + test[0] + Environment.NewLine;
                    textBoxAlgorithmResults.Text += "均值估计方差" + test[1] + Environment.NewLine;
                    textBoxAlgorithmResults.Update();
                    tabControlResultShow.SelectTab(tabPageNumericResults);
                }
                else if (shellName == "hpgcT_semivar")
                {
                    textBoxAlgorithmResults.Text += RunCores[i].ToString() + " Cores :" + Environment.NewLine;
                    double[] semivarResult = new double[3];
                    semivarResult = ExtractResultsValues(resultStr, 3);
                    string[] ResultVariables = {"块金值","拱高  ","变程  "};
                    for (int Rlt = 0; Rlt < 3;Rlt++ )
                    {
                        textBoxAlgorithmResults.Text += "    " + ResultVariables[Rlt] + " : " + semivarResult[Rlt].ToString() + Environment.NewLine;
                        textBoxAlgorithmResults.Update();
                    }
                    tabControlResultShow.SelectTab(tabPageNumericResults);
                    buttonDownFiles.Enabled = true;
                }
                else if (shellName == "naive")
                {
                    textBoxAlgorithmResults.Text += RunCores[i].ToString() + " Cores :" + Environment.NewLine;
                    string PD;
                    Regex ExtractPD = new Regex("\\[\\bP\\w{1}\\](.*)$", RegexOptions.Multiline);
                    Regex ExtractPD2 = new Regex(@"[+-]?\d+\.?\d*(e?)([+-]?)(\d+)$", RegexOptions.Multiline);
                    PD = ExtractPD2.Matches(ExtractPD.Matches(resultStr)[0].ToString())[0].ToString();
                    textBoxAlgorithmResults.Text += "    PD值 : " + PD + Environment.NewLine;
                    textBoxAlgorithmResults.Update();
                    tabControlResultShow.SelectTab(tabPageNumericResults);
                    buttonDownFiles.Enabled = true;
                }
                //else if (shellName == "popu")
                //{
                //    string tempFileName = "result_316_" + RunCores[i].ToString() + ".txt";
                //    DownloadFtp("F:\\863Results\\tempFiles", "popu", tempFileName, "192.168.6.55/Zhulj/", "test", "test", this.progressBarUtility);
                //    StreamReader sr = new StreamReader("F:\\863Results\\tempFiles\\" + tempFileName, Encoding.Default);
                //    string line;
                //    textBoxAlgorithmResults.Text += RunCores[i].ToString() + " Cores :" + Environment.NewLine;
                //    while ((line = sr.ReadLine()) != null)
                //    {
                //        textBoxAlgorithmResults.Text += line + Environment.NewLine;
                //    }
                //    textBoxAlgorithmResults.Update();
                //    tabControlResultShow.SelectTab(tabPageNumericResults);
                //    sr.Close();
                //}
                else if (shellName == "srs")
                {
                    Regex ExtractValues = new Regex(@"\:\s*[+-]?\d+\.\d*(e?)([+-]?)(\d+)", RegexOptions.Singleline);
                    string[] test = new string[ExtractValues.Matches(resultStr).Count];
                    for (int j = 0; j < test.Length; j++)
                    {
                        test[j] = ExtractValues.Matches(resultStr)[j].ToString().Trim();
                    }
                    textBoxAlgorithmResults.Text += RunCores[i].ToString() + " Cores :" + Environment.NewLine;
                    textBoxAlgorithmResults.Text += "        随机抽样模型统计推断" + Environment.NewLine;
                    textBoxAlgorithmResults.Text += "总体均值估计" + test[0] + Environment.NewLine;
                    textBoxAlgorithmResults.Text += "均值估计方差" + test[1] + Environment.NewLine;
                    textBoxAlgorithmResults.Update();
                    tabControlResultShow.SelectTab(tabPageNumericResults);
                }
                IOTime[i] = tempValues[0];
                CompTime[i] = tempValues[1];
                TotalTime[i] = tempValues[2];
                if (long_seimWIN)
                {
                    tempTimeLong = ExtractLongSEIMSubProcTime(resultStr);
                    for (int j = 0; j < SubProcNameLong.Length / 2; j++)
                    {
                        //SubProcTimeLong[i, j] = tempTimeLong[j];
                        //LongStr2TXT[j + 1] = SubProcNameLong[j, 0] + " " + SubProcNameLong[j, 1] + " ";
                        LongStr2TXT[j + 1] = LongStr2TXT[j + 1] + tempTimeLong[j] + " ";
                    }
                }
                if (storm_seimWIN)
                {
                    tempTimeStorm = ExtractStormSEIMSubProcTime(resultStr);
                    for (int j = 0; j < SubProcNameStorm.Length / 2; j++)
                    {
                        //SubProcTimeStorm[i, j] = tempTimeStorm[j];
                        //StormStr2TXT[j + 1] = SubProcNameStorm[j, 0] + " " + SubProcNameStorm[j, 1] + " ";
                        StormStr2TXT[j + 1] = StormStr2TXT[j + 1] + tempTimeStorm[j] + " ";
                    }
                }
            }
            string RunTimeFile = "";
            if (long_seimWIN)
            {
                File.WriteAllLines(@"F:\863ResultsDataNew\WatershedModeling\SubprocessTest\Daily\RunTimeTemp.txt", LongStr2TXT);
                RunTimeFile = @"F:\863ResultsDataNew\WatershedModeling\SubprocessTest\Daily" + "\\RunTimeTemp.txt";
            }
            if (storm_seimWIN)
            {
                File.WriteAllLines(@"F:\863ResultsDataNew\WatershedModeling\SubprocessTest\Storm\RunTimeTemp.txt", StormStr2TXT);
                RunTimeFile = @"F:\863ResultsDataNew\WatershedModeling\SubprocessTest\Storm" + "\\RunTimeTemp.txt"; 
            }
            DrawTimeLines(Cores, CompTime, IOTime, TotalTime);
            if (long_seimWIN || storm_seimWIN)
            {
                if (childSubProc == null || childSubProc.IsDisposed)
                {
                    childSubProc = new TestDemo.SubProc();
                    childSubProc.RunTimeFile = RunTimeFile;
                    childSubProc.Show();
                }
                else
                {
                    childSubProc.Close();
                    childSubProc = new TestDemo.SubProc();
                    childSubProc.RunTimeFile = RunTimeFile;
                    childSubProc.Show();
                }
            }
            
            labelProgramState.Text = state + "计算完毕！";
            progressBarUtility.Value = 0;
        }

#endregion


        //private void buttonExecute_Click(object sender, EventArgs e)
        //{
        //    ClearElementsBeforeExecute();
        //    string[] RunCores = SelectedStrs(checkedListBoxCores);
        //    ComboxItem tempCI = AlgorithmSelected(ref downFlag);
        //    if (tempCI.Value == "hpgcT_moran" || tempCI.Value == "gearyC" || tempCI.Value == "getisG" || tempCI.Value == "hpgcT_semivar" || tempCI.Value == "naive" || tempCI.Value == "srs" || tempCI.Value == "popu")
        //    {
        //        bothNotDown = true;
        //    }
            
        //    if (RunCores != null && tempCI.Value != "")
        //    {
        //        RunCommonAlgorithm(RunCores, tempCI);

        //        InitializeCombobox();
        //        //ChangeDownButton();
        //    }
        //    else
        //        MessageBox.Show("请正确选择算法及计算核心数后重试!");
        //}

        //public ComboxItem AlgorithmSelected(ref bool downFlag)
        //{
        //    string shellName, state;
        //    //bool downFlag = false;
        //    if ((comboBoxFirstClass.Text == "空间统计") && (comboBoxThirdClass.Text != ""))
        //    {
        //        shellName = ((ComboxItem)comboBoxThirdClass.SelectedItem).Value;
        //        state = comboBoxThirdClass.SelectedItem.ToString();
        //        downFlag = false;
        //    }
        //    else if ((comboBoxFirstClass.Text == "过程模拟") && (comboBoxSecondClass.Text != ""))
        //    {
        //        shellName = ((ComboxItem)comboBoxSecondClass.SelectedItem).Value;
        //        state = comboBoxSecondClass.SelectedItem.ToString();
        //        downFlag = true;
        //    }
        //    else
        //    {
        //        shellName = "";
        //        state = "";
        //        //MessageBox.Show("请正确选择算法后点击执行！");
        //    }
            
        //    ComboxItem CI = new ComboxItem(state,shellName);
        //    return CI;
        //}
        public int SelectedCoreNumFunc()
        {
            string[] CoreItems = SelectedStrs(checkedListBoxCores);
            if (CoreItems == null)
            {
                return 0;
            }
            else
                return CoreItems.Length;
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

        private void buttonSpaAnDown_Click(object sender, EventArgs e)
        {
            //bool InputOrNot = false;
            progressBarUtility.Value = 0;
            string[] OutFilesPaths = SelectedStrs(checkedListBoxOutputFiles);
            string[] InFilesPaths = SelectedStrs(checkedListBoxInputFiles);
            if (OutFilesPaths == null && InFilesPaths == null)
            {
                MessageBox.Show("请先选择需要下载的文件！");
            }
            else
            {
                DownloadOrNot = true;
                string[] AllDownFilesPaths = null;
                string AlgoriName = "";
                if (OutFilesPaths == null && InFilesPaths != null && !(WuhuiCalibration || storm_seimLINUX || long_seimWIN || storm_seimWIN || long_seimLINUX || OptBMPs))
                {
                    downInputs = true;
                    AllDownFilesPaths = new string[InFilesPaths.Length];
                    int NameIndex = InFilesPaths[0].Split('/').Length - 1;
                    AlgoriName = InFilesPaths[0].Split('/')[NameIndex - 2];
                    for (int k = 0; k < InFilesPaths.Length; k++)
                    {
                        AllDownFilesPaths[k] = "input/" + InFilesPaths[k].Split('/')[NameIndex];
                    }

                }
                else if (OutFilesPaths != null && InFilesPaths == null)
                {
                    if (WuhuiCalibration || storm_seimLINUX || long_seimWIN || storm_seimWIN || long_seimLINUX || OptBMPs)
                    {
                        MessageBox.Show("请同时选择输入文件！");
                        DownloadOrNot = false;
                        //downInputs = false;
                        //AllDownFilesPaths = new string[OutFilesPaths.Length];
                        //int NameIndex = OutFilesPaths[0].Split('/').Length - 1;
                        //AlgoriName = "wuhui_calibration";
                        //for (int k = 0; k < OutFilesPaths.Length; k++)
                        //{
                        //    AllDownFilesPaths[k] = OutFilesPaths[k].Split('/')[NameIndex - 1] + "/" + OutFilesPaths[k].Split('/')[NameIndex];
                        //}
                    }
                    else if(Yanglin)
                    {
                        DownloadOrNot = true;
                        downInputs = false;
                        AllDownFilesPaths = new string[OutFilesPaths.Length];
                        int NameIndex = OutFilesPaths[0].Split('/').Length - 1;
                        AlgoriName = OutFilesPaths[0].Split('/')[NameIndex - 2];
                        for (int k = 0; k < OutFilesPaths.Length; k++)
                        {
                            AllDownFilesPaths[k] = OutFilesPaths[k].Split('/')[NameIndex - 1] + "/" + OutFilesPaths[k].Split('/')[NameIndex];
                        }
                    }
                    else
                    {
                        DownloadOrNot = true;
                        downInputs = false;
                        AllDownFilesPaths = new string[OutFilesPaths.Length];
                        int NameIndex = OutFilesPaths[0].Split('/').Length - 1;
                        AlgoriName = OutFilesPaths[0].Split('/')[NameIndex - 1];
                        for (int k = 0; k < OutFilesPaths.Length; k++)
                        {
                            int tempIdx = OutFilesPaths[k].Split('/')[NameIndex].Split('.')[0].Split('_').Length - 1;
                            AllDownFilesPaths[k] = OutFilesPaths[k].Split('/')[NameIndex].Split('.')[0].Split('_')[tempIdx] + "Cores/" + OutFilesPaths[k].Split('/')[NameIndex];
                        }
                    }

                }
                else if (OutFilesPaths != null && InFilesPaths != null)
                {
                    DownloadOrNot = true;
                    downInputs = true;
                    if (WuhuiCalibration)
                    {
                        downInputs = true;
                        string[] AllDownFilesPathsIn = new string[InFilesPaths.Length];
                        int NameIndex2 = InFilesPaths[0].Split('/').Length - 1;
                        for (int k = 0; k < InFilesPaths.Length; k++)
                        {
                            AllDownFilesPathsIn[k] = "Demo/" + InFilesPaths[k].Split('/')[NameIndex2 - 2] + "/" + InFilesPaths[k].Split('/')[NameIndex2 - 1] + "/" + InFilesPaths[k].Split('/')[NameIndex2];
                        }
                        string[] AllDownFilesPathsOut = new string[OutFilesPaths.Length];
                        int NameIndex = OutFilesPaths[0].Split('/').Length - 1;
                        AlgoriName = "wuhui_calibration";
                        for (int k = 0; k < OutFilesPaths.Length; k++)
                        {
                            AllDownFilesPathsOut[k] = "Demo/" + OutFilesPaths[k].Split('/')[NameIndex2 - 2] + "/" + OutFilesPaths[k].Split('/')[NameIndex2 - 1] + "/" + OutFilesPaths[k].Split('/')[NameIndex2]; 
                        }
                        
                        string[] AllDownFilesPathsQandSED = new string[9 * 3 * CORE.Length];
                        for (int core = 0; core < CORE.Length;core++ )
                        {
                            for (int proc = 1; proc < 10; proc++)
                            {
                                AllDownFilesPathsQandSED[(proc - 1) * 3 + core * 3 * 9 + 0] = "Demo/core" + CORE[core].ToString() + "/final_pop" + proc.ToString() + "/1_Q_OUTLET.txt";
                                AllDownFilesPathsQandSED[(proc - 1) * 3 + core * 3 * 9 + 1] = "Demo/core" + CORE[core].ToString() + "/final_pop" + proc.ToString() + "/1_SED_OUTLET.txt";
                                AllDownFilesPathsQandSED[(proc - 1) * 3 + core * 3 * 9 + 2] = "Demo/core" + CORE[core].ToString() + "/final_pop" + proc.ToString() + "/1_SEDTOCH_T.txt";
                            }
                        }
                        
                        string[] tempAllDownFilesPaths = AllDownFilesPathsIn.Concat(AllDownFilesPathsOut).ToArray();
                        AllDownFilesPaths = tempAllDownFilesPaths.Concat(AllDownFilesPathsQandSED).ToArray();
                    }
                    else if (OptBMPs)
                    {
                        downInputs = true;
                        string[] AllDownFilesPathsIn = new string[InFilesPaths.Length];
                        int NameIndex2 = InFilesPaths[0].Split('/').Length - 1;
                        for (int k = 0; k < InFilesPaths.Length; k++)
                        {
                            AllDownFilesPathsIn[k] = "Demo/" + InFilesPaths[k].Split('/')[NameIndex2 - 2] + "/" + InFilesPaths[k].Split('/')[NameIndex2 - 1] + "/" + InFilesPaths[k].Split('/')[NameIndex2];
                        }
                        string[] AllDownFilesPathsOut = new string[OutFilesPaths.Length];
                        int NameIndex = OutFilesPaths[0].Split('/').Length - 1;
                        AlgoriName = "OptBMPs";
                        for (int k = 0; k < OutFilesPaths.Length; k++)
                        {
                            AllDownFilesPathsOut[k] = "Demo/" + OutFilesPaths[k].Split('/')[NameIndex2 - 2] + "/" + OutFilesPaths[k].Split('/')[NameIndex2 - 1] + "/" + OutFilesPaths[k].Split('/')[NameIndex2];
                        }

                        //string[] AllDownFilesPathsQandSED = new string[9 * 3 * CORE.Length];
                        //for (int core = 0; core < CORE.Length; core++)
                        //{
                        //    for (int proc = 1; proc < 10; proc++)
                        //    {
                        //        AllDownFilesPathsQandSED[(proc - 1) * 3 + core * 3 * 9 + 0] = "Demo/core" + CORE[core].ToString() + "/final_pop" + proc.ToString() + "/1_Q_OUTLET.txt";
                        //        AllDownFilesPathsQandSED[(proc - 1) * 3 + core * 3 * 9 + 1] = "Demo/core" + CORE[core].ToString() + "/final_pop" + proc.ToString() + "/1_SED_OUTLET.txt";
                        //        AllDownFilesPathsQandSED[(proc - 1) * 3 + core * 3 * 9 + 2] = "Demo/core" + CORE[core].ToString() + "/final_pop" + proc.ToString() + "/1_SEDTOCH_T.txt";
                        //    }
                        //}

                        AllDownFilesPaths = AllDownFilesPathsIn.Concat(AllDownFilesPathsOut).ToArray();
                        //AllDownFilesPaths = tempAllDownFilesPaths.Concat(AllDownFilesPathsQandSED).ToArray();
                    }
                    else if (Yanglin)
                    {
                        string[] AllDownFilesPathsIn = new string[InFilesPaths.Length];
                        int NameIndex2 = InFilesPaths[0].Split('/').Length - 1;
                        for (int k = 0; k < InFilesPaths.Length; k++)
                        {
                            AllDownFilesPathsIn[k] = "input/"+ InFilesPaths[k].Split('/')[NameIndex2];
                        }
                        AllDownFilesPaths = new string[OutFilesPaths.Length];
                        int NameIndex = OutFilesPaths[0].Split('/').Length - 1;
                        AlgoriName = OutFilesPaths[0].Split('/')[NameIndex - 2];
                        for (int k = 0; k < OutFilesPaths.Length; k++)
                        {
                            AllDownFilesPaths[k] = OutFilesPaths[k].Split('/')[NameIndex - 1] + "/" + OutFilesPaths[k].Split('/')[NameIndex];
                        }
                        AllDownFilesPaths = AllDownFilesPaths.Concat(AllDownFilesPathsIn).ToArray();
                    }
                    else if (long_seimWIN || storm_seimWIN)
                    {
                        string[] AllDownFilesPathsIn = new string[InFilesPaths.Length];
                        int NameIndex2 = InFilesPaths[0].Split('\\').Length - 1;
                        for (int k = 0; k < InFilesPaths.Length; k++)
                        {
                            AllDownFilesPathsIn[k] = InFilesPaths[k].Split('\\')[NameIndex2 - 1] + "\\" + InFilesPaths[k].Split('\\')[NameIndex2];
                        }
                        string[] AllDownFilesPathsOut = new string[OutFilesPaths.Length];
                        int NameIndex = OutFilesPaths[0].Split('\\').Length - 1;
                        for (int k = 0; k < OutFilesPaths.Length; k++)
                        {
                            AllDownFilesPathsOut[k] = OutFilesPaths[k].Split('\\')[NameIndex - 1] + "\\" + OutFilesPaths[k].Split('\\')[NameIndex];
                        }

                        AllDownFilesPaths = AllDownFilesPathsIn.Concat(AllDownFilesPathsOut).ToArray();
                    }
                    else
                    {
                        string[] AllDownFilesPathsOut = new string[OutFilesPaths.Length];
                        int NameIndex1 = OutFilesPaths[0].Split('/').Length - 1;
                        if (storm_seimLINUX )
                        {
                            AlgoriName = "storm_seim";
                            for (int k = 0; k < OutFilesPaths.Length; k++)
                            {
                                AllDownFilesPathsOut[k] = "results/" + OutFilesPaths[k].Split('/')[NameIndex1];
                            }
                        }
                        else if (long_seimLINUX)
                        {
                            AlgoriName = "long_seim";
                            for (int k = 0; k < OutFilesPaths.Length; k++)
                            {
                                AllDownFilesPathsOut[k] = "results/" + OutFilesPaths[k].Split('/')[NameIndex1];
                            }
                        }
                        else
                        {
                            AlgoriName = OutFilesPaths[0].Split('/')[NameIndex1 - 1];
                            for (int k = 0; k < OutFilesPaths.Length; k++)
                            {
                                //string s = OutFilesPaths[k].Split('/')[NameIndex1];
                                //AllDownFilesPathsOut[k] = OutFilesPaths[k].Split('/')[NameIndex1];
                                int tempIdx = OutFilesPaths[k].Split('/')[NameIndex1].Split('.')[0].Split('_').Length - 1;
                                AllDownFilesPathsOut[k] = OutFilesPaths[k].Split('/')[NameIndex1].Split('.')[0].Split('_')[tempIdx] + "Cores/" + OutFilesPaths[k].Split('/')[NameIndex1];
                            }
                        }
                        string[] AllDownFilesPathsIn = new string[InFilesPaths.Length];
                        int NameIndex2 = InFilesPaths[0].Split('/').Length - 1;
                        //AlgoriName = InFilesPaths[0].Split('/')[NameIndex2 - 1];
                        for (int k = 0; k < InFilesPaths.Length; k++)
                        {
                            AllDownFilesPathsIn[k] = "input/" + InFilesPaths[k].Split('/')[NameIndex2];
                        }
                        AllDownFilesPaths = AllDownFilesPathsIn.Concat(AllDownFilesPathsOut).ToArray();
                    }
                }
                if (DownloadOrNot && downFlag)  // Download HydroModeling Files
                {
                    folderBrowserDialogHydroDown.SelectedPath = "F:\\863Results";
                    //folderBrowserDialogHydroDown.ShowDialog();
                    string saveFileFolder;

                    //string[] AllDownFilesPathsIn = new string[InFilesPaths.Length];
                    //int NameIndex2 = InFilesPaths[0].Split('\\').Length - 1;
                    //for (int k = 0; k < InFilesPaths.Length; k++)
                    //{
                    //    AllDownFilesPathsIn[k] = InFilesPaths[k].Split('\\')[NameIndex2-1]+"\\" + InFilesPaths[k].Split('\\')[NameIndex2];
                    //}
                    //string[] AllDownFilesPathsOut = new string[OutFilesPaths.Length];
                    //int NameIndex = OutFilesPaths[0].Split('\\').Length - 1;
                    //for (int k = 0; k < OutFilesPaths.Length; k++)
                    //{
                    //    AllDownFilesPathsOut[k] = OutFilesPaths[k].Split('\\')[NameIndex - 1] + "\\" + OutFilesPaths[k].Split('\\')[NameIndex];
                    //}

                    //string[] HydroFiles = AllDownFilesPathsIn.Concat(AllDownFilesPathsOut).ToArray();
                    //string[] HydroFiles = {"1_D_INFIL_1.asc","1_D_NEPR_1.asc","1_D_P_1.asc","1_D_SOMO_1.asc","1_D_SURU_1.asc","1_Q0.txt","1_Q1.txt","1_Q2.txt","1_Q3.txt",
                    //                  "1_Q4.txt","1_Q5.txt","1_Q6.txt","1_Q_OUTLET.txt","1_SBOF.txt","web.config" };
                    if (folderBrowserDialogHydroDown.ShowDialog() == DialogResult.OK)
                    {
                        labelProgramState.Text = "正在下载流域模拟结果数据……";
                        saveFileFolder = folderBrowserDialogHydroDown.SelectedPath;
                        progressBarUtility.Show();
                        progressBarUtility.Maximum = AllDownFilesPaths.Length;
                        progressBarUtility.Value = 0;
                        for (int i = 0; i < AllDownFilesPaths.Length; i++)
                        {
                            string saveFilePath = saveFileFolder + "\\" + AllDownFilesPaths[i];
                            if (!Directory.Exists(saveFileFolder + "\\" + AllDownFilesPaths[i].Split('\\')[0]))
                            {
                                // Create the directory it does not exist.
                                Directory.CreateDirectory(saveFileFolder + "\\" + AllDownFilesPaths[i].Split('\\')[0]);
                            }
                            string httpUrl = null;
                            if (storm_seimWIN)
                            {
                                httpUrl = "http://192.168.6.56/hydroInfo/seim_storm/" + AllDownFilesPaths[i];
                            }
                            if (long_seimWIN)
                            {
                                httpUrl = "http://192.168.6.56/hydroInfo/seim_longterm/" + AllDownFilesPaths[i];
                            }

                            //string httpUrl = "http://192.168.6.56/hydroInfo/" + AllDownFilesPaths[i];
                            bool flag = DownloadHTTPFile(httpUrl, saveFilePath);
                            if (flag)
                                progressBarUtility.Value += 1;
                        }
                        labelProgramState.Text = "数据下载完成！";//请前往" + saveFileFolder + "查看！";
                        labelProgramState.Update();
                        //buttonShowMapViewer.Enabled = true;
                        for (int k = 0; k < CORE.Length; k++)
                        {
                            string[] files = new string[4];
                            files[0] = saveFileFolder + "\\input\\preci.txt";
                            files[1] = saveFileFolder + "\\input\\q_true.txt";
                            files[2] = saveFileFolder + "\\output_" + CORE[k].ToString() + "\\1_Q.txt";
                            files[3] = CORE[k].ToString() + " Cores : Discharge / m3/s";
                            //if (childHydro == null || childHydro.IsDisposed)
                            //{
                            childHydro = new TestDemo.Hydrograph();
                            childHydro.Files = files;
                            childHydro.Show();
                            //}
                            //else
                            //    child3.Activate();
                        }
                        progressBarUtility.Value = 0;
                        progressBarUtility.Update();
                    }
                }
                #region
                //saveFileDialogHydroDownload.InitialDirectory = "F:\\863Results";
                //string saveFilePath;
                //if (saveFileDialogHydroDownload.ShowDialog()==DialogResult.OK)
                //{
                //    saveFilePath = saveFileDialogHydroDownload.FileName;

                    //    bool flag = DownloadHTTPFile("http://192.168.6.56/hydroInfo/1_D_INFIL_1.asc", saveFilePath);
                //    MessageBox.Show(flag.ToString());
                //}
                #endregion

                else if (DownloadOrNot)
                {
                    folderBrowserDialogSpaAn.SelectedPath = "F:\\863Results";
                    if (folderBrowserDialogSpaAn.ShowDialog() == DialogResult.OK)
                    {
                        if (!(WuhuiCalibration || OptBMPs ||long_seimLINUX || storm_seimLINUX || Yanglin))
                        {
                            foreach (string sub in CORE)
                            {
                                if (!Directory.Exists(folderBrowserDialogSpaAn.SelectedPath + "\\" + sub + "Cores"))
                                {
                                    // Create the directory it does not exist.
                                    Directory.CreateDirectory(folderBrowserDialogSpaAn.SelectedPath + "\\" + sub + "Cores");
                                }
                            }
                        }
                        if (downInputs && !WuhuiCalibration && !OptBMPs)
                        {
                            if (!Directory.Exists(folderBrowserDialogSpaAn.SelectedPath + "\\input"))
                            {
                                // Create the directory it does not exist.
                                Directory.CreateDirectory(folderBrowserDialogSpaAn.SelectedPath + "\\input");
                            }
                        }
                        //if (WuhuiCalibration)
                        //{
                        //    if (!Directory.Exists(folderBrowserDialogSpaAn.SelectedPath + "\\output32"))
                        //    {
                        //        Directory.CreateDirectory(folderBrowserDialogSpaAn.SelectedPath + "\\output32");
                        //    }
                        //    for (int proc = 0; proc < 32; proc++)
                        //    {
                        //        if (!Directory.Exists(folderBrowserDialogSpaAn.SelectedPath + "\\process" + proc.ToString()))
                        //        {
                        //            Directory.CreateDirectory(folderBrowserDialogSpaAn.SelectedPath + "\\process" + proc.ToString());
                        //        }
                        //    }
                        //}
                        int FileCount = AllDownFilesPaths.Length;
                        //progressBarUtility.Maximum = FileCount;
                        //string[] FileNames = new string[FileCount];
                        //string[] AlgoriName = new string[FileCount];
                        for (int i = 0; i < FileCount; i++)
                        {
                            //progressBarUtility.Value++;
                           // progressBarUtility.Refresh();
                            //progressBarUtility.Update();
                            //int NameIndex = AllDownFilesPaths[i].Split('/').Length - 1;
                            //FileNames[i] = AllDownFilesPaths[i].Split('/')[NameIndex];
                            //AlgoriName[i] = AllDownFilesPaths[i].Split('/')[NameIndex - 1];
                            labelProgramState.Text = "正在下载空间分析数据 " + AllDownFilesPaths[i];
                            labelProgramState.Update();
                            string[] suffix = { ".shp", ".shx", ".sbx", ".sbn", ".prj", ".dbf" };
                            if (AllDownFilesPaths[i].Split('.')[AllDownFilesPaths[i].Split('.').Length - 1] == "shp")
                            {
                                for (int j = 0; j < suffix.Length; j++)
                                {
                                    //string tempName = AllDownFilesPaths[i].Split('.')[0] + suffix[j];
                                    string tempName = AllDownFilesPaths[i].Replace(".shp", suffix[j]);
                                    DownloadFtp(folderBrowserDialogSpaAn.SelectedPath, AlgoriName, tempName, "192.168.6.55/Zhulj/", "test", "test", this.progressBarUtility);
                                }
                            }
                            else
                            {

                                DownloadFtp(folderBrowserDialogSpaAn.SelectedPath, AlgoriName, AllDownFilesPaths[i], "192.168.6.55/Zhulj/", "test", "test",this.progressBarUtility);
                            }
                        }
                        labelProgramState.Text = "下载完成！";//请到目录：" + folderBrowserDialogSpaAn.SelectedPath + "下查看结果！";
                        labelProgramState.Update();
                        //System.Diagnostics.Process.Start("explorer.exe", folderBrowserDialogSpaAn.SelectedPath);
                        if (WuhuiCalibration)
                        {
                            string resultpath = folderBrowserDialogSpaAn.SelectedPath + "\\demo";
                            for (int core = 0; core < CORE.Length; core++)
                            {
                                if (Directory.Exists(resultpath + "\\core" + CORE[core].ToString()))
                                {
                                    child3 = new TestDemo.FormWuhui_calibration();
                                    string[] paramcali = { CORE[core].ToString(), resultpath + "\\core" + CORE[core].ToString() };
                                    child3.ParamCaliOutputProperty = paramcali;
                                    child3.Show();
                                }
                            }
                        }
                        if (storm_seimLINUX)
                        {
                            for (int i = 0; i < CORE.Length; i++)
                            {
                                
                                //if (childHydro == null || childHydro.IsDisposed)
                                //{
                                childHydro = new TestDemo.Hydrograph();
                                string[] files = new string[4];
                                files[0] = folderBrowserDialogSpaAn.SelectedPath + "\\input\\prec.txt";
                                files[1] = folderBrowserDialogSpaAn.SelectedPath + "\\input\\q_true.txt";
                                CorrectTime(folderBrowserDialogSpaAn.SelectedPath + "\\results\\q_1t_" + CORE[i] + "p");
                                files[2] = folderBrowserDialogSpaAn.SelectedPath + "\\results\\q_1t_" + CORE[i]+"pNew";
                                files[3] = CORE[i].ToString() + " Cores : Discharge / m3/s";
                                childHydro.Files = files;
                                childHydro.Show();
                                //}
                                //else
                                //    childHydro.Activate();
                            }
                        }
                        if (long_seimLINUX)
                        {
                            for (int i = 0; i < CORE.Length; i++)
                            {

                                //if (childHydro == null || childHydro.IsDisposed)
                                //{
                                childHydro = new TestDemo.Hydrograph();
                                string[] files = new string[4];
                                files[0] = folderBrowserDialogSpaAn.SelectedPath + "\\input\\preci_2007.txt";
                                files[1] = folderBrowserDialogSpaAn.SelectedPath + "\\input\\q_2007_true.txt";
                                files[2] = folderBrowserDialogSpaAn.SelectedPath + "\\results\\q_1t_" + CORE[i] + "p";
                                files[3] = CORE[i].ToString() + " Cores : Discharge / m3/s";
                                childHydro.Files = files;
                                childHydro.Show();
                                //}
                                //else
                                //    childHydro.Activate();
                            }
                        }
                        if (OptBMPs)
                        {
                            string[] XYInfoPath= new string[CORE.Length*2];
                            for (int curcore=0;curcore<CORE.Length;curcore++)
                            {
                                string destipath = folderBrowserDialogSpaAn.SelectedPath + "\\demo\\core" + CORE[curcore] + "\\output";
                                string cmd1 = "copy /Y F:\\863ResultsDataNew\\WatershedModeling\\OptBMPs\\field.asc" + " " + destipath;
                                string cmd2 = "copy /Y F:\\863ResultsDataNew\\WatershedModeling\\OptBMPs\\Gen_FieldBMP_ASC_File.py" + " " + destipath;
                                string cmd3 = "copy /Y F:\\863ResultsDataNew\\WatershedModeling\\OptBMPs\\ParetoPlots.py" + " " + destipath;
                                string cmd4 = "python " + destipath + "\\Gen_FieldBMP_ASC_File.py";
                                //string cmd5 = "python " + destipath + "\\ParetoPlots.py";
                                string cmdreslt = ExecuteCMD(cmd1, 0);
                                cmdreslt = ExecuteCMD(cmd2, 0);
                                cmdreslt = ExecuteCMD(cmd3, 0);
                                cmdreslt = ExecuteCMD(cmd4, 0);
                                //cmdreslt = ExecuteCMD(cmd5, 0);
                                XYInfoPath[curcore*2 + 0] = "cores" + CORE[curcore].ToString();
                                XYInfoPath[curcore * 2 + 1] = destipath + "\\final_nondom_pop.out";
                            }
                            childPareto = new TestDemo.FormPareto();
                            childPareto.XYINFO = XYInfoPath;
                            childPareto.Show();
                        }
                        if (Yanglin)
                        {
                            for (int curcore = 0; curcore < CORE.Length; curcore++)
                            {
                                string destipath = folderBrowserDialogSpaAn.SelectedPath+ "\\result_" +  CORE[curcore] ;
                                string cmd1 = "copy /Y F:\\863ResultsDataNew\\SpatialAnaly\\Estimation\\TypPtsDesign\\CSV2PtsShp.py" + " " + destipath;
                                string cmd2 = "python " + destipath + "\\CSV2PtsShp.py";
                                string cmdstr;
                                cmdstr = ExecuteCMD(cmd1, 0);
                                cmdstr = ExecuteCMD(cmd2, 0);
                            }
                        }
                    } 
                    //progressBarUtility.Value = 0;
                    //progressBarUtility.Update();
                    //buttonShowMapViewer.Enabled = true;
                    this.buttonDownFiles.Enabled = true;
                }
            }
            progressBarUtility.Value = 0;
            progressBarUtility.Update();
        }
        public void CorrectTime(string Qfile)
        {
            //string Qfile = "F:\\863Results\\results\\q_1t_8p";
            string[] QItems = File.ReadAllLines(Qfile);
            DateTime TimeItem0 = DateTime.Parse(System.Text.RegularExpressions.Regex.Split(QItems[0], @"\s+")[0] + " " + System.Text.RegularExpressions.Regex.Split(QItems[0], @"\s+")[1]);
            string[] QItemsNew = new string[QItems.Length];
            string QfileNew = Qfile + "New";
            for (int i = 0; i < QItems.Length; i++)
            {
                //DateTime TimeItem = DateTime.Parse(System.Text.RegularExpressions.Regex.Split(QItems[i], @"\s+")[0] + " " + System.Text.RegularExpressions.Regex.Split(QItems[i], @"\s+")[1]);
                string QItem = System.Text.RegularExpressions.Regex.Split(QItems[i], @"\s+")[2];
                DateTime TimeItem = TimeItem0.AddMinutes(30 * i);
                QItemsNew[i] = TimeItem.ToString("yyyy-MM-dd") + " " + TimeItem.ToLongTimeString() + " " + QItem;
                //Console.WriteLine(TimeItem.ToString("yyyy-MM-dd") + " " +TimeItem.ToLongTimeString()+ " " + QItem);
            }
            File.WriteAllLines(QfileNew, QItemsNew);
        }
        public static string ExecuteCMD(string command, int seconds)
        {
            string output = ""; //输出字符串  
            if (command != null && !command.Equals(""))
            {
                Process process = new Process();//创建进程对象  
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令  
                startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出  
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动  
                startInfo.RedirectStandardInput = false;//不重定向输入  
                startInfo.RedirectStandardOutput = true; //重定向输出  
                startInfo.CreateNoWindow = true;//不创建窗口  
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())//开始进程  
                    {
                        if (seconds == 0)
                        {
                            process.WaitForExit();//这里无限等待进程结束  
                        }
                        else
                        {
                            process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒  
                        }
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出  
                    }
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
            return output;
        }  

        private void buttonShowMapViewer_Click(object sender, EventArgs e)
        {
            EasyMapViewer MapViewerIns = new EasyMapViewer();
            MapViewerIns.Show();
        }

        private void comboBoxThirdClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ExecuteOrNot();
        }

        private void krigingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("Kriging", "kriging"));
        }

        private void ToolStripMenuItemRegularization_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("正则化", "hpgcT_nml"));
        }

        public void MenuRun(ComboxItem CI)
        {
            CORE = null;
            child = new TestDemo.SelectCoresFrm();
            if (child.ShowDialog() == DialogResult.OK)
            {
                CORE = child.Cores;
                if (CORE != null)
                {
                    RunCommonAlgorithm(CORE, CI);
                }
                
            }
        }
        private void ToolStripMenuItemDistriTrans_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("分布转换", "log"));
        }

        private void ToolStripMenuItemDiscretization_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("离散化", "dis"));
        }

        //private void moranToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MenuRun(new ComboxItem("Moran's I", "hpgcT_moran2"));
        //}

        //private void gearyToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MenuRun(new ComboxItem("Geary's C", "gearyC"));
        //}

        private void 空间权重矩阵ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 变异函数拟合ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("变异函数拟合", "hpgcT_semivar"));
        }

        private void hASMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("HSAM", "hasm"));
        }

        private void tINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("TIN", "tin"));
        }

        private void 移动平均ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("移动平均", "hpgcT_movingaverage"));
        }

        private void 反距离加权ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("反距离加权", "idw"));
        }

        private void 随意样点插值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("随意样点插值", "solim"));
        }

        private void 典型样点设计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("典型样点设计", "yanglin"));
        }

        private void 简单随机抽样ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("简单随机抽样", "srs"));
        }

        private void 多单元三明治ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("多单元三明治", "sandwich"));
        }

        private void mSNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("MSN", "msn"));
        }

        private void bShadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("B-Shade", "bshade"));
        }

        private void 朴素贝叶斯ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("朴素贝叶斯", "naive"));
        }

        private void eM算法ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("贝叶斯EM算法", "em"));
        }

        private void 爬山算法ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("贝叶斯爬山算法", "hillclimb"));
        }

        private void 贝叶斯网络ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("Gibbs算法", "gibbs"));
        }

        private void getisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("Getis G", "getisG"));
        }

        private void 时空扫描统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("时空扫描统计", "popu"));
        }
        private void 参数率定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            WuhuiCalibration = true;
            MenuRun(new ComboxItem("参数率定", "wuhui_calibration"));
            
        }


        private void 情景分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            OptBMPs = true;
            MenuRun(new ComboxItem("情景分析", "OptBMPs"));
            //WuhuiCalibration = true;
            //CORE = new string[1];
            //CORE[0] = "32";
            //this.checkedListBoxOutputFiles.Items.Add("/soft/jjc/Zhulj/wuhui_calibration/output32/all_nondom_pop.out");
            //this.checkedListBoxOutputFiles.Items.Add("/soft/jjc/Zhulj/wuhui_calibration/output32/all_pop.out");
            //this.checkedListBoxOutputFiles.Items.Add("/soft/jjc/Zhulj/wuhui_calibration/output32/final_nondom_pop.out");
            //this.checkedListBoxOutputFiles.Items.Add("/soft/jjc/Zhulj/wuhui_calibration/output32/final_pop.out");
            //this.checkedListBoxOutputFiles.Items.Add("/soft/jjc/Zhulj/wuhui_calibration/output32/RS_stats.out");
            //this.checkedListBoxOutputFiles.Items.Add("/soft/jjc/Zhulj/wuhui_calibration/output32/stats.out");
            //this.buttonDownFiles.Enabled = true;
        }

        private void buttonSelectAllInput_Click(object sender, EventArgs e)
        {
            SelectAllorNone(this.checkedListBoxInputFiles);
        }

        private void buttonSelectAllOutput_Click(object sender, EventArgs e)
        {
            SelectAllorNone(this.checkedListBoxOutputFiles);
        }

        public void SelectAllorNone(CheckedListBox CL)
        {
            int SelectedNum = 0;
            for (int i = 0; i < CL.Items.Count;i++ )
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
        public void SelectReverse(CheckedListBox CL)
        {
            for (int i = 0; i < CL.Items.Count; i++)
            {
                if (CL.GetItemChecked(i))
                {
                    CL.SetItemChecked(i, false);
                }
                else
                {
                    CL.SetItemChecked(i, true);
                }
            }
        }

        private void buttonSelectReverseInput_Click(object sender, EventArgs e)
        {
            SelectReverse(this.checkedListBoxInputFiles);
        }

        private void buttonSelectReverseOutput_Click(object sender, EventArgs e)
        {
            SelectReverse(this.checkedListBoxOutputFiles);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WuhuiCalibration = true;
            CORE = new string[1];
            CORE[0] = "32";
            this.checkedListBoxOutputFiles.Items.Add("/soft/jjc/Zhulj/wuhui_calibration/output32/all_nondom_pop.out");
            this.checkedListBoxOutputFiles.Items.Add("/soft/jjc/Zhulj/wuhui_calibration/output32/all_pop.out");
            this.checkedListBoxOutputFiles.Items.Add("/soft/jjc/Zhulj/wuhui_calibration/output32/final_nondom_pop.out");
            this.checkedListBoxOutputFiles.Items.Add("/soft/jjc/Zhulj/wuhui_calibration/output32/final_pop.out");
            this.checkedListBoxOutputFiles.Items.Add("/soft/jjc/Zhulj/wuhui_calibration/output32/RS_stats.out");
            this.checkedListBoxOutputFiles.Items.Add("/soft/jjc/Zhulj/wuhui_calibration/output32/stats.out");
            this.buttonDownFiles.Enabled = true;

        }

        private void 参数率定ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            //}
        }
        private void 典型样点设计ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InitialFlag();
            Yanglin = true;
            MenuRun(new ComboxItem("典型样点设计", "yanglin"));
        }

        

        private void 日尺度模拟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            long_seimWIN = true;
            downFlag = true;
            MenuRun(new ComboxItem("子过程测试-日尺度模拟", "long"));
            //long_seimWIN = false;
        }
        private void 次降水模拟11个ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            storm_seimWIN = true;
            downFlag = true;
            MenuRun(new ComboxItem("子过程测试-次降水模拟", "short"));
            //storm_seimWIN = false;
        }

        private void 次降水模拟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            storm_seimLINUX = true;
            MenuRun(new ComboxItem("次降水模拟", "storm_seim"));
            //storm_seimLINUX = false;
        }

        private void 冲沟浅沟系数ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InitialFlag();
            //downFlag = true;
            MenuRun(new ComboxItem("冲沟浅沟系数", "gully_erosion"));
        }

        private void 正则化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1,2,4,8,16,32};
            //Anhui data
            //double[] ComputingTime = { 1.42497, 0.689438, 0.651955, 0.36148, 0.179797, 0.110465 };
            //double[] IOTime = { 7.76044, 7.59699, 7.45933, 7.96465, 7.16579, 7.13761 };
            double[] ComputingTime = { 4.15348, 2.14679, 2.90498, 2.06193, 1.73298, 0.92015 };
            double[] IOTime = { 21.2392, 20.8624, 21.1627, 21.7312, 20.8973, 20.3031 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length;i++ )
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\PreProcess\nml");
        }

        private void 分布转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            //double[] ComputingTime = { 1.779, 1.35338, 0.637706, 0.403627, 0.209452, 0.184284 };
            //double[] IOTime = { 7.14104, 6.90962, 7.25565, 6.81946, 6.21358, 7.51648 };
            // big data 
            double[] ComputingTime = { 4.22875,2.19405,1.32456,0.941885,0.507555,0.251357 };
            double[] IOTime = { 21.7333, 20.5543, 19.5045, 19.9487, 19.1748, 19.9022 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\PreProcess\log");
        }

        private void 离散化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 753.721, 331.174, 193.56, 96.5679, 83.7587, 76.1487 };
            double[] IOTime = { 5.98562, 49.6916, 49.4458, 39.7065, 49.8904, 53.2012 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            //labelProgramState.Text = @"F:\863ResultsDataNew\SpatialAnaly\PreProcess\dis";
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\PreProcess\dis");
            //string ex = ExecuteCMD(@"F:\863ResultsDataNew\SpatialAnaly\PreProcess\dis\dis.mxd", 0);
        }

        private void moransIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 230.226,119.766,66.1,40.2205,18.6214,11.5302};
            double[] IOTime = { 0.364177, 0.390611, 0.41215, 0.355403, 0.355798, 0.439358 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Correlation\moransI");
            ReadTxt(@"F:\863ResultsDataNew\SpatialAnaly\Correlation\moransI\results.txt");
        }
        public void ReadTxt(string fpath)
        {
            StreamReader txt = new StreamReader(fpath, Encoding.Default);
            this.textBoxAlgorithmResults.Text = txt.ReadToEnd();
            this.tabControlResultShow.SelectTab(tabPageNumericResults);
        }
        private void gearysCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 247.33, 127.617, 68.1731, 42.2811, 20.0422, 11.8994 };
            double[] IOTime = { 0.356435, 0.391287, 0.408921, 0.353379, 0.360773, 0.36763 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Correlation\gearysC");
            ReadTxt(@"F:\863ResultsDataNew\SpatialAnaly\Correlation\gearysC\results.txt");
        }

        private void 空间权重矩阵ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 23.24,11.86,5.83,3.10,2.32,2.56};
            double[] IOTime = { 640.90, 340.71, 178.26, 98.11, 73.33, 73.84 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Correlation\swm");
        }

        private void 变异函数拟合ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 111.869, 64.7263, 33.6452, 18.7853, 11.1103, 7.65269 };
            double[] IOTime = { 0.436359,0.567869,0.523051,0.630807,0.65461,0.819747};
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Correlation\semivar");
            ReadTxt(@"F:\863ResultsDataNew\SpatialAnaly\Correlation\semivar\results.txt");
        }

        private void krigingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 1.8816, 1.09333, 0.654129, 0.361652, 0.193239, 0.133422 };
            double[] IOTime = { 0.800243, 0.871625, 0.807368, 0.981672, 0.963842, 0.93369 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Interpolation\Kriging");
        }

        private void hASMToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 66.2205, 33.3364, 16.8208, 9.19249, 4.66086, 2.71977 };
            double[] IOTime = { 3.16714, 3.44098, 3.58889, 3.34711, 3.28002, 3.43597 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Interpolation\HASM");
        }

        private void tINToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = {42.0456,19.5404,7.72425,3.36117,2.31976,1.82903};
            double[] IOTime = { 0.812296,0.286251,0.14287,0.104493,0.0496061,0.035331 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Interpolation\TIN");
        }

        private void 移动平均插值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 148.761, 81.6943, 37.3747, 24.1807, 12.1901, 7.95103 };
            double[] IOTime = { 0.898124, 0.947396, 1.17753, 1.01339, 1.10674, 1.48945 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Interpolation\MovingAvg");
        }

        private void 反距离加权ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 127.246, 77.5664, 45.3866, 21.4528, 12.2121, 8.11757 };
            double[] IOTime = { 0.897772, 1.01492, 1.05949, 1.13219, 1.04306, 1.15449 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Interpolation\IDW");
        }

        private void 随意样点插值ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 1250.19, 757.918, 426.078, 305.191, 120.124, 93.1074 };
            double[] IOTime = { 23.1279, 17.0776, 15.4304, 14.8494, 17.4202, 15.1451 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            //labelProgramState.Text = @"F:\863ResultsDataNew\SpatialAnaly\Interpolation\SoLIM";
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Interpolation\SoLIM");
            //string ex = ExecuteCMD(@"F:\863ResultsDataNew\SpatialAnaly\Interpolation\SoLIM\SoLIM.mxd", 0);
        }

        private void 简单随机抽样ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 0.00697303, 0.00371599, 0.00236106, 0.00172782, 0.00079608, 0.000670195 };
            double[] IOTime = { 2.50262, 2.53154, 2.42831, 2.62157, 2.67077, 2.68333 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Estimation\SimpleRandomSampling");
            ReadTxt(@"F:\863ResultsDataNew\SpatialAnaly\Estimation\SimpleRandomSampling\results.txt");
        }

        private void 多单元三明治ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 124.772, 64.0121, 32.5801, 19.7364, 12.083, 8.54798 };
            double[] IOTime = { 0.707081, 0.619065, 0.672775, 0.676828, 0.685002, 0.6857 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Estimation\Sandwich");
        }

        private void mSNToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 197.507, 90.6384, 43.6203, 24.759, 12.5939, 8.19469 };
            double[] IOTime = { 0.365763, 0.245402, 0.25577, 0.258203, 0.257152, 0.253367 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Estimation\MSN");
            ReadTxt(@"F:\863ResultsDataNew\SpatialAnaly\Estimation\MSN\results.txt");
        }

        private void bShadeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 166.193, 81.7016, 41.2282, 22.3083, 11.6222, 7.46352 };
            double[] IOTime = { 0.239368, 0.244588, 0.139436, 0.242706, 0.23429, 0.234688 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Estimation\B-Shade");
            ReadTxt(@"F:\863ResultsDataNew\SpatialAnaly\Estimation\B-Shade\results.txt");
        }

        private void 典型样点设计ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            InitialFlag();

            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 15.2841, 11.6256, 11.279, 11.5843, 11.7825, 11.2963 };
            double[] IOTime = { 3.35784, 3.22627, 3.39888, 3.63616, 3.17189, 3.43953 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            //labelProgramState.Text = @"F:\863ResultsDataNew\SpatialAnaly\Estimation\TypPtsDesign";
            //string ex = ExecuteCMD(@"F:\863ResultsDataNew\SpatialAnaly\Estimation\TypPtsDesign\TypPtsDesign.mxd", 0);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Estimation\TypPtsDesign");
        }

        private void 朴素贝叶斯ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 2.07762, 1.10397, 0.561985, 0.344832, 0.209619, 0.117696 };
            double[] IOTime = { 7.82909, 4.70972, 2.53297, 1.78132, 1.61312, 2.10599 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\StatisticsClassify\NaiveBayes");
            ReadTxt(@"F:\863ResultsDataNew\SpatialAnaly\StatisticsClassify\NaiveBayes\PDvalue.txt");
        }

        private void 贝叶斯EM算法ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 43.1617, 21.9764, 10.9468, 6.26615, 2.7665, 1.47348 };
            double[] IOTime = { 24.2474, 24.5424, 25.6605, 26.5723, 24.4592, 26.0232 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\StatisticsClassify\EM");
        }

        private void 贝叶斯爬山算法ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 23.3135, 14.3288, 9.179, 8.7163, 7.3571, 8.27525 };
            double[] IOTime = { 17.0807, 17.94, 18.8872, 18.5834, 19.4113, 19.8607 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\StatisticsClassify\hillclimb");
        }

        private void gibbsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 83.5117, 42.7139, 22.3193, 11.4521, 5.59069, 3.05466 };
            double[] IOTime = { 7.63083, 4.73839, 3.03338, 1.42633, 1.16053, 1.37203 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\StatisticsClassify\GibbsSampling");
        }

        private void getisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 1.779, 1.35338, 0.637706, 0.403627, 0.209452, 0.184284 };
            double[] IOTime = { 7.14104, 6.90962, 7.25565, 6.81946, 6.21358, 7.51648 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 1.779, 1.35338, 0.637706, 0.403627, 0.209452, 0.184284 };
            double[] IOTime = { 7.14104, 6.90962, 7.25565, 6.81946, 6.21358, 7.51648 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
        }

        private void 次降水模拟ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            //folderBrowserDialogHydroDown.SelectedPath = "F:\\863ResultsDataNew\\WatershedModeling\\WatershedModeling";
            string saveFileFolder = "F:\\863ResultsDataNew\\WatershedModeling\\WatershedModeling\\storm";
            //if (folderBrowserDialogHydroDown.ShowDialog() == DialogResult.OK)
            //{
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 144.039, 76.4784, 40.3373, 22.8313, 22.2443, 6.96403 };
            double[] IOTime = { 8.13719, 4.73387, 3.02951, 2.05405, 1.66501, 1.5072 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);

            //saveFileFolder = folderBrowserDialogHydroDown.SelectedPath;

            string[] files = new string[4];
            files[0] = saveFileFolder + "\\input\\prec.txt";
            files[1] = saveFileFolder + "\\input\\q_true.txt";
            for (int i = 0; i < Cores.Length; i++)
            {
                CorrectTime(saveFileFolder + "\\results\\q_1t_" + Cores[i].ToString() + "p");
                files[2] = saveFileFolder + "\\results\\q_1t_" + Cores[i].ToString() + "pNew";
                files[3] = "cores" + Cores[i].ToString() + "  Discharge / m3/s";
                childHydro = new TestDemo.Hydrograph();
                childHydro.Files = files;
                childHydro.Show();
            }
            //}
            
        }

        private void 日尺度模拟ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            //folderBrowserDialogHydroDown.SelectedPath = "F:\\863ResultsDataNew\\WatershedModeling\\WatershedModeling";
            string saveFileFolder = "F:\\863ResultsDataNew\\WatershedModeling\\WatershedModeling\\daily";
            //if (folderBrowserDialogHydroDown.ShowDialog() == DialogResult.OK)
            //{
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 2738.77, 1372.31, 692.418, 371.815, 329.162, 97.3255 };
            double[] IOTime = { 32.2674, 17.5307, 9.58131, 6.47234, 5.34381, 5.34381 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);

            //saveFileFolder = folderBrowserDialogHydroDown.SelectedPath;

            string[] files = new string[4];
            files[0] = saveFileFolder + "\\input\\preci_2007.txt";
            files[1] = saveFileFolder + "\\input\\q_2007_true.txt";
            for (int i = 0; i < Cores.Length; i++)
            {
                files[2] = saveFileFolder + "\\results\\q_1t_" + Cores[i].ToString() + "p";
                files[3] = "cores" + Cores[i].ToString() + "  Discharge / m3/s";
                childHydro = new TestDemo.Hydrograph();
                childHydro.Files = files;
                childHydro.Show();
            }
            //}
        }

        private void 冲沟浅沟系数ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 35.9287,19.0075,9.13011,4.88133,4.25597,3.87401 };
            double[] IOTime = { 7.1674,2.97362,2.08692,2.14415,2.48543,4.12681 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            labelProgramState.Text = @"F:\863ResultsDataNew\WatershedModeling\SubprocessTest\GullyShallowCoef";
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\WatershedModeling\SubprocessTest\GullyShallowCoef");
            //string ex = ExecuteCMD(@"F:\863ResultsDataNew\WatershedModeling\SubprocessTest\GullyShallowCoef\gully_erosion.mxd", 0);
        }
        private void 日尺度模拟22个ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ClearElementsBeforeExecute();
            //int[] Cores = { 1, 2, 4, 8, 16, 32 };
            //double[] ComputingTime = { 70.678, 41.442, 24.681, 28.954, 17.351, 22.826 };
            //double[] IOTime = { 2.389, 2.478, 2.312, 2.419, 2.268, 2.236 };


            //folderBrowserDialogHydroDown.SelectedPath = "F:\\863ResultsDataNew\\WatershedModeling\\SubprocessTest";
            string saveFileFolder = "F:\\863ResultsDataNew\\WatershedModeling\\SubprocessTest\\daily"; 
            //if (folderBrowserDialogHydroDown.ShowDialog() == DialogResult.OK)
            //{
            int[] Cores = { 1, 2, 4, 8, 16 };
            double[] ComputingTime = { 70.678, 41.442, 24.681, 28.954, 17.351 };
            double[] IOTime = { 2.389, 2.478, 2.312, 2.419, 2.268 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);

            //saveFileFolder = folderBrowserDialogHydroDown.SelectedPath;
            string RunTimeFile = saveFileFolder + "\\RunTime.txt";
            if (childSubProc == null || childSubProc.IsDisposed)
            {
                childSubProc = new TestDemo.SubProc();
                childSubProc.RunTimeFile = RunTimeFile;
                childSubProc.Show();
            }
            else
                childSubProc.Activate();
            string[] files = new string[4];
            files[0] = saveFileFolder + "\\input\\preci.txt";
            files[1] = saveFileFolder + "\\input\\q_true.txt";
            for (int i = 0; i < Cores.Length; i++)
            {
                files[2] = saveFileFolder + "\\output_" + Cores[i].ToString() + "\\1_Q.txt";
                files[3] = "cores" + Cores[i].ToString() + "  Discharge / m3/s";
                childHydro = new TestDemo.Hydrograph();
                childHydro.Files = files;
                childHydro.Show();
            }
            //}
        }
        private void 次降水模拟11个ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            //int[] Cores = { 1, 2, 4, 8, 16, 32 };
            //double[] ComputingTime = { 20.153, 11.009, 7.526, 4.797, 4.718, 5.755 };
            //double[] IOTime = { 1.583, 1.585, 1.548, 1.575, 1.616, 1.538 };
            

            //folderBrowserDialogHydroDown.SelectedPath = "F:\\863ResultsDataNew\\WatershedModeling\\SubprocessTest";
            string saveFileFolder = "F:\\863ResultsDataNew\\WatershedModeling\\SubprocessTest\\storm";
            //if (folderBrowserDialogHydroDown.ShowDialog() == DialogResult.OK)
            //{
            int[] Cores = { 1, 2, 4, 8, 16 };
            double[] ComputingTime = { 20.153, 11.009, 7.526, 4.797, 4.718 };
            double[] IOTime = { 1.583, 1.585, 1.548, 1.575, 1.616 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);

            //saveFileFolder = folderBrowserDialogHydroDown.SelectedPath;
            string RunTimeFile = saveFileFolder + "\\RunTime.txt";
            if (childSubProc == null || childSubProc.IsDisposed)
            {
                childSubProc = new TestDemo.SubProc();
                childSubProc.RunTimeFile = RunTimeFile;
                childSubProc.Show();
            }
            else
                childSubProc.Activate();
            string[] files = new string[4];
            files[0] = saveFileFolder + "\\input\\preci.txt";
            files[1] = saveFileFolder + "\\input\\q_true.txt";
            for (int i = 0; i < Cores.Length; i++)
            {
                files[2] = saveFileFolder + "\\output_" + Cores[i].ToString() + "\\1_Q.txt";
                files[3] = "cores" + Cores[i].ToString() + "  Discharge / m3/s";
                childHydro = new TestDemo.Hydrograph();
                childHydro.Files = files;
                childHydro.Show();
            }
            //}
        }

        private void 情景分析ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void 日尺度模拟ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InitialFlag();
            long_seimLINUX = true;
            MenuRun(new ComboxItem("日尺度模拟", "long_seim"));
        }

        //private void getisGToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    InitialFlag();
        //    MenuRun(new ComboxItem("Getis G", "getisG"));
        //}

        private void 时空扫描统计ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("时空扫描统计", "popu"));
        }

        private void 进化代数10代ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            //folderBrowserDialogHydroDown.SelectedPath = "F:\\863ResultsDataNew\\WatershedModeling\\Calibration";
            string saveFileFolder = "F:\\863ResultsDataNew\\WatershedModeling\\Calibration\\Demo10";
            //string saveFileFolder = @"F:\863Results\cali2\demo";
            //if (folderBrowserDialogHydroDown.ShowDialog() == DialogResult.OK)
            //{
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            //saveFileFolder = folderBrowserDialogHydroDown.SelectedPath;
            for (int core = 0; core < Cores.Length; core++)
            {
                if (Directory.Exists(saveFileFolder + "\\core" + Cores[core].ToString()))
                {
                    child3 = new TestDemo.FormWuhui_calibration();
                    string[] paramcali = { Cores[core].ToString(), saveFileFolder + "\\core" + Cores[core].ToString() };
                    child3.ParamCaliOutputProperty = paramcali;
                    child3.Show();
                }
            }
            double[] ComputingTime = { 2737.78, 1461.63, 690.5, 370.081, 341.632, 354.459 };
            double[] IOTime = { 0.00127959,0.00124884,0.00132084,0.00122738,0.00871062,0.0109682 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
        }

        private void 进化代数100代ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            //folderBrowserDialogHydroDown.SelectedPath = "F:\\863ResultsDataNew\\WatershedModeling\\Calibration";
            string saveFileFolder = "F:\\863ResultsDataNew\\WatershedModeling\\Calibration\\Demo100";
            //if (folderBrowserDialogHydroDown.ShowDialog() == DialogResult.OK)
            //{
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            //saveFileFolder = folderBrowserDialogHydroDown.SelectedPath;
            for (int core = 0; core < Cores.Length; core++)
            {
                if (Directory.Exists(saveFileFolder + "\\core" + Cores[core].ToString()))
                {
                    child3 = new TestDemo.FormWuhui_calibration();
                    string[] paramcali = { Cores[core].ToString(), saveFileFolder + "\\core" + Cores[core].ToString() };
                    child3.ParamCaliOutputProperty = paramcali;
                    child3.Show();
                }
            }
            double[] ComputingTime = { 29507.5, 14693.8, 7360.37, 4045.44, 3042.56, 3229.04 };
            double[] IOTime = { 0.016181,0.0169897,0.00671649,0.0218627,0.0261714,0.0584693 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
        }

        private void 进化代数10代ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            //folderBrowserDialogHydroDown.SelectedPath = "F:\\863ResultsDataNew\\WatershedModeling\\Calibration";
            string saveFileFolder = "F:\\863ResultsDataNew\\WatershedModeling\\OptBMPs\\Demo10";
            //string saveFileFolder = @"F:\863Results\cali2\demo";
            //if (folderBrowserDialogHydroDown.ShowDialog() == DialogResult.OK)
            //{
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            string[] XYInfoPathEX = new string[2 * Cores.Length];
            int count = 0;
            //saveFileFolder = folderBrowserDialogHydroDown.SelectedPath;
            for (int core = 0; core < Cores.Length; core++)
            {
                if (Directory.Exists(saveFileFolder + "\\core" + Cores[core].ToString()))
                {
                    XYInfoPathEX[count * 2 + 0] = "core" + Cores[core].ToString();
                    XYInfoPathEX[count * 2 + 1] = saveFileFolder + "\\core" + Cores[core].ToString() + "\\output\\final_nondom_pop.out";
                    count++;
                }
            }
            string[] XYInfoPath;
            if (count == Cores.Length)
            {
                XYInfoPath = XYInfoPathEX;
            }
            else
            {
                XYInfoPath = new string[2 * count];
                for (int c = 0; c < count;c++ )
                {
                    XYInfoPath[c * 2 + 0] = XYInfoPathEX[c * 2 + 0];
                    XYInfoPath[c * 2 + 1] = XYInfoPathEX[c * 2 + 1];
                }
            }
            childPareto = new TestDemo.FormPareto();
            //string[] XYInfoPath = { "core16", saveFileFolder + "\\core16\\output\\final_nondom_pop.out", "core32", "F:\\863ResultsDataNew\\WatershedModeling\\OptBMPs\\output32\\final_nondom_pop.out" };
            childPareto.XYINFO = XYInfoPath;
            childPareto.Show();
            double[] ComputingTime = {3420.64,1721.66,847.661,448.26,335.756,342.472};
            double[] IOTime = { 0.0193419, 0.0180492, 0.0245612, 0.0184851, 0.0178714, 0.102507 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
        }

        private void 时空扫描统计ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 703.211, 411.861, 251.645, 193.531, 165.955, 183.785 };
            double[] IOTime = { 0.00939012, 0.00824499, 0.052634, 0.0540311, 0.0951319, 0.0700641 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Correlation\satscan");
        }

        private void 进化代数100代ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            //folderBrowserDialogHydroDown.SelectedPath = "F:\\863ResultsDataNew\\WatershedModeling\\Calibration";
            string saveFileFolder = "F:\\863ResultsDataNew\\WatershedModeling\\OptBMPs\\Demo100";
            //string saveFileFolder = @"F:\863Results\cali2\demo";
            //if (folderBrowserDialogHydroDown.ShowDialog() == DialogResult.OK)
            //{
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            string[] XYInfoPathEX = new string[2 * Cores.Length];
            int count = 0;
            //saveFileFolder = folderBrowserDialogHydroDown.SelectedPath;
            for (int core = 0; core < Cores.Length; core++)
            {
                if (Directory.Exists(saveFileFolder + "\\core" + Cores[core].ToString()))
                {
                    XYInfoPathEX[count * 2 + 0] = "core" + Cores[core].ToString();
                    XYInfoPathEX[count * 2 + 1] = saveFileFolder + "\\core" + Cores[core].ToString() + "\\output\\final_nondom_pop.out";
                    count++;
                }
            }
            string[] XYInfoPath;
            if (count == Cores.Length)
            {
                XYInfoPath = XYInfoPathEX;
            }
            else
            {
                XYInfoPath = new string[2 * count];
                for (int c = 0; c < count; c++)
                {
                    XYInfoPath[c * 2 + 0] = XYInfoPathEX[c * 2 + 0];
                    XYInfoPath[c * 2 + 1] = XYInfoPathEX[c * 2 + 1];
                }
            }
            childPareto = new TestDemo.FormPareto();
            //string[] XYInfoPath = { "core16", saveFileFolder + "\\core16\\output\\final_nondom_pop.out", "core32", "F:\\863ResultsDataNew\\WatershedModeling\\OptBMPs\\output32\\final_nondom_pop.out" };
            childPareto.XYINFO = XYInfoPath;
            childPareto.Show();
            double[] ComputingTime = { 34680.6,17186.9,8475.64,4468.44,3346.71,3388.34 };
            double[] IOTime = { 0.305185, 0.170043, 0.320721, 0.172346, 0.426554, 1.28937 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
        }

        private void getisGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearElementsBeforeExecute();
            int[] Cores = { 1, 2, 4, 8, 16, 32 };
            double[] ComputingTime = { 240.953, 122.457, 66.1955, 42.8539, 19.2988, 10.8106 };
            double[] IOTime = { 0.366819, 0.379039, 0.411535, 0.356939, 0.356101, 0.369983 };
            double[] TotalTime = new double[ComputingTime.Length];
            for (int i = 0; i < ComputingTime.Length; i++)
            {
                TotalTime[i] = ComputingTime[i] + IOTime[i];
            }
            DrawTimeLines(Cores, ComputingTime, IOTime, TotalTime);
            System.Diagnostics.Process.Start("explorer.exe", @"F:\863ResultsDataNew\SpatialAnaly\Correlation\getisG");
            ReadTxt(@"F:\863ResultsDataNew\SpatialAnaly\Correlation\getisG\results.txt");
        }

        private void 大数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("Moran's I", "hpgcT_moran"));
        }

        private void 小数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuRun(new ComboxItem("Moran's I", "hpgcT_moran2"));
        }

        private void 大数据ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("Geary's C", "gearyC"));
        }

        private void 小数据ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("Geary's C", "gearyC2"));
        }

        private void 大数据ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("Getis G", "getisG"));
        }

        private void 小数据ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("Getis G", "getisG2"));
        }

        private void 大数据ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("空间权重矩阵", "swm"));
        }

        private void 小数据ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("空间权重矩阵", "swm2"));
        }

        private void 大数据ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("离散化", "dis"));
        }

        private void 小数据ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            InitialFlag();
            MenuRun(new ComboxItem("离散化", "dis2"));
        }
    }
}

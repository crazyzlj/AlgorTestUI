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
    public partial class FormFeatureToPolygon : Form
    {
        public FormFeatureToPolygon()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) //调用 执行函数
        {
            string WhetherFileExist =string.Empty ; //判断输出文件是否存在

            WhetherFileExist=textBox2.Text.ToString();

            if (System.IO.File.Exists(WhetherFileExist) == false) //判断输出文件是否存在
            {

                FeatureToPolygonFunction(textBox1.Text.ToString(), textBox2.Text.ToString());
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show("输出的文件已经存在，是否替换原文件？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                { 
                    FeatureToPolygonFunction(textBox1.Text.ToString(), textBox2.Text.ToString());
                }
                else
                {
                    return;
                }
                
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.FilterIndex = 1;
            string FileName;
            dlg.Filter = "矢量shape文件(*.shp)|*.shp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileName = dlg.FileName;

                if (FileName != "")
                {
                    string filenameshort = FileName;//获取文件路径
                    textBox1.Text = filenameshort;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save=new SaveFileDialog();   //调用保存对话框
            save.RestoreDirectory=true;
            save.Filter = "矢量shape文件(*.shp)|*.shp";

            string OutpathFileName=string.Empty ;
            if (DialogResult.OK == save.ShowDialog())
            {
                OutpathFileName = save.FileName;
                textBox2.Text = OutpathFileName;
            }
        }

        // 函数 可以移植的处理模块FeatureToPolygonFunction
        public void FeatureToPolygonFunction(string inshapefile,string outshapefile)
        {
            ESRI.ArcGIS.Geoprocessor.Geoprocessor GP = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
            GP.OverwriteOutput = true;
            ESRI.ArcGIS.DataManagementTools.FeatureToPolygon fpg = new ESRI.ArcGIS.DataManagementTools.FeatureToPolygon();

            //fpg.in_features = Application.StartupPath + "\\testdata" + "\\river.shp"; //参数1
            //fpg.out_feature_class = Application.StartupPath + "\\testdata" + "\\ToPolygon.shp"; //参数2
            fpg.in_features = inshapefile; //参数1
            fpg.out_feature_class =outshapefile; //参数2
            //fpg.attributes = "INPUT"; //参数3 保留属性


            //以下 执行工具，成功返回信息；不成功也返回！try catch 有用！
            try
            {
                GP.Execute(fpg, null);  // 最终执行命令 需要验证是否成功 返回成功说明
                string gpname;
                gpname = fpg.ToolboxDirectory + "   " + fpg.ToolboxName + "    " + fpg.ToolName + "   " + fpg.Alias;
                string strMessage = string.Empty;

                for (int ii = 0; ii < GP.MessageCount; ii++)
                {
                    strMessage += GP.GetMessage(ii).ToString() + "\r\n";
                }

                //你再跟踪下程序，gp.MessageCount一般为7是成功的，如果是6则不成功。看不成功信息提示是什么

                MessageBox.Show("执行成功！" + "\r\n" + "Gp工具信息：" + gpname + "\r\n" + " 工具执行信息：" + strMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {

                for (int i = 0; i < GP.MessageCount; i++)
                {
                    MessageBox.Show(GP.GetMessage(i), "工具执行错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void FormFeatureToPolygon_Load(object sender, EventArgs e)
        {

        }




    }
}

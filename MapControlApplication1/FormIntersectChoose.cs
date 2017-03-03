using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; //后加入的 文件操作

namespace TestDemo
{
    public partial class FormIntersectChoose : Form
    {
        public FormIntersectChoose()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void FormIntersectChoose_Load(object sender, EventArgs e) //窗体载入事件
        {
            textBox1.Text = Application.StartupPath + "\\testdata";
            textBox2.Text = "smallplace.shp";
            textBox3.Text = "landuse.shp";
            textBox4.Text = "InterectResult.shp";
            textBox5.Text = "0";
            comboBox1.Text = "All";
            comboBox2.Text = "INPUT";
        }


        private void button3_Click(object sender, EventArgs e) //关闭窗体
        {
            this.Close();
        }
        
        
        private void button1_Click(object sender, EventArgs e) //方法1 ：设置工作空间路径 完美选择！
        {
            Dialogs.FolderBrowser Fbrowser = new Dialogs.FolderBrowser(); //创建Dialogs.FolderBrowser 实例类 建议使用以下两个静态方法：
                                                                          //System.IO.Directory.Exists(string path)
                                                                          // System.IO.File.Exists(string path)
            Fbrowser.Description = "请选择目标目录"; //属性
            Fbrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.Desktop;
            Fbrowser.Style = Dialogs.FolderBrowser.fbStyles.BrowseForEverything;
            if (Fbrowser.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(Fbrowser.DirectoryPath)) //判断是文件还是文件夹 细节处理！！
                {
                    textBox1.Text = Fbrowser.DirectoryPath;
                }
                else
                {
                    MessageBox.Show("请选择正确文件夹路径，不要选择文件路径哦！" , "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }   
                
            }
        }

    
        private void button4_Click(object sender, EventArgs e) //选择 shp1的 名字 包括扩展名
        {
            Dialogs.FolderBrowser Fbrowser = new Dialogs.FolderBrowser(); //创建Dialogs.FolderBrowser 实例类 建议使用以下两个静态方法：
            //System.IO.Directory.Exists(string path)
            // System.IO.File.Exists(string path)
            Fbrowser.Description = "请选择shp文件"; //属性
           Fbrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.Desktop;


            Fbrowser.Style = Dialogs.FolderBrowser.fbStyles.BrowseForEverything;
            if (Fbrowser.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(Fbrowser.DirectoryPath)) //判断是文件还是文件夹 细节处理！！
                {
                    string filename=System.IO.Path.GetFileName(Fbrowser.DirectoryPath);
                    textBox2.Text = filename;
                }
                else
                {
                    MessageBox.Show("请选择正确shp文件路径，不要选择文件夹哦！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void button5_Click(object sender, EventArgs e) //shape2 的选择按钮 方法1
        {
            Dialogs.FolderBrowser Fbrowser = new Dialogs.FolderBrowser(); //创建Dialogs.FolderBrowser 实例类 建议使用以下两个静态方法：
            //System.IO.Directory.Exists(string path)
            // System.IO.File.Exists(string path)
            Fbrowser.Description = "请选择shp文件"; //属性
            Fbrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.Desktop;
            Fbrowser.Style = Dialogs.FolderBrowser.fbStyles.BrowseForEverything;
            if (Fbrowser.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(Fbrowser.DirectoryPath)) //判断是文件还是文件夹 细节处理！！
                {
                    string filename = System.IO.Path.GetFileName(Fbrowser.DirectoryPath);
                    textBox3.Text = filename;
                }
                else
                {
                    MessageBox.Show("请选择正确shp文件路径，不要选择文件夹哦！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void button6_Click(object sender, EventArgs e) //shape2 的选择按钮 方法2 更好 能记住上次的路径 而且 是系统自带的！
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
                      string filenameshort = System.IO.Path.GetFileName(FileName);
                      textBox3.Text = filenameshort;
                  }
              }
        }

        private void button7_Click(object sender, EventArgs e) //shape1的选择按钮 方法2 更好 能记住上次的路径 而且 是系统自带的！
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
                    string filenameshort = System.IO.Path.GetFileName(FileName);//获取文件名称（带扩展名）
                    textBox2.Text = filenameshort;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)//方法2 ：设置工作空间路径   更好
        {
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.RestoreDirectory = true;
            //dlg.FilterIndex = 1;
            //string FileName;
            //dlg.Filter = "矢量shape文件(*.shp)|*.shp";
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    FileName = dlg.FileName;

            //    if (FileName != "")
            //    {
            //        string filenameshort = System.IO.Path.GetDirectoryName(FileName);//获取文件目录
            //        textBox1.Text = filenameshort;
            //    }
            //}

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "选择文件夹路径（即工作空间）";
            dlg.ShowNewFolderButton  = true ;
         
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string FileName = dlg.SelectedPath ;

                if (FileName != "")
                {
                    //获取文件目录
                    textBox1.Text =  FileName;
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)   //执行 GP操作！
         {
            string WhetherFileExist =string.Empty ; //判断输出文件是否存在

            WhetherFileExist=textBox1.Text +"//" +textBox4.Text;

            if (System.IO.File.Exists(WhetherFileExist)==false) //判断输出文件是否存在
            {
                ESRI.ArcGIS.Geoprocessor.Geoprocessor gp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();

                ESRI.ArcGIS.AnalysisTools.Intersect pIntsect = new ESRI.ArcGIS.AnalysisTools.Intersect();

                gp.OverwriteOutput = true;
                //设置工作空间 // IGpEnumList gpEnumEnv = gp.ListEnvironments("*");

                string EnvironmentString;
                //EnvironmentString = Application.StartupPath + "\\testdata";  //参数1 只给文件夹路径
                EnvironmentString = textBox1.Text;
                gp.SetEnvironmentValue("workspace", @EnvironmentString);

                //错误的 不能调用路径  只能调用名称 pIntsect.in_features = "E:\\My first AE\\MapControlApplication1\\bin\\Debug\\testdata\\smallplace.shp;E:\\My first AE\\MapControlApplication1\\bin\\Debug\\testdata\\landuse.shp";
                string IntersectOne; //参数2 只选择裁剪名字
                string TobeIntersectOne;//参数3 只选择被裁减 名字 路径无效 字符串处理 提取最后的 或者用户输入 自己加上shp
                string InFeatureString;
                //IntersectOne = "\\smallplace.shp";
                //TobeIntersectOne = "\\landuse.shp";

                IntersectOne = "\\" + textBox2.Text;

                TobeIntersectOne = "\\" + textBox3.Text;

                InFeatureString = IntersectOne + ";" + TobeIntersectOne;

                pIntsect.in_features = InFeatureString;

                //pIntsect.in_features = "\\smallplace.shp;\\landuse.shp";

                pIntsect.out_feature_class = textBox4.Text; //参数4 输出文件名字 同理

                pIntsect.cluster_tolerance = textBox5.Text;//参数5 容限值 可选

                pIntsect.join_attributes = comboBox1.Text;//参数6

                pIntsect.output_type = comboBox2.Text;//参数7


                string xiaozhu;
                xiaozhu = gp.Execute(pIntsect, null).ToString();  // 最终执行命令 需要验证是否成功 返回成功说明

                string strMessage = string.Empty;

                for (int i = 0; i < gp.MessageCount; i++)
                {
                    strMessage += gp.GetMessage(i).ToString() + "\r\n";
                }

                //你再跟踪下程序，gp.MessageCount一般为7是成功的，如果是6则不成功。看不成功信息提示是什么

                MessageBox.Show("裁剪成功！" + "Gp对象名称" + xiaozhu + " 对象gp.GetMessage获取内容：" + strMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //gp.ExecuteAsync(pIntsect);

            }
            else
            {
                MessageBox.Show("输出的文件已经存在，请换一名字吧！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

      

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }




    }
}
  
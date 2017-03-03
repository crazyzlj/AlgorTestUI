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
    public partial class FormClassEvaluate : Form
    {
        public FormClassEvaluate()
        {
            InitializeComponent();
        }

        double[] MatrixPercent1; //用于制作第三个矩阵的参数
        double[] MatrixPercent2;

        private void FormClassEvaluate_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "3";
            comboBox2.Text = "class";
            comboBox3.Text = "class_1";
            comboBox4.Text = "F_AREA";
            comboBox5.Text = "请选择";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //private void button2_Click(object sender, EventArgs e)  // **** 从属性窗口调入的表格 输出矩阵结果界面 
        //{
        //    int classNum = Convert.ToInt32(comboBox1.Text); //类别个数
        //    AttributeTableFrm openform = new AttributeTableFrm();  //调用的 矩阵计算函数 所在窗体（类的机制）

        //    //第一个矩阵
        //    ulong[,] MatrixReturn1 = new ulong[classNum, classNum];
        //    MatrixReturn1 = openform.CaluateErrorMatrixFirst(classNum, comboBox2.Text, comboBox3.Text, comboBox4.Text);
        //    //第二个矩阵
        //    ulong[,] MatrixReturn2 = new ulong[classNum, classNum];
        //    MatrixReturn2 = openform.CaluateErrorMatrixSecond(classNum, comboBox2.Text, comboBox3.Text, comboBox4.Text, comboBox5.Text);
        //    //打开输出结果窗体，构造2个误差矩阵
        //    FormDataGridView caluateform = new FormDataGridView();
        //    caluateform.Show();
        //    MatrixPercent1 = caluateform.CreatDataGrid1(MatrixReturn1, classNum); //在另一个窗体 构造第一个矩阵
        //    MatrixPercent2 = caluateform.CreatDataGrid2(MatrixReturn2, classNum); //在另一个窗体 构造第一个矩阵

        //    ////在另一个窗体 构造第三个矩阵 
        //    caluateform.CreatDataGrid3(MatrixPercent1, MatrixPercent2, classNum);
        //    #region 中间检验输出试验代码
        //    //int calculater = 0;
        //    //string RuturnMatrixMessage = string.Empty;
        //    //foreach (ulong one in MatrixReturn) //遍历每一行 foreach
        //    //{
        //    //    calculater += 1;
        //    //    double rowchange = Convert.ToDouble(calculater) / Convert.ToDouble(classNum);

        //    //    if (openform.IsNumber(rowchange.ToString(), 32, 0) == false)  //引用其他窗体的 方法函数 比控件要简单的多！
        //    //    {
        //    //        RuturnMatrixMessage += one.ToString() + "  ";
        //    //    }
        //    //    else
        //    //    {
        //    //        RuturnMatrixMessage += one.ToString() + Environment.NewLine;
        //    //    }
        //    //}

        //    //MessageBox.Show(RuturnMatrixMessage, "矩阵数值结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    #endregion

        //    this.Close(); 
        //}
        private void button2_Click_1(object sender, EventArgs e)   // **** 从属性窗口调入的表格 输出矩阵结果界面 
        {
            int classNum = Convert.ToInt32(comboBox1.Text); //类别个数
            AttributeTableFrm openform = new AttributeTableFrm();  //调用的 矩阵计算函数 所在窗体（类的机制）

            //第一个矩阵
            ulong[,] MatrixReturn1 = new ulong[classNum, classNum];
            MatrixReturn1 = openform.CaluateErrorMatrixFirst(classNum, comboBox2.Text, comboBox3.Text, comboBox4.Text);
            //第二个矩阵
            ulong[,] MatrixReturn2 = new ulong[classNum, classNum];
            MatrixReturn2 = openform.CaluateErrorMatrixSecond(classNum, comboBox2.Text, comboBox3.Text, comboBox4.Text, comboBox5.Text);
            //打开输出结果窗体，构造2个误差矩阵
            FormDataGridView caluateform = new FormDataGridView();
            caluateform.Show();
            MatrixPercent1 = caluateform.CreatDataGrid1(MatrixReturn1, classNum); //在另一个窗体 构造第一个矩阵
            MatrixPercent2 = caluateform.CreatDataGrid2(MatrixReturn2, classNum); //在另一个窗体 构造第一个矩阵

            ////在另一个窗体 构造第三个矩阵 
            caluateform.CreatDataGrid3(MatrixPercent1, MatrixPercent2, classNum);
            #region 中间检验输出试验代码
            //int calculater = 0;
            //string RuturnMatrixMessage = string.Empty;
            //foreach (ulong one in MatrixReturn) //遍历每一行 foreach
            //{
            //    calculater += 1;
            //    double rowchange = Convert.ToDouble(calculater) / Convert.ToDouble(classNum);

            //    if (openform.IsNumber(rowchange.ToString(), 32, 0) == false)  //引用其他窗体的 方法函数 比控件要简单的多！
            //    {
            //        RuturnMatrixMessage += one.ToString() + "  ";
            //    }
            //    else
            //    {
            //        RuturnMatrixMessage += one.ToString() + Environment.NewLine;
            //    }
            //}

            //MessageBox.Show(RuturnMatrixMessage, "矩阵数值结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            #endregion

            this.Close();
        }


        private void button3_Click(object sender, EventArgs e)  //主窗口调入的
        { 
            int classNum = Convert.ToInt32(comboBox1.Text);
            ulong[,] MatrixReturn = new ulong[classNum, classNum];
            //AttributeTableFrm openform = new AttributeTableFrm();  //你先声明窗体实例
            MainForm openform = new MainForm ();
            MatrixReturn = openform.CaluateErrorMatrix1(classNum, comboBox2.Text, comboBox3.Text, comboBox4.Text);

            int calculater = 0;
            string RuturnMatrixMessage = string.Empty;
            foreach (ulong one in MatrixReturn) //遍历每一行 foreach
            {
                calculater += 1;
                double rowchange = Convert.ToDouble(calculater) / Convert.ToDouble(classNum);

                if (openform.IsNumber(rowchange.ToString(), 32, 0) == false)  //引用其他窗体的 方法函数 比控件要简单的多！
                {
                    RuturnMatrixMessage += one.ToString() + "  ";
                }
                else
                {
                    RuturnMatrixMessage += one.ToString() + Environment.NewLine;
                }
            }

            MessageBox.Show(RuturnMatrixMessage, "矩阵数值结果", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        public void AddField(string[] FieldString) //添加字段 数组从0开始 （主窗口的）
        {

            foreach (string One in FieldString)
            {
                comboBox2.Items.Add(One.ToString());
                comboBox3.Items.Add(One.ToString());
                comboBox4.Items.Add(One.ToString());
                comboBox5.Items.Add(One.ToString());

            }
            button2.Visible = false; //主窗口调入 所以隐藏另一个按钮
        }


        public void AddFieldcopy(string[] FieldString) //添加字段 数组从0开始 （属性窗口的）
        {

            foreach (string One in FieldString)
            {
                comboBox2.Items.Add(One.ToString());
                comboBox3.Items.Add(One.ToString());
                comboBox4.Items.Add(One.ToString());
                comboBox5.Items.Add(One.ToString());


            }
            button3.Visible = false; //属性窗口调入 所以隐藏另一个按钮
        }

        private void button1_Click(object sender, EventArgs e)  //第二个矩阵的运算 试验中
        {
            int classNum = Convert.ToInt32(comboBox1.Text);
            ulong[,] MatrixReturn = new ulong[classNum, classNum];
            AttributeTableFrm openform = new AttributeTableFrm();  //你先声明窗体实例
            MatrixReturn = openform.CaluateErrorMatrixSecond(classNum, comboBox2.Text, comboBox3.Text, comboBox4.Text,comboBox5 .Text);

            int calculater = 0;
            string RuturnMatrixMessage = string.Empty;
            ulong allArea = 0;
            foreach (ulong one in MatrixReturn) //遍历每一行 foreach
            {
                calculater += 1;
                double rowchange = Convert.ToDouble(calculater) / Convert.ToDouble(classNum);

                if (openform.IsNumber(rowchange.ToString(), 32, 0) == false)  //引用其他窗体的 方法函数 比控件要简单的多！
                {
                    RuturnMatrixMessage += one.ToString() + "  ";
                }
                else
                {
                    RuturnMatrixMessage += one.ToString() + Environment.NewLine;
                }
            }

            foreach (ulong one in MatrixReturn) //遍历每一行 foreach
            {

                allArea += one;
              
            }

            MessageBox.Show(RuturnMatrixMessage, "矩阵数值结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("总面积"+allArea.ToString (), "矩阵数值结果", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

       



        //// 函数 可以移植的处理模块 :矢量分类真实性评价的统计出 矩阵
        ////输出参数为4个：分类个数x int ； 分类结果字段str ； 真实地物类别字段str ； 统计字段 str； 
        ////返回是一个二维数组 Matrix[X-1,X-1] 
        //public ulong[,] CaluateErrorMatrix(int X, string ResultClassField, string RealGroundField, string StatisticField)
        //{
        //    AttributeTableFrm openform = new AttributeTableFrm();
        //    ulong[,] Matrix = new ulong[X, X]; //这里X代表的数组的维数 （3*3） 但是数组下标是从0~2  区分这个概念OK！

        //    DataTable dt = (DataTable)openform.dataGridView1.DataSource ;
        //    for (int i = 0; i < openform.dataGridView1.RowCount; i++)
        //    {
        //        int MatrixFirstNum;
        //        int MatrixSecondNum;
        //        double calnum;
        //        MatrixFirstNum = Convert.ToInt32(dt.Rows[i][ResultClassField].ToString());
        //        MatrixSecondNum = Convert.ToInt32(dt.Rows[i][RealGroundField].ToString());
        //        calnum = Convert.ToUInt64(dt.Rows[i][StatisticField].ToString());

        //        // Matrix[MatrixFirstNum - 1, MatrixSecondNum - 1] += Convert.ToDouble(calnum);//注意顺序 行和列的区分！此时 行为结果 列为真实
        //        Matrix[MatrixSecondNum - 1, MatrixFirstNum - 1] += Convert.ToUInt64(calnum);
        //        // MessageBox.Show(Matrix[MatrixFirstNum - 1, MatrixSecondNum - 1].ToString(), "遍历", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        if (i == openform.dataGridView1.RowCount - 2) //因为编辑状态下 最后1条是空记录！！
        //        {
        //            return Matrix;
        //        }

        //    }
        //    return Matrix;
        //}


        ///**/
        ///// <summary>  
        ///// 判断一个字符串是否为合法数字(指定整数位数和小数位数)  
        ///// </summary>  
        ///// <param name="s">字符串</param>  
        ///// <param name="precision">整数位数</param>  
        ///// <param name="scale">小数位数</param>  
        ///// <returns></returns>  
        //public static bool IsNumber(string s, int precision, int scale)
        //{
        //    if ((precision == 0) && (scale == 0))
        //    {
        //        return false;
        //    }
        //    string pattern = @"(^\d{1," + precision + "}";
        //    if (scale > 0)
        //    {
        //        pattern += @"\.\d{0," + scale + "}$)|" + pattern;
        //    }
        //    pattern += "$)";
        //    return System.Text.RegularExpressions.Regex.IsMatch(s, pattern);
        //}  



      







    }

}

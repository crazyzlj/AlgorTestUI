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
    public partial class FormDataGridView : Form
    {
        public FormDataGridView()
        {
            InitializeComponent();
        }

        
        private void FormDataGridView_Load(object sender, EventArgs e)
        {

            dataGridView1.ReadOnly = true;
            dataGridView1.Name = "误差矩阵1 对象内部精度";

            dataGridView2.ReadOnly = true;
            dataGridView2.Name = "误差矩阵2 面积加权";

            dataGridView3.ReadOnly = true;
            dataGridView3.Name = "误差矩阵3 切割精度指标";

         

            #region 练习试验代码
            //dataGridView1.Columns[0].Name = " ";
            //dataGridView1.Columns[1].Name = "真实1类 ";
            //dataGridView1.Columns[2].Name = "真实2类 ";
            //dataGridView1.Columns[3].Name = "真实3类 ";
            //dataGridView1.Columns[4].Name = "总和 ";
                 
            //    string[]   row0   =   {   "结果1类 ",   "29 ",  " 34 ", "452",   "66"   }; 
            //    string[]   row1   =   {   "结果2类",   "6 ",   "32 ",    "33 ",   "88 "   };
            //    string[]   row2   =   {   "结果3类 ", "1 ", "55 ", "23 ", "234 " }; 
            //    string[]   row3   =   {   "总和 ",   "13 ",    "32 ",    "33",   "54"   }; 
              

            //    dataGridView1.Rows.Add(row0);
            //    dataGridView1.Rows.Add(row1);
            //    dataGridView1.Rows.Add(row2);
            //    dataGridView1.Rows.Add(row3);
             
              //this.dataGridView1.Rows[0].Cells[columnName].Value = 值

            //dataGridView1.Rows[1]

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

        }


        public double[] CreatDataGrid1(ulong[,] inMatrix, int classnum)
        {
          
            //第一大步：建立字段 加矩阵数据
            dataGridView1.ColumnCount = classnum+2+1;//加2 是“空列字段” 和“总和字段”，加1 是“参数统计字段”

            for (int i=0; i < classnum + 2; i++)  //添加 列名 第0列空 最后一列 是 “总和” ：多了2列，从0计数
            {
                if (i == 0)
                {
                    dataGridView1.Columns[i].Name = "";
                }
                else
                {
                    if (i == classnum + 1)
                    {
                        dataGridView1.Columns[i].Name = "列总和" ;
                    }
                    else
                    {
                        dataGridView1.Columns[i].Name = "真实" + i + "类";
                    }
                }

            }

            for (int i = 0; i < classnum + 1; i++)  //添加 行名称  最后一行 是 “总和”： 多了1行，从0计数
            {
                dataGridView1.Rows.Add();

                if (i == classnum)
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = "行总和";
                }
                else
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = "结果" + (i+1) + "类";
                }

            }



            for (int i = 0; i < classnum ; i++)  //添加 矩阵的N*N 数据 哇咔咔！ 注意此时 行数值不变 列要加1
            {
                for (int j = 0; j < classnum; j++)  
                {

                    this.dataGridView1.Rows[i].Cells[j + 1].Value = inMatrix[i,j].ToString ();

                }


            }


            ulong[] rowValue = new ulong[classnum];  //生成每一行的总和
            for (int i = 0; i < classnum; i++)  //从第一行到第三行
            {
                for (int j = 0; j < classnum; j++)//每行的 从第一列到第三列 相加
                {
                    rowValue[i] += inMatrix[i, j];
                    

                }
            this.dataGridView1.Rows[i].Cells[classnum+1].Value = rowValue[i].ToString();
            }



            ulong[] colValue = new ulong[classnum];  //生成每一列的总和
            for (int j = 0; j < classnum; j++)  //从第1行到第3行
            {
                for (int i = 0; i < classnum; i++)//每列的 从第1行到第3行 相加
                {
                    colValue[j] += inMatrix[i, j];


                }
                this.dataGridView1.Rows[classnum].Cells[j + 1].Value = colValue[j].ToString();
            }


            ulong AllValue =0; //计算最后的总值
            for (int i = 0; i < classnum; i++)  //从第一行到第三行
            {
                for (int j = 0; j < classnum; j++)//每行的 从第一列到第三列 相加
                {
                    AllValue += inMatrix[i, j];

                }
                this.dataGridView1.Rows[classnum].Cells[classnum + 1].Value = AllValue.ToString();
            }


            #region 检验总体数值正确与否

            //ulong AllValue2 = 0; //计算最后的总值
            //for (int i = 0; i < classnum; i++)  //从第一行到第三行
            //{

            //    AllValue2 += rowValue[i];

            //}
            //MessageBox.Show(AllValue2.ToString (), "矩阵数值结果", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //ulong AllValue3 = 0; //计算最后的总值
            //for (int j = 0; j < classnum; j++)  //从第一行到第三行
            //{

            //    AllValue3 += colValue[j];

            //}
            //MessageBox.Show(AllValue3.ToString(), "矩阵数值结果", MessageBoxButtons.OK, MessageBoxIcon.Information);

            #endregion
            //第二大步：每一类正确分类面积百分比（分类精度）和 总体分类精度:误差矩阵指标

            //第三大步：输出第三矩阵需要的 “分类精度的数组”类别个数+总体的

            dataGridView1.Columns[classnum + 2].Name = "误差矩阵指标"; //最后加的统计一列
            double[] Parameterstatistics = new double[classnum];
            double RightPercentAll=0;

            double[] matrixOut = new double[classnum + 1]; //记住数组个数（4个） 和 数组起始点（从0 1 2 3）不一样


            for (int i = 0; i < classnum; i++)  //添加 矩阵的N*N 数据 哇咔咔！ 注意此时 行数值不变 列要加1
            {
                Parameterstatistics[i] = Convert.ToDouble(inMatrix[i, i]) / Convert.ToDouble(rowValue[i]);
                this.dataGridView1.Rows[i].Cells[classnum + 2].Value = (Math.Round(Parameterstatistics[i],5)*100).ToString()+"%";       

                matrixOut[i] = Parameterstatistics[i]; 
            }
            //计算 总体误差矩阵：公式=正确分类面积之和/所有面积总和
            double CorrectClassAreanTotal = 0;//正确分类面积之和

            for (int i = 0; i < classnum; i++)  //00 11 22 33 44....
            {
                CorrectClassAreanTotal += inMatrix[i, i];
            }

            RightPercentAll = CorrectClassAreanTotal / Convert.ToDouble(AllValue);
            this.dataGridView1.Rows[classnum].Cells[classnum + 2].Value = (Math.Round(RightPercentAll, 5) * 100).ToString() + "%"; ;
            
            matrixOut[classnum] = RightPercentAll;

             return matrixOut;

        }



        public double[] CreatDataGrid2(ulong[,] inMatrix, int classnum)
        {

            dataGridView2.ColumnCount = classnum + 2 + 1;//加2 是“空列字段” 和“总和字段”，加1 是“参数统计字段”

            for (int i = 0; i < classnum + 2; i++)  //添加 列名 第0列空 最后一列 是 “总和” ：多了2列，从0计数
            {
                if (i == 0)
                {
                    dataGridView2.Columns[i].Name = "";
                }
                else
                {
                    if (i == classnum + 1)
                    {
                        dataGridView2.Columns[i].Name = "列总和";
                    }
                    else
                    {
                        dataGridView2.Columns[i].Name = "真实" + i + "类";
                    }
                }

            }

            for (int i = 0; i < classnum + 1; i++)  //添加 行名称  最后一行 是 “总和”： 多了1行，从0计数
            {
                dataGridView2.Rows.Add();

                if (i == classnum)
                {
                    this.dataGridView2.Rows[i].Cells[0].Value = "行总和";
                }
                else
                {
                    this.dataGridView2.Rows[i].Cells[0].Value = "结果" + (i + 1) + "类";
                }

            }



            for (int i = 0; i < classnum; i++)  //添加 矩阵的N*N 数据 哇咔咔！ 注意此时 行数值不变 列要加1
            {
                for (int j = 0; j < classnum; j++)
                {

                    this.dataGridView2.Rows[i].Cells[j + 1].Value = inMatrix[i, j].ToString();

                }


            }


            ulong[] rowValue = new ulong[classnum];  //生成每一行的总和
            for (int i = 0; i < classnum; i++)  //从第一行到第三行
            {
                for (int j = 0; j < classnum; j++)//每行的 从第一列到第三列 相加
                {
                    rowValue[i] += inMatrix[i, j];


                }
                this.dataGridView2.Rows[i].Cells[classnum + 1].Value = rowValue[i].ToString();
            }



            ulong[] colValue = new ulong[classnum];  //生成每一列的总和
            for (int j = 0; j < classnum; j++)  //从第1行到第3行
            {
                for (int i = 0; i < classnum; i++)//每列的 从第1行到第3行 相加
                {
                    colValue[j] += inMatrix[i, j];


                }
                this.dataGridView2.Rows[classnum].Cells[j + 1].Value = colValue[j].ToString();
            }


            ulong AllValue = 0; //计算最后的总值
            for (int i = 0; i < classnum; i++)  //从第一行到第三行
            {
                for (int j = 0; j < classnum; j++)//每行的 从第一列到第三列 相加
                {
                    AllValue += inMatrix[i, j];

                }
                this.dataGridView2.Rows[classnum].Cells[classnum + 1].Value = AllValue.ToString();
            }


            //以上生成完整矩阵，以下为了计算切割精度而计算的每一类正确分类面积百分比 :误差矩阵指标

            dataGridView2.Columns[classnum + 2].Name = "误差矩阵指标"; //最后加的统计一列
            double[] Parameterstatistics = new double[classnum];
            double RightPercentAll = 0;

            //第三大步：输出第三矩阵需要的 指标数组 参数和类别参数
            double[] matrixOut = new double[classnum + 1];

            for (int i = 0; i < classnum; i++)  //添加 矩阵的N*N 数据 哇咔咔！ 注意此时 行数值不变 列要加1
            {
                Parameterstatistics[i] = Convert.ToDouble(inMatrix[i, i]) / Convert.ToDouble(rowValue[i]);
                this.dataGridView2.Rows[i].Cells[classnum + 2].Value = (Math.Round(Parameterstatistics[i], 5) * 100).ToString() + "%";       
                matrixOut[i] = Parameterstatistics[i];
            }

            //计算 总体误差矩阵：公式=正确分类面积之和/所有面积总和
            double CorrectClassAreanTotal = 0;//正确分类面积之和

            for (int i = 0; i < classnum; i++)  //00 11 22 33 44....
            {
                CorrectClassAreanTotal += inMatrix[i, i];
            }

            RightPercentAll = CorrectClassAreanTotal / Convert.ToDouble(AllValue);

            this.dataGridView2.Rows[classnum].Cells[classnum + 2].Value = (Math.Round(RightPercentAll, 5) * 100).ToString() + "%"; 
             matrixOut[classnum ] = RightPercentAll;

       
            return matrixOut;

         
        }


        public void CreatDataGrid3(double[] FirstMatrix, double[] SecondMatrix, int classnum)
        {
            // 添加 列的字段 4列即可
            dataGridView3.ColumnCount = 4;
            dataGridView3.Columns[0].Name  = "";
            dataGridView3.Columns[1].Name = "矩阵1指标";
            dataGridView3.Columns[2].Name = "矩阵2指标";
            dataGridView3.Columns[3].Name = "分割精度";

            //添加 行名称  最后一行 是 “总和”： 多了1行，从0计数
            for (int i = 0; i < classnum + 1; i++)  
            {
                dataGridView3.Rows.Add();

                if (i == classnum)
                {
                    this.dataGridView3.Rows[i].Cells[0].Value = "total";
                }
                else
                {
                    this.dataGridView3.Rows[i].Cells[0].Value = "结果" + (i + 1) + "类";
                }

            }

            //添加 数据
            for (int i = 0; i < classnum+1; i++)  //先固定 每一行
            {
                //this.dataGridView3.Rows[i].Cells[1].Value = FirstMatrix[i].ToString();
                //this.dataGridView3.Rows[i].Cells[2].Value = SecondMatrix[i].ToString();
                //this.dataGridView3.Rows[i].Cells[3].Value = FirstMatrix[i]-SecondMatrix[i];
              //百分号表示
                this.dataGridView3.Rows[i].Cells[1].Value = (Math.Round(FirstMatrix[i], 5) * 100).ToString() + "%";
                this.dataGridView3.Rows[i].Cells[2].Value = (Math.Round(SecondMatrix[i], 5) * 100).ToString() + "%";
                this.dataGridView3.Rows[i].Cells[3].Value = (Math.Round(Math.Abs(FirstMatrix[i] - SecondMatrix[i]), 5) * 100).ToString() + "%";
                
            }



        }











        private void label1_Click(object sender, EventArgs e)
        {

        }



    }
}

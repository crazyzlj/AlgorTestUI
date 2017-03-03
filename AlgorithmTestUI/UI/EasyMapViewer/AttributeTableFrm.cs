using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Text.RegularExpressions; //为了应用isnumber函数 调用的库
using System.Collections;

namespace AlgorithmTest
{
    public partial class AttributeTableFrm : Form
    {
        static DataTable attributeTable; //表格内“数据表”的静态变量 不让他释放  因此可以重复 
                                           //从而实现 从另一个窗体返回继续调用 否则变量生存周期消失  只能是NULL
        
       



        public AttributeTableFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 根据图层字段创建一个只含字段的空DataTable
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static DataTable CreateDataTableByLayer(ILayer pLayer, string tableName)
        {
            if (pLayer is IGroupLayer || pLayer is ICompositeLayer) return null;
            //创建一个DataTable表
            DataTable pDataTable = new DataTable(tableName);

                //取得ITable接口
                ITable pTable = pLayer as ITable;
                IField pField = null;
                DataColumn pDataColumn;
                //根据每个字段的属性建立DataColumn对象
                for (int i = 0; i < pTable.Fields.FieldCount; i++)
                {
                    pField = pTable.Fields.get_Field(i);

                    //新建一个DataColumn并设置其属性
                    pDataColumn = new DataColumn(pField.Name);

                    if (pField.Name == pTable.OIDFieldName)
                    {
                        pDataColumn.Unique = true;//字段值是否唯一
                    }

                    //字段值是否允许为空
                    pDataColumn.AllowDBNull = pField.IsNullable;

                    //字段别名
                    pDataColumn.Caption = pField.AliasName;

                    //字段数据类型
                    pDataColumn.DataType = System.Type.GetType(ParseFieldType(pField.Type));

                    //字段默认值
                    pDataColumn.DefaultValue = pField.DefaultValue;

                    //当字段为String类型是设置字段长度
                    if (pField.VarType == 8)
                    {
                        pDataColumn.MaxLength = pField.Length;
                    }

                    //字段添加到表中
                    pDataTable.Columns.Add(pDataColumn);
                    pField = null;
                    pDataColumn = null;
                }

            return pDataTable;
        }
        private static DataTable CreateDataTableByITable(ITable pTable, string tableName)  //用于表格添加 字段的函数
        {
            //创建一个DataTable表
            DataTable pDataTable = new DataTable(tableName);

            IField pField = null;
            DataColumn pDataColumn;
            //根据每个字段的属性建立DataColumn对象
            for (int i = 0; i < pTable.Fields.FieldCount; i++)
            {
                pField = pTable.Fields.get_Field(i);

                //新建一个DataColumn并设置其属性
                pDataColumn = new DataColumn(pField.Name);

                if (pField.Name == pTable.OIDFieldName)
                {
                    pDataColumn.Unique = true;//字段值是否唯一
                }

                //字段值是否允许为空
                pDataColumn.AllowDBNull = pField.IsNullable;

                //字段别名
                pDataColumn.Caption = pField.AliasName;

                //字段数据类型
                pDataColumn.DataType = System.Type.GetType(ParseFieldType(pField.Type));

                //字段默认值
                pDataColumn.DefaultValue = pField.DefaultValue;

                //当字段为String类型是设置字段长度
                if (pField.VarType == 8)
                {
                    pDataColumn.MaxLength = pField.Length;
                }

                //字段添加到表中
                pDataTable.Columns.Add(pDataColumn);
                pField = null;
                pDataColumn = null;
            }

            return pDataTable;
        }

        /// <summary>
        /// 将GeoDatabase字段类型转换成.Net相应的数据类型
        /// </summary>
        /// <param name="fieldType">字段类型</param>
        /// <returns></returns>
        public static string ParseFieldType(esriFieldType fieldType)
        {
            switch (fieldType)
            {

                case esriFieldType.esriFieldTypeBlob:

                    return "System.String";

                case esriFieldType.esriFieldTypeDate:

                    return "System.DateTime";

                case esriFieldType.esriFieldTypeDouble:

                    return "System.Double";

                case esriFieldType.esriFieldTypeGeometry:

                    return "System.String";

                case esriFieldType.esriFieldTypeGlobalID:

                    return "System.String";

                case esriFieldType.esriFieldTypeGUID:

                    return "System.String";

                case esriFieldType.esriFieldTypeInteger:

                    return "System.Int32";

                case esriFieldType.esriFieldTypeOID:

                    return "System.String";

                case esriFieldType.esriFieldTypeRaster:

                    return "System.String";

                case esriFieldType.esriFieldTypeSingle:

                    return "System.Single";

                case esriFieldType.esriFieldTypeSmallInteger:

                    return "System.Int32";

                case esriFieldType.esriFieldTypeString:

                    return "System.String";

                default:

                    return "System.String";

            }
        }

        /// <summary>
        /// 得到地图服务的某个图层的ID.
        /// </summary>
        /// <param name="pMapServer">地图服务</param>
        /// <param name="pLayer">图层</param>
        /// <param name="dataFrameName">一般用缺省状态""</param>
        /// <returns>图层ID，不存在该图层则返回-1.</returns>
        public static int GetServerLayerID(IMapServer pMapServer, ILayer pLayer,string dataFrameName)
        {
            //缺省状态下dataframe。
            if (dataFrameName == "")
                dataFrameName = pMapServer.DefaultMapName;
            
            int layerID = -1;
            IMapServerInfo pMapServerInfo = new MapServerInfoClass();

            pMapServerInfo = pMapServer.GetServerInfo(dataFrameName);
            IMapLayerInfos layerInfos = pMapServerInfo.MapLayerInfos;
            IMapLayerInfo layerInfo = new MapLayerInfoClass();
            for (int j = 0; j < layerInfos.Count; j++)
            {
                layerInfo = layerInfos.get_Element(j);
                if (layerInfo.Name == pLayer.Name)
                {
                    layerID = j;
                    break;
                }
            }
            return layerID;
        }
        /// <summary> 
        /// 填充DataTable中的数据
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetAttributeDataTable(ILayer pLayer, string tableName,IMapServer pMapServer)
        {
            if (pLayer is IGroupLayer || pLayer is ICompositeLayer) return null;
            //创建空DataTable
            DataTable pDataTable = null;

            //加服务器图层的判断。
            if (pLayer is IMapServerLayer||pLayer is IMapServerGroupLayer||pLayer is IMapServerSublayer)
            {
                if (pMapServer == null) return null;
                //pDataTable = CreateDataTableByLayer(pLayer, tableName, pMapServer,ref layerID);
                int layerID = GetServerLayerID(pMapServer, pLayer, pMapServer.DefaultMapName);
                IRecordSet rSet = pMapServer.QueryFeatureData(pMapServer.DefaultMapName,layerID,null);
                if (!rSet.IsFeatureCollection) return null;
                ITable pTable = rSet.Table;
                //取得图层类型
                IFeatureClass fC = pTable as IFeatureClass;
                string shapeType = getShapeType(fC);

                pDataTable = DealWithDataTable(pTable, shapeType, tableName);
            }
            return pDataTable;
        }
        public static DataTable GetAttributeDataTable(ILayer pLayer, string tableName)  //这个函数 是基本调用的！！ 通过他在调用各种内在函数 可以获得一个 完整的 表格型的变量给 datagridwiew
        {
            if (pLayer is IGroupLayer || pLayer is ICompositeLayer || pLayer is IMapServerLayer || pLayer is IMapServerGroupLayer || pLayer is IMapServerSublayer) 
                return null;

            //创建空DataTable
                 DataTable pDataTable = null; //这是返回用的

            //从ILayer查询到ITable
                 ITable pTable = pLayer as ITable; //这是一个借口的传递 把Ilayr对象 传给 table对象

            //取得图层类型，这个比较重要！ 用于分析 是哪种矢量图层 栅格图层
                string shapeType = getShapeType(pLayer); //从“图层对象”中获取 结果为polygon line point

            pDataTable = DealWithDataTable(pTable, shapeType, tableName);
            return pDataTable;

        }
        public static DataTable CreateDataTable(ILayer pLayer, string tableName)
        {
            if (pLayer is IGroupLayer || pLayer is ICompositeLayer) return null;
            //创建空DataTable
            DataTable pDataTable = null;

            //取得图层类型
            string shapeType = getShapeType(pLayer);

            pDataTable = CreateDataTableByLayer(pLayer, tableName);
            //创建DataTable的行对象
            DataRow pDataRow = null;

            //从ILayer查询到ITable
            ITable pTable = pLayer as ITable;
            ICursor pCursor = pTable.Search(null, false);

            //取得ITable中的行信息
            IRow pRow = pCursor.NextRow();
            int n = 0;

            while (pRow != null)
            {
                //新建DataTable的行对象
                pDataRow = pDataTable.NewRow();

                for (int i = 0; i < pRow.Fields.FieldCount; i++)
                {
                    //如果字段类型为esriFieldTypeGeometry，则根据图层类型设置字段值
                    if (pRow.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        pDataRow[i] = shapeType;
                    }

                    //当图层类型为Anotation时，要素类中会有esriFieldTypeBlob类型的数据，
                    //其存储的是标注内容，如此情况需将对应的字段值设置为Element
                    else if (pRow.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeBlob)
                    {
                        pDataRow[i] = "Element";
                    }
                    else
                    {
                        pDataRow[i] = pRow.get_Value(i);
                    }
                }

                //添加DataRow到DataTable
                pDataTable.Rows.Add(pDataRow);
                pDataRow = null;
                n++;

                //为保证效率，一次只装载最多条记录
                if (n == 3000)
                {
                    pRow = null;
                }
                else
                {
                    pRow = pCursor.NextRow();
                }
            }
            return pDataTable;

        }
        public static DataTable DealWithDataTable(ITable pTable, string shapeType, string tableName)//最终这个大函数 创建一个带表名和 字段名称的 还有内部数据的 表格型变量
        {
            DataTable pDataTable = CreateDataTableByITable(pTable, tableName); //创建一个带表名和 字段名称的 表格型变量
            
            //创建DataTable的行对象
            DataRow pDataRow = null;  //datarow的值是从 prow中 循环获取来的

            ICursor pCursor = pTable.Search(null, false); //指针吧！！？？用于每行 移动的

            //取得ITable中的行信息
            IRow pRow = pCursor.NextRow();
            int n = 0;

            while (pRow != null) //当每一行都有值
            {
                //新建DataTable的行对象
                pDataRow = pDataTable.NewRow();

                for (int i = 0; i < pRow.Fields.FieldCount; i++)
                {
                    //如果字段类型为esriFieldTypeGeometry，则根据图层类型设置字段值
                    if (pRow.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        pDataRow[i] = shapeType;
                    }

                    //当图层类型为Anotation时，要素类中会有esriFieldTypeBlob类型的数据，
                    //其存储的是标注内容，如此情况需将对应的字段值设置为Element
                    else if (pRow.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeBlob)
                    {
                        pDataRow[i] = "Element";
                    }
                    else
                    {
                        pDataRow[i] = pRow.get_Value(i);  //这里获取数值了！ 
                    }
                }

                //添加DataRow到DataTable
                pDataTable.Rows.Add(pDataRow);  //这时候 表格变量:有 表名  和 字段  还有每一行每一列 的数据 了
                pDataRow = null;
                n++;

                //为保证效率，一次只装载最多条记录
                if (n == 2000)  //这里其实 2000都多 
                {
                    pRow = null;
                }
                else
                {
                    pRow = pCursor.NextRow();
                }
            }

            return pDataTable;
        }


        /// <summary>
        /// 获得图层的Shape类型
        /// </summary>
        /// <param name="pLayer">图层</param>
        /// <returns></returns>
        public static string getShapeType(ILayer pLayer)  //获取 图层 类型的函数！ 目前只能处理矢量！  无法处理栅格
        {
            IFeatureLayer pFeatLyr = (IFeatureLayer)pLayer;
            if (pFeatLyr == null) return "";
            return getShapeType(pFeatLyr.FeatureClass);
        }
        public static string getShapeType(IFeatureClass fC) //利用这个可以 返回栅格！ 目前只能返回 点线面 
        {
            switch (fC.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:

                    return "Point";

                case esriGeometryType.esriGeometryPolyline:

                    return "Polyline";

                case esriGeometryType.esriGeometryPolygon:

                    return "Polygon";

                default:

                    return "";
            }
        }

        /// <summary> 
        /// 绑定DataTable到DataGridView!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="player"></param>
        public void CreateAttributeTable(DataTable pDataTable,string tableName)
        {
            attributeTable = pDataTable;

            this.dataGridView1.DataSource = pDataTable;
            this.Text = "属性表[" + tableName + "] " + "记录数：" + pDataTable.Rows.Count.ToString();
        }

        /// <summary>
        /// 替换数据表名中的点.因为DataTable的表名不允许含有“.”，用“_”替换。函数如下：
        /// </summary>
        /// <param name="FCname"></param>
        /// <returns></returns>
        public static string getValidFeatureClassName(string FCname)
        {
            int dot = FCname.IndexOf(".");
            if (dot != -1)
            {
                return FCname.Replace(".", "_");
            }
            return FCname;
        }

        /// <summary>
        /// 返回某个地图服务某个图层的查询结果。
        /// </summary>
        /// <param name="pMapServer">地图服务</param>
        /// <param name="pLayer">图层</param>
        /// <param name="queryFilter">查询条件过滤器,null返回所有。</param>
        /// <returns>要素记录集</returns>
        public static IRecordSet QueryFeatureData(IMapServer pMapServer,ILayer pLayer,IQueryFilter queryFilter)
        {
            int layerID = GetServerLayerID(pMapServer, pLayer, pMapServer.DefaultMapName);
            IRecordSet rSet = pMapServer.QueryFeatureData(pMapServer.DefaultMapName, layerID, queryFilter);
            return rSet;
        }





        //窗体载入****************

        private void AttributeTableFrm_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true; //不可编辑

            attributeTable  = (DataTable)this.dataGridView1.DataSource;
            dataGridView1.Dock = DockStyle.Fill; //可以显示行号

            //DataTable DataSetSendBetweenTwoForm = (DataTable)this.dataGridView1.DataSource;


            //DataGridView = dataGridView1;

            //FormDatagridView2 window = new FormDatagridView2();

            //window.Show(); // Returns immediately

           // FormDatagridView2 FormDatagridView2 = new Form(AttributeTableFrm);

            

        }


        /**/
        /// <summary>  
        /// 判断一个字符串是否为合法数字(指定整数位数和小数位数)  
        /// </summary>  
        /// <param name="s">字符串</param>  
        /// <param name="precision">整数位数</param>  
        /// <param name="scale">小数位数</param>  
        /// <returns></returns>  
        public bool IsNumber(string s, int precision, int scale)    //函数 判断是否是正整数 不是则返回 从而换行输出矩阵
        {
            if ((precision == 0) && (scale == 0))
            {
                return false;
            }
            string pattern = @"(^\d{1," + precision + "}";
            if (scale > 0)
            {
                pattern += @"\.\d{0," + scale + "}$)|" + pattern;
            }
            pattern += "$)";
            return System.Text.RegularExpressions.Regex.IsMatch(s, pattern);
        }  


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        public string[] LayerFields(DataTable inputtable)  //函数 计算字段组
        {


            string[] returnstring = new string[inputtable.Columns.Count];


            for (int i = 0; i < inputtable.Columns.Count; i++)
            {

                if (inputtable.Columns[i].ColumnName == null)
                {
                }
                else
                {
                    returnstring[i] = inputtable.Columns[i].ColumnName;
                }

            }

            return returnstring;

        }


        public ulong[,] CaluateErrorMatrixSecond(int X, string ResultClassField, string RealGroundField, string StatisticField,string OnlyIdField)
        {
            DataTable dt = attributeTable; //操作的属性表
            ulong[,] Matrix = new ulong[X, X]; //这里X代表的数组的维数 （3*3） 但是数组下标是从0~2  区分这个概念OK！

            ArrayList OnlyID = new System.Collections.ArrayList(); //ArrayList 类 第一次用

            for (int i = 0; i < dt.Rows.Count; i++) //1.首先把分类结果的每个独立ID找出来，这样才能对每一块判断最大面积
             {
                if (!OnlyID.Contains(Convert.ToInt32(dt.Rows[i][OnlyIdField].ToString()))) //contains是一个查询函数 函数
               {
                   OnlyID.Add ( Convert.ToInt32(dt.Rows[i][OnlyIdField].ToString()));
               }
            }


            for(int j=0;j<OnlyID.Count ;j++)  //2.对每一个裁剪前的结果类别小版块 进行计算，找出最大面积的小版块的属性 并赋给最终二维矩阵（属性 和 总面积）
            {
                int MatrixFirstNum=0; //最大面积对应的类别 编号 class class_1  输出的
                int MatrixSecondNum=0;

                ulong maxarea = 0;  //最大面积 用于确定MatrixFirstNum，MatrixLastNum 
                ulong areaplusing = 0;//中间计算相加的面积
                ulong areaSmallClass=0; //每个小类别版块 的总面积   输出的


                for (int i = 0; i < dt.Rows.Count; i++)   //循环表的 所有行
                {
                    int classIDonly; //表中字段的ID   
                    classIDonly = Convert.ToInt32(dt.Rows[i][OnlyIdField].ToString());

                    if (Convert.ToInt32(OnlyID[j].ToString()) == classIDonly) //如果OnlyIdField字段 和 唯一ID数据组【j】 吻合 
                                                                //则需要统计 最大面积的class 和class_1 以及面积总和
                    {
                        areaplusing = Convert.ToUInt64(dt.Rows[i][StatisticField]);
                        if (Convert.ToUInt64(dt.Rows[i][StatisticField]) > maxarea)//找最大面积
                        {
                             maxarea = Convert.ToUInt64(dt.Rows[i][StatisticField]); //更换最大面积
                             MatrixFirstNum = Convert.ToInt32(dt.Rows[i][ResultClassField]); //更换最大面积对应的类别编号
                             MatrixSecondNum = Convert.ToInt32(dt.Rows[i][RealGroundField]);
                        }
                        areaSmallClass += areaplusing; //计算面积和
                    }
                }

                Matrix[MatrixFirstNum - 1, MatrixSecondNum - 1] += areaSmallClass;// 为矩阵加入数值 注意顺序 行和列的区分！此时 行为结果 列为真实
            }
            
            return Matrix;
        }



    }
}

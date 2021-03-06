using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using System.Collections.Generic;
using ESRI.ArcGIS.DataSourcesRaster;  //为了打开栅格数据 及操作
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Output; //地图输出
using ESRI.ArcGIS.DataManagementTools;


namespace AlgorithmTest
{
    public sealed partial class EasyMapViewer : Form
    {
        #region 定义成员变量。

        private ESRI.ArcGIS.Controls.IMapControl3 m_mapControl = null; //定义一个MAP 控件型变量 方便判断地图是否改变或者为空 
        private ESRI.ArcGIS.Controls.IPageLayoutControl2 m_pageLayoutControl = null; //定义一个pagelayout 控件型变量 
        private IMapDocument pMapDocument;    //地图文档型变量
        private ControlsSynchronizer m_controlsSynchronizer = null;

        private string sMapUnits;

        //TOCControl控件变量 
        private ITOCControl2 m_tocControl = null;

        //TOCControl中Map菜单
        private IToolbarMenu m_menuMap = null;  //这是 地图右键 弹出的菜单型变量  完全代码实现的！
        //TOCControl中图层菜单
        private IToolbarMenu m_menuLayer = null; //这是 图例邮件 弹出的菜单型变量

        private string m_mapDocumentName = string.Empty;

        //当前toccontrol中选中查看属性表的图层。
        private ILayer m_layer = null;    //****这个重要 可以 获取该图层的属性！


        //为闪烁做钩子。
        HookHelperClass m_hookHelper = null;

        //图层变量引用。
        private List<Tools.CSLayer> m_nodeLayerList = null;//节点图层
        private List<Tools.CSLayer> m_groupLayerList = null;//图层组
        private List<Tools.CSLayer> m_layerList = null;//所有图层集合

        //当前连接的地图服务。
        private IMapServer m_mapServer = null;

        public esriTOCControlEdit LabelEdit { get; set; }

        static DataTable attributeTable; //表格内“数据表”的静态变量 不让他释放  因此可以重复 
        //从而实现 从另一个窗体返回继续调用 否则变量生存周期消失  只能是NULL
        
        #endregion

        #region 构造函数 主窗口调用的InitializeComponent()。
        public EasyMapViewer()
        {
            InitializeComponent();
        }
        #endregion

        #region 菜单栏  按钮的事件处理。
        
        //  菜单按钮 1. 新建 地图文档 ---------------------------------------------------------------------------------------------------------
        private void menuNewDoc_Click(object sender, EventArgs e) 
        {
            ////execute New Document command
            //ICommand command = new CreateNewDocument();
            //command.OnCreate(m_mapControl.Object);
            //command.OnClick();

            //询问是否保存当前地图
            if (this.axMapControl1.LayerCount > 0)//细节处理：如果存在地图文档 需要保存再打开
            {
                DialogResult res = MessageBox.Show("是否保存当前地图?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//弹出提示窗口msgbox

                if (res == DialogResult.Yes) //如果要保存，调用另存为对话框
                {

                    ICommand command = new ControlsSaveAsDocCommandClass(); //类的作用：Saves current map document to a new file，
                    //接口（Interfaces）是：ICommand,可被创建对象实例，包含若干属性、方法事件
                    if (m_mapControl != null)  //如果 MAP控件型变量 为null空值 做赋予它当前地图的变量
                        command.OnCreate(m_controlsSynchronizer.MapControl.Object);
                    else
                        command.OnCreate(m_controlsSynchronizer.PageLayoutControl.Object); // 执行command对象实例的OnCreate方法： Occurs when this command is created. 
                    command.OnClick();
                }
                //创建新的地图实例
                IMap map = new MapClass();
                map.Name = "Map";
                m_controlsSynchronizer.MapControl.DocumentFilename = string.Empty;
                //更新新建地图实例的共享地图文档
                m_controlsSynchronizer.ReplaceMap(map);

                menuSaveDoc.Enabled = true;  //保存按钮可用
            }
        }

        //  菜单按钮 2. 打开 地图文档 ---------------------------------------------------------------------------------------------------------
        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            ////execute Open Document command
            //ICommand command = new ControlsOpenDocCommandClass();
            //command.OnCreate(m_mapControl.Object);
            //command.OnClick();

            //if (this.axMapControl1.LayerCount > 0)//细节处理：如果存在地图文档 需要保存再打开
            //{
            //    DialogResult result = MessageBox.Show("是否保存当前地图？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //    if (result == DialogResult.Cancel) return;
            //    if (result == DialogResult.Yes) this.menuSaveDoc_Click(null, null);
            //}
            //询问是否保存当前地图

            if (this.axMapControl1.LayerCount > 0)//细节处理：如果存在地图文档 需要保存再打开
            {
                DialogResult res = MessageBox.Show("是否保存当前地图?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);//弹出提示窗口msgbox

                if (res == DialogResult.Yes) //如果要保存，调用另存为对话框
                {

                    ICommand command = new ControlsSaveAsDocCommandClass(); //类的作用：Saves current map document to a new file，
                    //接口（Interfaces）是：ICommand,可被创建对象实例，包含若干属性、方法事件
                    if (m_mapControl != null)  //如果 MAP控件型变量 为null空值 做赋予它当前地图的变量
                        command.OnCreate(m_controlsSynchronizer.MapControl.Object);
                    else
                        command.OnCreate(m_controlsSynchronizer.PageLayoutControl.Object); // 执行command对象实例的OnCreate方法： Occurs when this command is created. 
                        command.OnClick();
                }

            }
            OpenNewMapDocument openMapDoc = new OpenNewMapDocument(m_controlsSynchronizer);
            openMapDoc.OnCreate(m_controlsSynchronizer.MapControl.Object);
            openMapDoc.OnClick();
            
            menuSaveDoc.Enabled = true;  //保存按钮可用
        }
        //  菜单按钮 3. 保存 地图文档 ---------------------------------------------------------------------------------------------------------
        private void menuSaveDoc_Click(object sender, EventArgs e) //有问题！！
        {
            ////execute Save Document command
           
            //if (null != m_pageLayoutControl.DocumentFilename && m_mapControl.CheckMxFile(m_mapDocumentName))
            //{
            //    //create a new instance of a MapDocument
            //    IMapDocument mapDoc = new MapDocumentClass();
            //    mapDoc.Open(m_mapDocumentName, string.Empty);

            //    //Make sure that the MapDocument is not readonly
            //    if (mapDoc.get_IsReadOnly(m_mapDocumentName))
            //    {
            //        MessageBox.Show("地图文档是只读的!");
            //        mapDoc.Close();
            //        return;
            //    }

            //    //Replace its contents with the current map
            //    mapDoc.ReplaceContents((IMxdContents)m_pageLayoutControl.PageLayout);

            //    //save the MapDocument in order to persist it
            //    mapDoc.Save(mapDoc.UsesRelativePaths, false);

            //    //close the MapDocument
            //    mapDoc.Close();
            //}
            this.menuSaveAs_Click(null, null);  //保存有问题 无法运行！且 AE的自定义工具中也没保存 只有另存为！

        }
        //  菜单按钮 4. 另存为 地图文档 ---------------------------------------------------------------------------------------------------------
        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }
        //  菜单按钮 5. 退出  ---------------------------------------------------------------------------------------------------------
        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }

        private void 打开shpToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string[] ShapeFile = OpenShapeFile();   //方法2 代码方式

            if (ShapeFile[0] != null && ShapeFile[1] != null)   //细节处理！如果没有输入
            {
                axMapControl1.AddShapeFile(ShapeFile[0], ShapeFile[1]);
                axMapControl2.AddShapeFile(ShapeFile[0], ShapeFile[1]);

                //刷新代码 不出错 但是 无法让map1 和 2的显示是一个颜色，同时图例改变也不能反应到map2 ；重点是map2要与map1的全图一样！！ 
                m_controlsSynchronizer.ActivatePageLayout(); 
                m_controlsSynchronizer.ActivateMap();
               
                this.axMapControl1.ActiveView.Refresh();
                this.axTOCControl1.ActiveView.Refresh();
                this.axMapControl2.ActiveView.Refresh();
                this.axTOCControl1.Refresh();
                this.axMapControl1.Refresh ();
                this.axMapControl2 .Refresh();

                //m_controlsSynchronizer.ActivatePageLayout();
                //m_controlsSynchronizer.ActivateMap();

                //this.axMapControl1.ActiveView.Refresh();
                //this.axTOCControl1.Refresh();
                //this.axMapControl2.ActiveView.Refresh();


                //if (this.tabControl1.SelectedIndex == 0)  //此处用于刷新 图例！！ 先是1 不出错哦~~2011.10.11
                //{
                //    this.axToolbarControl1.Visible = true;
                //    this.axToolbarControl2.Visible = false;
                //    this.axToolbarControl1.Dock = DockStyle.Fill;

                //    //激活MapControl
                //    m_controlsSynchronizer.ActivateMap();
                //}
                //else if (this.tabControl1.SelectedIndex == 1)
                //{
                //    this.axToolbarControl1.Visible = false;
                //    this.axToolbarControl2.Visible = true;
                //    this.axToolbarControl2.Location = this.axToolbarControl1.Location;
                //    this.axToolbarControl2.Size = this.axToolbarControl1.Size;
                //    this.axToolbarControl2.Dock = DockStyle.Fill;

                //    //激活PageLayoutControl
                //    m_controlsSynchronizer.ActivatePageLayout();
                //}
            } 
             
        }

        private void 打开任意格式ToolStripMenuItem_Click(object sender, EventArgs e)  //BUG有问题！ 如何让鹰眼一起显示？？
        {
            ICommand pAddData = new ControlsAddDataCommandClass();  //方法1： 调用ARcgis工具栏

            pAddData.OnCreate(axMapControl1.Object);
            
            pAddData.OnClick();
            try
            {
                AddLayerToOverViewMap();  //这个函数很有用 把map1的内容添加到map2 解决了问题！
            }
            catch
            {
                 MessageBox.Show("载入的是栅格文件，暂时无法在鹰眼显示，保存地图文档后再次载入可以显示！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Error);      

            }
            //刷新代码 不出错 但是 无法让map1 和 2的显示是一个颜色，同时图例改变也不能反应到map2 ；重点是map2要与map1的全图一样！！ 
            m_controlsSynchronizer.ActivatePageLayout();
            m_controlsSynchronizer.ActivateMap();

         
            this.axMapControl1.ActiveView.Refresh();
            this.axTOCControl1.ActiveView.Refresh();
            this.axMapControl2.ActiveView.Refresh();
            this.axTOCControl1.Refresh();
            this.axMapControl1.Refresh();
            this.axMapControl2.Refresh();

        }
        #endregion

        #region 窗体 事件处理。
        //1. 事件一： 主窗体（MapViewer）载入事件
        private void MapViewer_Load(object sender, EventArgs e)
        {

            //用代码设置伙伴控件更保险
            axTOCControl1.SetBuddyControl(axMapControl1);
            axToolbarControl1.SetBuddyControl(axMapControl1);
            //禁用鹰眼MapControl的滚轮放大缩小功能
            axMapControl2.AutoMouseWheel = false;


            sMapUnits = "未知";
            //开始 就是各种赋值 去除NULL（=XXXobject；=new 类）-----------------------------------------------------------------------------------
            m_tocControl = (ITOCControl2)this.axTOCControl1.Object; // 图例控件1：开始为声明的几个对象性变量（此时null） 赋予对象 方便调用
            m_mapControl = (IMapControl3)this.axMapControl1.Object;// 地图显示控件1
            m_pageLayoutControl = (IPageLayoutControl2)this.axPageLayoutControl1.Object;// 绘制专题地图控件1
            m_menuMap = new ToolbarMenuClass();  // 同理，地图工具栏控件 一共二个 
            m_menuLayer = new ToolbarMenuClass();
           
            //添加自定义菜单项到TOCCOntrol的Map菜单中！！是图例控件中的 菜单！非常有用！！ --------------------------------------------------------------------------------------- 
            
            
            //打开文档菜单
            m_menuMap.AddItem(new OpenNewMapDocument(m_controlsSynchronizer), -1, 0, false, esriCommandStyles.esriCommandStyleIconAndText);
            //添加数据菜单
            m_menuMap.AddItem(new ControlsAddDataCommandClass(), -1, 1, false, esriCommandStyles.esriCommandStyleIconAndText);
            //打开全部图层菜单
            m_menuMap.AddItem(new LayerVisibility(), 1, 2, false, esriCommandStyles.esriCommandStyleTextOnly);
            //关闭全部图层菜单
            m_menuMap.AddItem(new LayerVisibility(), 2, 3, false, esriCommandStyles.esriCommandStyleTextOnly);

            //以二级菜单的形式添加内置的“选择”菜单
            //m_menuMap.AddSubMenu("esriControls.ControlsFeatureSelectionMenu", 4, true);
            m_menuMap.AddSubMenu("esriControls.ControlsFeatureSelectionMenu", 4, true);

            //以二级菜单的形式添加内置的“地图浏览”菜单
            m_menuMap.AddSubMenu("esriControls.ControlsMapViewMenu", 5, true);

           
            //添加自定义菜单项到TOCCOntrol的图层菜单中
            m_menuLayer = new ToolbarMenuClass();
            //添加“移除图层”菜单项
            m_menuLayer.AddItem(new RemoveLayer(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            //添加“放大到整个图层”菜单项
            m_menuLayer.AddItem(new ZoomToLayer(), -1, 1, true, esriCommandStyles.esriCommandStyleTextOnly);

            //设置菜单的Hook-------------------------------------------------------------------------------------------------------------------
            m_menuLayer.SetHook(m_mapControl);
            m_menuMap.SetHook(m_mapControl);

            
            
            //初始化controls synchronization calss 不懂？？----------------------------------------------------------------------------------------------
            m_controlsSynchronizer = new ControlsSynchronizer(m_mapControl, m_pageLayoutControl);

            //把MapControl和PageLayoutControl绑定起来(两个都指向同一个Map),然后设置MapControl为活动的Control-----------------------------------------
            m_controlsSynchronizer.BindControls(true);
            //为了在切换MapControl和PageLayoutControl视图同步，要添加Framework Control
            m_controlsSynchronizer.AddFrameworkControl(axToolbarControl1.Object);
            m_controlsSynchronizer.AddFrameworkControl(this.axTOCControl1.Object);

            // 添加打开命令按钮到工具条  代码实现的菜单栏操作！----------------------------------------------------------------------------------------------------------
            OpenNewMapDocument openMapDoc = new OpenNewMapDocument(m_controlsSynchronizer);
            axToolbarControl1.AddItem(openMapDoc, -1, 0, false, -1, esriCommandStyles.esriCommandStyleIconOnly);
            //disable the Save menu (since there is no document yet)
            menuSaveDoc.Enabled = false;

            //绑定制图工具条2 和 制图输出控件---------------------------------------------------------------------------------------------------
            this.axToolbarControl2.SetBuddyControl(this.axPageLayoutControl1);

            //测试新控件。SelectFeatureByLocation ？？？没有用！！----------------------------------------------------------------------------
            //SelectFeatureByLocation sfbl = new SelectFeatureByLocation();
            //this.axToolbarControl1.AddItem(sfbl,-1,0,false,-1,esriCommandStyles.esriCommandStyleIconOnly);
            

            //为闪烁做钩子。 不懂 干什么的？？---------------------------------------------------------------------------------------------------
            m_hookHelper = new HookHelperClass();
            
            //maptips。 地图窗口的 提示功能打开 并且设置显示格式---------------------------------------------------------------------------------------------------------------------
            m_mapControl.ShowMapTips = true;
            m_mapControl.TipDelay = 1000;
            m_mapControl.TipStyle = esriTipStyle.esriTipStyleSolid;

            //标签编辑，手动。自动？？----------------------------------------------------------------------------------------------------------------------
            axTOCControl1.LabelEdit = esriTOCControlEdit.esriTOCControlManual;
             //axTOCControl1.LabelEdit = esriTOCControlEdit.esriTOCControlAutomatic;

            //以下函数着书上的代码敲过一个鹰眼的例子，但是只能同步通过mxd文件加载进来的图层，
            //当单独加载一个图层进来时，鹰眼中没有相应的加进来这个图层，今天学习到通过一个事件来达到这个效果，跟大家分享一下。
            //主要是通过一个事件ItemAdded来实现的。新加进来一个图层就会触发ItemAdded事件，
            //通过委托再加进自己写的一个方法，这个方法就是将新加进来的图层加到鹰眼中。
            IMap pMap;
            pMap = axMapControl1.Map;//主视图
            IActiveViewEvents_Event pAE;
            pAE = pMap as IActiveViewEvents_Event;
            pAE.ItemAdded += new IActiveViewEvents_ItemAddedEventHandler(this.OnItemAdded); //此函数在代码最后面 后加入的
            
        }

        //2. 事件二： 主窗体（MapViewer）关闭事件
        private void MapViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
        }

        //3. 事件三： 地图窗口1的（axMapControl1）OnMapReplaced 事件  **实现 鹰眼和maptips
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //首先  刷新图层列表变量的内容。
            m_layerList = Tools.GetLayerListByMapReturnAll(axMapControl1.Map, ref m_nodeLayerList, ref m_groupLayerList);

            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                menuSaveDoc.Enabled = false;
                CoordinateLabel.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                menuSaveDoc.Enabled = true;
                CoordinateLabel.Text = System.IO.Path.GetFileName(m_mapDocumentName);
            }

            // 当主地图显示控件的地图更换时，鹰眼中的地图也跟随更换 
            this.axMapControl2.Map = new MapClass();
            // 添加主地图控件中的所有图层到鹰眼控件中 
            AddLayerToOverViewMap();

            //for (int i = 1; i <= this.axMapControl1.LayerCount; i++)
            //{
            //    this.axMapControl2.AddLayer(this.axMapControl1.get_Layer(this.axMapControl1.LayerCount - i));
            //}
       

            // 设置 MapControl 显示范围至数据的全局范围 
            this.axMapControl2.Extent = this.axMapControl1.FullExtent;
            // 刷新鹰眼控件地图 
            this.axMapControl2.Refresh();

            //maptips
            SetMapTips(axMapControl1);
        }

        //4. 事件四： 地图窗口1的（axMapControl1）鼠标移动 事件  **实现 状态栏的 比例尺 和 坐标显示
        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            // 显示当前比例尺 
            ScaleLabel.Text = " 比例尺 1:" + ((long)this.axMapControl1.MapScale).ToString();
            // 显示当前坐标 
            CoordinateLabel.Text = " 当前坐标 X = " + e.mapX.ToString("#######.##") + " Y = " + e.mapY.ToString("#######.##") + " " + this.axMapControl1.MapUnits.ToString().Substring(4);
            //CoordinateLabel.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
            //CoordinateLabel.Text = m_mapControl.ActiveView.get_TipText(e.mapX, e.mapY);
        }

        //5. 事件五： 地图窗口1的（axMapControl1）范围变化 事件  **实现 鹰眼 和 比例尺变化状态栏也变化
        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            //地图比例尺变化了，状态栏就要跟着变。
            // 显示当前比例尺 
            ScaleLabel.Text = " 比例尺 1:" + ((long)this.axMapControl1.MapScale).ToString();

            // 得到新范围 

            IEnvelope pEnv = (IEnvelope)e.newEnvelope;

            IGraphicsContainer pGra = axMapControl2.Map as IGraphicsContainer;

            IActiveView pAv = pGra as IActiveView;

            // 在绘制前，清除 axMapControl2 中的任何图形元素 

            pGra.DeleteAllElements();

            IRectangleElement pRectangleEle = new RectangleElementClass();

            IElement pEle = pRectangleEle as IElement;

            pEle.Geometry = pEnv;

            // 设置鹰眼图中的红线框 

            IRgbColor pColor = new RgbColorClass();

            pColor.Red = 255;

            pColor.Green = 0;

            pColor.Blue = 0;

            pColor.Transparency = 255;

            // 产生一个线符号对象 

            ILineSymbol pOutline = new SimpleLineSymbolClass();

            pOutline.Width = 2;

            pOutline.Color = pColor;

            // 设置颜色属性 

            pColor = new RgbColorClass();

            pColor.Red = 255;

            pColor.Green = 0;

            pColor.Blue = 0;

            pColor.Transparency = 0;

            // 设置填充符号的属性 

            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();

            pFillSymbol.Color = pColor;

            pFillSymbol.Outline = pOutline;

            IFillShapeElement pFillShapeEle = pEle as IFillShapeElement;

            pFillShapeEle.Symbol = pFillSymbol;

            pGra.AddElement((IElement)pFillShapeEle, 0);

            // 刷新 

            pAv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        //6. 事件六： 地图窗口1的（axMapControl1）鼠标按下 事件  **实现 右键弹出m_menuMap菜单
        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {
                //弹出右键菜单
                m_menuMap.PopupMenu(e.x, e.y, m_mapControl.hWnd);
            }
        }

        //7. 事件七： 地图窗口2的（axMapControl2）鼠标按下 事件  **实现 在鹰眼窗口左右键 小矩形对 大地图的反操作
        private void axMapControl2_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (this.axMapControl2.Map.LayerCount != 0)
            {
                // 按下鼠标左键移动矩形框 

                if (e.button == 1)
                {

                    IPoint pPoint = new PointClass();

                    pPoint.PutCoords(e.mapX, e.mapY);

                    IEnvelope pEnvelope = this.axMapControl1.Extent;

                    pEnvelope.CenterAt(pPoint);

                    this.axMapControl1.Extent = pEnvelope;

                    this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

                }

                // 按下鼠标右键绘制矩形框 

                else if (e.button == 2)
                {

                    IEnvelope pEnvelop = this.axMapControl2.TrackRectangle();

                    this.axMapControl1.Extent = pEnvelop;

                    this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

                }

            } 
        }

        //8. 事件八： 地图窗口2的（axMapControl2）鼠标移动 事件  **实现 地图 随鼠标移动而动
        private void axMapControl2_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            // 如果不是左键按下就直接返回 
            if (e.button != 1) return;
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(e.mapX, e.mapY);
            this.axMapControl1.CenterAt(pPoint);
            this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        //9. 事件九： 图例控件的（axTOCControl1）鼠标按下事件  **  在图例中 右键弹出菜单功能
        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            #region 不要处理左键事件，这样会导致编辑label的事件不能被捕获。
            //if (e.button == 1)
            //{
            //    esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            //    IBasicMap map = null;
            //    ILayer layer = null;
            //    object other = null;
            //    object index = null;

            //    //判断所选菜单的类型
            //    m_tocControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);

            //    //确定选定的类型，Map或是图层
            //    if (item == esriTOCControlItem.esriTOCControlItemMap)
            //        m_tocControl.SelectItem(map, null);
            //    else
            //    {
            //        m_tocControl.SelectItem(layer, null);
            //    }
            //}
            //如果不是右键按下直接返回
            //else 
            #endregion
            if (e.button == 2)
            {
                esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap map = null;
                ILayer layer = null;
                object other = null;
                object index = null;

                //判断所选菜单的类型
                m_tocControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);

                //确定选定的菜单类型，Map或是图层菜单
                if (item == esriTOCControlItem.esriTOCControlItemMap)
                    m_tocControl.SelectItem(map, null);
                else if (item == esriTOCControlItem.esriTOCControlItemLayer)
                    m_tocControl.SelectItem(layer, null);

                //设置CustomProperty为layer (用于自定义的Layer命令)                   
                m_mapControl.CustomProperty = layer;

                //弹出右键菜单
                if (item == esriTOCControlItem.esriTOCControlItemMap)
                    m_menuMap.PopupMenu(e.x, e.y, m_tocControl.hWnd);

                else if (item == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    if (m_menuLayer.Count >= 4) //加过菜单项的要删除以前的。
                    {
                        //移除OpenAttributeTable菜单项，以防止重复添加
                        m_menuLayer.Remove(3);
                        m_menuLayer.Remove(2);
                    }

                    #region 测试。
                    //string s = "";
                    //if (layer is IMapServerGroupLayer)
                    //    s += "IMapServerGroupLayer";
                    //if (layer is IMapServerLayer)
                    //    s += "IMapServerLayer";
                    //if (layer is IMapServerData)
                    //    s += "IMapServerData";
                    //if (layer is IMapServerDataAccess)
                    //    s += "IMapServerDataAccess";
                    //if (layer is IMapServerFindResult)
                    //    s += "IMapServerFindResult";
                    //if (layer is IMapServerFindResults)
                    //    s += "IMapServerFindResults";
                    //if (layer is IMapServerIdentifyObject)
                    //    s += "IMapServerIdentifyObject";
                    //if (layer is IMapServerIdentifyResult)
                    //    s += "IMapServerIdentifyResult";
                    //if (layer is IMapServerIdentifyResults)
                    //    s += "IMapServerIdentifyResults";
                    //if (layer is IMapServerInfo)
                    //    s += "IMapServerInfo";
                    //if (layer is IMapServerInit)
                    //    s += "IMapServerInit";
                    //if (layer is IMapServerLayout)
                    //    s += "IMapServerLayout";
                    //if (layer is IMapServerLegendClass)
                    //    s += "IMapServerLegendClass";
                    //if (layer is IMapServerLegendGroup)
                    //    s += "IMapServerLegendGroup";
                    //if (layer is IMapServerLegendInfo)
                    //    s += "IMapServerLegendInfo";
                    //if (layer is IMapServerLegendPatch)
                    //    s += "IMapServerLegendPatch";
                    //if (layer is IMapServerRow)
                    //    s += "IMapServerRow";
                    //if (layer is IMapServerRows)
                    //    s += "IMapServerRows";
                    //if (layer is IMapServerSublayer)
                    //    s += "IMapServerSublayer";
                    //if (layer is IMapTableDescription)
                    //    s += "IMapTableDescription";
                    //if (layer is IMapTableInfo)
                    //    s += "IMapTableInfo";
                    //MessageBox.Show(s);
                    #endregion

                    //动态添加OpenAttributeTable菜单项
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    OpenAttributeTable openAttributeTable = new OpenAttributeTable(layer, this.dataGridView1, this.tabPageProperty);
                    m_menuLayer.AddItem(openAttributeTable, -1, 2, true, esriCommandStyles.esriCommandStyleTextOnly);
                    openAttributeTable = new OpenAttributeTable(layer,m_mapServer);
                    m_menuLayer.AddItem(openAttributeTable, -1, 3, true, esriCommandStyles.esriCommandStyleTextOnly);

                    this.m_layer = layer;
                    m_menuLayer.PopupMenu(e.x, e.y, m_tocControl.hWnd);
                }
                else if (item == esriTOCControlItem.esriTOCControlItemHeading)
                {
                    MessageBox.Show("heading:"+this.axMapControl1.LayerCount);
                }
                else if (item == esriTOCControlItem.esriTOCControlItemLegendClass) // ？？？？如何加入地图符号？
                {
                    MessageBox.Show("lengendClass");
                }
            }
        }

        //10. 事件十： 图例控件的（axTOCControl1）OnEndLabelEdit事件  **  实现 禁止在编辑标签时键入空字串
        private void axTOCControl1_OnEndLabelEdit(object sender, ITOCControlEvents_OnEndLabelEditEvent e)
        {
            // 禁止在编辑标签时键入空字串
            string newLabel = e.newLabel;
            if (newLabel.Trim() == "")
            {
                e.canEdit = false;
            }
        }

        //11. 事件十一： 图例控件的（axTOCControl1）左键双击 事件  **  实现 地图符号选择器的功能！！哈哈 解决了
        private void axTOCControl1_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {

            #region 符号选择器一，调用arcgisDesktop选择器 在10下貌似不好使
            //esriTOCControlItem toccItem = esriTOCControlItem.esriTOCControlItemNone;
            //ILayer iLayer = null;
            //IBasicMap iBasicMap = null;
            //object unk = null;
            //object data = null;

            //if (e.button == 1)
            //{
            //    axTOCControl1.HitTest(e.x, e.y, ref toccItem, ref iBasicMap, ref iLayer, ref unk, ref data);
            //    System.Drawing.Point pos = new System.Drawing.Point(e.x, e.y);
            //    if (toccItem == esriTOCControlItem.esriTOCControlItemLegendClass)
            //    {
            //        ESRI.ArcGIS.Carto.ILegendClass pLC = new LegendClassClass();
            //        ESRI.ArcGIS.Carto.ILegendGroup pLG = new LegendGroupClass();
            //        if (unk is ILegendGroup)
            //        {
            //            pLG = (ILegendGroup)unk;
            //        }
            //        pLC = pLG.get_Class((int)data);
            //        ISymbol pSym;
            //        pSym = pLC.Symbol;
            //        ESRI.ArcGIS.DisplayUI.ISymbolSelector pSS = new ESRI.ArcGIS.DisplayUI.SymbolSelectorClass();

            //        bool bOK = false;

            //        pSS.AddSymbol(pSym);
            //        bOK = pSS.SelectSymbol(0);
            //        if (bOK)
            //        {
            //            pLC.Symbol = pSS.GetSymbolAt(0);
            //        }

            //        this.axMapControl1.ActiveView.Refresh();
            //        this.axTOCControl1.Refresh();
            //    }
            //}
            #endregion //在10中不好使


            //10.0版本。重新写SymbolSelectorFrm类（一个窗体）。--------------------------------------------------------------------------------

            #region 符号选择器二，AE自定义实现

            esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap basicMap = null;
            ILayer layer = null;
            object unk = null;
            object data = null;
            axTOCControl1.HitTest(e.x, e.y, ref itemType, ref basicMap, ref layer, ref unk, ref data);

            if (e.button == 1)
            {
                if (itemType == esriTOCControlItem.esriTOCControlItemLegendClass)
                {
                    //取得图例
                    ILegendClass pLegendClass = ((ILegendGroup)unk).get_Class((int)data);

                    //创建符号选择器SymbolSelector实例 ** 实现调用了SymbolSelectorFrm 这个窗体（也是一个类）
                    SymbolSelectorFrm SymbolSelectorFrm = new SymbolSelectorFrm(pLegendClass, layer);

                    if (SymbolSelectorFrm.ShowDialog() == DialogResult.OK)
                    {
                        //局部更新主Map控件
                        m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

                        //设置新的符号
                        pLegendClass.Symbol = SymbolSelectorFrm.pSymbol;

                        //更新主Map控件和图层（图例）控件
                        this.axMapControl1.ActiveView.Refresh();
                        this.axTOCControl1.Refresh();
                    }
                }
            }
            #endregion
        }

        //12. 事件十二： 地图工具栏控件1的（axToolbarControl1）鼠标移动 事件  **  用于 获取当前图标的 基本信息
        private void axToolbarControl1_OnMouseMove(object sender, IToolbarControlEvents_OnMouseMoveEvent e)
        {
            // 取得鼠标所在工具的索引号 
            int index = axToolbarControl1.HitTest(e.x, e.y, false);
            if (index != -1)
            {
                // 取得鼠标所在工具的 ToolbarItem 
                IToolbarItem toolbarItem = axToolbarControl1.GetItem(index);
                // 设置状态栏信息 
                MessageLabel.Text = toolbarItem.Command.Message;
            }
            else
            {
                MessageLabel.Text = " 就绪 ";
            } 
        }

        //13. 事件十三： 切换 控件1的（tabControl1）选择项变化 事件  **  用于 激活MapControl 或者 激活PageLayoutControl
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                this.axToolbarControl1.Visible = true;
                this.axToolbarControl2.Visible = false;
                this.axToolbarControl1.Dock = DockStyle.Fill;

                //激活MapControl
                m_controlsSynchronizer.ActivateMap();
            }
            else if (this.tabControl1.SelectedIndex == 1)
            {
                this.axToolbarControl1.Visible = false;
                this.axToolbarControl2.Visible = true;
                this.axToolbarControl2.Location = this.axToolbarControl1.Location;
                this.axToolbarControl2.Size = this.axToolbarControl1.Size;
                this.axToolbarControl2.Dock = DockStyle.Fill;

                //激活PageLayoutControl
                m_controlsSynchronizer.ActivatePageLayout();
            }
        }

        //14. 事件十四： 
        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            this.tabControl2.SelectedTab = this.tabPageProperty;
        }

        //15. 事件十五：切换 控件2 （tabControl2）选择项变化 事件 ** 属性 还是 图例
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl2.SelectedTab == this.tabPageProperty) //如果是却换到属性界面 就执行 tabPageProperty是一个切换控件 的属性 在design。CS中定义的
            {
                esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone; //item 是获取了在图例控件中 选择的图层
                IBasicMap map = null;
                ILayer layer = null;
                object other = null;
                object data = null;

                //判断所选菜单的类型
                m_tocControl.GetSelectedItem(ref item, ref map, ref layer, ref other, ref data);
                if (item == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    //if (layer is MapServerLayer) return;
                    if (this.l_layerName.Text == layer.Name) return; //相同属性表 无需操作  这里的layer是一个全局变量吧？

                    this.l_layerName.Text = layer.Name; //同理l_layerName也是一个全局变量
                    this.m_layer = layer; //同理m_layer也是一个全局变量


                    if (m_mapServer == null)  //如果不是地图服务 就是正常的本地地图
                    {
                        this.dataGridView1.DataSource = AttributeTableFrm.GetAttributeDataTable(layer, ""); //这句是NB啊！调用另一个窗体的 具有2个参数的函数！ 
                                                                                //第一个 被选择的图层对象  第二个 表格名称 ； 返回的是 一个具有 表名 字段 和数据 的表格型变量！直接赋予前者的datasource属性 
                                                                                                           //之后调用一下 事件14 执行下一句 
                        attributeTable = (DataTable)this.dataGridView1.DataSource;  //这个是静态变量 为了不让这个表格数据消亡 从而可以多次调用 并统计！
                    }
                    else
                    {
                        this.dataGridView1.DataSource = AttributeTableFrm.GetAttributeDataTable(layer, "", this.m_mapServer);
                    }
                }
                else
                {
                    MessageBox.Show("请在图例中选择一个图层", "注意：", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }


        //16. 事件十六：属性表的操作  表头单击操作在地图上闪烁！但是出错！
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //清除选中的要素集合。
            this.axMapControl1.Map.ClearSelection();

            System.Windows.Forms.DataGridViewRow pRow = this.dataGridView1.Rows[e.RowIndex];
            //string featureID = pRow.Cells["FID"].Value.ToString();
            int featureID = -1;
            #region 方法三。
            //IFeatureLayer pLayer;
            //pLayer = this.m_layer as IFeatureLayer;
            //IFeatureLayer featureLayer = pLayer;
            //ITable table = featureLayer as ITable;
            ITable table = null;
            ILayer pLayer = this.m_layer;
            //加服务器图层的判断。
            if (pLayer is IMapServerLayer || pLayer is IMapServerGroupLayer || pLayer is IMapServerSublayer)
            {
                if (this.m_mapServer == null) return;

                IRecordSet rSet = AttributeTableFrm.QueryFeatureData(this.m_mapServer, pLayer, null);
                if (!rSet.IsFeatureCollection) return;
                table = rSet.Table;

                //服务图层要素ID从1开始。有的是OBJECTID，有的是FID。
                featureID = Convert.ToInt32(pRow.Cells["OBJECTID"].Value) - 1;
            }
            else if (pLayer is IFeatureLayer)
            {
                IFeatureLayer featureLayer = pLayer as IFeatureLayer;
                table = featureLayer as ITable;

                
                
               // featureID = Convert.ToInt32(pRow.Cells["FID"].Value.ToString()); 
                //featureID = Convert.ToInt32(pRow.Cells["OBJECTID"].Value.ToString()); //这就是出错点！！有的文件是OBJECTID字段 有的文件是FID字段！ 

                //这句话是最优秀的 就是选中行的 第一列字段 解决的问题 哈哈哈哈！2011.10.10 
                featureID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                
            }
            else
            {
                return;
            }

            IRow row = table.GetRow(featureID);
            IFeature feature = row as IFeature;
            if (feature == null) return;

            //选中。
            if (pLayer.MaximumScale == 0.0)
                this.axMapControl1.MapScale = 5000;
            else
                this.axMapControl1.MapScale = pLayer.MaximumScale;

            try
            {
                this.axMapControl1.Map.SelectFeature(pLayer, feature);
                //执行定位到选中要素的命令。
                ICommand command = new ControlsZoomToSelectedCommandClass();
                command.OnCreate(m_mapControl.Object);
                command.OnClick();
                this.axMapControl1.SuspendLayout();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            //闪烁。
            Tools.FlashFeature(this.axMapControl1, feature, this.axMapControl1.Map);

            #endregion

            #region 方法二。
            //IFeatureLayer pLayer;
            //pLayer = this.m_layer as IFeatureLayer;
            //IFeatureCursor pCursor;
            //IQueryFilter pFilter = new QueryFilterClass();
            //pFilter.WhereClause = "FID >= " + featureID;
            //pCursor = pLayer.Search(pFilter, false);
            //IArray geoArray = new ArrayClass();
            //IFeature feature = pCursor.NextFeature();
            //while (feature != null)
            //{
            //    geoArray.Add(feature.Shape);
            //    feature = pCursor.NextFeature();
            //}

            //if (geoArray.Count == 1)
            //    FlashSingleObject(geoArray.get_Element(0) as IGeometry);
            //else
            //    FlashAndIdentify(geoArray);
            #endregion

            #region 方法一。
            //IFeatureLayer featureLayer = pLayer;
            //ITable table = featureLayer as ITable;
            //IRow row = table.GetRow(Convert.ToInt32(featureID));
            //IFeature feature = row as IFeature;
            //if (feature == null) return;

            //IEnvelope envelope = feature.Shape.Envelope;
            //if (envelope.Height == 0.0) envelope.Height = 0.1;
            //if (envelope.Width == 0.0) envelope.Width = 0.1;
            //ISymbol symbol = null;
            //switch (feature.Shape.GeometryType)
            //{
            //    case esriGeometryType.esriGeometryPoint:
            //        IPoint point = feature.Shape as IPoint;
            //        envelope.Expand(2.7, 2.7, true);
            //        axMapControl1.FlashShape(point, 1, 300, null);
            //        axMapControl1.Extent = envelope;
            //        Application.DoEvents();
            //        axMapControl1.FlashShape(point, 6, 300, symbol);
            //        break;
            //    case esriGeometryType.esriGeometryPolyline:
            //        IPolyline polyline = feature.Shape as IPolyline;
            //        envelope.Expand(1.7, 1.7, true);
            //        axMapControl1.FlashShape(polyline, 1, 300, null);
            //        axMapControl1.Extent = envelope;
            //        Application.DoEvents();
            //        axMapControl1.FlashShape(polyline, 15, 300, null);
            //        break;
            //    case esriGeometryType.esriGeometryPolygon:
            //        IPolygon polygon = feature.Shape as IPolygon;
            //        envelope.Expand(1.7, 1.7, true);
            //        axMapControl1.FlashShape(polygon, 1, 300, null);
            //        axMapControl1.Extent = envelope;
            //        Application.DoEvents();
            //        ITopologicalOperator topOperator = polygon as ITopologicalOperator;
            //        IPolyline line = new PolylineClass();
            //        line = topOperator.Boundary as IPolyline;
            //        axMapControl1.FlashShape(line, 15, 300, null);
            //        break;
            //}
            #endregion
        }

        //17. 事件十七： 必须有 刷新操作 调用函数！！refreshMapCtrl2LayerVisibility()
        private void axMapControl1_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            refreshMapCtrl2LayerVisibility();
        }

       
        #endregion


        #region 工具方法。
        //1 添加工具栏控件。-----------------------------------------------------------------------------------
        private void initToolbar()
        {
            string progID = "";
            // 增加PageLayout导航命令

            progID = "esriControlToolsPageLayout.ControlsPageZoomInTool";

            axToolbarControl1.AddItem(progID, -1, -1, true, 0,ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
        }
        //2 设置maptips。-----------------------------------------------------------------------------------
        private void SetMapTips(AxMapControl mapCtrl)
        {
            ILayer layer = null;
            for (int i = 0; i < mapCtrl.Map.LayerCount; i++)
            {
                layer = mapCtrl.Map.get_Layer(i);
                layer.ShowTips = true;
            }
        }
        //3 刷新鹰眼图图层可见性-----------------------------------------------------------------------------------
        private void refreshMapCtrl2LayerVisibility()
        {
            //处理当toccontrol中图层可见性改变时，mapcontrol1变化而鹰眼图图层可见性不变的问题。
            ILayer pLayer = null;
            ILayer layer = null;
            axMapControl2.ActiveView.Refresh();
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap bMap = null;
            object unk = null;
            object data = null;
            this.axTOCControl1.GetSelectedItem(ref item, ref bMap, ref layer, ref unk, ref data);
            if (layer == null) return;
            #region 不支持含图层组的情况。
            for (int i = 0; i < axMapControl2.Map.LayerCount; i++)
            {
                pLayer = axMapControl2.Map.get_Layer(i);
                if (pLayer.Name == layer.Name)
                {
                    pLayer.Visible = layer.Visible;
                    axMapControl2.ActiveView.Refresh();
                    break;
                }
            }
            #endregion
            #region 新图层处理。
            
            #endregion

        }
        /// <summary>
        /// 4 绘制点集合。未测试。-----------------------------------------------------------------------------------
        /// </summary>
        /// <param name="dtPoints">带点坐标的点数据表</param>
        /// <param name="_layer">添加点要素的目标图层</param>
        /// <param name="jingduColName">数据表中经度列名</param>
        /// <param name="weiduColName">数据表中纬度列名</param>
        private void DrawPoints(DataTable dtPoints,ILayer _layer,string jingduColName,string weiduColName)
        {

            //获得地图最上层的图层，因此事先需要在需绘图的地图上创建一个点图层
            IFeatureLayer layer = _layer as IFeatureLayer;
            IFeatureClass fc = layer.FeatureClass;
            IWorkspaceEdit we = (fc as IDataset).Workspace as IWorkspaceEdit;
            IFeature f;
            IPoint p;

            //开始编辑事务，此时其它程序不可打开相同地图，否则由于互斥导致异常
            we.StartEditing(false);

            //开始编辑
            we.StartEditOperation();
            for (int i = 0; i < dtPoints.Rows.Count; i++)
            {

                //创建地物
                f = fc.CreateFeature();

                //创建点
                
                p = new PointClass();
                //设置点坐标
                p.PutCoords((double)dtPoints.Rows[i][jingduColName], (double)dtPoints.Rows[i][weiduColName]);
                f.Shape = p;

                //设置值 2表示索引,设置的是第3个字段
                //f.set_Value(2, dtPoints.Rows[i][2]);
                //存储其他属性字段。
                for (int j = 0; j < dtPoints.Columns.Count; j++)
                {
                    if (dtPoints.Columns[j].ColumnName == jingduColName || dtPoints.Columns[j].ColumnName == weiduColName) continue;
                    f.set_Value(j + 2, dtPoints.Rows[i][j]);
                }

                //保存
                f.Store();
            }

            //结束编辑
            we.StopEditOperation();

            //结束事务
            we.StopEditing(true);
        }
        /// <summary>
        /// 对所选择要素集进行高亮显示//并打开属性查询窗口--------------------------------------------------------
        /// </summary>
        /// <param name="inArray">所选择的要素集</param>
        private void FlashAndIdentify(IArray inArray)
        {
            try
            {
                if (inArray == null)
                    return;
                m_hookHelper.Hook = axMapControl1.Object;

                IHookActions hookAction = (IHookActions)m_hookHelper;
                hookAction.DoActionOnMultiple(inArray, esriHookActions.esriHookActionsZoom);
                Application.DoEvents();
                
                hookAction.DoActionOnMultiple(inArray, esriHookActions.esriHookActionsFlash);

                //frmIdentify newFrm = frmIdentify.CreatIdentify((IHookHelper)hookHelper, inArray);
                //newFrm.Show();
                //newFrm.ShowProperty(inArray);

            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }
        /// <summary>
        /// 5  对所选择单要素集进行高亮显示。-----------------------------------------------------------------------------------
        /// </summary>
        /// <param name="featrue">所选要素</param>
        private void FlashSingleObject(IGeometry featrueShape)
        {
            try
            {
                if (featrueShape == null)
                    return;
                m_hookHelper.Hook = axMapControl1.Object;

                IHookActions hookAction = (IHookActions)m_hookHelper;
                hookAction.DoAction(featrueShape, esriHookActions.esriHookActionsZoom);
                Application.DoEvents();
                
                hookAction.DoAction(featrueShape, esriHookActions.esriHookActionsFlash);
            }
            catch (Exception exc) { MessageBox.Show(exc.Message); }
        }

        /// <summary>
        /// 6  打开shapefile 格式 ！返回一个长度为2的一维数组，string[0]表示Shape所在的文件夹，string[1]表示Shape文件名称
        /// </summary>
        /// <returns></returns>
        public string[] OpenShapeFile()
        {


            string[] ShapeFile = new string[2];

            OpenFileDialog OpenShpFile = new OpenFileDialog();

            OpenShpFile.Title = "打开Shape文件";

            // OpenShpFile.InitialDirectory = Application.StartupPath + "\\testdata";

            OpenShpFile.Filter = "Shape文件(*.shp)|*.shp";


            if (OpenShpFile.ShowDialog() == DialogResult.OK)
            {

                string ShapPath = OpenShpFile.FileName;
                //利用"\\"将文件路径分成两部分

                int Position = ShapPath.LastIndexOf("\\");

                string FilePath = ShapPath.Substring(0, Position);

                string ShpName = ShapPath.Substring(Position + 1);

                ShapeFile[0] = FilePath;

                ShapeFile[1] = ShpName;

            }

            return ShapeFile;
        }

        #endregion

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void 打开栅格ToolStripMenuItem_Click(object sender, EventArgs e) //打开栅格数据集
        {
            // IRasterWorkspace pRasterWs = GetRasterWorkspace(@".\textdata");

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.FilterIndex = 1;
            string FileName;
            dlg.Filter = "栅格tiff文件(*.tif)|*.tif|image文件(*.img)|*.img|All files|*.*";
            //dlg.Filter = "栅格文件(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileName = dlg.FileName;

                if (FileName != "")
                {
                    string foldername = System.IO.Path.GetDirectoryName (FileName);//获取文件名称（带扩展名）
                    string filename = System.IO.Path.GetFileName(FileName);//获取文件名称（带扩展名）

                    IRasterWorkspace pRasterWs = GetRasterWorkspace(@foldername);

                    IRasterDataset pRasterDataset = pRasterWs.OpenRasterDataset(filename);

                    IRasterLayer pRasterLayer = new RasterLayerClass();

                    pRasterLayer.CreateFromDataset(pRasterDataset);

                    axMapControl1.Map.AddLayer(pRasterLayer as ILayer);

                   
                    //刷新代码 不出错 但是 无法让map1 和 2的显示是一个颜色，同时图例改变也不能反应到map2 ；重点是map2要与map1的全图一样！！ 
                    m_controlsSynchronizer.ActivatePageLayout();
                    m_controlsSynchronizer.ActivateMap();

                    this.axMapControl1.ActiveView.Refresh();
                    this.axTOCControl1.ActiveView.Refresh();
                    this.axMapControl2.ActiveView.Refresh();
                    this.axTOCControl1.Refresh();
                    this.axMapControl1.Refresh();
                    this.axMapControl2.Refresh();
                }
            }
            
            
        }



        IRasterWorkspace GetRasterWorkspace(string pWsName) //一个自定义函数 设置栅格工作空间
        {

            try
            {
                IWorkspaceFactory pWorkFact = new RasterWorkspaceFactoryClass();
                return pWorkFact.OpenFromFile(pWsName, 0) as IRasterWorkspace;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void pdf格式ToolStripMenuItem_Click(object sender, EventArgs e)  /// 导出PDF
        {
            ExportPDF();
        }

        private void ExportPDF()
        {

            IActiveView pActiveView;
            pActiveView = axPageLayoutControl1.ActiveView;
          
            string pathFileName;

            SaveFileDialog save=new SaveFileDialog();   //调用保存对话框
            save.RestoreDirectory=true;
            save.Filter = "pdf文件(*.pdf)|*.pdf";
            if (DialogResult.OK == save.ShowDialog())
            {
                pathFileName = save.FileName;
                if (pActiveView == null || !(pathFileName.EndsWith(".pdf")))
                {
                    return;
                }
                else
                {
                    IEnvelope pEnv;
                    pEnv = pActiveView.Extent;
                    IExport pExport;
                    pExport = new ExportPDFClass();
                    
                    pExport.ExportFileName = @pathFileName; //输出路径
                    pExport.Resolution =100; //分辨率
                  
                    tagRECT exportRECT; //之前输出问题在于这里 应该赋予动态窗口（如地图map）的范围才对
                    exportRECT = pActiveView.ExportFrame;
                 
                    IEnvelope pPixelBoundsEnv;
                    pPixelBoundsEnv = new EnvelopeClass();
                    pPixelBoundsEnv.PutCoords(exportRECT.left, exportRECT.bottom, exportRECT.right, exportRECT.top);
                    pExport.PixelBounds = pPixelBoundsEnv;
                    int hDC;
                    hDC = pExport.StartExporting();

                    pActiveView.Output(hDC, (int)pExport.Resolution, ref exportRECT, null, null);

                    pExport.FinishExporting();
                    pExport.Cleanup();
                }
            }
        }

        private void jepg格式ToolStripMenuItem_Click(object sender, EventArgs e)  //输出图片~~
        {
            ///<summary>Creates a .jpg (JPEG) file from IActiveView. Default values of 96 DPI are used for the image creation.</summary>
            ///
            ///<param name="activeView">An IActiveView interface</param>
            ///<param name="pathFileName">A System.String that the path and filename of the JPEG you want to create. Example: "C:\temp\test.jpg"</param>
            /// 
            ///<returns>A System.Boolean indicating the success</returns>
            /// 
            ///<remarks></remarks>  //parameter check

            //设计的是函数 public System.Boolean CreateJPEGFromActiveView(ESRI.ArcGIS.Carto.IActiveView activeView, System.String pathFileName);//声明 输出地图
            
            
            string pathFileName;   //参数1 保存路径
            IActiveView activeView;//参数2 哪个设备的窗体
            activeView = axMapControl1.ActiveView;

            //pathFileName = @"E:\My first AE\MapControlApplication1\bin\Debug\testdata\test.jpg";

            SaveFileDialog save=new SaveFileDialog();   //调用保存对话框
            save.RestoreDirectory=true;
            save.Filter = "图片jpg文件(*.jpg)|*.jpg";
            if(DialogResult.OK==save.ShowDialog())   
            {  
                 pathFileName=save.FileName ;
                 if (activeView == null || !(pathFileName.EndsWith(".jpg")))
                 {
                     return;
                 }
                 else
                 {
                     ESRI.ArcGIS.Output.IExport export = new ESRI.ArcGIS.Output.ExportJPEGClass();
                     export.ExportFileName = pathFileName;

                     // Microsoft Windows default DPI resolution
                     export.Resolution = 100;  //参数3 分辨率96 是默认
                     //ESRI.ArcGIS.Display.tagRECT exportRECT = activeView.ExportFrame;
                     ESRI.ArcGIS.Geometry.IEnvelope envelope = new ESRI.ArcGIS.Geometry.EnvelopeClass();
                     envelope.PutCoords(activeView.ExportFrame.left, activeView.ExportFrame.top, activeView.ExportFrame.right, activeView.ExportFrame.bottom);
                     export.PixelBounds = envelope;
                     System.Int32 hDC = export.StartExporting();

                     tagRECT exportRECT = activeView.ExportFrame;// 啊哈 难点突破！！声明tagWECT 对象 方可使用 只能单独声明

                     activeView.Output(hDC, (System.Int16)export.Resolution, ref exportRECT, null, null);

                     // Finish writing the export file and cleanup any intermediate files
                     export.FinishExporting();
                     export.Cleanup();

                     //return true;

                 }
            }  
                    
           
            
         }

        private void 矢量转栅格ToolStripMenuItem1_Click(object sender, EventArgs e)
         {

            

        }

        /// <summary>
        /// 主地图更换时,更新鹰眼窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMapControl_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
            AddLayerToOverViewMap();
        }
        /// <summary>
        /// 把地图加到鹰眼里  函数
        /// </summary>
        private void AddLayerToOverViewMap()
        {
            axMapControl2.ClearLayers();
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                IObjectCopy objectcopy = new ObjectCopyClass();
                object toCopyLayer = axMapControl1.get_Layer(i);
                object copiedLayer = objectcopy.Copy(toCopyLayer);
                ILayer C = (new FeatureLayerClass()) as ILayer;
                object toOverwriteLayer = C;
                objectcopy.Overwrite(copiedLayer, ref toOverwriteLayer);
                axMapControl2.AddLayer(C, i);
            }
        }


        private void OnItemAdded(object item)
        {
            ILayer pLayer;
            pLayer = item as ILayer;
            axMapControl2.AddLayer(pLayer, 0);//鹰眼视图
        }

        private void 打开栅格数据2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.FilterIndex = 1;
            string FileName;
            dlg.Filter = "栅格tiff文件(*.tif)|*.tif|image文件(*.img)|*.img|All files|*.*";
            //dlg.Filter = "栅格文件(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileName = dlg.FileName;
                IRasterLayer rasterLayer = new RasterLayerClass();
                rasterLayer.CreateFromFilePath(FileName); // fileName指存本地的栅格文件路径
                axMapControl1.AddLayer(rasterLayer, 0);
            }
        }

        private void 读取栅格属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
//  5、如何读取栅格数据的属性和遍历栅格数据
//栅格数据的属性包括栅格大小，行数，列数，投影信息，栅格范围等等，见下面代码
//（假设当前加载的栅格文件栅格值存储方式为：UShort类型）

            //IRasterProps rasterProps = (IRasterProps)clipRaster;
            //int dHeight = rasterProps.Height;//当前栅格数据集的行数
            //int dWidth = rasterProps.Width; //当前栅格数据集的列数
            //double dX = rasterProps.MeanCellSize().X; //栅格的宽度
            //double dY = rasterProps.MeanCellSize().Y; //栅格的高度
            //IEnvelope extent = rasterProps.Extent; //当前栅格数据集的范围
            //rstPixelType pixelType = rasterProps.PixelType; //当前栅格像素类型
            //IPnt pntSize = new PntClass();
            //pntSize.SetCoords(dX, dY);
            //IPixelBlock pixelBlock = clipRaster.CreatePixelBlock(pntSize);
            //IPnt pnt = new PntClass();
            //for (int i = 0; i < dHeight; i++)
            //    for (int j = 0; j < dWidth; j++)
            //    {
            //        pnt.SetCoords(i, j);
            //        clipRaster.Read(pnt, pixelBlock);
            //        if (pixelBlock != null)
            //        {
            //            object obj = pixelBlock.GetVal(0, 0, 0);
            //            MessageBox.Show(Convert.ToUInt32(obj).ToString());
            //        }
            //    }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }





        public  bool IsNumber(string s, int precision, int scale)  //函数 判断是否是正整数 不是则返回 从而换行输出矩阵
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
            return System.Text.RegularExpressions .Regex.IsMatch(s, pattern);
        }

        public string[] LayerFields(DataTable inputtable)  //函数 计算字段组
        {


            string[] returnstring = new string[inputtable.Columns.Count];

     
            for (int i = 0; i < inputtable.Columns.Count; i++)
            {
                 
                if (inputtable.Columns[i].ColumnName==null)
                {
                }
                else
                {
                returnstring[i]=inputtable.Columns[i].ColumnName ;
                }
 
            }

            return returnstring;

        }



        private void 打开当前图层属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1 首先 定义item 等 变量， 初始值为空
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone; //item 是获取了在图例控件中 选择的图层
            IBasicMap map = null;
            ILayer layer = null;
            object other = null;
            object data = null;

            //2 利用tocControl控件的GetSelectedItem方法 对上述赋值，这里只用到layer
            m_tocControl.GetSelectedItem(ref item, ref map, ref layer, ref other, ref data);

            if (item == esriTOCControlItem.esriTOCControlItemLayer) //这句用来判断是不是没有选择 即空图层
            {
                //if (item == esriTOCControlItem.esriTOCControlItemLayer) //相同属性表 无需操作  这里的layer是一个全局变量吧？
                //if (this.l_layerName.Text == layer.Name) return;  

                this.l_layerName.Text = layer.Name; //更换全局变量此时记住的 图层名称
                this.m_layer = layer; //m_layer也是一个全局变量，作为下面调用函数的 图层参数

                //3 调用AttributeTableFrm类中函数

                AttributeTableFrm attributeTable = new AttributeTableFrm();

                string layerName = AttributeTableFrm.getValidFeatureClassName(m_layer.Name); //图层名称

                //if (m_mapServer != null)
                //    attributeTable.CreateAttributeTable(AttributeTableFrm.GetAttributeDataTable(m_layer, "", m_mapServer), layerName);
                //else
                //    attributeTable.CreateAttributeTable(AttributeTableFrm.GetAttributeDataTable(m_layer, ""), layerName);
                //attributeTable.ShowDialog();

                showAttributeTable(layer);
            }

            else
            {
                MessageBox.Show("请在图例中选择一个图层", "注意：", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }



        private void showAttributeTable(ILayer pLyr)
        {
            if (pLyr is IFeatureLayer)
            {
                DataTable pTable = new DataTable();
                IFeatureLayer pFealyr = pLyr as IFeatureLayer;
                IFeatureClass pFCls = pFealyr.FeatureClass;
                string shape = "";
                if (pFCls.ShapeType == esriGeometryType.esriGeometryPoint)

                    shape = "Point";
                else if (pFCls.ShapeType == esriGeometryType.esriGeometryPolyline)

                    shape = "Polyline";
                else if (pFCls.ShapeType == esriGeometryType.esriGeometryPolygon)

                    shape = "Polygon";
                for (int i = 0; i < pFCls.Fields.FieldCount; i++)
                {

                    pTable.Columns.Add(pFCls.Fields.get_Field(i).Name);

                }


                IFeatureCursor pCursor = pFCls.Search(null, false);
                int ishape = pFCls.Fields.FindField("Shape");
                IFeature pFea = pCursor.NextFeature();
                while (pFea != null)
                {
                    DataRow pRow = pTable.NewRow();
                    for (int i = 0; i < pFCls.Fields.FieldCount; i++)
                    {
                        if (i == ishape)
                        {

                            pRow[i] = shape;


                            continue;

                        }

                        pRow[i] = pFea.get_Value(i).ToString();

                    }

                    pTable.Rows.Add(pRow);

                    pFea = pCursor.NextFeature();

                }

                dataGridView1.DataSource = pTable;

            }
            else if (pLyr is IRasterLayer)
            {
                IRasterLayer pRlyr = pLyr as IRasterLayer;
                IRaster pRaster = pRlyr.Raster;
                IRasterProps pProp = pRaster as IRasterProps;

                pProp.PixelType = rstPixelType.PT_LONG;
                if (pProp.PixelType == rstPixelType.PT_LONG)
                {
                    IRasterBandCollection pBcol = pRaster as IRasterBandCollection;
                    IRasterBand pBand = pBcol.Item(0);
                    ITable pRTable = pBand.AttributeTable;
                    DataTable pTable = new DataTable();
                    for (int i = 0; i < pRTable.Fields.FieldCount; i++)

                        pTable.Columns.Add(pRTable.Fields.get_Field(i).Name);
                    ICursor pCursor = pRTable.Search(null, false);
                    IRow pRrow = pCursor.NextRow();
                    while (pRrow != null)
                    {
                        DataRow pRow = pTable.NewRow();
                        for (int i = 0; i < pRrow.Fields.FieldCount; i++)
                        {

                            pRow[i] = pRrow.get_Value(i).ToString();

                        }

                        pTable.Rows.Add(pRow);

                        pRrow = pCursor.NextRow();

                    }

                    dataGridView1.DataSource = pTable;

                }

            }
        }
    }
}
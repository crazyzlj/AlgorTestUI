using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.SystemUI;

namespace AlgorithmTest
{
    public class Tools
    {
        //闪烁线
        static void FlashLine(AxMapControl mapControl, IScreenDisplay iScreenDisplay, IGeometry iGeometry)
        {
            ISimpleLineSymbol iLineSymbol = new SimpleLineSymbol();
            iLineSymbol.Width = 4;
            IRgbColor iRgbColor = new RgbColor();
            iRgbColor.Red = 255;
            iLineSymbol.Color = iRgbColor;
            ISymbol iSymbol = (ISymbol)iLineSymbol;
            iSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            mapControl.FlashShape(iGeometry, 3, 200, iSymbol);
        }
        //闪烁面
        static void FlashPolygon(AxMapControl mapControl, IScreenDisplay iScreenDisplay, IGeometry iGeometry)
        {
            ISimpleFillSymbol iFillSymbol = new SimpleFillSymbol();
            iFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
            iFillSymbol.Outline.Width = 12;

            IRgbColor iRgbColor = new RgbColor();
            iRgbColor.RGB = System.Drawing.Color.FromArgb(100, 180, 180).ToArgb();
            iFillSymbol.Color = iRgbColor;

            ISymbol iSymbol = (ISymbol)iFillSymbol;
            iSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            iScreenDisplay.SetSymbol(iSymbol);
            mapControl.FlashShape(iGeometry, 3, 200, iSymbol);
        }
        //闪烁点
        static void FlashPoint(AxMapControl mapControl, IScreenDisplay iScreenDisplay, IGeometry iGeometry)
        {
            ISimpleMarkerSymbol iMarkerSymbol = new SimpleMarkerSymbol();
            iMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            IRgbColor iRgbColor = new RgbColor();
            iRgbColor.RGB = System.Drawing.Color.FromArgb(0, 0, 0).ToArgb();
            iMarkerSymbol.Color = iRgbColor;
            ISymbol iSymbol = (ISymbol)iMarkerSymbol;
            iSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            mapControl.FlashShape(iGeometry, 3, 200, iSymbol);
        }
        //闪烁目标
        public static void FlashFeature(AxMapControl mapControl, IFeature iFeature, IMap iMap)
        {
            IActiveView iActiveView = iMap as IActiveView;
            //mapControl.SuspendLayout();
            //update很重要。先让前面的缩放操作更新好了，再闪烁。
            mapControl.Update();
            if (iActiveView != null)
            {
                iActiveView.ScreenDisplay.StartDrawing(0, (short)esriScreenCache.esriNoScreenCache);

                //根据几何类型调用不同的过程
                switch (iFeature.Shape.GeometryType)
                {
                    case esriGeometryType.esriGeometryPolyline:
                        FlashLine(mapControl, iActiveView.ScreenDisplay, iFeature.Shape);
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        FlashPolygon(mapControl, iActiveView.ScreenDisplay, iFeature.Shape);
                        break;
                    case esriGeometryType.esriGeometryPoint:
                        FlashPoint(mapControl, iActiveView.ScreenDisplay, iFeature.Shape);
                        break;
                    default:
                        break;
                }
                iActiveView.ScreenDisplay.FinishDrawing();
            }
        }

        //加载地图图层。
        public static void LoadSdeLayer(AxMapControl MapCtr, bool ChkSdeLinkModle)
        {
            //定义一个属性 
            IPropertySet Propset = new PropertySetClass();
            if (ChkSdeLinkModle == true) // 采用SDE连接 
            {
                //设置数据库服务器名，服务器所在的IP地址 
                Propset.SetProperty("SERVER", "my");
                //设置SDE的端口，这是安装时指定的，默认安装时"port:5151" 
                Propset.SetProperty("INSTANCE", "port:5151");
                //SDE的用户名 
                Propset.SetProperty("USER", "sa");
                //密码 
                Propset.SetProperty("PASSWORD", "sa");
                //设置数据库的名字,只有SQL Server  Informix 数据库才需要设置 
                Propset.SetProperty("DATABASE", "sde");
                //SDE的版本,在这为默认版本 
                Propset.SetProperty("VERSION", "SDE.DEFAULT");
            }
            else // 直接连接 
            {
                //设置数据库服务器名,如果是本机可以用"sde:sqlserver:." 
                Propset.SetProperty("INSTANCE", "sde:sqlserver:zhpzh");
                //SDE的用户名 
                Propset.SetProperty("USER", "sa");
                //密码 
                Propset.SetProperty("PASSWORD", "sa");
                //设置数据库的名字,只有SQL Server  Informix 数据库才需要设置             
                Propset.SetProperty("DATABASE", "sde");
                //SDE的版本,在这为默认版本 
                Propset.SetProperty("VERSION", "SDE.DEFAULT");
            }
            //定义一个工作空间,并实例化为SDE的工作空间 
            IWorkspaceFactory Fact = new SdeWorkspaceFactoryClass();
            //打开SDE工作空间,并转化为地物工作空间 
            IFeatureWorkspace Workspace = (IFeatureWorkspace)Fact.Open(Propset, 0);
            /*定义一个地物类,并打开SDE中的管点地物类,写的时候一定要写全.如SDE中有一个管点层,你不能写成IFeatureClass Fcls = Workspace.OpenFeatureClass ("管点");这样,一定要写成下边的样子.*/
            IFeatureClass Fcls = Workspace.OpenFeatureClass("sde.dbo.管点");

            IFeatureLayer Fly = new FeatureLayerClass();
            Fly.FeatureClass = Fcls;
            MapCtr.Map.AddLayer(Fly);
            MapCtr.ActiveView.Refresh();
        } 

        //buffer查询并闪烁。
        public static void BufferSelectAndFlash(IMapControl4 mapCtrl,IGeometry baseGeo,esriSpatialRelEnum spatialRef,ILayer layer)
        {
            IFeatureLayer featureLayer = layer as IFeatureLayer;
            if (mapCtrl == null ||featureLayer == null) return;
            IFeatureClass fC = featureLayer.FeatureClass;
            ISpatialFilter pFilter = new SpatialFilterClass();
            pFilter.Geometry = baseGeo;
            pFilter.GeometryField = "SHAPE";
            pFilter.SpatialRel = spatialRef;
            IFeatureCursor pFeatureCursor  = fC.Search(pFilter, false);

            IArray inArray = new ArrayClass();
            IFeature fe = pFeatureCursor.NextFeature();
            mapCtrl.Map.ClearSelection();
            while (fe != null)
            {
                inArray.Add(fe);
                mapCtrl.Map.SelectFeature(layer, fe);
                fe = pFeatureCursor.NextFeature();
            }

            if (inArray == null) return;

            HookHelper m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = mapCtrl.Object;
            IHookActions hookAction = (IHookActions)m_hookHelper;

            ICommand cmd = new ControlsZoomToSelectedCommandClass();
            cmd.OnCreate(m_hookHelper.Hook);
            cmd.OnClick();
            Application.DoEvents();
            hookAction.DoActionOnMultiple(inArray, esriHookActions.esriHookActionsFlash);
        }

        /// <summary>
        /// 自定义图层类，包含图层信息，图层编号（父图层的编号+‘_’本层编号）
        /// </summary>
        public class CSLayer
        {
            #region 成员。
            public ILayer _layer;
            public string _ID;
            public bool isGroupLayer;
            #endregion
            public CSLayer(ILayer layer, string ID)
            {
                isGroupLayer = false;
                this._layer = layer;
                this._ID = ID;
                if (layer is IGroupLayer)
                    isGroupLayer = true;
            }
        }
        /// <summary>
        /// 获得IMap中的layers列表，并对应上一级图层。
        /// </summary>
        public static List<CSLayer> GetLayerListByMapReturnAll(IMap map,ref List<CSLayer> nodeLayerList,ref List<CSLayer> groupLayerList)
        {
            List<CSLayer> layerList = null;
            ILayer layer = null;
            if (map == null || map.LayerCount == 0) return null;
            for (int i = 0; i < map.LayerCount; i++)
            {
                layer = map.get_Layer(i);
                GetLayerListByLayer(ref nodeLayerList,ref groupLayerList, layer, i.ToString());
            }
            if (groupLayerList != null || nodeLayerList != null)
            {
                if (layerList == null)
                    layerList = new List<CSLayer> { };
                if(groupLayerList!=null)
                    layerList.AddRange(groupLayerList);
                if(nodeLayerList!=null)
                    layerList.AddRange(nodeLayerList);
            }
            return layerList;
        }
        /// <summary>
        /// 获得IMap中的layers列表，并对应上一级图层。
        /// </summary>
        public static void GetLayerListByMap(IMap map, ref List<CSLayer> nodeLayerList, ref List<CSLayer> groupLayerList)
        {
            ILayer layer = null;
            for (int i = 0; i < map.LayerCount; i++)
            {
                layer = map.get_Layer(i);
                GetLayerListByLayer(ref nodeLayerList, ref groupLayerList, layer, i.ToString());
            }
        }
        /// <summary>
        /// 从图层获取CSLayer。
        /// </summary>
        /// <param name="layer">待递归的图层</param>
        /// <param name="layerID">待递归的图层编号（CSLayer相关的编号）（父图层的编号+‘_’本层编号）,根节点ID为""</param>
        /// <returns></returns>
        public static void GetLayerListByLayer( ref List<CSLayer> nodeLayerList,ref List<CSLayer> groupLayerList, ILayer layer,string layerID)
        {
            if (layer is IGroupLayer||layer is ICompositeLayer)
            {
                if (groupLayerList == null) groupLayerList = new List<CSLayer> { };
                CSLayer csLayer = new CSLayer(layer, layerID);
                groupLayerList.Add(csLayer);

                //IGroupLayer groupLayer = layer as IGroupLayer;
                //ICompositeLayer cLayer = groupLayer as ICompositeLayer;
                ICompositeLayer cLayer = layer as ICompositeLayer;
                ILayer tmpLayer = null;
                for (int i = 0; i < cLayer.Count; i++)
                {
                    tmpLayer = cLayer.get_Layer(i);
                    GetLayerListByLayer(ref nodeLayerList,ref groupLayerList, tmpLayer, layerID + "_" + i.ToString());
                }
            }
            else
            {
                if (nodeLayerList == null) nodeLayerList = new List<CSLayer> { };
                CSLayer csLayer = new CSLayer(layer, layerID);
                nodeLayerList.Add(csLayer);
            }
        }

        
    }
}

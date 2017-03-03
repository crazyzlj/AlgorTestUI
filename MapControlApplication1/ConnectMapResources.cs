using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;

namespace TestDemo
{
    public class ConnectMapResources
    {
        ConnectMapResources()
        {

        }

        /// <summary>
        /// 获得地图服务器上的map,并填充到mapCtrl中。
        /// </summary>
        /// <param name="axMapControl1"></param>
        /// <param name="serverHostName">发布服务的主机名称</param>
        /// <param name="mapName">发布的地图服务名称</param>
        /// <returns>返回连接的地图服务对象</returns>
        public static IMapServer ConnectToMapServerAndDisplay(AxMapControl axMapControl1, string serverHostName, string mapName)
        {
            try
            {
                IMapServer mapServer = null;
                //IMapServerLayer layer = ConnectMapServer("t-a6d5804bbede4", "FileMap");
                IMapServerLayer layer = ConnectMapServer(serverHostName,mapName,ref mapServer);
                axMapControl1.ClearLayers();
                #region 方法二.图层能加进去，但是不显示。不知道为什么。
                //ICompositeLayer2 pCompositeLayer = layer as ICompositeLayer2;
                //for (int i = 0; i < pCompositeLayer.Count; i++)
                //{
                //    ILayer pLayer = pCompositeLayer.get_Layer(i);
                //    axMapControl1.AddLayer(pLayer,0);
                //}
                #endregion
                #region 方法一
                axMapControl1.AddLayer(layer as ILayer);

                IAGSServerObjectName serverName = null;
                string mapLocation = "";
                string mapLayerName = "";
                layer.GetConnectionInfo(out serverName, out mapLocation, out mapLayerName);
                axMapControl1.Map.Name = serverName.AGSServerConnectionName.ConnectionProperties.GetProperty("url").ToString() + "_" + serverName.Name + "_" + mapLocation + "_" + mapLayerName;
                axMapControl1.Refresh();
                #endregion
                return mapServer;
            }
            catch(Exception ex)
            {
                MessageBox.Show("连接失败，错误:" + ex.Message);
                return null;
            }
        }

        //连接地理信息服务。
        /// <summary>
        /// 获得地图服务器上的map。
        /// </summary>
        /// <param name="serverHostName">发布服务的主机名称</param>
        /// <param name="mapName">发布的地图服务名称</param>
        /// <returns>mapserver实际上是一个dataframe，发布机器上的一个map。</returns>
        public static IMapServerLayer ConnectMapServer(string serverHostName, string mapName, ref IMapServer mapServer)
        {
            IAGSServerConnectionFactory connectionFactory = new AGSServerConnectionFactory();
            IPropertySet propertySet = new PropertySet();
            IAGSServerConnection connection;
            propertySet.SetProperty("url", "http://" + serverHostName + "/arcgis/services");
            connection = connectionFactory.Open(propertySet, 0);
            //获取 "MapService"服务器对象。

            IAGSEnumServerObjectName serverObjectNames;
            IAGSServerObjectName serverObjectName;
            serverObjectNames = connection.ServerObjectNames;
            while ((serverObjectName = serverObjectNames.Next()) != null)
            {
                if (serverObjectName.Name.Equals(mapName) && serverObjectName.Type.Equals("MapServer"))
                    break;
            }
            if (serverObjectName == null)
                return null;
            IName name = serverObjectName as IName;
            mapServer = name.Open() as IMapServer;

            // 用 AGS数据源新建 MapServerLayer。实际上是一个dataframe，一个map。
            ESRI.ArcGIS.Carto.IMapServerLayer layer = new MapServerLayer() as IMapServerLayer;
            layer.ServerConnect(serverObjectName, mapServer.DefaultMapName);
            return layer;
        }
    }
}

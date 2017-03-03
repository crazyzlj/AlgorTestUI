using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;

namespace TestDemo
{
    public partial class ConnectMapServiceFrm : Form
    {
        private AxMapControl m_mapCtrl = null;
        private IMapServer m_mapServer = null;
        public ConnectMapServiceFrm(AxMapControl mapCtrl)
        {
            InitializeComponent();
            this.m_mapCtrl = mapCtrl;
        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radio_internet_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_internet.Checked)
            {
                t_url.Enabled = true;
                t_hostName.Enabled = false;
            }
            else
            {
                t_url.Enabled = false;
                t_hostName.Enabled = true;
            }
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            if (list_services.SelectedItem == null||list_services.SelectedItem.ToString().Trim()=="") return;

            IPropertySet propertySet = (IPropertySet)this.list_services.Tag;

            IAGSServerObjectName serverObjectName;
            IMapServer mapServer = GetMapServer(propertySet,list_services.SelectedItem.ToString(),out serverObjectName);

            // 用 AGS数据源新建 MapServerLayer。实际上是一个dataframe，一个map。
            ESRI.ArcGIS.Carto.IMapServerLayer layer = new MapServerLayer() as IMapServerLayer;
            if (mapServer == null||serverObjectName == null) return;
            layer.ServerConnect(serverObjectName, mapServer.DefaultMapName);
            this.m_mapCtrl.AddLayer(layer as ILayer);
            this.DialogResult = DialogResult.OK;
            this.m_mapServer = mapServer;
            this.Hide();
        }

        private void t_url_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string urlStr = this.t_url.Text.Trim();
            if (urlStr != "")
            {
                IPropertySet propertySet = new PropertySet();
                propertySet.SetProperty("url", urlStr);
                if (t_userName.Text.Trim() != "" || t_userPwd.Text.Trim() != "")
                {
                    propertySet.SetProperty("user", t_userName.Text.Trim());
                    propertySet.SetProperty("password", t_userPwd.Text.Trim());
                }
                RefreshServerObjectList(propertySet);
            }
        }

        private void t_hostName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string hostName = this.t_hostName.Text.Trim();
            if (hostName != "")
            {
                IPropertySet propertySet = new PropertySet();
                propertySet.SetProperty("machine", hostName);
                if (t_userName.Text.Trim() != "" || t_userPwd.Text.Trim() != "")
                {
                    propertySet.SetProperty("user", t_userName.Text.Trim());
                    propertySet.SetProperty("password", t_userPwd.Text.Trim());
                }
                RefreshServerObjectList(propertySet);
            }
        }

        /// <summary>
        /// 获取mapserver对象。
        /// </summary>
        /// <param name="propertySet">连接属性（url/machine），包括连接方式，用户名密码等</param>
        /// <param name="serverObjectNameStr">地图服务名称</param>
        /// <returns>mapserver对象。</returns>
        /// <param name="serverObjectName">传出地图服务名对象，用于配合mapserver打开serverlayer。</param>
        public static IMapServer GetMapServer(IPropertySet propertySet, string serverObjectNameStr, out IAGSServerObjectName serverObjectName)
        {
            IAGSServerConnectionFactory connectionFactory = new AGSServerConnectionFactory();
            IAGSServerConnection connection;
            IMapServer mapServer = null; ;

            try
            {
                connection = connectionFactory.Open(propertySet, 0);

                //获取 "MapService"服务器对象。

                IAGSEnumServerObjectName serverObjectNames;
                //IAGSServerObjectName serverObjectName;
                serverObjectNames = connection.ServerObjectNames;

                while ((serverObjectName = serverObjectNames.Next()) != null)
                {
                    if (serverObjectNameStr == serverObjectName.Name)
                    {
                        IName name = serverObjectName as IName;
                        mapServer = name.Open() as IMapServer;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败:" + ex.Message);
                serverObjectName = null;
                return null;
            }
            return mapServer;
        }

        //返回窗体最后连接的mapserver。
        public IMapServer GetMapServer()
        {
            return this.m_mapServer;
        }

        //刷新地图服务列表。
        private void RefreshServerObjectList(IPropertySet propertySet)
        {
            this.list_services.Items.Clear();

            IAGSServerConnectionFactory connectionFactory = new AGSServerConnectionFactory();
            IAGSServerConnection connection;

            try
            {
                connection = connectionFactory.Open(propertySet, 0);

                //获取 "MapService"服务器对象。

                IAGSEnumServerObjectName serverObjectNames;
                IAGSServerObjectName serverObjectName;
                serverObjectNames = connection.ServerObjectNames;
                this.list_services.Tag = propertySet;
                while ((serverObjectName = serverObjectNames.Next()) != null)
                {
                    if (serverObjectName.Type == "MapServer")
                        this.list_services.Items.Add(serverObjectName.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败:" + ex.Message);
            }
        }
        private void t_url_Validated(object sender, EventArgs e)
        {
            string urlStr = this.t_url.Text.Trim();
            if (urlStr != "")
            {
                IPropertySet propertySet = new PropertySet();
                propertySet.SetProperty("url", urlStr);
                if (t_userName.Text.Trim() != "" || t_userPwd.Text.Trim() != "")
                {
                    propertySet.SetProperty("user", t_userName.Text.Trim());
                    propertySet.SetProperty("password", t_userPwd.Text.Trim());
                }
                RefreshServerObjectList(propertySet);
            }
        }

        private void t_hostName_Validated(object sender, EventArgs e)
        {
            string hostName = this.t_hostName.Text.Trim();
            if (hostName != "")
            {
                IPropertySet propertySet = new PropertySet();
                propertySet.SetProperty("machine", hostName);
                if (t_userName.Text.Trim() != "" || t_userPwd.Text.Trim() != "")
                {
                    propertySet.SetProperty("user", t_userName.Text.Trim());
                    propertySet.SetProperty("password", t_userPwd.Text.Trim());
                }
                RefreshServerObjectList(propertySet);
            }
        }
    }
}

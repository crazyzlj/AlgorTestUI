using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MapControlApplication1
{
    /// <summary>
    /// Command that works in ArcMap/Map/PageLayout
    /// </summary>
    [Guid("3fc60b89-25ad-44b5-9977-8380ba6cde8b")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MapControlApplication1.SelectFeatureByLocation")]
    public sealed class SelectFeatureByLocation : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        private IMap map = null;


        public SelectFeatureByLocation()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "按位置选择要素"; //localizable text
            base.m_caption = "按位置选择要素";  //localizable text 
            base.m_message = "在ArcMap/MapControl/PageLayoutControl中作用。";  //localizable text
            base.m_toolTip = "按位置选择要素";  //localizable text
            base.m_name = "SelectFeatureByLocation";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;
            
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                    m_hookHelper = null;
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            ToolbarControl toolbar = null;
            IToolbarItem toolItem = null;
            if (m_hookHelper.Hook is ToolbarControl)
            {
                toolbar = (ToolbarControl)m_hookHelper.Hook;

                for (int i = 0; i < toolbar.Count; i++)
                {
                    toolItem = toolbar.GetItem(i);
                    if (toolItem.Command is SelectFeatureByLocation)
                    {
                        toolItem.CustomProperty = map;
                        break;
                    }
                }
            }
            try
            {
                map = m_hookHelper.FocusMap;

                List<Tools.CSLayer> nodeLayerList = null;
                List<Tools.CSLayer> groupLayerList = null;
                Tools.GetLayerListByMap(map, ref nodeLayerList, ref groupLayerList);
                ILayer layer = nodeLayerList[0]._layer;
                IEnvelope ienv = m_hookHelper.ActiveView.Extent;
                IPoint p = new PointClass();
                p.PutCoords((ienv.XMax+ienv.XMin)/2,(ienv.YMax+ienv.YMin)/2);
                ienv = new EnvelopeClass();
                ienv.XMax = p.Envelope.XMax + 1; ienv.XMin = p.Envelope.XMin - 1; ienv.YMax = p.Envelope.YMax + 1; ienv.YMin = p.Envelope.YMin - 1;


                IGeometry geoo = ienv as IGeometry;
                Tools.BufferSelectAndFlash(((IMapControl4)((ToolbarControl)m_hookHelper.Hook).Buddy), geoo, esriSpatialRelEnum.esriSpatialRelIntersects, layer);
            }
            catch(Exception ex)
            {
                string s = ex.Message;
            }
        }

        #endregion
    }
}

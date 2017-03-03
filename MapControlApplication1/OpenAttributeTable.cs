using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;

namespace TestDemo
{
    /// <summary>
    /// Summary description for OpenAttributeTable.
    /// </summary>
    [Guid("022ef107-c81c-4897-87b4-7f3b5fbcc5b6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("testMap1.OpenAttributeTable")]
    public sealed class OpenAttributeTable : BaseCommand
    {
        //��Ա������
        private ILayer m_pLayer;

        private DataGridView m_dataGridView;
        private TabPage m_tab;

        private IMapServer m_mapServer;

     

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
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper;

        public OpenAttributeTable(ILayer pLayer,IMapServer pMapServer)
        {
            //
            // TODO: Define values for the public properties
            //

            base.m_category = ""; //localizable text
            base.m_caption = "�����Ա��´���"; //localizable text
            base.m_message = "�����Ա��´���"; //localizable text 
            base.m_toolTip = "�����Ա��´���"; //localizable text 
            base.m_name = "�����Ա��´���"; //unique id, non-localizable (e.g. "MyCategory_MyCommand")
            m_pLayer = pLayer;
            m_mapServer = pMapServer;

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
                System.Diagnostics.Trace.WriteLine(ex.Message, "�����õ� Bitmap");
            }
        }
        //���ع���
        public OpenAttributeTable(ILayer pLayer,DataGridView gridview,TabPage tab)
        {
            //
            // TODO: Define values for the public properties
            //

            base.m_category = ""; //localizable text
            base.m_caption = "�鿴���Ա�"; //localizable text
            base.m_message = "�鿴���Ա�"; //localizable text 
            base.m_toolTip = "�鿴���Ա�"; //localizable text 
            base.m_name = "�鿴���Ա�"; //unique id, non-localizable (e.g. "MyCategory_MyCommand")
            m_pLayer = pLayer;
            m_dataGridView = gridview;
            m_tab = tab;
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
                System.Diagnostics.Trace.WriteLine(ex.Message, "�����õ� Bitmap");
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add OpenAttributeTable.OnClick implementation
            AttributeTableFrm attributeTable = new AttributeTableFrm();
       
            
            if (m_dataGridView != null && m_tab != null)
            {
                TabControl tabCtrl = ((TabControl)this.m_tab.Parent);
                tabCtrl.SelectedTab = m_tab;
            }
            else
            {
               // AttributeTableFrm attributeTable = new AttributeTableFrm();
                string layerName = AttributeTableFrm.getValidFeatureClassName(m_pLayer.Name);
                if (m_mapServer != null)
                    attributeTable.CreateAttributeTable(AttributeTableFrm.GetAttributeDataTable(m_pLayer,"", m_mapServer),layerName);
                else
                    attributeTable.CreateAttributeTable(AttributeTableFrm.GetAttributeDataTable(m_pLayer,""),layerName);
                    attributeTable.ShowDialog();
            }

            
        }


        #endregion
    }
}

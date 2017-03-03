using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS;

namespace TestDemo
{
    static class EasyMapViewerMain
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!RuntimeManager.Bind(ProductCode.Engine))
            {
                if (!RuntimeManager.Bind(ProductCode.Desktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }
            try
            {
                Application.EnableVisualStyles();
				ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch(Exception ex)
            {
                MessageBox.Show("主程序遇到问题，需要关闭:"+ex.Message);
            }
        }
    }
}
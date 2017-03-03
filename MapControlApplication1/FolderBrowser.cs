using System;
using System.Windows.Forms.Design;
using System.Windows.Forms;
// Download by http://www.codefans.net
namespace Dialogs
{	/// <summary>
    /// Use this class to display the browse for folder dialog box.
    /// Browse for a specific kind of object (Folders, Printers, Computers, etc).
    /// </summary>
    public class FolderBrowser : FolderNameEditor
    {
        /// <summary>
        /// Types of objects you can look for.
        /// </summary>
        public enum fbStyles
        {
            BrowseForComputer = FolderNameEditor.FolderBrowserStyles.BrowseForComputer,
            BrowseForEverything = FolderNameEditor.FolderBrowserStyles.BrowseForEverything,
            BrowseForPrinter = FolderNameEditor.FolderBrowserStyles.BrowseForPrinter,
            RestrictToDomain = FolderNameEditor.FolderBrowserStyles.RestrictToDomain,
            RestrictToFilesystem = FolderNameEditor.FolderBrowserStyles.RestrictToFilesystem,
            RestrictToSubfolders = FolderNameEditor.FolderBrowserStyles.RestrictToSubfolders,
            ShowTextBox = FolderNameEditor.FolderBrowserStyles.ShowTextBox,
        }

        /// <summary>
        /// 
        /// </summary>
        public enum fbFolder
        {
            Desktop = FolderNameEditor.FolderBrowserFolder.Desktop,
            Favorites = FolderNameEditor.FolderBrowserFolder.Favorites,
            MyComputer = FolderNameEditor.FolderBrowserFolder.MyComputer,
            MyDocuments = FolderNameEditor.FolderBrowserFolder.MyDocuments,
            MyPictures = FolderNameEditor.FolderBrowserFolder.MyPictures,
            NetAndDialUpConnections = FolderNameEditor.FolderBrowserFolder.NetAndDialUpConnections,
            NetworkNeighborhood = FolderNameEditor.FolderBrowserFolder.NetworkNeighborhood,
            Printers = FolderNameEditor.FolderBrowserFolder.Printers,
            Recent = FolderNameEditor.FolderBrowserFolder.Recent,
            SendTo = FolderNameEditor.FolderBrowserFolder.SendTo,
            StartMenu = FolderNameEditor.FolderBrowserFolder.StartMenu,
            Templates = FolderNameEditor.FolderBrowserFolder.Templates
        }

        // 
        private FolderNameEditor.FolderBrowser m_obBrowser = null;


        public FolderBrowser()
        {
            m_obBrowser = new FolderNameEditor.FolderBrowser();
        }

        /// <summary>
        /// 默认路径.
        /// </summary>
        public Dialogs.FolderBrowser.fbFolder StartLocation
        {
            set
            {
                switch (value)
                {
                    case fbFolder.Desktop:
                        m_obBrowser.StartLocation = FolderBrowserFolder.Desktop;
                        break;
                    case fbFolder.Favorites:
                        m_obBrowser.StartLocation = FolderBrowserFolder.Favorites;
                        break;
                    case fbFolder.MyComputer:
                        m_obBrowser.StartLocation = FolderBrowserFolder.MyComputer;
                        break;
                    case fbFolder.MyDocuments:
                        m_obBrowser.StartLocation = FolderBrowserFolder.MyDocuments;
                        break;
                    case fbFolder.MyPictures:
                        m_obBrowser.StartLocation = FolderBrowserFolder.MyPictures;
                        break;
                    case fbFolder.NetAndDialUpConnections:
                        m_obBrowser.StartLocation = FolderBrowserFolder.NetAndDialUpConnections;
                        break;
                    case fbFolder.NetworkNeighborhood:
                        m_obBrowser.StartLocation = FolderBrowserFolder.NetworkNeighborhood;
                        break;
                    case fbFolder.Printers:
                        m_obBrowser.StartLocation = FolderBrowserFolder.Printers;
                        break;
                    case fbFolder.Recent:
                        m_obBrowser.StartLocation = FolderBrowserFolder.Recent;
                        break;
                    case fbFolder.SendTo:
                        m_obBrowser.StartLocation = FolderBrowserFolder.SendTo;
                        break;
                    case fbFolder.StartMenu:
                        m_obBrowser.StartLocation = FolderBrowserFolder.StartMenu;
                        break;
                    case fbFolder.Templates:
                        m_obBrowser.StartLocation = FolderBrowserFolder.Templates;
                        break;

                }
            }
            get
            {
                return (fbFolder)this.m_obBrowser.StartLocation;
            }
        }

        /// <summary>
        /// The type of object to look for.
        /// </summary>
        public Dialogs.FolderBrowser.fbStyles Style
        {
            set
            {
                switch (value)
                {
                    case fbStyles.BrowseForComputer:
                        m_obBrowser.Style = FolderBrowserStyles.BrowseForComputer;
                        break;
                    case fbStyles.BrowseForEverything:
                        m_obBrowser.Style = FolderBrowserStyles.BrowseForEverything;
                        break;
                    case fbStyles.BrowseForPrinter:
                        m_obBrowser.Style = FolderBrowserStyles.BrowseForPrinter;
                        break;
                    case fbStyles.RestrictToDomain:
                        m_obBrowser.Style = FolderBrowserStyles.RestrictToDomain;
                        break;
                    case fbStyles.RestrictToFilesystem:
                        m_obBrowser.Style = FolderBrowserStyles.RestrictToFilesystem;
                        break;
                    case fbStyles.RestrictToSubfolders:
                        m_obBrowser.Style = FolderBrowserStyles.RestrictToSubfolders;
                        break;
                    case fbStyles.ShowTextBox:
                        m_obBrowser.Style = FolderBrowserStyles.ShowTextBox;
                        break;

                }


            }
            get
            {
                return (fbStyles)this.m_obBrowser.Style;
            }
        }

        /// <summary>
        /// 显示对话框的那串文字.
        /// </summary>
        public string Description
        {
            set
            {
                m_obBrowser.Description = value;
            }
            get
            {
                return this.m_obBrowser.Description;
            }
        }

        /// <summary>
        /// 返回路径名.
        /// </summary>
        public string DirectoryPath
        {
            get
            {
                try
                {
                    return this.m_obBrowser.DirectoryPath;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 显示浏览对话框.
        /// </summary>
        /// <returns>
        /// </returns>
        public DialogResult ShowDialog()
        {
            return m_obBrowser.ShowDialog();
        }
    }
}
//Dialogs.FolderBrowser fBrowser = new Dialogs.FolderBrowser();
//			//
//fBrowser.Description = "从下面选择目标目录.";
//			// 默认路径		
//fBrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.Desktop;
//			//fBrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.Favorites;
//			//fBrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.MyComputer;
//			//fBrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.MyDocuments;
//			//fBrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.MyPictures;
//			//fBrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.NetAndDialUpConnections;
//			//fBrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.NetworkNeighborhood;
//			//fBrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.Printers;
//			//fBrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.Recent;
//			//fBrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.SendTo;
//			//fBrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.StartMenu;
//			//fBrowser.StartLocation = Dialogs.FolderBrowser.fbFolder.Templates;
//			// Try the diferents styles
//			//fBrowser.Style = Dialogs.FolderBrowser.fbStyles.RestrictToSubfolders;
//			//fBrowser.Style = Dialogs.FolderBrowser.fbStyles.BrowseForComputer;
//fBrowser.Style = Dialogs.FolderBrowser.fbStyles.BrowseForEverything;
//			//fBrowser.Style = Dialogs.FolderBrowser.fbStyles.BrowseForPrinter;
//			//fBrowser.Style = Dialogs.FolderBrowser.fbStyles.RestrictToDomain;
//			//fBrowser.Style = Dialogs.FolderBrowser.fbStyles.RestrictToFilesystem;
//			//fBrowser.Style = Dialogs.FolderBrowser.fbStyles.ShowTextBox;
//
//if (fBrowser.ShowDialog()==DialogResult.OK) 
//{
//txtDirectoryPath.Text = fBrowser.DirectoryPath;
//}	

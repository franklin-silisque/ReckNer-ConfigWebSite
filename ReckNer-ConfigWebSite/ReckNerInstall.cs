using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ReckNer_ConfigWebSite
{
    [RunInstaller(true)]
    public partial class ReckNerInstall : System.Configuration.Install.Installer
    {
        public ReckNerInstall()
        {
            InitializeComponent();
        }
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            try
            {
                AddConfigurationFileDetails();
            }
            catch (Exception e)
            {
                MessageBox.Show("Falta actualizar el archivo de configuración: " + e.Message);
                base.Rollback(savedState);
            }
        }
        private void showParameters()
        {
            StringBuilder sb = new StringBuilder();
            StringDictionary myStringDictionary = this.Context.Parameters;
            if (this.Context.Parameters.Count > 0)
            {
                foreach (string myString in this.Context.Parameters.Keys)
                {
                    sb.AppendFormat("String={0} Value= {1}\n", myString,
                    this.Context.Parameters[myString]);
                }
            }
            MessageBox.Show(sb.ToString());
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }
        private void AddConfigurationFileDetails()
        {
            try
            {
                string AdminUser = Context.Parameters["TXTUSER"];
                string WhiteList = Context.Parameters["TXTWHITELIST"];
                string FileDisclaimer = Context.Parameters["TXTURLFILE"];
                string URLAdminSite = Context.Parameters["TXTTENANTSITE"];

                // Get the path to the executable file that is being installed on the target computer  
                string assemblypath = Context.Parameters["assemblypath"];
                string appConfigPath = assemblypath + ".config";

                // Write the path to the app.config file  
                XmlDocument doc = new XmlDocument();
                doc.Load(appConfigPath);

                XmlNode configuration = null;
                foreach (XmlNode node in doc.ChildNodes)
                    if (node.Name == "configuration")
                        configuration = node;

                if (configuration != null)
                {
                    //MessageBox.Show("configuration != null");  
                    // Get the ‘appSettings’ node  
                    XmlNode settingNode = null;
                    foreach (XmlNode node in configuration.ChildNodes)
                    {
                        if (node.Name == "appSettings")
                            settingNode = node;
                    }

                    if (settingNode != null)
                    {
                        //MessageBox.Show("settingNode != null");  
                        //Reassign values in the config file  
                        foreach (XmlNode node in settingNode.ChildNodes)
                        {
                            //MessageBox.Show("node.Value = " + node.Value);  
                            if (node.Attributes == null)
                                continue;
                            XmlAttribute attribute = node.Attributes["value"];
                            //MessageBox.Show("attribute != null ");  
                            //MessageBox.Show("node.Attributes['value'] = " + node.Attributes["value"].Value);  
                            if (node.Attributes["key"] != null)
                            {
                                //MessageBox.Show("node.Attributes['key'] != null ");  
                                //MessageBox.Show("node.Attributes['key'] = " + node.Attributes["key"].Value);  
                                switch (node.Attributes["key"].Value)
                                {
                                    case "UserAdmin":
                                        attribute.Value = AdminUser;
                                        break;
                                    case "URLSiteAdmin":
                                        attribute.Value = URLAdminSite;
                                        break;
                                    case "WhiteList":
                                        attribute.Value = WhiteList;
                                        break;
                                    case "FileDisclaimerURL":
                                        attribute.Value = FileDisclaimer;
                                        break;
                                }
                            }
                        }
                    }
                    doc.Save(appConfigPath);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

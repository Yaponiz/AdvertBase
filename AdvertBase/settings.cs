// -----------------------------------------------------------------------
// <copyright file="settings.cs" company="Hewlett-Packard">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AdvertBase
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class settingsData
    {
        public string dbname = "", server = "", dbuser = "", dbpass = "", dbPort = "";

        public void getSettings()
        {
            try
            {
                FileStream f = new FileStream("properties.xml", FileMode.OpenOrCreate);

                XmlTextReader settings = new XmlTextReader(f);
                while (settings.Read())
                {
                    if (settings.NodeType == XmlNodeType.Element)
                    {
                        if (settings.Name.Equals("server"))
                        {
                            server = settings.GetAttribute("servername");
                            dbname = settings.GetAttribute("dbname");
                            dbuser = settings.GetAttribute("dbuser");
                            dbpass = settings.GetAttribute("dbpass");
                            dbPort = settings.GetAttribute("dbport");
                        }
                    }
                }
                f.Close();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
                 
        }


    }
}

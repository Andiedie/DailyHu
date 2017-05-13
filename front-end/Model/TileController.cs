using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace front_end.Model {
    public static class TileController {
        public static void run() {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText("Assets/Tile.xml"));
            TileUpdater updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.Clear();
            TileNotification titleNotification = new TileNotification(doc);
            updater.Update(titleNotification);
            updater.Update(changeSite(doc, Site.tuicool));
            updater.Update(changeSite(doc, Site.cnode));
            updater.Update(changeSite(doc, Site.zhihu));
            updater.Update(changeSite(doc, Site.sina));
            updater.EnableNotificationQueue(true);
        }

        private static TileNotification changeSite(XmlDocument doc, Site site) {
            var nodes = doc.GetElementsByTagName("image");
            char[] size = { 's', 'm', 'w', 'l' };
            int i = 0;
            foreach (IXmlNode node in nodes) {
                var a = node.Attributes.First() as XmlAttribute;
                a.Value = $"Assets/Site/{site.ToString()}-{size[i++]}.jpg";
            }
            return new TileNotification(doc);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace front_end.Model {
    // 用于更新动态磁贴
    public static class TileController {
        public static void run(List<Site> sites) {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText("Assets/Tile.xml"));
            TileUpdater updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.Clear();
            TileNotification titleNotification = new TileNotification(doc);
            updater.Update(titleNotification);
            sites.ForEach(site => updater.Update(changeSite(doc, site)));
            updater.EnableNotificationQueue(true);
        }

        private static TileNotification changeSite(XmlDocument doc, Site site) {
            var nodes = doc.GetElementsByTagName("image");
            ((XmlAttribute)nodes[0].Attributes.First()).Value = site.TileImage.Small;
            ((XmlAttribute)nodes[1].Attributes.First()).Value = site.TileImage.Medium;
            ((XmlAttribute)nodes[2].Attributes.First()).Value = site.TileImage.Wide;
            ((XmlAttribute)nodes[3].Attributes.First()).Value = site.TileImage.Large;
            return new TileNotification(doc);
        }
    }
}

using System;
using System.Collections.Generic;
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
            TileNotification titleNotification = new TileNotification(doc);
            TileUpdater updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.Update(titleNotification);
            updater.EnableNotificationQueue(true);
        }
    }
}

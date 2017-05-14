using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace front_end.Model {
    public static class DB {
        private static SQLiteConnection connection = new SQLiteConnection("ribaohu.db");
        static DB() {
            var state = connection.Prepare(
              @"CREATE TABLE IF NOT EXISTS sites (
                    name TEXT,
                    icon TEXT,
                    small TEXT,
                    medium TEXT,
                    wide TEXT,
                    large TEXT
                ); ");
            state.Step();
        }
        public static void update(List<Site> sites) {
            var state = connection.Prepare("DELETE FROM sites;");
            state.Step();
            sites.ForEach(site => {
                state = connection.Prepare("INSERT INTO sites VALUES (?,?,?,?,?,?);");
                state.Bind(1, site.Name);
                state.Bind(2, site.Icon);
                state.Bind(3, site.TileImage.Small);
                state.Bind(4, site.TileImage.Medium);
                state.Bind(5, site.TileImage.Wide);
                state.Bind(6, site.TileImage.Large);
                state.Step();
            });
        }
        public static List<Site> getSites() {
            var sites = new List<Site>();
            var state = connection.Prepare("SELECT name, icon, small, medium, wide, large FROM sites;");
            state.Step();
            while (state[0] != null) {
                sites.Add(new Site((string)state[0], (string)state[1], new TileImage((string)state[2], (string)state[3], (string)state[4], (string)state[5])));
                state.Step();
            }
            return sites;
        }
    }
}

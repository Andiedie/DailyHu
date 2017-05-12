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
        static DB(){
            var state = connection.Prepare(
              @"CREATE TABLE IF NOT EXISTS ribaohu (
                    site INTEGER
                ); ");
            state.Step();
            state = connection.Prepare("SELECT site FROM ribaohu;");
            state.Step();
            if (state[0] == null) {
                state = connection.Prepare("INSERT INTO ribaohu (site) VALUES (0);");
                state.Step();
            }
        }
        public static Site getSite() {
            var state = connection.Prepare("SELECT site FROM ribaohu;");
            state.Step();
            return (Site)Convert.ToInt32(state[0]);
        }
        public static void updateSite(Site site) {
            var state = connection.Prepare("UPDATE ribaohu SET site = ?;");
            state.Bind(1, (int)site);
            state.Step();
        }
    }
}

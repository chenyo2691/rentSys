using MySql.Data.MySqlClient;
using rentSys.Common;
using rentSys.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentSys.ModalHelper
{
    public class userModalHelper : BaseModalHelper
    {
        public userModalHelper(MYSQLWapper wap, string table_name = "user") : base(wap, table_name) { }

        public userEntity getCurrentLoginUserInfo(int uid)
        {
            userEntity curUser = null;
            if (!string.IsNullOrWhiteSpace(uid.ToString()))
            {
                MySqlCommand cmd = dbWapper.Adapter.SelectCommand;
                cmd.CommandText = "select * from [#tname#] where id=" + uid;
                cmd.CommandText = cmd.CommandText.Replace("[#tname#]", this.TableName);
                cmd.Parameters.Clear();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        curUser = new userEntity()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            username = reader.GetString(reader.GetOrdinal("username")),
                            password = reader.GetString(reader.GetOrdinal("password"))
                        };
                    }
                }
            }
            return curUser;
        }


        public userEntity getCurrentLoginUserInfo(string username, string psw)
        {
            userEntity curUser = null;

            MySqlCommand cmd = dbWapper.Adapter.SelectCommand;
            cmd.CommandText = "select * from [#tname#] where username=\"" + username + "\" and password=\"" + psw + "\"";
            cmd.CommandText = cmd.CommandText.Replace("[#tname#]", this.TableName);
            cmd.Parameters.Clear();

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    curUser = new userEntity()
                    {
                        id = reader.GetInt32(reader.GetOrdinal("id")),
                        username = reader.GetString(reader.GetOrdinal("username")),
                        password = reader.GetString(reader.GetOrdinal("password"))
                    };
                }
            }
            return curUser;
        }
    }
}

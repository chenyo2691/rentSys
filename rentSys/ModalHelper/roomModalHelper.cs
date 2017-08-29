using MySql.Data.MySqlClient;
using rentSys.Common;
using rentSys.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rentSys.ModalHelper
{
    public class roomModalHelper : BaseModalHelper, ICRUD<roomEntity>
    {
        public roomModalHelper(MYSQLWapper wap, string table_name = "room") : base(wap, table_name) { }

        public List<roomEntity> RetrieveAll()
        {
            List<roomEntity> data = new List<roomEntity>();

            MySqlCommand cmd = dbWapper.Adapter.SelectCommand;
            cmd.CommandText = "select * from [#tname#]";
            cmd.CommandText = cmd.CommandText.Replace("[#tname#]", this.TableName);
            cmd.Parameters.Clear();

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    roomEntity m = new roomEntity();
                    PropertyInfo[] pps = m.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var pp in pps)
                    {
                        string propertyName = pp.Name;
                        PropertyInfo targetPP = m.GetType().GetProperty(propertyName);
                        if (pp.PropertyType == typeof(string))
                        {

                        }
                        else if (pp.PropertyType == typeof(Int32))
                        {

                        }
                        else if (pp.PropertyType == typeof(decimal))
                        {

                        }



                        object value = null;
                        if (defaultVal.GetType() == typeof(Int32))
                        {
                            value = reader.GetInt32(reader.GetOrdinal(propertyName));
                        }
                        else
                        {
                            value = reader.GetString(reader.GetOrdinal(propertyName));
                        }

                        if (targetPP != null && value != null)
                        {
                            targetPP.SetValue(m, value, null);
                        }
                    }
                    //roomEntity m = new roomEntity()
                    //{
                    //    id = reader.GetInt32(reader.GetOrdinal("id")),
                    //    floor = reader.GetInt32(reader.GetOrdinal("floor")),
                    //    number = reader.GetString(reader.GetOrdinal("number")),
                    //    price = reader.GetDecimal(reader.GetOrdinal("price")),
                    //    remark = reader.GetString(reader.GetOrdinal("remark")),
                    //    status = reader.GetString(reader.GetOrdinal("status")),
                    //};
                    data.Add(m);
                }
            }
            return data;
        }

        public int Create(roomEntity data)
        {
            MySqlCommand cmd = dbWapper.Adapter.InsertCommand;
            cmd.CommandText = @"insert into [#tname#] (floor,number,price,remark,status) values (@floor,@number,@price,@remark,@status)";
            cmd.CommandText = cmd.CommandText.Replace("[#tname#]", this.TableName);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@floor", data.floor);
            cmd.Parameters.AddWithValue("@number", data.number);
            cmd.Parameters.AddWithValue("@price", data.price);
            cmd.Parameters.AddWithValue("@remark", data.remark);
            cmd.Parameters.AddWithValue("@status", data.status);

            int rc = cmd.ExecuteNonQuery();
            if (rc == 1)
            {
                int rowid = dbWapper.Get_Last_Insert_ID();
                data.id = rowid;
                return rowid;
            }
            else
            {
                throw new Exception(this.TableName + " Create Error");
            }
        }

        public int Delete(roomEntity data)
        {
            MySqlCommand cmd = dbWapper.Adapter.DeleteCommand;
            cmd.CommandText = @"delete from [#tname#] where id = @id";
            cmd.CommandText = cmd.CommandText.Replace("[#tname#]", this.TableName);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", data.id);
            int rc = cmd.ExecuteNonQuery();
            if (rc > 0)
            {
                return rc;
            }
            else
            {
                throw new Exception(this.TableName + " Delete Error");
            }
        }

        public int Update(roomEntity data)
        {
            if (data.id <= 0)
            {
                throw new Exception(this.TableName + " Update ID Error");
            }

            MySqlCommand cmd = dbWapper.Adapter.UpdateCommand;
            cmd.CommandText = @"update [#tname#] 
                                set floor=@floor,number=@number,price=@price,remark=@remark,status=@status";
            cmd.CommandText = cmd.CommandText.Replace("[#tname#]", this.TableName);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", data.id);
            cmd.Parameters.AddWithValue("@floor", data.floor);
            cmd.Parameters.AddWithValue("@number", data.number);
            cmd.Parameters.AddWithValue("@price", data.price);
            cmd.Parameters.AddWithValue("@remark", data.remark);
            cmd.Parameters.AddWithValue("@status", data.status);

            int rc = cmd.ExecuteNonQuery();
            if (rc == 1)
            {
                return rc;
            }
            else
            {
                throw new Exception(this.TableName + " Update Error");
            }
        }
    }
}

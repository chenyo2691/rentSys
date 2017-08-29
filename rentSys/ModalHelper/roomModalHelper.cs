using MySql.Data.MySqlClient;
using rentSys.Common;
using rentSys.Entitys;
using System;
using System.Collections.Generic;
using System.Reflection;

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
                    roomEntity entity = new roomEntity();
                    SmartDataReader smr_reader = new SmartDataReader(reader);
                    PropertyInfo[] PropertyInfos = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var Property in PropertyInfos)
                    {
                        string propertyName = Property.Name;// 属性名称
                        PropertyInfo instance = entity.GetType().GetProperty(propertyName);// 属性 

                        #region Value赋值
                        object value = null;
                        if (Property.PropertyType == typeof(string))
                        {
                            value = smr_reader.GetString(propertyName);
                        }
                        else if (Property.PropertyType == typeof(Int32))
                        {
                            value = smr_reader.GetInt32(propertyName);
                        }
                        else if (Property.PropertyType == typeof(Int16))
                        {
                            value = smr_reader.GetInt16(propertyName);
                        }
                        else if (Property.PropertyType == typeof(decimal))
                        {
                            value = smr_reader.GetDecimal(propertyName);
                        }
                        else if (Property.PropertyType == typeof(double))
                        {
                            value = smr_reader.GetDouble(propertyName);
                        }
                        else if (Property.PropertyType == typeof(DateTime))
                        {
                            value = smr_reader.GetDateTime(propertyName);
                        }
                        else if (Property.PropertyType == typeof(float))
                        {
                            value = smr_reader.GetFloat(propertyName);
                        }
                        else if (Property.PropertyType == typeof(Single))
                        {
                            value = smr_reader.GetSingle(propertyName);
                        }
                        else if (Property.PropertyType == typeof(Boolean))
                        {
                            value = smr_reader.GetBoolean(propertyName);
                        }
                        else if (Property.PropertyType == typeof(Byte))
                        {
                            value = smr_reader.GetBytes(propertyName);
                        }
                        else if (Property.PropertyType == typeof(Guid))
                        {
                            value = smr_reader.GetGuid(propertyName);
                        }
                        #endregion

                        if (instance != null && value != null)
                        {
                            instance.SetValue(entity, value, null);
                        }
                    }
                    data.Add(entity);
                }
            }
            return data;
        }

        public int Create(roomEntity data)
        {
            string sqlFront = string.Join(",", getPropertiesFromEntity(data, new List<string>() { "id" }));
            string sqlEnd = string.Join(",", getPropertiesFromEntity(data, new List<string>() { "id" }, "@"));

            MySqlCommand cmd = dbWapper.Adapter.InsertCommand;
            //cmd.CommandText = @"insert into [#tname#] (floor,number,price,remark,status) values (@floor,@number,@price,@remark,@status)";
            cmd.CommandText = string.Format(@"insert into [#tname#] ({0}) values ({1})", sqlFront, sqlEnd);
            cmd.CommandText = cmd.CommandText.Replace("[#tname#]", this.TableName);
            cmd.Parameters.Clear();

            PropertyInfo[] PropertyInfos = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var Property in PropertyInfos)
            {
                string propertyName = Property.Name;// 属性
                if (!propertyName.Equals("id", StringComparison.CurrentCultureIgnoreCase))
                {
                    PropertyInfo instance = data.GetType().GetProperty(propertyName);// 实例  
                    cmd.Parameters.AddWithValue("@" + propertyName, instance.GetValue(data, null));
                }
            }
            //cmd.Parameters.AddWithValue("@floor", data.floor);
            //cmd.Parameters.AddWithValue("@number", data.number);
            //cmd.Parameters.AddWithValue("@price", data.price);
            //cmd.Parameters.AddWithValue("@remark", data.remark);
            //cmd.Parameters.AddWithValue("@status", data.status);

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

            PropertyInfo[] PropertyInfos = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<string> lstPros = new List<string>();
            foreach (var Property in PropertyInfos)
            {
                string propertyName = Property.Name;// 属性
                if (!propertyName.Equals("id", StringComparison.CurrentCultureIgnoreCase))
                {
                    lstPros.Add(propertyName + "=@" + propertyName);
                }
            }

            MySqlCommand cmd = dbWapper.Adapter.UpdateCommand;
            //cmd.CommandText = @"update [#tname#] set floor=@floor,number=@number,price=@price,remark=@remark,status=@status";
            cmd.CommandText = string.Format(@"update [#tname#] set {0}", string.Join(",", lstPros));
            cmd.CommandText = cmd.CommandText.Replace("[#tname#]", this.TableName);
            cmd.Parameters.Clear();

            foreach (var Property in PropertyInfos)
            {
                string propertyName = Property.Name;// 属性
                PropertyInfo instance = data.GetType().GetProperty(propertyName);// 实例  
                cmd.Parameters.AddWithValue("@" + propertyName, instance.GetValue(data, null));
            }


            //cmd.Parameters.AddWithValue("@id", data.id);
            //cmd.Parameters.AddWithValue("@floor", data.floor);
            //cmd.Parameters.AddWithValue("@number", data.number);
            //cmd.Parameters.AddWithValue("@price", data.price);
            //cmd.Parameters.AddWithValue("@remark", data.remark);
            //cmd.Parameters.AddWithValue("@status", data.status);

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

        /// <summary>
        /// 通过实例获取属性
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ignoreColumns"></param>
        /// <param name="charsetFront"></param>
        /// <returns></returns>
        private List<string> getPropertiesFromEntity(roomEntity data, List<string> ignoreColumns, string charsetFront = "")
        {
            List<string> lst = new List<string>();
            PropertyInfo[] PropertyInfos = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var Property in PropertyInfos)
            {
                string propertyName = Property.Name;// 属性
                if (!ignoreColumns.Contains(propertyName))
                    lst.Add(charsetFront + propertyName);
            }
            return lst;
        }
    }
}

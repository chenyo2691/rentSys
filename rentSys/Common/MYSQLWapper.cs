using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace rentSys.Common
{
    public class MYSQLWapper
    {
        public MySqlConnection Conn { get; private set; }
        public MySqlDataAdapter Adapter { get; private set; }
        MySqlTransaction Tra = null;

        string Database = "db_rente";
        string Server = "localhost";
        string UserID = "root";
        string Password = "";

        public MYSQLWapper()
        {
            this.Initial();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void Initial()
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Database = Database;
                builder.Server = Server;
                builder.UserID = UserID;
                builder.Password = Password;
                builder.CharacterSet = "utf8";

                Conn = new MySqlConnection(builder.ToString());
                Adapter = new MySqlDataAdapter();
                Adapter.SelectCommand = new MySqlCommand("", Conn);
                Adapter.InsertCommand = new MySqlCommand("", Conn);
                Adapter.UpdateCommand = new MySqlCommand("", Conn);
                Adapter.DeleteCommand = new MySqlCommand("", Conn);
            }
            catch (MySqlException exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 开始一个事务
        /// </summary>
        public void BeginTransaction()
        {
            this.Tra = this.Conn.BeginTransaction();

            Adapter.SelectCommand.Transaction = this.Tra;
            Adapter.InsertCommand.Transaction = this.Tra;
            Adapter.UpdateCommand.Transaction = this.Tra;
            Adapter.DeleteCommand.Transaction = this.Tra;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            if (this.Tra != null)
            {
                this.Tra.Commit();
            }
            else
            {
                throw new Exception("Transaction Is Null");
            }
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void RollbackTransaction()
        {
            if (this.Tra != null)
            {
                this.Tra.Rollback();
            }
            else
            {
                throw new Exception("Transaction Is Null");
            }
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        public void Open()
        {
            if (this.Conn.State != ConnectionState.Open)
            {
                this.Conn.Open();
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (this.Conn.State != ConnectionState.Closed)
            {
                this.Conn.Close();
            }
        }

        /// <summary>
        /// 获取最新的自增ID值
        /// </summary>
        /// <returns></returns>
        public int Get_Last_Insert_ID()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", this.Conn);
            if (this.Tra != null)
            {
                cmd.Transaction = this.Tra;
            }
            object val = cmd.ExecuteScalar();
            return Convert.ToInt32(val);
        }

    }
}

using rentSys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentSys.ModalHelper
{
    public class BaseModalHelper
    {
        protected MYSQLWapper dbWapper = new MYSQLWapper();

        protected string TableName = string.Empty;

        public BaseModalHelper(MYSQLWapper wapper, string table_name)
        {
            dbWapper = wapper;
            TableName = table_name;
        }
    }
}

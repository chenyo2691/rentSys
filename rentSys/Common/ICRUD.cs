using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentSys.Common
{
    public interface ICRUD<T>
    {
        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        List<T> RetrieveAll();

        /// <summary>
        /// 创建一个记录
        /// </summary>
        /// <param name="data"></param>
        int Create(T data);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="data"></param>
        int Update(T data);

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="data"></param>
        int Delete(T data);
    }
}

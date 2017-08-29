using rentSys.Common;
using rentSys.Entitys;
using rentSys.ModalHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentSys.Modals
{
    public class roomModal
    {
        List<roomEntity> lst_room = null;

        public List<roomEntity> RetrieveAll()
        {
            if (lst_room == null)
            {
                MYSQLWapper wap = new MYSQLWapper();
                roomModalHelper dal_room = new roomModalHelper(wap);
                wap.Open();
                wap.BeginTransaction();
                lst_room = dal_room.RetrieveAll();
                wap.CommitTransaction();
                wap.Close();
            }
            return lst_room;
        }

        public bool Create(roomEntity data)
        {
            MYSQLWapper wap = new MYSQLWapper();
            roomModalHelper dal_room = new roomModalHelper(wap);
            wap.Open();
            wap.BeginTransaction();
            int result = dal_room.Create(data);
            wap.CommitTransaction();
            wap.Close();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(roomEntity data)
        {
            MYSQLWapper wap = new MYSQLWapper();
            roomModalHelper dal_room = new roomModalHelper(wap);
            wap.Open();
            wap.BeginTransaction();
            int result = dal_room.Update(data);
            wap.CommitTransaction();
            wap.Close();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(roomEntity data)
        {
            MYSQLWapper wap = new MYSQLWapper();
            roomModalHelper dal_room = new roomModalHelper(wap);
            wap.Open();
            wap.BeginTransaction();
            int result = dal_room.Delete(data);
            wap.CommitTransaction();
            wap.Close();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

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
    public class userModal
    {
        userEntity userInfo = null;

        public userEntity getUserInfo(int uid)
        {
            if (userInfo == null)
            {
                MYSQLWapper wap = new MYSQLWapper();
                userModalHelper dal_user = new userModalHelper(wap);
                wap.Open();
                wap.BeginTransaction();
                userInfo = dal_user.getCurrentLoginUserInfo(uid);
                wap.CommitTransaction();
                wap.Close();
            }
            return userInfo;
        }

        public userEntity getUserInfo(string username, string password)
        {
            if (userInfo == null)
            {
                MYSQLWapper wap = new MYSQLWapper();
                userModalHelper dal_user = new userModalHelper(wap);
                wap.Open();
                wap.BeginTransaction();
                userInfo = dal_user.getCurrentLoginUserInfo(username, password);
                wap.CommitTransaction();
                wap.Close();
            }
            return userInfo;
        }
    }
}

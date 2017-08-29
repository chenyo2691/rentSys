using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using WpfPageTransitions;
using MahApps.Metro.Controls.Dialogs;
using rentSys.Entitys;
using rentSys.Modals;

namespace rentSys
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool checkUserToken()
        {
            int cur_uid = rentSys.Properties.Settings.Default.uid;
            string username = rentSys.Properties.Settings.Default.username;
            string logintime = rentSys.Properties.Settings.Default.logintime;
            bool remember = rentSys.Properties.Settings.Default.remember;

            #region 第一次登录
            if (cur_uid == 0 || string.IsNullOrWhiteSpace(username))
            {
                return false;
            }
            #endregion

            userModal modal = new userModal();
            userEntity curUser = modal.getUserInfo(cur_uid);

            #region 用户不存在
            if (curUser == null)
            {
                return false;
            }
            #endregion

            #region 登录状态已过期
            if (logintime == null || string.IsNullOrWhiteSpace(logintime))
            {
                return false;
            }

            int rememberDays = 7;
            if (remember == false)
                rememberDays = 1;

            DateTime lst = Convert.ToDateTime(logintime);
            //MessageBox.Show("上次登录时间：" + lst);
            DateTime now = DateTime.Now;
            //MessageBox.Show("今次登录时间：" + now);
            TimeSpan span = now.Subtract(lst);

            if (span.Days > rememberDays)
            {
                return false;
            }
            #endregion

            #region 状态登录成功
            //rentSys.Properties.Settings.Default.logintime = DateTime.Now.ToString();
            //rentSys.Properties.Settings.Default.Save();
            return true;
            #endregion
        }

        private async void loginSys()
        {
            LoginDialogData result = await this.ShowLoginAsync(
                   "用户登录",
                   "您好，请登录",
                   new LoginDialogSettings
                   {
                       ColorScheme = this.MetroDialogOptions.ColorScheme,
                       InitialUsername = rentSys.Properties.Settings.Default.username,
                       UsernameWatermark = "用户名...",
                       PasswordWatermark = "密码...",
                       RememberCheckBoxText = "记住我",
                       RememberCheckBoxVisibility = Visibility.Visible,
                       AffirmativeButtonText = "登录"
                   });
            if (result == null)
            {
                loginSys();
            }
            else
            {
                #region 登录-数据库验证
                string username = result.Username;
                string password = result.Password;
                bool remember = result.ShouldRemember;
                userModal modal = new userModal();
                userEntity curUser = modal.getUserInfo(username, password);
                if (curUser == null)
                {
                    if (MessageDialogResult.Affirmative == await this.ShowMessageAsync(
                    "系统提醒", "登录失败！",
                    MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { AffirmativeButtonText = "重试", NegativeButtonText = "退出系统" }))
                    {
                        loginSys();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    rentSys.Properties.Settings.Default.uid = curUser.id;
                    rentSys.Properties.Settings.Default.username = curUser.username;
                    rentSys.Properties.Settings.Default.logintime = DateTime.Now.ToString();
                    rentSys.Properties.Settings.Default.remember = remember;
                    rentSys.Properties.Settings.Default.Save();
                    return;
                }
                #endregion
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.checkUserToken())
            {
                loginSys();
            }
            //PageTransitionControl.ShowPage(new rentSys.Views.home());
            PageTransitionControl.ShowPage(new rentSys.Views.room.index());
        }

        private void home_click(object sender, RoutedEventArgs e)
        {
            PageTransitionControl.ShowPage(new rentSys.Views.home());
        }

        private void roomManage_click(object sender, RoutedEventArgs e)
        {
            PageTransitionControl.ShowPage(new rentSys.Views.room.index());
        }

        private void personMange_click(object sender, RoutedEventArgs e)
        {
            saySthNotReady();
        }

        private void baseManger_click(object sender, RoutedEventArgs e)
        {
            saySthNotReady();
        }

        private void logManager_click(object sender, RoutedEventArgs e)
        {
            saySthNotReady();
        }

        private void payManager_click(object sender, RoutedEventArgs e)
        {
            saySthNotReady();
        }

        private async void saySthNotReady()
        {
            await this.ShowMessageAsync("系统提示", "稍后推出");
            //await this.ShowInputAsync("title", "???");


        }

        private async void logout_click(object sender, RoutedEventArgs e)
        {
            //loginSys
            MessageDialogResult result = await this.ShowMessageAsync("系统提醒", "注销将重启系统，是否继续！",
                MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { AffirmativeButtonText = "是", NegativeButtonText = "否" });

            if (MessageDialogResult.Affirmative == result)
            {
                rentSys.Properties.Settings.Default.uid = 0;
                rentSys.Properties.Settings.Default.username = "";
                rentSys.Properties.Settings.Default.logintime = "";
                rentSys.Properties.Settings.Default.remember = false;
                rentSys.Properties.Settings.Default.Save();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }
    }
}

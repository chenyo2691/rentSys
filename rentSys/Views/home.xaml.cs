using System.Windows.Controls;

namespace rentSys.Views
{
    /// <summary>
    /// home.xaml 的交互逻辑
    /// </summary>
    public partial class home
    {
        public home()
        {
            InitializeComponent();
        }

        private void home_loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            string username = rentSys.Properties.Settings.Default.username;
            this.tb_hello.Text = "您好！" + username;
        }
    }
}

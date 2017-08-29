using rentSys.Entitys;
using rentSys.Modals;
using System.Collections.Generic;
using System.Windows.Controls;

namespace rentSys.Views.room
{
    public enum statusOpt
    {
        未入住,
        已入住,
        其他,
    }

    /// <summary>
    /// index.xaml 的交互逻辑
    /// </summary>
    public partial class index
    {
        public index()
        {
            InitializeComponent();
        }

        private void index_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            roomModal modal = new roomModal();
            List<roomEntity> list = modal.RetrieveAll();
            this.grid.ItemsSource = list;
        }

        private void add(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void modify(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void delete(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}

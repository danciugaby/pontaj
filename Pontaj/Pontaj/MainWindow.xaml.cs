using DAL;
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

namespace Pontaj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller controller;
        public MainWindow()
        {
            InitializeComponent();
            controller = new Controller();
            grdUserManager.IsEnabled = false;
        }

        private void BtnLoadUser_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = controller.GetUsersFromDB();
            foreach (var user in users)
            {
                lstUsers.Items.Add(user);
            }
        }
        void EnableControls(Boolean enable)
        {
            grdUserManager.IsEnabled = enable;

        }
        private void LstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUsers.SelectedIndex < lstUsers.Items.Count)
            {
                EnableControls(true);
                User u = controller.users.Users[lstUsers.SelectedIndex];
                txtNume.Text = u.Name;
                txtGrad.Text = u.Rank;
            }
            else
                EnableControls(false);
        }
    }
}

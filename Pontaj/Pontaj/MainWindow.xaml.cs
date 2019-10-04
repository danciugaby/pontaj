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
            LoadUsers();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = controller.GetUsersFromDB();
            string name = userNameTextBoxPersonalTab.Text;
            bool canInsert = true;
            if (isEmpty(name))
            {
                MessageBox.Show("Te rog introdu numele corect!");
                canInsert = false;
                emptyNameField();
            }
            string rank = userRankTextBoxPersonalTab.Text;
            if (canInsert && isEmpty(rank))
            {
                MessageBox.Show("Te rog introdu gradul corect!");
                canInsert = false;
                emptyRankField();
            }

            if (canInsert)
            {
                User newUser = new User(name, rank);
                bool canAdd = true;
                foreach (var user in users)
                    if (user.Equals(newUser))
                        canAdd = false;
                if (canAdd)
                {
                    users.Add(newUser);
                    controller.AddUserInDB(newUser);
                    emptyFields();
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Utilizatorul exista deja!");
                }
                
              
            }
        }

        private bool isEmpty(string str)
        {
            return str.Trim().Equals("");
        }
        private void emptyNameField()
        {
            userNameTextBoxPersonalTab.Clear();
        }
        private void emptyRankField()
        {
            userRankTextBoxPersonalTab.Clear();
        }
        private void emptyFields()
        {
            emptyNameField();
            emptyRankField();
        }

        private void LstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selectedUser = lstUsers.SelectedItem as User;
            if (selectedUser != null)
            {
                populateFields(selectedUser);
                setEnabledUptadeUserButton();
                setEnabledDeleteUserButton();
               
            }

        }

        private void populateFields(User user)
        {
            userNameTextBoxPersonalTab.Text = user.Name;
            userRankTextBoxPersonalTab.Text = user.Rank;
        }
        private void setEnabledUptadeUserButton()
        {
            btnUpdateUser.IsEnabled = true;
        }
        private void setDisabledUptadeUserButton()
        {
            btnUpdateUser.IsEnabled = false;
        }
        private void setEnabledDeleteUserButton()
        {
            btnDeleteUser.IsEnabled = true;
        }
        private void setDisabledDeleteUserButton()
        {
            btnDeleteUser.IsEnabled = false;
        }
       
       
        private bool canChangeUserInDB(User user)
        {
            bool canChange = true;
            if (isEmpty(user.Name) || isEmpty(user.Rank))
            {
                MessageBox.Show("Nu se poate face modificarea!");
                canChange = false;
            }
            return canChange;
        }

        private void BtnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = lstUsers.SelectedItem as User;
            if (selectedUser != null)
            {
                string name = userNameTextBoxPersonalTab.Text;
                string rank = userRankTextBoxPersonalTab.Text;
                User modifiedUser = new User(name, rank);
                if (canChangeUserInDB(modifiedUser))
                    if (selectedUser.Equals(modifiedUser))
                    {
                        MessageBox.Show("Nu ai facut nicio modificare!");
                    }
                    else
                    {
                        controller.UpdateUserInDB(modifiedUser, selectedUser);
                        emptyFields();
                        setDisabledUptadeUserButton();
                        setDisabledDeleteUserButton();
                        LoadUsers();
                    }
            }
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = lstUsers.SelectedItem as User;
            if (selectedUser != null)
            {
                controller.DeleteUserFromDB(selectedUser);
               
                setDisabledUptadeUserButton();
                setDisabledDeleteUserButton();
                emptyFields();
                LoadUsers();
                
            }
        }
        public void LoadUsers()
        {
            lstUsers.Items.Clear();
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

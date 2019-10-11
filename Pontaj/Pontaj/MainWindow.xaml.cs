﻿using DAL;
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
                emptyField(userNameTextBoxPersonalTab);
            }
            string rank = userRankTextBoxPersonalTab.Text;
            if (canInsert && isEmpty(rank))
            {
                MessageBox.Show("Te rog introdu gradul corect!");
                canInsert = false;
                emptyField(userRankTextBoxPersonalTab);
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
        private void emptyField(TextBox box)
        {
            box.Clear();
        }  

        private void emptyFields()
        {
            emptyField(userNameTextBoxPersonalTab);
            emptyField(userRankTextBoxPersonalTab);
        }

        private void LstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selectedUser = lstUsers.SelectedItem as User;
            if (selectedUser != null)
            {
                populateFields(selectedUser);
                setEnabledButton(btnUpdateUser);
                setEnabledButton(btnDeleteUser);

            }

        }

        private void populateFields(User user)
        {
            userNameTextBoxPersonalTab.Text = user.Name;
            userRankTextBoxPersonalTab.Text = user.Rank;
        }
        private void setEnabledButton(Button button)
        {
            button.IsEnabled = true;
        }
        private void setDisabledButton(Button button)
        {
            button.IsEnabled = false;
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
                        setDisabledButton(btnUpdateUser);
                        setDisabledButton(btnDeleteUser);
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

                setDisabledButton(btnUpdateUser);
                setDisabledButton(btnDeleteUser);
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
        private void LoadTypes()
        {
            lstTypes.Items.Clear();
            List<ClockingType> types = controller.GetTypesFromDB();
            foreach (var type in types)
            {
                lstTypes.Items.Add(type);
            }
        }

        private void BtnLoadClocking_Click(object sender, RoutedEventArgs e)
        {
            LoadTypes();
        }

        private void BtnAddClockingType_Click(object sender, RoutedEventArgs e)
        {
            List<ClockingType> types = controller.GetTypesFromDB();
            string type = clockingTextBox.Text;
            bool canInsert = true;
            if (isEmpty(type))
            {
                MessageBox.Show("Te rog introdu tipul de pontaj corect!");
                canInsert = false;
                
            }
            if (canInsert)
            {
                ClockingType newType = new ClockingType(type);
                bool canAdd = true;
                foreach (var x in types)
                    if (x.Equals(newType))
                        canAdd = false;
                if (canAdd)
                {
                    types.Add(newType);
                    controller.AddTypeInDB(newType);
                    emptyField(clockingTextBox);
                    LoadTypes();
                }
                else
                {
                    MessageBox.Show("Tipul de pontaj exista deja!");
                }


            }
        }

        private void LstTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClockingType selectedType = lstTypes.SelectedItem as ClockingType;
            if (selectedType != null)
            {
                populateTypeField(selectedType);
                setEnabledButton(btnUpdateClockingType);
                setEnabledButton(btnDeleteClockingType);

            }
        }
        private void populateTypeField(ClockingType type)
        {
            clockingTextBox.Text = type.Type;
        }
    }
}

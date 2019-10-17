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
                        List<User> users = controller.GetUsersFromDB();
                        bool canUpdate = true;
                        foreach (var x in users)
                            if (x.Equals(modifiedUser))
                            {
                                MessageBox.Show("Utilizatorul exista deja!");
                                canUpdate = false;
                                break;
                            }
                        if (canUpdate)
                        {
                            controller.UpdateUserInDB(modifiedUser, selectedUser);
                            emptyFields();
                            setDisabledButton(btnUpdateUser);
                            setDisabledButton(btnDeleteUser);
                            LoadUsers();
                        }
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

        private void BtnUpdateClockingType_Click(object sender, RoutedEventArgs e)
        {
            ClockingType selectedType = lstTypes.SelectedItem as ClockingType;
            if (selectedType != null)
            {
                string type = clockingTextBox.Text;

                ClockingType modifiedType = new ClockingType(type);
                if (!isEmpty(type))
                    if (selectedType.Equals(modifiedType))
                    {
                        MessageBox.Show("Nu ai facut nicio modificare!");
                    }
                    else
                    {
                        List<ClockingType> types = controller.GetTypesFromDB();
                        bool canUpdate = true;
                        foreach (var x in types)
                            if (x.Equals(modifiedType))
                            {
                                MessageBox.Show("Tipul de pontaj exista deja!");
                                canUpdate = false;
                                break;
                            }
                        if (canUpdate)
                        {
                            controller.UpdateTypeInDB(modifiedType, selectedType);
                            emptyField(clockingTextBox);
                            setDisabledButton(btnUpdateClockingType);
                            setDisabledButton(btnDeleteClockingType);
                            LoadTypes();
                        }
                    }
            }
        }

        private void BtnDeleteClockingType_Click(object sender, RoutedEventArgs e)
        {

            ClockingType selectedType = lstTypes.SelectedItem as ClockingType;
            if (selectedType != null)
            {
                controller.DeleteTypeFromDB(selectedType);

                setDisabledButton(btnUpdateClockingType);
                setDisabledButton(btnDeleteClockingType);
                emptyField(clockingTextBox);
                LoadTypes();

            }
        }

        private void LoadUsersIntoComboBox()
        {
            if (controller.users.Users == null || controller.users.Users.Count() == 0)
            {
                LoadUsers();
                if (controller.users.Users.Count() == 0)
                    userComboBox.Items.Add("Nu s-a gasit nicio persoana");
                else
                {

                    userComboBox.Items.Clear();
                    foreach (var user in controller.users.Users)
                    {
                        userComboBox.Items.Add(user);
                    }
                }
            }
            else
            {
                userComboBox.Items.Clear();
                foreach (var user in controller.users.Users)
                {
                    userComboBox.Items.Add(user);
                }
            }
        }
        private User CastToUser(ComboBoxItem item)
        {
            string[] values = item.ToString().Split(',');
            return new User(values[0], values[1]);

        }

        private void LoadTypesIntoComboBox()
        {
            if (controller.types.Types.Count()==0)
            {
                LoadTypes();
                if (controller.types.Types.Count() == 0)
                    clockingTypeComboBox.Items.Add("Nu s-a gasit niciun tip de pontaj");
                else
                {
                    clockingTypeComboBox.Items.Clear();
                    foreach (var type in controller.types.Types)
                    {
                        clockingTypeComboBox.Items.Add(type);
                    }
                }

            }
            else
            {
                clockingTypeComboBox.Items.Clear();
                foreach (var type in controller.types.Types)
                {
                    clockingTypeComboBox.Items.Add(type);
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl.SelectedItem.Equals(usersTabItem))
            {

            }
            else if (tabControl.SelectedItem.Equals(typesTabItem))
            {

            }
            else if (tabControl.SelectedItem.Equals(worksTabItem))
            {
                LoadTypesIntoComboBox();
                LoadUsersIntoComboBox();
            }
        }



        private void StartDateCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            endDateCalendar.BlackoutDates.Clear();
            var startDate = (DateTime)startDateCalendar.SelectedDate;

            endDateCalendar.SelectedDate = startDate;
            DateTime endDate;
            try
            {
                endDate = new DateTime(startDate.Year, startDate.Month, startDate.Day + 2);

            }
            catch (ArgumentOutOfRangeException ex)
            {
                try
                {
                    endDate = new DateTime(startDate.Year, startDate.Month + 1, 1);

                }
                catch (ArgumentOutOfRangeException ex2)
                {
                    endDate = new DateTime(startDate.Year + 1, 1, 1);
                }
            }
            DateTime leftDate;
            try
            {
                leftDate = new DateTime(startDate.Year, startDate.Month, startDate.Day - 1);

            }
            catch (ArgumentOutOfRangeException ex)
            {
                try
                {
                    leftDate = new DateTime(startDate.Year, startDate.Month - 1, GetPreviousMonthLastDay(startDate.Month - 1));

                }
                catch (ArgumentOutOfRangeException ex2)
                {
                    leftDate = new DateTime(startDate.Year - 1, 12, GetPreviousMonthLastDay(startDate.Month - 1));
                }
            }
            DateTime blackoutToTheLeft = new DateTime(leftDate.Year - 1);
            endDateCalendar.BlackoutDates.Add(new CalendarDateRange(blackoutToTheLeft, leftDate));
            endDateCalendar.BlackoutDates.Add(new CalendarDateRange(endDate, endDate.AddYears(1)));
            hourOfStartDateTextBox.Text = "08:00";
            hourOfEndDateTextBox.Text = "16:00";


        }
        private int GetPreviousMonthLastDay(int month)
        {
            if (month == 2)
                return 28;
            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                return 31;
            else
                return 30;

        }

        private void EndDateCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            emptyField(hourOfStartDateTextBox);
            emptyField(hourOfEndDateTextBox);
        }

        private void BtnAddClockingOnManagement_Click(object sender, RoutedEventArgs e)
        {



        }

        private void UserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnLoadManagement_Click(object sender, RoutedEventArgs e)
        {

           
            lstManagement.Items.Clear();
            List<Work> works = controller.GetWorksFromDB();
            foreach (var work in works)
            {
                lstManagement.Items.Add(work);
            }

      
        }
    }
}

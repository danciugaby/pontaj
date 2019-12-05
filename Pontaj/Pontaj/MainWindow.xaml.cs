using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
        private static List<Work> works;
        public MainWindow()
        {
            InitializeComponent();
            controller = new Controller();
            works = controller.GetWorksFromDB();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = controller.GetUsersFromDB();
            string lastName = userLastNameTextBoxPersonalTab.Text;
            string firstName = userFirstNameTextBoxPersonalTab.Text;
            bool canInsert = true;
            if (isEmpty(lastName))
            {
                MessageBox.Show("Te rog introdu numele corect!");
                canInsert = false;

            }
            if (canInsert && isEmpty(firstName))
            {
                MessageBox.Show("Te rog introdu prenumele corect!");
                canInsert = false;

            }
            if (userRankComboBoxPersonalTab.SelectedItem == null)
            {
                MessageBox.Show("Te rog selecteaza gradul!");
                canInsert = false;
            }
            if (userUnitComboBoxPersonalTab.SelectedItem == null)
            {
                MessageBox.Show("Te rog selecteaza unitatea!");
                canInsert = false;
            }

            if (canInsert)
            {
                Rank rank = userRankComboBoxPersonalTab.SelectedItem as Rank;
                Unit unit = userUnitComboBoxPersonalTab.SelectedItem as Unit;
                User newUser = new User(firstName, lastName, rank, unit);
                bool canAdd = true;
                foreach (var user in users)
                    if (user.Equals(newUser))
                        canAdd = false;
                if (canAdd)
                {
                    users.Add(newUser);
                    controller.AddUserInDB(newUser);
                    clearComboBox(userRankComboBoxPersonalTab);
                    clearComboBox(userUnitComboBoxPersonalTab);
                    emptyTextBox(userFirstNameTextBoxPersonalTab);
                    emptyTextBox(userLastNameTextBoxPersonalTab);
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

        private void clearComboBox(ComboBox comboBox)
        {
            comboBox.SelectedIndex = -1;
        }

        private void populateFields(User user)
        {
            userFirstNameTextBoxPersonalTab.Text = user.FirstName;
            userLastNameTextBoxPersonalTab.Text = user.LastName;
        }

        private void LstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selectedUser = lstUsers.SelectedItem as User;
            if (selectedUser != null)
            {
                populateFields(selectedUser);
                populateRankComboBox(selectedUser);
                populateUnitComboBox(selectedUser);
                setEnabledButton(btnUpdateUser);
                setEnabledButton(btnDeleteUser);

            }
            e.Handled = true;

        }
        private void populateRankComboBox(User user)
        {
            if (lstRanks.Items.Count != 0)
            {
                foreach (var rank in lstRanks.Items)
                    if (rank.Equals(user.Rank))
                    {
                        userRankComboBoxPersonalTab.SelectedValue = rank;
                        break;
                    }
            }
        }
        private void populateUnitComboBox(User user)
        {
            if (lstUnits.Items.Count != 0)
            {
                foreach (var unit in lstUnits.Items)
                    if (unit.Equals(user.Unit))
                    {

                        userUnitComboBoxPersonalTab.SelectedValue = unit;
                        break;
                    }
            }
        }

        private void populateMonthYearComboBox()
        {
            DateTime date = DateTime.Now;
            string value = date.Month + "." + date.Year;
            monthYearComboBoxWork.Items.Add(value);
            monthYearComboBoxWork.SelectedValue = value;
            int year = date.Year;
            int month = date.Month;
            for (int i = 0; i < 12; ++i)
            {
                --month;
                value = month + "." + year;
                if (month == 1)
                {
                    --year;
                    month = 13;
                }
                monthYearComboBoxWork.Items.Add(value);

            }


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
            if (isEmpty(user.FirstName) || isEmpty(user.LastName))
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
                string lastName = userLastNameTextBoxPersonalTab.Text;
                string firstName = userFirstNameTextBoxPersonalTab.Text;
                bool canUpdate = true;
                if (isEmpty(lastName))
                {
                    MessageBox.Show("Te rog introdu numele corect!");
                    canUpdate = false;

                }
                if (canUpdate && isEmpty(firstName))
                {
                    MessageBox.Show("Te rog introdu prenumele corect!");
                    canUpdate = false;

                }
                if (userRankComboBoxPersonalTab.SelectedItem == null)
                {
                    MessageBox.Show("Te rog selecteaza gradul!");
                    canUpdate = false;
                }
                if (userUnitComboBoxPersonalTab.SelectedItem == null)
                {
                    MessageBox.Show("Te rog selecteaza unitatea!");
                    canUpdate = false;
                }
                if (canUpdate)
                {
                    Rank rank = userRankComboBoxPersonalTab.SelectedItem as Rank;
                    Unit unit = userUnitComboBoxPersonalTab.SelectedItem as Unit;
                    User modifiedUser = new User(firstName, lastName, rank, unit);

                    if (selectedUser.Equals(modifiedUser))
                    {
                        MessageBox.Show("Nu ai facut nicio modificare!");
                    }
                    else
                    {
                        List<User> users = controller.GetUsersFromDB();

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
                            emptyField(userLastNameTextBoxPersonalTab);
                            emptyField(userFirstNameTextBoxPersonalTab);
                            userRankComboBoxPersonalTab.SelectedIndex = -1;
                            userUnitComboBoxPersonalTab.SelectedIndex = -1;
                            setDisabledButton(btnUpdateUser);
                            setDisabledButton(btnDeleteUser);
                            LoadUsers();
                        }
                    }
                }
            }
        }
        private void emptyTextBox(TextBox textBox)
        {
            textBox.Clear();
        }
        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = lstUsers.SelectedItem as User;
            if (selectedUser != null)
            {
                controller.DeleteUserFromDB(selectedUser);

                setDisabledButton(btnUpdateUser);
                setDisabledButton(btnDeleteUser);

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

        private void LoadUnits()
        {
            lstUnits.Items.Clear();
            List<Unit> units = controller.GetUnitsFromDB();
            foreach (var unit in units)
            {
                lstUnits.Items.Add(unit);
            }
        }
        private void LoadRanks()
        {
            lstRanks.Items.Clear();
            List<Rank> ranks = controller.GetRanksFromDB();
            foreach (var rank in ranks)
            {
                lstRanks.Items.Add(rank);
            }
        }

        private void LoadHolidays()
        {
            lstHolidays.Items.Clear();
            List<Holiday> holidays = controller.GetHolidaysFromDB();
            foreach (var holiday in holidays)
            {
                lstHolidays.Items.Add(holiday);
            }

        }
        private void LoadTypeDescriptions()
        {
            lstTypeDescription.Items.Clear();
            List<TypeDescription> typeDescriptions = controller.GetTypeDescriptionsFromDB();
            foreach (var typeDescription in typeDescriptions)
            {
                lstTypeDescription.Items.Add(typeDescription);
            }

        }



        //private void populateHolidayComboBox(ClockingType type)
        //{
        //    if (lstHolidays.Items.Count != 0)
        //    {
        //        foreach (var holiday in lstHolidays.Items)
        //            if (holiday.Equals(type.Holiday))
        //            {
        //                holidayClockingComboBox.SelectedValue = holiday;
        //                break;
        //            }
        //    }
        //}
        //private void populateTypeDescriptionComboBox(ClockingType type)
        //{
        //    if (lstTypeDescription.Items.Count != 0)
        //    {
        //        foreach (var typeDescription in lstTypeDescription.Items)
        //            if (typeDescription.Equals(type.TypeDescription))
        //            {

        //                clockingDescriptionTypeComboBox.SelectedValue = typeDescription;
        //                break;
        //            }
        //    }
        //}


        private void LoadUsersIntoComboBox()
        {
            if (controller.users.Users == null || controller.users.Users.Count() == 0)
            {
                LoadUsers();
                userComboBoxWork.Items.Clear();
                if (controller.users.Users.Count() == 0)
                    userComboBoxWork.Items.Add("Nu s-a gasit nicio persoana");
                else
                {

                    userComboBoxWork.Items.Clear();
                    foreach (var user in controller.users.Users)
                    {
                        userComboBoxWork.Items.Add(user);
                    }
                }
            }
            else
            {
                userComboBoxWork.Items.Clear();
                foreach (var user in controller.users.Users)
                {
                    userComboBoxWork.Items.Add(user);
                }
            }
        }
        private User CastToUser(ComboBoxItem item)
        {
            string[] values = item.ToString().Split(',');
            return new User(values[0], values[1]);

        }

        private void ClearTypeComboBoxesFromWork()
        {
            typeFirstHoursComboBoxWork.Items.Clear();
            typeSecondHoursComboBoxWork.Items.Clear();
            typeThirdHoursComboBoxWork.Items.Clear();
        }
        private void AddEmptyFieldOnTypeComboBoxes()
        {
            typeFirstHoursComboBoxWork.Items.Add("");
            typeSecondHoursComboBoxWork.Items.Add("");
            typeThirdHoursComboBoxWork.Items.Add("");
        }

        private void LoadTypesIntoComboBox()
        {
            if (controller.typeDescriptions.TypeDescriptions.Count() == 0)
            {
                LoadTypeDescriptions();
                ClearTypeComboBoxesFromWork();
                AddEmptyFieldOnTypeComboBoxes();
                if (controller.typeDescriptions.TypeDescriptions.Count() == 0)
                {
                    typeFirstHoursComboBoxWork.Items.Add("Nu s-a gasit niciun tip de pontaj");
                    typeSecondHoursComboBoxWork.Items.Add("Nu s-a gasit niciun tip de pontaj");
                    typeThirdHoursComboBoxWork.Items.Add("Nu s-a gasit niciun tip de pontaj");
                }
                else
                {
                    ClearTypeComboBoxesFromWork();
                    AddEmptyFieldOnTypeComboBoxes();
                    foreach (var type in controller.typeDescriptions.TypeDescriptions)
                    {
                        typeFirstHoursComboBoxWork.Items.Add(type);
                        typeSecondHoursComboBoxWork.Items.Add(type);
                        typeThirdHoursComboBoxWork.Items.Add(type);
                    }
                }

            }
            else
            {
                ClearTypeComboBoxesFromWork();
                AddEmptyFieldOnTypeComboBoxes();
                foreach (var type in controller.typeDescriptions.TypeDescriptions)
                {
                    typeFirstHoursComboBoxWork.Items.Add(type);
                    typeSecondHoursComboBoxWork.Items.Add(type);
                    typeThirdHoursComboBoxWork.Items.Add(type);
                }
            }
        }
        private void LoadUnitsIntoComboBox()
        {
            if (controller.units.Units.Count() == 0)
            {
                LoadUnits();
                userUnitComboBoxPersonalTab.Items.Clear();
                if (controller.units.Units.Count() == 0)
                    userUnitComboBoxPersonalTab.Items.Add("Nu s-a gasit nicio unitate");
                else
                {
                    userUnitComboBoxPersonalTab.Items.Clear();
                    foreach (var unit in controller.units.Units)
                    {
                        userUnitComboBoxPersonalTab.Items.Add(unit);
                    }
                }

            }
            else
            {
                userUnitComboBoxPersonalTab.Items.Clear();
                foreach (var unit in controller.units.Units)
                {
                    userUnitComboBoxPersonalTab.Items.Add(unit);
                }
            }
        }
        private void LoadRanksIntoComboBox()
        {
            if (controller.ranks.Ranks.Count() == 0)
            {
                LoadRanks();
                userRankComboBoxPersonalTab.Items.Clear();
                if (controller.ranks.Ranks.Count() == 0)
                    userRankComboBoxPersonalTab.Items.Add("Nu s-a gasit nicio unitate");
                else
                {
                    userRankComboBoxPersonalTab.Items.Clear();
                    foreach (var rank in controller.ranks.Ranks)
                    {
                        userRankComboBoxPersonalTab.Items.Add(rank);
                    }
                }

            }
            else
            {
                userRankComboBoxPersonalTab.Items.Clear();
                foreach (var rank in controller.ranks.Ranks)
                {
                    userRankComboBoxPersonalTab.Items.Add(rank);
                }
            }
        }
        private void ClearHolidayComboBoxesItemsFromWork()
        {
            holidayFirstHoursComboBoxWork.Items.Clear();
            holidaySecondHoursComboBoxWork.Items.Clear();
            holidayThirdHoursComboBoxWork.Items.Clear();

        }
        private void AddEmptyValueForHolidayComboBoxesWork()
        {
            holidayFirstHoursComboBoxWork.Items.Add("");
            holidaySecondHoursComboBoxWork.Items.Add("");
            holidayThirdHoursComboBoxWork.Items.Add("");
        }

        private void LoadHolidaysIntoComboBox()
        {
            if (controller.holidays.Holidays == null || controller.holidays.Holidays.Count() == 0)
            {
                LoadHolidays();

                ClearHolidayComboBoxesItemsFromWork();
                AddEmptyValueForHolidayComboBoxesWork();
                if (controller.holidays.Holidays.Count() == 0)
                {
                    holidayFirstHoursComboBoxWork.Items.Add("Nu s-a gasit niciun concediu");
                    holidaySecondHoursComboBoxWork.Items.Add("Nu s-a gasit niciun concediu");
                    holidayThirdHoursComboBoxWork.Items.Add("Nu s-a gasit niciun concediu");
                }
                else
                {

                    ClearHolidayComboBoxesItemsFromWork();
                    AddEmptyValueForHolidayComboBoxesWork();
                    foreach (var holiday in controller.holidays.Holidays)
                    {
                        holidayFirstHoursComboBoxWork.Items.Add(holiday);
                        holidaySecondHoursComboBoxWork.Items.Add(holiday);
                        holidayThirdHoursComboBoxWork.Items.Add(holiday);
                    }
                }
            }
            else
            {
                ClearHolidayComboBoxesItemsFromWork();
                AddEmptyValueForHolidayComboBoxesWork();
                foreach (var holiday in controller.holidays.Holidays)
                {
                    holidayFirstHoursComboBoxWork.Items.Add(holiday);
                    holidaySecondHoursComboBoxWork.Items.Add(holiday);
                    holidayThirdHoursComboBoxWork.Items.Add(holiday);
                }
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (tabControl.SelectedItem.Equals(usersTabItem))
            {
                LoadUsers();
                LoadUnitsIntoComboBox();
                LoadRanksIntoComboBox();
                emptyTextBox(userFirstNameTextBoxPersonalTab);
                emptyTextBox(userLastNameTextBoxPersonalTab);
                progressBar.Value = 20;
            }
            else if (tabControl.SelectedItem.Equals(rankUnitTabItem))
            {
                LoadRanks();
                LoadUnits();
                emptyTextBox(rankTextBox);
                emptyTextBox(unitTextBox);
                progressBar.Value = 40;
            }
            else if (tabControl.SelectedItem.Equals(descriptionHolidayTabItem))
            {
                LoadHolidays();
                LoadTypeDescriptions();
                emptyTextBox(holidayTextBox);
                progressBar.Value = 60;
            }
            else if (tabControl.SelectedItem.Equals(worksTabItem))
            {

                populateMonthYearComboBox();
                LoadUsersIntoComboBox();
                LoadHolidaysIntoComboBox();
                LoadTypesIntoComboBox();
                populateComingAndLeavingTextBoxes();
                progressBar.Value = 80;
            }
            else if (tabControl.SelectedItem.Equals(reportTabItem))
            {
                progressBar.Value = 100;
            }
            e.Handled = true;
        }
        private void populateComingAndLeavingTextBoxes()
        {
            comingFirstHourOfWork.Text = "07:00";
            leavingFirstHourOfWork.Text = "15:00";
        }


        //private void StartDateCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    endDateCalendar.BlackoutDates.Clear();
        //    var startDate = (DateTime)startDateCalendar.SelectedDate;

        //    endDateCalendar.SelectedDate = startDate;

        //    DateTime endDate;
        //    try
        //    {
        //        endDate = new DateTime(startDate.Year, startDate.Month, startDate.Day + 2);

        //    }
        //    catch (ArgumentOutOfRangeException ex)
        //    {
        //        try
        //        {
        //            endDate = new DateTime(startDate.Year, startDate.Month + 1, 2);

        //        }
        //        catch (ArgumentOutOfRangeException ex2)
        //        {
        //            endDate = new DateTime(startDate.Year + 1, 1, 2);
        //        }
        //    }
        //    DateTime leftDate;
        //    try
        //    {
        //        leftDate = new DateTime(startDate.Year, startDate.Month, startDate.Day - 1);

        //    }
        //    catch (ArgumentOutOfRangeException ex)
        //    {
        //        try
        //        {
        //            leftDate = new DateTime(startDate.Year, startDate.Month - 1, GetPreviousMonthLastDay(startDate.Month - 1));

        //        }
        //        catch (ArgumentOutOfRangeException ex2)
        //        {
        //            leftDate = new DateTime(startDate.Year - 1, 12, GetPreviousMonthLastDay(startDate.Month - 1));
        //        }
        //    }
        //    DateTime blackoutToTheLeft = new DateTime(leftDate.Year - 1);
        //    endDateCalendar.BlackoutDates.Add(new CalendarDateRange(blackoutToTheLeft, leftDate));
        //    endDateCalendar.BlackoutDates.Add(new CalendarDateRange(endDate, endDate.AddYears(1)));
        //    hourOfStartDateTextBox.Text = "08:00";
        //    hourOfEndDateTextBox.Text = "16:00";

        //    e.Handled = true;


        //}
        private int GetPreviousMonthLastDay(int month)
        {
            if (month == 2)
                return 28;
            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                return 31;
            else
                return 30;

        }

        //private void EndDateCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    emptyField(hourOfStartDateTextBox);
        //    emptyField(hourOfEndDateTextBox);
        //    hourOfStartDateTextBox.Text = "HH:mm";
        //    hourOfEndDateTextBox.Text = "HH:mm";
        //    e.Handled = true;
        //}

        //private void BtnAddClockingOnManagement_Click(object sender, RoutedEventArgs e)
        //{
        //    List<Work> works = controller.GetWorksFromDB();
        //    User user = null;
        //    ClockingType type = null;
        //    DateTime startDate = DateTime.ParseExact("24/01/2013 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        //    DateTime endDate = DateTime.ParseExact("24/01/2013 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        //    bool canInsert = true;
        //    if (userComboBox.SelectedValue != null && !userComboBox.SelectedValue.ToString().Equals(""))
        //    {

        //        user = GetUserFromString(userComboBox.SelectedValue.ToString());
        //    }
        //    else
        //    {
        //        MessageBox.Show("Te rog selecteaza persoana!");
        //        canInsert = false;
        //    }
        //    if (canInsert)
        //    {
        //        if (clockingTypeComboBox.SelectedValue != null && !clockingTypeComboBox.SelectedValue.ToString().Equals(""))
        //        {

        //            type = clockingTypeComboBox.SelectedItem as ClockingType;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Te rog selecteaza tipul de pontaj!");
        //            canInsert = false;
        //        }
        //    }
        //    if (canInsert)
        //    {
        //        if (startDateCalendar.SelectedDate != null && endDateCalendar.SelectedDate != null)
        //        {

        //            startDate = (DateTime)startDateCalendar.SelectedDate;
        //            endDate = (DateTime)endDateCalendar.SelectedDate;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Te rog selecteaza datele calendaristice!");
        //            canInsert = false;
        //        }
        //    }


        //    if (canInsert)
        //    {
        //        if (hourOfStartDateTextBox.Text.ToString().Equals("") || hourOfEndDateTextBox.Text.ToString().Equals(""))
        //            MessageBox.Show("Te rog introdu orele de lucru!");
        //        if (WorkingHoursAreFine())
        //        {
        //            int hour = getHourFromString(hourOfStartDateTextBox.Text.ToString());
        //            int minutes = getMinutesFromString(hourOfStartDateTextBox.Text.ToString());
        //            TimeSpan ts = new TimeSpan(hour, minutes, 0);
        //            startDate += ts;

        //            hour = getHourFromString(hourOfEndDateTextBox.Text.ToString());
        //            minutes = getMinutesFromString(hourOfEndDateTextBox.Text.ToString());
        //            ts = new TimeSpan(hour, minutes, 0);
        //            endDate += ts;

        //            List<User> users = controller.GetUsersFromDB();
        //            foreach (User x in users)
        //            {
        //                if (x.Equals(user))
        //                {
        //                    user = x;
        //                    break;
        //                }

        //            }
        //            List<ClockingType> types = controller.GetTypesFromDB();
        //            foreach (ClockingType x in types)
        //            {
        //                if (x.Equals(type))
        //                {
        //                    type = x;
        //                    break;
        //                }

        //            }
        //            Work work = new Work(user, type, startDate, endDate);
        //            controller.AddWorkInDB(work);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Formatul orelor este de 24, HH:mm!");
        //        }

        //    }


        //}
        private int getHourFromString(string str)
        {
            string[] values = str.Split(':');
            try
            {
                return int.Parse(values[0]);
            }
            catch (System.FormatException ex)
            {
                return 0;
            }
        }
        private int getMinutesFromString(string str)
        {
            string[] values = str.Split(':');
            try
            {
                return int.Parse(values[1]);
            }
            catch (System.FormatException ex)
            {
                return 0;
            }
        }
        //private bool WorkingHoursAreFine()
        //{
        //    string startHours = hourOfStartDateTextBox.Text.ToString();
        //    string endingHours = hourOfEndDateTextBox.Text.ToString();
        //    if (CheckIfHourIsCorrect(startHours) && CheckIfHourIsCorrect(endingHours))
        //        return true;
        //    else
        //        return false;

        //}
        private bool CheckIfHourIsCorrect(string hour)
        {
            string[] values = hour.Split(':');
            int h = 0;
            int min = 0;
            try
            {
                h = int.Parse(values[0]);
                if (values.Length > 1)
                    min = int.Parse(values[1]);
                else
                {
                    return false;
                }
            }
            catch (System.FormatException ex)
            {
                return false;
            }
            if (h < 0 || h >= 24)
                return false;
            if (min < 0 || min >= 60)
                return false;
            return true;
        }

        //private void UserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (userComboBox.SelectedValue != null)
        //    {
        //        User user = GetUserFromString(userComboBox.SelectedValue.ToString());
        //        lstManagement.Items.Clear();
        //        List<Work> forOnlyOneUser = GetOnlyWorksOfOneUser(user);
        //        foreach (var work in forOnlyOneUser)
        //            lstManagement.Items.Add(work);
        //        clockingTypeComboBox.SelectedIndex = -1;
        //    }
        //    e.Handled = true;
        //}
        private List<Work> GetOnlyWorksOfOneUser(User user)
        {


            List<Work> onlyForOne = new List<Work>();
            foreach (Work work in works)
            {
                if (work.User.Equals(user))
                    onlyForOne.Add(work);
            }
            return onlyForOne;
        }





        //private void ClockingTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (clockingTypeComboBox.SelectedValue != null)
        //    { 
        //        ClockingType type = clockingTypeComboBox.SelectedItem as ClockingType;
        //        lstManagement.Items.Clear();
        //        List<Work> forOnlyOneType;
        //        if (userComboBox.SelectedValue != null)
        //        {
        //            User user = GetUserFromString(userComboBox.SelectedValue.ToString());
        //            forOnlyOneType = GetOnlyWorksOfOneType(GetOnlyWorksOfOneUser(user), type);
        //        }
        //        else
        //        {
        //            forOnlyOneType = GetOnlyWorksOfOneType(controller.GetWorksFromDB(), type);
        //        }
        //        foreach (var work in forOnlyOneType)
        //            lstManagement.Items.Add(work);
        //    }
        //    e.Handled = true;
        //}
        //private List<Work> GetOnlyWorksOfOneType(List<Work> works, ClockingType type)
        //{
        //    List<Work> onlyForOne = new List<Work>();
        //    foreach (Work work in works)
        //    {
        //        if (work.Type.Equals(type))
        //            onlyForOne.Add(work);
        //    }
        //    return onlyForOne;
        //}
        private User GetUserFromString(string value)
        {
            string[] userValues = value.Split(',');
            string[] splitName = userValues[0].Split(' ');
            return new User(splitName[1].Trim(), splitName[0].Trim(), new Rank(userValues[1]), new Unit(userValues[2]));
        }

        private void UserUnitComboBoxPersonalTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userUnitComboBoxPersonalTab.SelectedValue != null && lstUsers.SelectedItem == null)
            {
                Unit unit = userUnitComboBoxPersonalTab.SelectedItem as Unit;
                lstUsers.Items.Clear();
                List<User> forOnlyOneUnit;
                if (userRankComboBoxPersonalTab.SelectedValue != null)
                {
                    Rank rank = userRankComboBoxPersonalTab.SelectedItem as Rank;
                    forOnlyOneUnit = GetOnlyUsersOfOneUnit(GetOnlyUsersOfOneRank(controller.GetUsersFromDB(), rank), unit);
                }
                else
                {
                    forOnlyOneUnit = GetOnlyUsersOfOneUnit(controller.GetUsersFromDB(), unit);
                }
                foreach (var user in forOnlyOneUnit)
                    lstUsers.Items.Add(user);
            }
            e.Handled = true;
        }

        private void UserRankComboBoxPersonalTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userRankComboBoxPersonalTab.SelectedValue != null && lstUsers.SelectedItem == null)
            {
                Rank rank = userRankComboBoxPersonalTab.SelectedItem as Rank;
                lstUsers.Items.Clear();
                List<User> forOnlyOneRank = GetOnlyUsersOfOneRank(controller.GetUsersFromDB(), rank);
                foreach (var user in forOnlyOneRank)
                    lstUsers.Items.Add(user);
                userUnitComboBoxPersonalTab.SelectedIndex = -1;
            }
            e.Handled = true;
        }
        private List<User> GetOnlyUsersOfOneUnit(List<User> users, Unit unit)
        {
            List<User> onlyForOne = new List<User>();
            foreach (User user in users)
            {
                if (user.Unit.Equals(unit))
                    onlyForOne.Add(user);
            }
            return onlyForOne;
        }
        private List<User> GetOnlyUsersOfOneRank(List<User> users, Rank rank)
        {
            List<User> onlyForOne = new List<User>();
            foreach (User user in users)
            {
                if (user.Rank.Equals(rank))
                    onlyForOne.Add(user);
            }
            return onlyForOne;
        }

        private void BtnAddRank_Click(object sender, RoutedEventArgs e)
        {
            string rank = rankTextBox.Text;
            if (isEmpty(rank))
            {
                MessageBox.Show("Te rog introdu gradul!");

            }
            else
            {
                controller.AddRankInDB(new Rank(rank));
                emptyField(rankTextBox);
                LoadRanks();
            }
        }

        private void BtnModifyRank_Click(object sender, RoutedEventArgs e)
        {
            string rank = rankTextBox.Text;
            if (isEmpty(rank))
            {
                MessageBox.Show("Te rog modifica gradul!");

            }
            else
            {
                Rank selectedRank = lstRanks.SelectedItem as Rank;
                if (selectedRank != null)
                {
                    controller.UpdateRankInDB(new Rank(rank), selectedRank);
                    emptyField(rankTextBox);
                    LoadRanks();
                }
                else
                {
                    MessageBox.Show("Te rog selecteaza gradul!");
                }

            }
        }

        private void LstRanks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rank selectedRank = lstRanks.SelectedItem as Rank;
            if (selectedRank != null)
            {
                rankTextBox.Text = selectedRank.Name;
                setEnabledButton(btnUpdateRank);
                setEnabledButton(btnDeleteRank);

            }
            e.Handled = true;
        }

        private void BtnDeleteRank_Click(object sender, RoutedEventArgs e)
        {
            string rank = rankTextBox.Text;
            if (isEmpty(rank))
            {
                MessageBox.Show("Te rog selecteaza gradul!");

            }
            else
            {
                Rank selectedRank = lstRanks.SelectedItem as Rank;
                if (selectedRank != null)
                {
                    controller.DeleteRankFromDB(selectedRank);
                    emptyField(rankTextBox);
                    LoadRanks();
                }
                else
                {
                    MessageBox.Show("Te rog selecteaza gradul!");
                }
            }
        }

        private void BtnAddUnit_Click(object sender, RoutedEventArgs e)
        {
            string unit = unitTextBox.Text;
            if (isEmpty(unit))
            {
                MessageBox.Show("Te rog introdu unitatea!");

            }
            else
            {
                controller.AddUnitInDB(new Unit(unit));
                emptyField(unitTextBox);
                LoadUnits();
            }
        }

        private void BtnModifyUnit_Click(object sender, RoutedEventArgs e)
        {
            string unit = unitTextBox.Text;
            if (isEmpty(unit))
            {
                MessageBox.Show("Te rog introdu unitatea!");

            }
            else
            {
                Unit selectedUnit = lstUnits.SelectedItem as Unit;
                if (selectedUnit != null)
                {
                    controller.UpdateUnitInDB(new Unit(unit), selectedUnit);
                    emptyField(unitTextBox);
                    LoadUnits();
                }
                else
                {
                    MessageBox.Show("Te rog selecteaza unitatea!");
                }

            }
        }

        private void BtnDeleteUnit_Click(object sender, RoutedEventArgs e)
        {
            string unit = unitTextBox.Text;
            if (isEmpty(unit))
            {
                MessageBox.Show("Te rog selecteaza unitatea!");

            }
            else
            {
                Unit selectedUnit = lstUnits.SelectedItem as Unit;
                if (selectedUnit != null)
                {
                    controller.DeleteUnitFromDB(selectedUnit);
                    emptyField(unitTextBox);
                    LoadUnits();
                }
                else
                {
                    MessageBox.Show("Te rog selecteaza unitatea!");
                }

            }
        }

        private void LstUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Unit selectedUnit = lstUnits.SelectedItem as Unit;
            if (selectedUnit != null)
            {
                unitTextBox.Text = selectedUnit.Name;
                setEnabledButton(btnUpdateUnit);
                setEnabledButton(btnDeleteUnit);

            }
            e.Handled = true;
        }

        private void LstTypeDescription_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypeDescription selectedTypeDescription = lstTypeDescription.SelectedItem as TypeDescription;
            if (selectedTypeDescription != null)
            {
                typeDescriptionTextBox.Text = selectedTypeDescription.Name;
                setEnabledButton(btnUpdateTypeDescription);
                setEnabledButton(btnDeleteTypeDescription);

            }
            e.Handled = true;
        }

        private void BtnAddTypeDescription_Click(object sender, RoutedEventArgs e)
        {
            string typeDescription = typeDescriptionTextBox.Text;
            if (isEmpty(typeDescription))
            {
                MessageBox.Show("Te rog introdu tipul!");

            }
            else
            {
                controller.AddTypeDescriptionInDB(new TypeDescription(typeDescription));
                emptyField(typeDescriptionTextBox);
                LoadTypeDescriptions();
            }
        }

        private void BtnUpdateTypeDescription_Click(object sender, RoutedEventArgs e)
        {
            string typeDescription = typeDescriptionTextBox.Text;
            if (isEmpty(typeDescription))
            {
                MessageBox.Show("Te rog introdu tipul!");

            }
            else
            {
                TypeDescription selected = lstTypeDescription.SelectedItem as TypeDescription;
                if (selected != null)
                {
                    controller.UpdateTypeDescriptionInDB(new TypeDescription(typeDescription), selected);
                    emptyField(typeDescriptionTextBox);
                    LoadTypeDescriptions();
                }
                else
                {
                    MessageBox.Show("Te rog selecteaza tipul!");
                }

            }
        }

        private void BtnDeleteTypeDescription_Click(object sender, RoutedEventArgs e)
        {
            string typeDescription = typeDescriptionTextBox.Text;
            if (isEmpty(typeDescription))
            {
                MessageBox.Show("Te rog selecteaza tipul!");

            }
            else
            {
                TypeDescription selectedTypeDescription = lstTypeDescription.SelectedItem as TypeDescription;
                if (selectedTypeDescription != null)
                {
                    controller.DeleteTypeDescriptionFromBD(selectedTypeDescription);
                    emptyField(typeDescriptionTextBox);
                    LoadTypeDescriptions();
                }
                else
                {
                    MessageBox.Show("Te rog selecteaza tipul!");
                }

            }
        }

        private void LstHolidays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Holiday selected = lstHolidays.SelectedItem as Holiday;
            if (selected != null)
            {
                holidayTextBox.Text = selected.Name;
                setEnabledButton(btnUpdateHoliday);
                setEnabledButton(btnDeleteHoliday);

            }
            e.Handled = true;
        }

        private void BtnAddHoliday_Click(object sender, RoutedEventArgs e)
        {
            string holiday = holidayTextBox.Text;
            if (isEmpty(holiday))
            {
                MessageBox.Show("Te rog introdu concediul!");

            }
            else
            {
                controller.AddHolidayInDB(new Holiday(holiday));
                emptyField(holidayTextBox);
                LoadHolidays();
            }
        }

        private void BtnUpdateHoliday_Click(object sender, RoutedEventArgs e)
        {
            string holiday = holidayTextBox.Text;
            if (isEmpty(holiday))
            {
                MessageBox.Show("Te rog introdu concediul!");

            }
            else
            {
                Holiday selected = lstHolidays.SelectedItem as Holiday;
                if (selected != null)
                {
                    controller.UpdateHolidayInDB(new Holiday(holiday), selected);
                    emptyField(holidayTextBox);
                    LoadHolidays();
                }
                else
                {
                    MessageBox.Show("Te rog selecteaza concediul!");
                }

            }
        }

        private void BtnDeleteHoliday_Click(object sender, RoutedEventArgs e)
        {
            string holiday = holidayTextBox.Text;
            if (isEmpty(holiday))
            {
                MessageBox.Show("Te rog selecteaza concediul!");

            }
            else
            {
                Holiday selected = lstHolidays.SelectedItem as Holiday;
                if (selected != null)
                {
                    controller.DeleteHolidayFromDB(selected);
                    emptyField(holidayTextBox);
                    LoadHolidays();
                }
                else
                {
                    MessageBox.Show("Te rog selecteaza concediul!");
                }

            }
        }

        private void MonthYearComboBoxWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string value = DateTime.Now.Month + "." + DateTime.Now.Year;
            if (monthYearComboBoxWork.SelectedValue != null)
                value = monthYearComboBoxWork.SelectedValue as string;
            List<Work> worksForOne = GetOnlyWorksOfOneUser(userComboBoxWork.SelectedItem as User);
            List<Work> workedThisMonth = GetDaysOfMonthWorkedFromList(worksForOne, value);

            InsertDaysOfMonthsOnDataGrid(workedThisMonth, value);
            e.Handled = true;


        }
        private void InsertDaysOfMonthsOnDataGrid(List<Work> workedThisMonth, string value)
        {
            int days = GetMonthsLengthBasedOnYear(value);

            dataGridWork.Columns.Clear();
            DataTable dt = new DataTable();
            var row = dt.NewRow();
            string[] splitted = value.Split('.');
            int[] daysOfWork = GetDaysOfWork(workedThisMonth, int.Parse(splitted[0]));
            var secondRow = dt.NewRow();
            dt.Rows.Add(row);
            dt.Rows.Add(secondRow);
            for (int i = 1; i <= days; ++i)
            {

                dt.Columns.Add(i.ToString(), typeof(string));
                row[i.ToString()] = i;
                if (daysOfWork[i - 1] != 0)
                    secondRow[i.ToString()] = "P";


            }


            dataGridWork.ItemsSource = dt.DefaultView;
        }
        private int GetMonthsLengthBasedOnYear(string value)
        {

            string[] splitted = value.Split('.');
            int month = int.Parse(splitted[0]);
            int year = int.Parse(splitted[1]);

            bool isLeapYear = false;
            if (year % 4 == 0 && year % 100 != 0)
                isLeapYear = true;
            if (year % 400 == 0)
                isLeapYear = true;

            return GetMonthDaysBasedOnMonth(month, isLeapYear);
        }
        private int GetMonthDaysBasedOnMonth(int month, bool isLeapYear)
        {
            int days = 30;
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                days = 31;
            else if (month == 2)
                if (isLeapYear)
                    days = 29;
                else
                    days = 28;
            return days;
        }
        private int[] GetDaysOfWork(List<Work> works, int month)
        {
            int[] days = new int[31];
            foreach (var work in works)
            {
                if (work.StartDate.Month == month)
                    days[work.StartDate.Day - 1] = 1;

            }
            return days;
        }
        private void UserComboBoxWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = DateTime.Now.Month + "." + DateTime.Now.Year;
            monthYearComboBoxWork.SelectedIndex = -1;
            monthYearComboBoxWork.SelectedValue = value;
            e.Handled = true;
        }
        private List<Work> GetDaysOfMonthWorkedFromList(List<Work> works, string date)
        {

            List<Work> workedThisMonth = new List<Work>();
            string[] splitted = date.Split('.');
            int month = int.Parse(splitted[0]);
            int year = int.Parse(splitted[1]);
            foreach (var work in works)
            {
                if (work.StartDate.Month == month && work.StartDate.Year == year || work.EndDate.Month == month && work.EndDate.Year == year)
                    workedThisMonth.Add(work);

            }
            return workedThisMonth;
        }


        private void HolidayFirstHoursComboBoxWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void HolidaySecondHoursComboBoxWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void HolidayThirdHoursComboBoxWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void TypeFirstHoursComboBoxWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void TypeSecondHoursComboBoxWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void TypeThirdHoursComboBoxWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void CurrentTimeFirstComing_Click(object sender, RoutedEventArgs e)
        {
            GenerateCurrentTimeForTextBox(comingFirstHourOfWork);
        }

        private void CurrentTimeFirstLeaving_Click(object sender, RoutedEventArgs e)
        {
            GenerateCurrentTimeForTextBox(leavingFirstHourOfWork);
        }

        private void CurrentTimeSecondComing_Click(object sender, RoutedEventArgs e)
        {
            GenerateCurrentTimeForTextBox(comingSecondHourOfWork);
        }

        private void CurrentTimeSecondLeaving_Click(object sender, RoutedEventArgs e)
        {
            GenerateCurrentTimeForTextBox(leavingSecondHourOfWork);
        }

        private void CurrentTimeComingThird_Click(object sender, RoutedEventArgs e)
        {
            GenerateCurrentTimeForTextBox(comingThirdHourOfWork);
        }

        private void CurrentTimeThirdLeaving_Click(object sender, RoutedEventArgs e)
        {
            GenerateCurrentTimeForTextBox(leavingThirdHourOfWork);
        }
        private void GenerateCurrentTimeForTextBox(TextBox textBox)
        {
            textBox.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
        }
        private void DecreaseHoursForTextBox(TextBox textBox)
        {
            string[] splitted = textBox.Text.Split(':');
            int hours = ReturnTheCorrectDecreasedHours(splitted[0]);
            int minutes = 0;
            string value = "";
            if (hours < 10)
            {
                value = "0";
            }
            try
            {
                minutes = int.Parse(splitted[1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Minute incorecte!");
                value = "08:00";
            }
            string valuem = "";
            if (minutes < 10)
            {
                valuem = "0" + minutes;
            }
            else
                valuem += minutes;

            if (!value.Equals("08:00"))
                value += hours.ToString() + ":" + valuem;

            textBox.Text = value;
        }
        private void IncreaseHoursForTextBox(TextBox textBox)
        {
            string[] splitted = textBox.Text.Split(':');
            int hours = ReturnTheCorrectIncreasedHours(splitted[0]);
            int minutes = 0;
            string value = "";
            if (hours < 10)
            {
                value = "0";
            }
            try
            {
                minutes = int.Parse(splitted[1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Minute incorecte!");
                value = "08:00";
            }
            string valuem = "";
            if (minutes < 10)
            {
                valuem = "0" + minutes;
            }
            else
                valuem += minutes;

            if (!value.Equals("08:00"))
                value += hours.ToString() + ":" + valuem;

            textBox.Text = value;
        }
        private int ReturnTheCorrectDecreasedHours(string value)
        {

            int hours = 0;
            try
            {
                hours = int.Parse(value);
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show("Ora incorecta!");
            }
            if (hours == 0)
                hours = 23;
            else
                --hours;
            if (hours > 23 || hours < 0)
                hours = 0;

            return hours;
        }
        private int ReturnTheCorrectIncreasedHours(string value)
        {
            int hours = 0;
            try
            {
                hours = int.Parse(value);
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show("Ora incorecta!");
            }
            if (hours == 23)
                hours = 0;
            else
                ++hours;
            if (hours > 23 || hours < 0)
                hours = 0;
            return hours;
        }

        private void DecreaseHourComingFirstWork_Click(object sender, RoutedEventArgs e)
        {
            DecreaseHoursForTextBox(comingFirstHourOfWork);
        }

        private void IncreaseHourComingFirstWork_Click(object sender, RoutedEventArgs e)
        {
            IncreaseHoursForTextBox(comingFirstHourOfWork);
        }

        private void DecreaseHourFirstLeavingWork_Click(object sender, RoutedEventArgs e)
        {
            DecreaseHoursForTextBox(leavingFirstHourOfWork);
        }

        private void IncreaseHourFirstLeavingWork_Click(object sender, RoutedEventArgs e)
        {
            IncreaseHoursForTextBox(leavingFirstHourOfWork);
        }

        private void DecreaseHourComingSecondWork_Click(object sender, RoutedEventArgs e)
        {
            DecreaseHoursForTextBox(comingSecondHourOfWork);
        }

        private void IncreaseHourComingSecondWork_Click(object sender, RoutedEventArgs e)
        {
            IncreaseHoursForTextBox(comingSecondHourOfWork);
        }

        private void DecreaseHourSecondLeavingWork_Click(object sender, RoutedEventArgs e)
        {
            DecreaseHoursForTextBox(leavingSecondHourOfWork);
        }

        private void IncreaseHourSecondLeavingWork_Click(object sender, RoutedEventArgs e)
        {
            IncreaseHoursForTextBox(leavingSecondHourOfWork);
        }

        private void DecreaseHourComingThirdWork_Click(object sender, RoutedEventArgs e)
        {
            DecreaseHoursForTextBox(comingThirdHourOfWork);
        }

        private void IncreaseHourComingThirdWork_Click(object sender, RoutedEventArgs e)
        {
            IncreaseHoursForTextBox(comingThirdHourOfWork);
        }

        private void DecreaseHourThirdLeavingWork_Click(object sender, RoutedEventArgs e)
        {
            DecreaseHoursForTextBox(leavingThirdHourOfWork);
        }

        private void IncreaseHourThirdLeavingWork_Click(object sender, RoutedEventArgs e)
        {
            IncreaseHoursForTextBox(leavingThirdHourOfWork);
        }


        private void DataGridWork_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {


            User user = userComboBoxWork.SelectedItem as User;
            if (user != null)
            {
                IList<DataGridCellInfo> cells = dataGridWork.SelectedCells;
                string selectedDay = cells[0].Column.Header as string;
                string monthYear = monthYearComboBoxWork.SelectedValue as string;
                populateComboBoxesAndTimeOfWorks(user, selectedDay, monthYear);
                displayDateTextBlock.Text = selectedDay + "." + monthYear;
            }

        }

        private void populateComboBoxesAndTimeOfWorks(User user, string selectedDay, string monthYear)
        {
            clearHoursAndMinutesTextBox(comingFirstHourOfWork, comingSecondHourOfWork, comingThirdHourOfWork);
            clearHoursAndMinutesTextBox(leavingFirstHourOfWork, leavingSecondHourOfWork, leavingThirdHourOfWork);
            clearTypeDescriptionComboBoxes(typeFirstHoursComboBoxWork, typeSecondHoursComboBoxWork, typeThirdHoursComboBoxWork);
            clearHolidaysComboBoxes(holidayFirstHoursComboBoxWork, holidaySecondHoursComboBoxWork, holidayThirdHoursComboBoxWork);
            List<Work> forOnlyOneUser = GetOnlyWorksOfOneUser(user);
            int day = int.Parse(selectedDay);
            string[] splitted = monthYear.Split('.');
            int month = int.Parse(splitted[0]);
            int year = int.Parse(splitted[1]);
            TypeDescription type;
            Holiday holiday;
            int hoursComing;
            int minutesComing;
            int hoursLeaving;
            int minutesLeaving;
            int number = 0;
            foreach (Work work in forOnlyOneUser)
            {
                if (work.StartDate.Year == year && work.StartDate.Month == month && work.StartDate.Day == day)
                {
                    ++number;
                    type = work.Type;
                    holiday = work.Holiday;
                    hoursComing = work.StartDate.Hour;
                    minutesComing = work.StartDate.Minute;
                    hoursLeaving = work.EndDate.Hour;
                    minutesLeaving = work.EndDate.Minute;

                    if (number == 1)
                    {
                        populateFirstGridOfWork(holiday, type, hoursComing, minutesComing, hoursLeaving, minutesLeaving);
                    }
                    else if (number == 2)
                    {
                        populateSecondGridOfWork(holiday, type, hoursComing, minutesComing, hoursLeaving, minutesLeaving);
                    }
                    else
                    {
                        populateThirdGridOfWork(holiday, type, hoursComing, minutesComing, hoursLeaving, minutesLeaving);
                    }
                }
            }
            if (number == 0)
            {
                noDataInDBWork();
            }

        }
        private void populateHoursAndMinutesTextBox(TextBox textBox, int hours, int minutes)
        {
            string value = "";
            if (hours < 10)
                value += "0";
            value += hours + ":";
            if (minutes < 10)
                value += "0";
            value += minutes;
            textBox.Text = value;
        }
        private void clearHoursAndMinutesTextBox(TextBox textBox1, TextBox textBox2, TextBox textBox3)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

        }
        private void clearHolidaysComboBoxes(ComboBox comboBox1, ComboBox comboBox2, ComboBox comboBox3)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }
        private void clearTypeDescriptionComboBoxes(ComboBox comboBox1, ComboBox comboBox2, ComboBox comboBox3)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }
        private void enableSecondGridToInsert()
        {
            secondClockingGrid.Visibility = Visibility.Visible;
            insertStateSecondGridButtons();
        }
        private void enableThirdGridToInsert()
        {
            thirdClockingGrid.Visibility = Visibility.Visible;
            insertStateThirdGridButtons();
        }
        private void enableSecondGridToUpdateOrDelete()
        {
            secondClockingGrid.Visibility = Visibility.Visible;
            updateOrDeleteStateSecondGridButtons();
        }
        private void enableThirdGridToUpdateOrDelete()
        {
            thirdClockingGrid.Visibility = Visibility.Visible;
            updateOrDeleteStateThirdGridButtons();
        }
        private void disableSecondGrid()
        {
            secondClockingGrid.Visibility = Visibility.Hidden;
        }
        private void disableThirdGrid()
        {
            thirdClockingGrid.Visibility = Visibility.Hidden;
        }
        private void insertStateFirstGridButtons()
        {
            addFirstHoursWork.IsEnabled = true;
            modifyFirstHoursWork.IsEnabled = false;
            deleteFirstHoursWork.IsEnabled = false;
        }
        private void insertStateSecondGridButtons()
        {
            addSecondHoursWork.IsEnabled = true;
            modifySecondHoursWork.IsEnabled = false;
            deleteSecondHoursWork.IsEnabled = false;
        }
        private void insertStateThirdGridButtons()
        {
            addThirdHoursWork.IsEnabled = true;
            modifyThirdHoursWork.IsEnabled = false;
            deleteThirdHoursWork.IsEnabled = false;
        }
        private void updateOrDeleteStateFirstGridButtons()
        {
            addFirstHoursWork.IsEnabled = false;
            modifyFirstHoursWork.IsEnabled = true;
            deleteFirstHoursWork.IsEnabled = true;
        }
        private void updateOrDeleteStateSecondGridButtons()
        {
            addSecondHoursWork.IsEnabled = false;
            modifySecondHoursWork.IsEnabled = true;
            deleteSecondHoursWork.IsEnabled = true;
        }
        private void updateOrDeleteStateThirdGridButtons()
        {
            addThirdHoursWork.IsEnabled = false;
            modifyThirdHoursWork.IsEnabled = true;
            deleteThirdHoursWork.IsEnabled = true;
        }
        private void populateFirstGridOfWork(Holiday holiday, TypeDescription type, int hoursComing, int minutesComing, int hoursLeaving, int minutesLeaving)
        {
            updateOrDeleteStateFirstGridButtons();
            enableSecondGridToInsert();
            disableThirdGrid();
            holidayFirstHoursComboBoxWork.SelectedItem = holiday;
            typeFirstHoursComboBoxWork.SelectedItem = type;
            populateHoursAndMinutesTextBox(comingFirstHourOfWork, hoursComing, minutesComing);
            populateHoursAndMinutesTextBox(leavingFirstHourOfWork, hoursLeaving, minutesLeaving);
        }
        private void populateSecondGridOfWork(Holiday holiday, TypeDescription type, int hoursComing, int minutesComing, int hoursLeaving, int minutesLeaving)
        {

            updateOrDeleteStateFirstGridButtons();
            enableSecondGridToUpdateOrDelete();
            enableThirdGridToInsert();
            holidaySecondHoursComboBoxWork.SelectedItem = holiday;
            typeSecondHoursComboBoxWork.SelectedItem = type;
            populateHoursAndMinutesTextBox(comingSecondHourOfWork, hoursComing, minutesComing);
            populateHoursAndMinutesTextBox(leavingSecondHourOfWork, hoursLeaving, minutesLeaving);
        }
        private void populateThirdGridOfWork(Holiday holiday, TypeDescription type, int hoursComing, int minutesComing, int hoursLeaving, int minutesLeaving)
        {
            updateOrDeleteStateFirstGridButtons();
            enableSecondGridToUpdateOrDelete();
            enableThirdGridToUpdateOrDelete();
            holidayThirdHoursComboBoxWork.SelectedItem = holiday;
            typeThirdHoursComboBoxWork.SelectedItem = type;
            populateHoursAndMinutesTextBox(comingThirdHourOfWork, hoursComing, minutesComing);
            populateHoursAndMinutesTextBox(leavingThirdHourOfWork, hoursLeaving, minutesLeaving);
        }
        private void noDataInDBWork()
        {
            insertStateFirstGridButtons();
            disableSecondGrid();
            disableThirdGrid();
            comingFirstHourOfWork.Text = "07:00";
            leavingFirstHourOfWork.Text = "15:00";
        }

        private void AddFirstHoursWork_Click(object sender, RoutedEventArgs e)
        {
            AddWorkInDB(comingFirstHourOfWork, leavingFirstHourOfWork, typeFirstHoursComboBoxWork, holidayFirstHoursComboBoxWork);
        }

        private DateTime getSelectedDateFromDataGrid(int hours, int minutes)
        {
            IList<DataGridCellInfo> cells = dataGridWork.SelectedCells;
            string selectedDay = cells[0].Column.Header as string;
            string monthYear = monthYearComboBoxWork.SelectedValue as string;
            string[] splitted = monthYear.Split('.');
            return new DateTime(int.Parse(splitted[1]), int.Parse(splitted[0]), int.Parse(selectedDay), hours, minutes, 0);

        }
        private bool canInsertUserInDBWork(User user)
        {
            if (user == null)
            {
                return false;
            }
            return true;
        }
        private bool canInsertHolidayAndTypeInDBWork(TypeDescription type, Holiday holiday)
        {
            int no = 0;
            try
            {
                if (type.Name.Equals(""))
                {
                    ++no;
                }
            }
            catch (NullReferenceException ex)
            {
                ++no;
            }
            try
            {
                if (holiday.Name.Equals(""))
                {
                    ++no;
                }
            }
            catch (NullReferenceException ex)
            {
                ++no;
            }
            if (no == 2)
                return false;
            else
                return true;
        }
        private void AddWorkInDB(TextBox comingTextBox, TextBox leavingTextBox, ComboBox typeHoursComboBox, ComboBox holidayHoursComboBox)
        {
            User user = userComboBoxWork.SelectedItem as User;
            bool canInsert = true;
            string message = "";
            canInsert = canInsertUserInDBWork(user);
            if (!canInsert)
                message += "Selecteaza persoana!\n";
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate;
            int hoursComing = 0;
            int minutesComing = 0;
            if (!CheckIfHourIsCorrect(comingTextBox.Text))
            {
                canInsert = false;
                message += "Ora de venire introdusa este incorecta!\n";
            }
            else
            {
                hoursComing = getHourFromString(comingTextBox.Text);
                minutesComing = getMinutesFromString(comingTextBox.Text);
            }

            try
            {
                startDate = getSelectedDateFromDataGrid(hoursComing, minutesComing);

            }
            catch (ArgumentOutOfRangeException ex)
            {
                canInsert = false;
                message += "Selecteaza data!\n";
            }
            int hoursLeaving = 0;
            int minutesLeaving = 0;
            if (!CheckIfHourIsCorrect(leavingTextBox.Text))
            {
                canInsert = false;
                message += "Ora de plecare introdusa este incorecta!\n";
            }
            else
            {
                hoursLeaving = getHourFromString(leavingTextBox.Text);
                minutesLeaving = getMinutesFromString(leavingTextBox.Text);
            }

            TypeDescription type = typeHoursComboBox.SelectedItem as TypeDescription;
            Holiday holiday = holidayHoursComboBox.SelectedItem as Holiday;
            canInsert = canInsertHolidayAndTypeInDBWork(type, holiday);
            if (!canInsert)
            {
                message += "Selecteaza tipul de pontaj sau concediu!\n";
                MessageBox.Show(message);
            }
            int day = startDate.Day;
            string leavingDate = "";
            if (hoursLeaving < hoursComing)
            {

            }

            try
            {
                endDate = new DateTime(startDate.Year, startDate.Month, day, hoursLeaving, minutesLeaving, 0);

            }
            catch (ArgumentOutOfRangeException ex)
            {
                canInsert = false;
            }
        }
        private string checkEndOfMonth(int year, int month, int day)
        {
            if (month == 2 && day == 28)
            {
                day = 1;
                month += 1;
            }
            if(day == 31 &&(month == 1||month ==3 ||month==5||month==7||month==8||month==10))
            {
                day = 1;
                month += 1;
            }
            if (day == 30 && (month == 4 || month == 6 || month == 9 || month == 11))
            {
                day = 1;
                month += 1;
            }

            return "";

        }

    }
}

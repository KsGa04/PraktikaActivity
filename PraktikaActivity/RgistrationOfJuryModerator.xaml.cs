using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PraktikaActivity
{
    /// <summary>
    /// Логика взаимодействия для RgistrationOfJuryModerator.xaml
    /// </summary>
    public partial class RgistrationOfJuryModerator : Window
    {
        public ActivityEntities db = new ActivityEntities();
        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\Users\";
        private string _photoName = null;
        private string _filePath = null;
        public RgistrationOfJuryModerator()
        {
            InitializeComponent();
            role.Items.Add("Модераторы");
            role.Items.Add("Жюри");

            gender.Items.Add("женский");
            gender.Items.Add("мужской");
            GenerateUniqueId();
            events.IsEnabled = false;

            int newId = GenerateUniqueId();
            id.Text = newId.ToString();
        }
        private static readonly Regex _phoneNumberRegex = new Regex(@"^\+7\((\d{3})\)\s-\s(\d{3})-(\d{2})-(\d{2})$");

        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            return _phoneNumberRegex.IsMatch(phoneNumber);
        }
        public static int GenerateUniqueId()
        {
            using (ActivityEntities db = new ActivityEntities())
            {
                Random random = new Random();
                int newId;

                do
                {
                    newId = random.Next(1000, 10000);
                } while (db.Users.Any(u => u.Id == newId));

                return newId;
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            id.Text = string.Empty;
            FIO.Text = string.Empty;
            gender.SelectedIndex = -1;
            role.SelectedIndex = -1;
            email.Text = string.Empty;
            phone.Text = string.Empty;
            direction.Text = string.Empty;
            events.Text = string.Empty;
            password.Password = string.Empty;
            repeat_password.Password = string.Empty;
        }
        string ChangePhotoName()
        {
            string x = _currentDirectory + _photoName;
            string photoname = _photoName;
            int i = 0;
            if (File.Exists(x))
            {
                while (File.Exists(x))
                {
                    i++;
                    x = _currentDirectory + i.ToString() + photoname;
                }
                photoname = i.ToString() + photoname;
            }
            return photoname;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (id.Text == string.Empty || FIO.Text == string.Empty || email.Text == string.Empty || phone.Text == string.Empty || direction.Text == string.Empty || events.Text == string.Empty || password.Password == string.Empty || repeat_password.Password == string.Empty || gender.SelectedIndex == -1 || role.SelectedIndex == -1) {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                if (IsPhoneNumberValid(phone.Text))
                {
                    int y = ValidatePassword(password.Password);
                    if (y != 5)
                    {
                        if (y == 0)
                        {
                            MessageBox.Show("Пароль не соответствует требованиям, пароль должен содержать цифры");
                        }
                        else if (y == 1)
                        {
                            MessageBox.Show("Пароль не соответствует требованиям, пароль должен содержать строчые буквы");
                        }
                        else if (y == 2)
                        {
                            MessageBox.Show("Пароль не соответствует требованиям, пароль должен содержать заглавные буквы");
                        }
                        else if (y == 3)
                        {
                            MessageBox.Show("Пароль не соответствует требованиям, пароль должен содержать специальные символы");
                        }
                        else if (y == 4)
                        {
                            MessageBox.Show("Пароль не соответствует требованиям, пароль должен содержать от 8 до 20 символов");
                        }
                        password.Background = new SolidColorBrush(Colors.Red);
                        password.Password = "";
                    }
                    else
                    {

                        if (password.Password == repeat_password.Password)
                        {

                            Users users = new Users();
                            users.FullName = FIO.Text;
                            users.GenderId = gender.SelectedIndex + 1;
                            users.PhoneNumber = phone.Text;
                            users.Email = email.Text;
                            users.Password = password.Password;
                            users.DirectionID = Convert.ToInt32(direction.Text);
                            users.DateOfBirth = new DateTime(1999, 1, 1);
                            if (_filePath != null)
                            {
                                string photo = ChangePhotoName();
                                string dest = _currentDirectory + photo;
                                File.Copy(_filePath, dest);
                                users.Photo = photo;
                            }
                            if (role.SelectedIndex == 0)
                            {
                                users.RoleId = role.SelectedIndex + 1;
                            }
                            else
                            {
                                users.RoleId = role.SelectedIndex + 2;
                            }
                            db.Users.Add(users);
                            db.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Пароли не совпадают");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Телефон имеет неверный формат");
                }
                
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files(*.gif) | *.gif";
                if (op.ShowDialog() == true)
                {
                    FileInfo fileInfo = new FileInfo(op.FileName);
                    if (fileInfo.Length > (1024 * 1024 * 2))
                    {
                        throw new Exception("Размер файла должен быть меньше 2Мб");
                    }
                    Image.Source = new BitmapImage(new Uri(op.FileName));
                    _photoName = op.SafeFileName;
                    _filePath = op.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
                _filePath = null;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            events.IsEnabled = true;
        }

        private void check_events_Unchecked(object sender, RoutedEventArgs e)
        {
            events.IsEnabled = false;
        }
        public static int ValidatePassword(string x)
        {
            bool hasDigit = false;
            bool hasLower = false;
            bool hasUpper = false;
            bool hasSpeciaChar = false;
            int kol = 0;

            foreach (char c in x)
            {
                kol++;
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else if (char.IsLower(c))
                {
                    hasLower = true;
                }
                else if (char.IsUpper(c))
                {
                    hasUpper = true;
                }
                else if ((char.IsSymbol(c)) || (c == '!') || (c == '@') || (c == '$') || (c == '#'))
                {
                    hasSpeciaChar = true;
                }
            }
            if (hasDigit == false)
            {

                return 0;
            }
            else if (hasLower == false)
            {

                return 1;
            }
            else if (hasUpper == false)
            {

                return 2;
            }
            else if (hasSpeciaChar == false)
            {

                return 3;
            }
            else if ((kol < 8) || (kol > 20))
            {

                return 4;
            }
            else
            {
                return 5;
            }
        }


            private void pass_check_Checked(object sender, RoutedEventArgs e)
        {
            password.IsEnabled = true;
            password.PasswordChar = '\0';
            repeat_password.IsEnabled = true;
            repeat_password.PasswordChar = '\0';
        }

        private void pass_check_Unchecked(object sender, RoutedEventArgs e)
        {
            password.IsEnabled = true;
            password.PasswordChar = '●';
            repeat_password.IsEnabled = true;
            repeat_password.PasswordChar = '●';
        }
    }
}

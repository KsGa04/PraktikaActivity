using EasyCaptcha.Wpf;
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
using System.Windows.Shapes;

namespace PraktikaActivity
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public ActivityEntities db = new ActivityEntities();
        string answer = "";
        int incorrectCaptchaAttempts = 0;
        public Authorization()
        {
            InitializeComponent();
            RemadeCaptcha();
        }
        public static bool PerformAuthorization(int userId, string password)
        {
            using (ActivityEntities db = new ActivityEntities())
            {
                var user = db.Users.Where(u => u.Id == userId && u.Password == password).FirstOrDefault();
                if (user != null)
                {
                    // Проверка роли и переход на соответствующую страницу
                    switch (user.RoleId)
                    {
                        case 1:
                            // Открыть страницу Participant
                            return true;
                        case 2:
                            // Открыть страницу Moderator
                            return true;
                        case 3:
                            // Открыть страницу Jury
                            return true;
                        case 4:
                            // Открыть страницу Organaizer
                            return true;
                        default:
                            // Отобразить сообщение об ошибке
                            return false;
                    }
                }
                else
                {
                    // Отобразить сообщение об ошибке
                    return false;
                }
            }
        }
        private void RemadeCaptcha()
        {
            AuthCaptcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 4);
        }
        private void autho_Click(object sender, RoutedEventArgs e)
        {
            using (ActivityEntities db = new ActivityEntities())
            {
                answer = Answer.Text;
                if (incorrectCaptchaAttempts < 3)
                {
                    if (answer != AuthCaptcha.CaptchaText)
                    {
                        incorrectCaptchaAttempts += 1;
                        MessageBox.Show("Неверная CAPTCHA");
                    }
                    else
                    {
                        int id = Convert.ToInt32(IdNumber.Text);
                        var result = db.Users.Where(x => x.Id == id && x.Password == Pass.Password).FirstOrDefault();
                        if (result != null)
                        {
                            {
                                CurrentUser.currentUserId = result.Id;

                                if (result.RoleId == 1)
                                {
                                    Participant participant = new Participant();
                                    participant.Show();
                                    this.Close();
                                }
                                else if (result.RoleId == 2)
                                {
                                    Moderator moderator = new Moderator();
                                    moderator.Show();
                                    this.Close();
                                }
                                else if (result.RoleId == 3)
                                {
                                    Jury jury = new Jury();
                                    jury.Show();
                                    this.Close();
                                }
                                else if (result.RoleId == 4)
                                {
                                    Organaizer organaizer = new Organaizer();
                                    organaizer.Show();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Неправильный email или пароль");
                                }
                            }
                        }
                    }
                }
                    else
                {
                    MessageBox.Show("Слишком много неверных попыток. Система заблокирована на 10 секунд.");
                    System.Threading.Thread.Sleep(10000);
                    incorrectCaptchaAttempts -= 1;
                }
                
            }
        }
    }
}

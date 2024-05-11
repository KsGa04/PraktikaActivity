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
        public Authorization()
        {
            InitializeComponent();
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
        private void autho_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(IdNumber.Text);
            if (db.Users.Where(x => x.Id == id || x.Password == Pass.Password).Any())
            {
                Users users = db.Users.Where(x => x.Id == id || x.Password == Pass.Password).FirstOrDefault();
                switch (users.RoleId)
                {
                    case 1:
                        Participant participant = new Participant();
                        participant.Show();
                        this.Hide();
                        break;
                    case 2:
                        Moderator moderator = new Moderator();
                        moderator.Show();
                        this.Hide();
                        break;
                    case 3:
                        Jury jury = new Jury();
                        jury.Show();
                        this.Hide();
                        break;
                    case 4:
                        Organaizer organaizer = new Organaizer(users);
                        organaizer.Show();
                        this.Hide();
                        break;
                    default:
                        MessageBox.Show("Данный пользователь не имеет роли");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Данный пользователь отсутсвтует");
            }
        }
    }
}

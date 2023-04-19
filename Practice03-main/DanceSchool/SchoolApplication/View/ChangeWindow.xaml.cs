using SchoolApplication.DbEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace SchoolApplication.View
{
    /// <summary>
    /// Логика взаимодействия для ChangeWindow.xaml
    /// </summary>
    public partial class ChangeWindow : Window
    {
        private InfoStudent _infoStudent;
        public ChangeWindow(InfoStudent infoSportsman)
        {
            InitializeComponent();

            foreach (var item in App.Current.Windows)
            {
                if (item is ApplicationWindow)
                {
                    this.Owner = item as Window;
                }
            }

            if (infoSportsman is null)
            {
                _infoStudent = infoSportsman = new InfoStudent();
            }
            else
            {
                _infoStudent = infoSportsman;
            }
            this.DataContext = _infoStudent;
        }




        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new examEntities())
            {
                try
                {

                    var validateRes = ValidateEntity();
                    if (validateRes.Length > 0)
                    {
                        MessageBox.Show(validateRes.ToString(), "Информация", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    db.InfoStudent.AddOrUpdate(_infoStudent);
                    db.SaveChanges();
                    MessageBox.Show("данные успешно сохранены", "успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    (Owner as ApplicationWindow)?.Refresh();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private StringBuilder ValidateEntity()
        {
            var errors = new StringBuilder();

            if (_infoStudent != null)
            {
                if (string.IsNullOrEmpty(_infoStudent.StudentSurname))
                {
                    errors.AppendLine("Поле Фамилия спортсмена не может быть пустым!");
                }
                if (string.IsNullOrEmpty(_infoStudent.StudentName))
                {
                    errors.AppendLine("Поле Имя спортсмена не может быть пустым!");
                }
                if (_infoStudent.StudentAge <= 0)
                {
                    errors.AppendLine("Поле Возраст спортсмена не может быть пустым");
                }
            }
            return errors;
        }
    }
}

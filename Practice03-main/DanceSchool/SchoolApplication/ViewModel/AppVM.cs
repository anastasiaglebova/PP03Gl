using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SchoolApplication.DbEntity;

namespace SchoolApplication.ViewModel
{
    public class AppVM : BaseVM
    {
        private InfoStudent _selectedItem;
        private ObservableCollection<InfoStudent> _infoStudent;
        private ObservableCollection<InfoCoach> _infoCoach;
        private ObservableCollection<InfoGroup> _infoGroup;
        private ObservableCollection<Training> _training;

        public ObservableCollection<InfoStudent> InfoStudent
        {
            get => _infoStudent;
            set
            {
                _infoStudent = value;
                OnPropertyChanged(nameof(InfoStudent));
            }
        }

        public ObservableCollection<InfoCoach> InfoCoach
        {
            get => _infoCoach;
            set
            {
                _infoCoach = value;
                OnPropertyChanged(nameof(InfoCoach));
            }
        }

        public ObservableCollection<InfoGroup> InfoGroup
        {
            get => _infoGroup;
            set
            {
                _infoGroup = value;
                OnPropertyChanged(nameof(InfoGroup));
            }
        }

        public ObservableCollection<Training> Training
        {
            get => _training;
            set
            {
                _training = value;
                OnPropertyChanged(nameof(Training));
            }
        }

        public InfoStudent SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public AppVM()
        {
            InfoStudent = new ObservableCollection<InfoStudent>();
            InfoCoach = new ObservableCollection<InfoCoach>();
            InfoGroup = new ObservableCollection<InfoGroup>();
            Training = new ObservableCollection<Training>();

            LoadData();
        }

        public void LoadData()
        {
            if (InfoStudent.Count != 0)
            {
                InfoStudent.Clear();
            }

            var result = DbStorage.DB_s.InfoStudent.ToList();
            result.ForEach(elem => InfoStudent?.Add(elem));
            if (InfoCoach.Count != 0)
            {
                InfoCoach.Clear();
            }

            var result1 = DbStorage.DB_s.InfoCoach.ToList();
            result1.ForEach(elem => InfoCoach?.Add(elem));
            if (InfoGroup.Count != 0)
            {
                InfoGroup.Clear();
            }

            var result2 = DbStorage.DB_s.InfoGroup.ToList();
            result2.ForEach(elem => InfoGroup?.Add(elem));
            if (Training.Count != 0)
            {
                Training.Clear();
            }

        }

            public void DeleteSelectedItem()
            {
                if (!(SelectedItem is null))
                {
                    using (var db = new examEntities())
                    {
                        var result = MessageBox.Show("Вы действительно хотите удалить этот элемент?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                            try
                            {
                                var ItemForDelete = db.InfoStudent.Where(elem => elem.StudentID == SelectedItem.StudentID).FirstOrDefault();
                                db.InfoStudent.Remove(ItemForDelete);
                                db.SaveChanges();
                                LoadData();
                                MessageBox.Show("Данные успешно удалены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Удаление завершено успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                }
            }
        
    }
}
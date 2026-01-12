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

namespace Anastasia423WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AutoModel.xaml
    /// </summary>
    public partial class AutoModel : Page
    {
        public AutoModel()
        {
            InitializeComponent();
            List<Car> cars = new List<Car>()
            {
                new Car
                {
                    Name = "Toyota",
                    Price = 2000000,
                    Color = "Красный"
                },
                new Car
                {
                    Name = "BMW X5",
                    Price = 3500000,
                    Color = "Белый"
                },
                new Car
                {
                    Name = "Mercedes-Benz",
                    Price = 4000000,
                    Color = "Черный"
                }
            };
           
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

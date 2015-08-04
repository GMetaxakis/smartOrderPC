using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SmartOrder
{

    public class UserIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                      System.Globalization.CultureInfo culture)
        {
            int userId = (int)value;
            foreach (User u in DBController.LoadUsers())
                if (u.User_id == userId)
                    return u.Name;

            return "ERROR";
        }
        public object ConvertBack(object value, Type targetType, object parameter,
                      System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ItemIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                      System.Globalization.CultureInfo culture)
        {
            int itemId = (int)value;
            foreach (Items i in DBController.LoadItems())
                if (i.Id == itemId)
                    return i.Name;

            return "ERROR";
        }
        public object ConvertBack(object value, Type targetType, object parameter,
                      System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

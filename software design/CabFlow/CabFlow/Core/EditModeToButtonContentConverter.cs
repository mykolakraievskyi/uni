using System.Globalization;
using System.Windows.Data;

namespace CabFlow.Core
{
    public class EditModeToButtonContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isEditing)
            {
                return isEditing ? "Save" : "Edit";
            }
            return "Edit";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string buttonText)
            {
                return buttonText == "Save";
            }
            return new ArgumentException("Invalid value for converter");
        }
    }
}

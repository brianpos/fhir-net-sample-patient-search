using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace LocalConverters
{
	public class PhotoConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return null;

			byte[] extractme = null;

			if (value is byte[])
				extractme = value as byte[];
			else if (value is ObservableCollection<byte>)
			{
				extractme = (value as ObservableCollection<byte>).ToArray();
			}

			if (extractme != null && extractme.Length > 0)
			{
				MemoryStream stream = new MemoryStream(value as byte[]);
				BitmapImage image = new BitmapImage();
				image.BeginInit();
				image.StreamSource = stream;
				image.CacheOption = BitmapCacheOption.OnLoad;
				image.EndInit();
				return image;
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

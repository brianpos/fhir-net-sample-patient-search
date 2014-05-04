using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace LocalConverters
{
	public class HumanNameToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is List<Hl7.Fhir.Model.HumanName> && (value as List<Hl7.Fhir.Model.HumanName>).Count > 0)
			{
				return Convert((value as List<Hl7.Fhir.Model.HumanName>).First(), targetType, parameter, culture);
			}
			else if (value is Hl7.Fhir.Model.HumanName)
			{
				Hl7.Fhir.Model.HumanName name = value as Hl7.Fhir.Model.HumanName;
				StringBuilder sb = new StringBuilder();
				if (name.Prefix != null && name.Prefix.Count() > 0)
				{
					if (sb.Length > 0)
						sb.Append(" ");
					sb.Append(name.Prefix.FirstOrDefault());
				}
				if (name.Given != null && name.Given.Count() > 0)
				{
					if (sb.Length > 0)
						sb.Append(" ");
					sb.Append(name.Given.FirstOrDefault());
				}
				if (name.Family != null && name.Family.Count() > 0)
				{
					if (sb.Length > 0)
						sb.Append(" ");
					sb.Append(name.Family.FirstOrDefault());
				}
				if (name.Suffix != null && name.Suffix.Count() > 0)
				{
					if (sb.Length > 0)
						sb.Append(" ");
					sb.Append(name.Suffix.FirstOrDefault());
				}
				return sb.ToString();
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Demo.Common.Extensions
{
	public static class StringExtensions
	{
		public static string GetSuffix(this string value, string separator)
		{
			var arrSplit = value.Split(separator.ToCharArray());
			return arrSplit.Length > 0 ? arrSplit.Last() : value;
		}

		/// <summary>
		/// Removes a number of char on the right side of the string
		/// </summary>
		public static string RemoveRight(this string value, int number)
		{
			if (number > value.Length)
				throw new InvalidOperationException("Can't remove more then it contains");

			return value.Substring(0, value.Length - number);
		}

		public static string ToHttpHeaderTransportString(this string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
			}
			else
			{
				return value;
			}
		}

		public static string FromHttpHeaderTransportString(this string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return Encoding.UTF8.GetString(Convert.FromBase64String(value));
			}
			else
			{
				return value;
			}
		}

		public static string ToCamelCase(this string value)
		{
			if (value == null) return value;

			var words = value.Split(new[] { "_", " " }, StringSplitOptions.RemoveEmptyEntries);
			var leadWord = Regex.Replace(words[0], @"([A-Z])([A-Z]+|[a-z0-9]+)($|[A-Z]\w*)",
				m =>
				{
					return m.Groups[1].Value.ToLower() + m.Groups[2].Value.ToLower() + m.Groups[3].Value;
				});
			var tailWords = words.Skip(1)
				.Select(word => char.ToUpper(word[0]) + word.Substring(1))
				.ToArray();
			return $"{leadWord}{string.Join(string.Empty, tailWords)}";
		}

		public static string ToTitleCase(this string value)
		{
			return value?.ToCamelCase()?.FirstCharToUpper();
		}

		public static string FirstCharToUpper(this string value)
		{
			if (value == null) return value;
			return value[0].ToString().ToUpper() + value.Substring(1);
		}

	}
}

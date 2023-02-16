using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Cryptography
{

	public static class CryptographyExtension
	{

		public static string Encode(this string input)
		{
			return new Cryptography().Encode(input);
		}


		public static string Decode(this string input)
		{
			return new Cryptography().Decode(input);
		}

		public static string Base64Encode(this string input)
		{
			var bytes = Encoding.UTF8.GetBytes(input);
			return Convert.ToBase64String(bytes);
		}

		public static string Base64Decode(this string input)
		{
			var bytes = Convert.FromBase64String(input);
			return Encoding.UTF8.GetString(bytes);
		}
	}

	public class Cryptography
	{
		E_Type Tmp_s = new E_Type(1024);
		E_Bytes Tmp_b = new E_Bytes(1024);

		/// <summary>
		/// Verschlüsselt die Eingabedaten
		/// </summary>
		/// <param name="text">Der zu verschlüsselnde Klartext</param>
		/// <returns>Die resultierende Verschlüsselung des text-Parameters als verschlüsselter Text.</returns>
		[System.Diagnostics.DebuggerStepThrough]
		public string Encode(string text)
		{
			return Crypt(text);
		}


		/// <summary>
		/// Entschlüsselt die Eingabedaten 
		/// </summary>
		/// <param name="text"> Der verschlüsselte Text, der entschlüsselt werden soll.</param>
		/// <returns>Die resultierende Entschlüsselung des text-Parameters in Klartext</returns>
		[System.Diagnostics.DebuggerStepThrough]
		public string Decode(string text)
		{
			return Crypt(text);
		}

		#region Base64
		public string Base64Encode(string plainText)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
		}

		public string Base64Decode(string base64EncodedData)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
		}
		#endregion

		#region Utilities
		[System.Diagnostics.DebuggerStepThrough]
		private string Crypt(string text)
		{
			int tmp_pos = 0;
			string out_str = "";
			byte tmp_char = 0;
			int char_cnv = 0;
			int t_len = 0;
			int t_rest = 0;
			int t_strt = 0;

			if (string.IsNullOrEmpty(text))
				return string.Empty;

			if (text == " ")
				return " ";

			if (text.Length < 1)
				return text;

			t_len = text.Length;
			do
			{
				t_rest = t_len - t_strt;
				if (t_rest > 1023)
					t_rest = 1023;

				Tmp_s.t_str = new StringBuilder(text.Substring(t_strt, t_rest));
				Tmp_b.t_bytes = Encoding.Default.GetBytes(text.ToString());

				for (tmp_pos = 0; tmp_pos < t_rest; tmp_pos++)
				{
					tmp_char = Tmp_b.t_bytes[tmp_pos];

					if (tmp_char == 2)
					{
						Tmp_b.t_bytes[tmp_pos] = 124;
					}
					else if (tmp_char == 3)
					{
						Tmp_b.t_bytes[tmp_pos] = 39;
					}
					else if (tmp_char == 39)
					{
						Tmp_b.t_bytes[tmp_pos] = 3;
					}
					else if (tmp_char == 124)
					{
						Tmp_b.t_bytes[tmp_pos] = 2;
					}
					else if (tmp_char > 3 && tmp_char < 47 || tmp_char == 123 || tmp_char > 124)
					{
						Tmp_b.t_bytes[tmp_pos] = tmp_char;
					}
					else
					{
						char_cnv = tmp_char ^ (int)0x0F;
						if (char_cnv < 48 || char_cnv == 123 || char_cnv >= 124)
						{
							Tmp_b.t_bytes[tmp_pos] = tmp_char;
						}
						else
						{
							Tmp_b.t_bytes[tmp_pos] = (byte)char_cnv;
						}
					}
				}

				Tmp_s.t_str = new StringBuilder(Encoding.Default.GetString(Tmp_b.t_bytes));
				out_str += Tmp_s.t_str.ToString().Substring(0, t_rest);
				t_strt += t_rest;
			} while (t_strt < t_len);

			return out_str;
		}


		private struct E_Bytes
		{
			public byte[] t_bytes;
			public E_Bytes(int size)
			{
				t_bytes = new byte[size];
			}

		}

		private struct E_Type
		{
			public StringBuilder t_str;
			public E_Type(int size)
			{
				t_str = new StringBuilder(size);
			}
		}
		#endregion
	}
}

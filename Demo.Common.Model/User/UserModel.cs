using Demo.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common.Model.User
{
	public class UserModel : BaseModel
	{
		public string RgNumKreis { get; set; }
		public int? Grp4 { get; set; }
		public string Bank { get; set; }
		public string Bic { get; set; }
		public int? Grp2 { get; set; }
		public string ArztNr { get; set; }
		public string Strasse { get; set; }
		public string Telefon { get; set; }
		public int? Grp3 { get; set; }
		public int? Grp8 { get; set; }
		public DateTime? XChg { get; set; }
		public string Name { get; set; }
		public string Vorname { get; set; }
		public string TelefonPriv { get; set; }
		public int? IdStandort { get; set; }
		public string Blz { get; set; }
		public string Ort { get; set; }
		public string Zusatz { get; set; }
		public int? Grp6 { get; set; }
		public int? PassAb { get; set; }
		public string Plz { get; set; }
		public string Titel { get; set; }
		public int? Grp7 { get; set; }
		public string Fax { get; set; }
		public int? Grp5 { get; set; }
		public int? LoginVersuche { get; set; }
		public int? Grp1 { get; set; }
		public string FlReferenznr { get; set; }
		public string Zertifikat { get; set; }
		public string Iban { get; set; }
		public string XId { get; set; }
		public bool Inactive { get; set; }
		public bool Abteilungsleiter { get; set; }
		public DateTime? LastChg { get; set; }
		public string Passwort { get; set; }
		public string Email { get; set; }
		public new int Id { get; set; }
		public string Konto { get; set; }
		public string Fach { get; set; }
		public string ErgonoflexPermissions { get; set; }
		public string Benutzer { get; set; }
		public bool ErgodatUser { get; set; }
		public int? ErgonoflexZuordnung { get; set; }
	}
}

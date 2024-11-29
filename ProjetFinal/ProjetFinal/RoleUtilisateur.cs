using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal static class RoleUtilisateur
    {
		private static bool admin;

		public static bool Admin
		{
			get { return admin; }
			set { admin = value; }
		}

		private static string utilisateurConnecte;

		public static string UtilisateurConnecte
		{
			get { return utilisateurConnecte; }
			set { utilisateurConnecte = value; }
		}
	}
}

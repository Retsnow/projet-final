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

		private static bool utilisateurConnecte;

		public static bool UtilisateurConnecte
		{
			get { return utilisateurConnecte; }
			set { utilisateurConnecte = value; }
		}
	}
}

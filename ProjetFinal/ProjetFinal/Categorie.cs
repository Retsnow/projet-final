using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class Categorie
    {
        private int id;
        private string nom;

        public Categorie(string p_id, string p_nom)
        {
            Id = Convert.ToInt32(p_id);
            Nom = p_nom;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
    }
}

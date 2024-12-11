using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    public class ActiviteNote
    {

        private string nom;

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        private double note;

        public double Note
        {
            get { return note; }
            set { note = value; }
        }

        public ActiviteNote(string p_nom, double p_note)
        {
            Nom = p_nom;
            Note = p_note;
        }
    }
}

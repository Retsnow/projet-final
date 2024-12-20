﻿using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class Seance
    {

        private int id;
        private DateOnly date;
        private TimeOnly heure;
        private int nb_place_disponible;
        private string nom_activite;
        private int id_categorie;
        private Visibility btnDelete;
        private bool utilisateurInscrit;

        public Visibility BtnDelete
        {
            get
            {
                btnDelete = RoleUtilisateur.Admin ? Visibility.Visible : Visibility.Collapsed;
                this.OnPropertyChanged(nameof(BtnDelete));
                return btnDelete;
            }
        }

       

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                this.OnPropertyChanged(nameof(Id));
            }
        }

        public DateOnly Date
        {
            get { return date; }
            set
            {
                date = value;
                this.OnPropertyChanged(nameof(Date));
            }
        }

        public TimeOnly Heure
        {
            get { return heure; }
            set
            {
                heure = value;
                this.OnPropertyChanged(nameof(Heure));
            }
        }

        public int Nb_place_disponible
        {
            get { return nb_place_disponible; }
            set
            {
                nb_place_disponible = value;
                this.OnPropertyChanged(nameof(Nb_place_disponible));
            }
        }

        public string Nom_activite
        {
            get { return nom_activite; }
            set
            {
                nom_activite = value;
                this.OnPropertyChanged(nameof(Nom_activite));
            }
        }

        public int Id_categorie
        {
            get { return id_categorie; }
            set
            {
                id_categorie = value;
                this.OnPropertyChanged(nameof(Id_categorie));
            }
        }

        public Seance(DateOnly p_date, TimeOnly p_heure, int p_nb_place_disponible, string p_nom_activite, int p_id_categorie)
        {
            Date = p_date;
            Heure = p_heure;
            Nb_place_disponible = p_nb_place_disponible;
            Nom_activite = p_nom_activite;
            Id_categorie = p_id_categorie;
        }

        public bool UtilisateurInscrit
        {
            get
            {
                utilisateurInscrit = SingletonRequete.getListeSeance(Nom_activite).Count > 0;
                this.OnPropertyChanged(nameof(UtilisateurInscrit));
                return utilisateurInscrit;
            }
        }







        public Seance(int p_id, DateOnly p_date, TimeOnly p_heure, int p_nb_place_disponible, string p_nom_activite, int p_id_categorie)
        {
            Id = p_id;
            Date = p_date;
            Heure = p_heure;
            Nb_place_disponible = p_nb_place_disponible;
            Nom_activite = p_nom_activite;
            Id_categorie = p_id_categorie;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

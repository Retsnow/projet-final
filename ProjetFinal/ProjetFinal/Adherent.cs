﻿using Google.Protobuf.WellKnownTypes;
using Microsoft.UI.Xaml;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class Adherent
    {
        private string id;
        private string nom;
        private string prenom;
        private string adresse;
        private DateOnly date_naissance;
        private int age;
        private Visibility btnDelete;


        public Adherent(string id, string nom, string prenom, string adresse, DateOnly date_naissance, int age)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Date_naissance = date_naissance;
            Age = age;
        }

        public Adherent(string nom, string prenom, string adresse, DateOnly date_naissance, int age)
        {
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Date_naissance = date_naissance;
            Age = age;
        }

        public Visibility BtnDelete
        {
            get
            {
                btnDelete = RoleUtilisateur.Admin ? Visibility.Visible : Visibility.Collapsed;
                this.OnPropertyChanged(nameof(BtnDelete));
                return btnDelete;
            }
        }

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                this.OnPropertyChanged(nameof(Id));
            }
        }

        public string Nom
        {
            get { return nom; }
            set
            {
                nom = value;
                this.OnPropertyChanged(nameof(Nom));
            }
        }

        public string Prenom
        {
            get { return prenom; }
            set
            {
                prenom = value;
                this.OnPropertyChanged(nameof(prenom));
            }
        }

        public string Adresse
        {
            get { return adresse; }
            set
            {
                adresse = value;
                this.OnPropertyChanged(nameof(adresse));
            }
        }

        public DateOnly Date_naissance
        {
            get { return date_naissance; }
            set
            {
                date_naissance = value;
                this.OnPropertyChanged(nameof(date_naissance));
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                this.OnPropertyChanged(nameof(age));
            }
        }

        public string NomComplet
        {
            get { return prenom + " " + nom; } 
        }

        public string StringCSV
        {
            get
            {
                return $"{Id};{Nom};{Prenom};{Adresse};{Date_naissance};{Age}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

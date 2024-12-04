using Google.Protobuf.WellKnownTypes;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class Activite
    {

        private string nom;
        private double cout_organisation;
        private double prix_vente;
        private int id_categorie;
        private Visibility btnDelete;
        private Visibility btnInscription;


        public Visibility BtnDelete
        {
            get
            {
                btnDelete = RoleUtilisateur.Admin ? Visibility.Visible : Visibility.Collapsed;
                this.OnPropertyChanged(nameof(BtnDelete));
                return btnDelete;
            }
        }

        public Visibility BtnInscription
        {
            get
            {
                btnInscription = ((RoleUtilisateur.UtilisateurConnecte == null || RoleUtilisateur.UtilisateurConnecte == "") ? Visibility.Collapsed : SingletonRequete.AdherantEstInscritActivite(RoleUtilisateur.UtilisateurConnecte, Nom) ? Visibility.Collapsed : Visibility.Visible);
                this.OnPropertyChanged(nameof(BtnInscription));
                return btnInscription;
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

        public double Cout_organisation
        {
            get { return cout_organisation; }
            set
            {
                cout_organisation = value;
                this.OnPropertyChanged(nameof(Cout_organisation));
            }
        }

        public double Prix_vente
        {
            get { return prix_vente; }
            set
            {
                prix_vente = value;
                this.OnPropertyChanged(nameof(Prix_vente));
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

        public Activite(double p_cout_organisation, double p_prix_vente, int p_id_categorie)
        {
            Cout_organisation = p_cout_organisation;
            Prix_vente = p_prix_vente;
            Id_categorie = p_id_categorie;
        }

        public Activite(string p_nom, double p_cout_organisation, double p_prix_vente, int p_id_categorie)
        {
            Nom = p_nom;
            Cout_organisation = p_cout_organisation;
            Prix_vente = p_prix_vente;
            Id_categorie = p_id_categorie;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}

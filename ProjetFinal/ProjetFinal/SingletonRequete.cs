using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal class SingletonRequete
    {
        static MySqlConnection conn = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420335ri_eq15;Uid=1751579;Pwd=1751579;");

        public SingletonRequete()
        {

        }

        public static async void AjouterSelonCSV(object target, Frame currentFrame)
        {

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".csv");

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(target);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            //sélectionne le fichier à lire
            Windows.Storage.StorageFile monFichier = await picker.PickSingleFileAsync();

            //ouvre le fichier et lit le contenu
            if (monFichier != null)
            {
                var lignes = await Windows.Storage.FileIO.ReadLinesAsync(monFichier);

                /*boucle permettant de lire chacune des lignes du fichier
                * et de remplir un tableau d'objets de type string
                */
                foreach (var ligne in lignes)
                {
                    var v = ligne.Split(";");

                    ajouter(v[0], Convert.ToDouble(v[1]), v[2]);
                }
            }

            currentFrame.Navigate(currentFrame.CurrentSourcePageType);
        }

        public static void ajouter(string nom, double prix, string categorie)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "insert into produits (nom, prix, categorie) values(@nom,@prix,@categorie)";
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prix", prix);
                commande.Parameters.AddWithValue("@categorie", categorie);

                conn.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }


        public static ObservableCollection<Activite> RechercherProduit(double petitPrix, double grandPrix, string categorie)
        {

            ObservableCollection<Activite> liste = new ObservableCollection<Activite>();
            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select * from produits where " + (grandPrix < 0 ? "" : " prix < " + grandPrix + " and ") + "prix > " + petitPrix +
                (categorie != "" ? " and categorie = '" + categorie + "'" : "");
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                   liste.Add(new Activite(Convert.ToInt32(r["id"]), r["nom"].ToString(), Convert.ToDouble(r["prix"]), r["categorie"].ToString()));
                }

                r.Close();
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return liste;

        }


        public static int NbProduit(int categorie)
        {

            int nb = 0;
            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select * from produits" + (categorie == 1 ? " where categorie = 'materiel de construction'" : categorie == 2 ? " where categorie = 'salle de bain'" :
                    categorie == 3 ? " where categorie = 'cuisine'" : categorie == 4 ? " where categorie = 'electromenagers'" : "");
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    nb++;
                }

                r.Close();
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return nb;

        }

        public static string ProduitPlusCher()
        {
            string st = "";
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select * from produits where prix = (Select MAX(prix) from produits)";
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    st = r["id"] + " - " + r["nom"].ToString() + " (" + r["prix"] + " $)";
                }


                r.Close();
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return st;
        }

        public static void supprimer(int id)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "delete from produits where id=@id";
                commande.Parameters.AddWithValue("@id", id);

                conn.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public static ObservableCollection<Activite> getListe()
        {
            ObservableCollection<Activite> liste = new ObservableCollection<Activite>();
            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select * from produits";
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    liste.Add(new Activite(Convert.ToInt32(r["id"]), r["nom"].ToString(), Convert.ToDouble(r["prix"]), r["categorie"].ToString()));
                }

                r.Close();
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return liste;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

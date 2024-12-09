using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
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

        public static void ajouterActivite(string nom, double coutOrganisation, double prixVente, string idCategorie)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "insert into activite (nom, cout_organisation, prix_vente, id_categorie) values(@nom,@cout,@prix,@categorie)";
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@cout", coutOrganisation);
                commande.Parameters.AddWithValue("@prix", prixVente);
                commande.Parameters.AddWithValue("@categorie", idCategorie);

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

        public static void modifierActivite(string nom, double coutOrganisation, double prixVente, string idCategorie)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "update activite SET cout_organisation=@cout, prix_vente=@prix, id_categorie=@categorie WHERE nom=@nom";
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@cout", coutOrganisation);
                commande.Parameters.AddWithValue("@prix", prixVente);
                commande.Parameters.AddWithValue("@categorie", idCategorie);

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

        public static void supprimerActivite(string nom)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "delete from activite where nom=@nom";
                commande.Parameters.AddWithValue("@nom", nom);

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


        public static void supprimerSeance(int id)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "delete from seance where id=@id";
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


        public static ObservableCollection<Activite> getListeActivite()
        {
            ObservableCollection<Activite> liste = new ObservableCollection<Activite>();
            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select * from activite";
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    liste.Add(new Activite(r["nom"].ToString(), Convert.ToDouble(r["cout_organisation"]), Convert.ToDouble(r["prix_vente"]), Convert.ToInt32(r["id_categorie"])));
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


        public static ObservableCollection<Categorie> getListeCategorie()
        {
            ObservableCollection<Categorie> liste = new ObservableCollection<Categorie>();
            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select * from categorie";
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    liste.Add(new Categorie(r["id"].ToString(), r["nom"].ToString()));
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


        public static ObservableCollection<Seance> getListeSeance(string nomActivite)
        {
            ObservableCollection<Seance> liste = new ObservableCollection<Seance>();
            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select * from seance where nom_activite ='" + nomActivite + "'";
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    DateTime date = Convert.ToDateTime(r["date"]);

                    TimeSpan timeSpan = TimeSpan.Parse(r["heure"].ToString());
                    TimeOnly heure = TimeOnly.FromTimeSpan(timeSpan);
                    int nb_place_disponible = Convert.ToInt32(r["nb_place_disponible"]);
                    string nom_activite = r["nom_activite"].ToString();
                    int id_categorie = Convert.ToInt32(r["id_categorie"]);

                    liste.Add(new Seance(date, heure, nb_place_disponible, nom_activite, id_categorie));
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

        public static ObservableCollection<Adherent> getListeAdherent()
        {
            ObservableCollection<Adherent> liste = new ObservableCollection<Adherent>();
            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select * from adherent";
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    liste.Add(new Adherent(r["id"].ToString(), r["nom"].ToString(), r["prenom"].ToString(), r["adresse"].ToString(), DateOnly.FromDateTime(Convert.ToDateTime(r["date_naissance"])), Convert.ToInt32(r["age"])));
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

        public static string InscriptionAdherantSeance(string idAdherent, int idSeance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("InscrireAdherentAUneSeance");
                commande.Connection = conn;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("p_id_seance", idSeance);
                commande.Parameters.AddWithValue("p_id_adherent", idAdherent);

                conn.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();

                conn.Close();

            }
            catch (Exception ex)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                return ex.Message;
            }
            return "";
        }

        public static float prendreNote(string idAdherent, int idSeance)
        {
            float result = -1;
            try
            {
                MySqlCommand commande = new MySqlCommand("NoteAdherentSeance");
                commande.Connection = conn;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("p_id_adherent", idAdherent);
                commande.Parameters.AddWithValue("p_id_seance", idSeance);
                commande.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "resultat",
                    MySqlDbType = MySqlDbType.Byte,
                    Direction = System.Data.ParameterDirection.ReturnValue
                });

                conn.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                result = Convert.ToInt32(commande.Parameters["resultat"].Value);

                conn.Close();

            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return result;
        }


        public static void AdherentNoteSeance(string idAdherent,double note, int idSeance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("AdherentNoteSeance");
                commande.Connection = conn;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("p_id_adherent", idAdherent);
                commande.Parameters.AddWithValue("p_note", note);
                commande.Parameters.AddWithValue("p_id_seance", idSeance);

                conn.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();

                conn.Close();

            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public static int TrouverIdSeance(string nomActivite, DateTime date)
        {
            int result = -1;
            try
            {
                MySqlCommand commande = new MySqlCommand("TrouverIdSeance");
                commande.Connection = conn;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("p_nom_activite", nomActivite);
                commande.Parameters.AddWithValue("p_date", date);
                commande.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "resultat",
                    MySqlDbType = MySqlDbType.Byte,
                    Direction = System.Data.ParameterDirection.ReturnValue
                });

                conn.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                result = Convert.ToInt32(commande.Parameters["resultat"].Value);

                conn.Close();

            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return result;
        }

        public static bool UtilisateurEstInscritSeance(string idAdherent, int idSeance)
        {
            bool estConnecte = false;
            try
            {
                MySqlCommand commande = new MySqlCommand("UtilisateurEstInscritSeance");
                commande.Connection = conn;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("p_id_adherent", idAdherent);
                commande.Parameters.AddWithValue("p_id_seance", idSeance);
                commande.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "resultat",
                    MySqlDbType = MySqlDbType.Byte,
                    Direction = System.Data.ParameterDirection.ReturnValue
                });

                conn.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                int resultat = Convert.ToInt32(commande.Parameters["resultat"].Value);
                estConnecte = (resultat >= 1);

                conn.Close();

            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return estConnecte;
        }

        public static bool connexionAdherant(string idEntree)
        {
            bool b = false;

            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select id from adherent where id = '" + idEntree + "'";
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();


                b = r.Read();

                if (b)
                {
                    RoleUtilisateur.UtilisateurConnecte = idEntree;
                }
                conn.Close();

            }

            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }


            return b;
        }


        public static bool connexionAdmin(string utili, string pass)
        {
            bool b = false;

            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select user, password from administrateur where user = '" + utili + "' && password = '" + pass + "'";
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();


                b = r.Read();

                if (b)
                {
                    RoleUtilisateur.Admin = true;
                }
                conn.Close();

            }

            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }


            return b;
        }

        
        public static ArrayList nbAdherentParActivite()
        {
            ArrayList tabActivite = new ArrayList();

            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select * from nb_participant_activite";
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    tabActivite.Add(r["nom_activite"] + ": " + r["nb_participant"] + " participants");
                    
                }

                r.Close();
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return tabActivite;
        }

        public static int nbTotalAdherent()
        {
            int nb = 0;

            try
            {
                MySqlCommand commande = new MySqlCommand("NbTotalAdherent");
                commande.Connection = conn;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "resultat",
                    MySqlDbType = MySqlDbType.Byte,
                    Direction = System.Data.ParameterDirection.ReturnValue
                });

                conn.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                nb = Convert.ToInt32(commande.Parameters["resultat"].Value);

                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return nb;
        }

        public static int nbTotalActivite()
        {
            int nb = 0;

            try
            {
                MySqlCommand commande = new MySqlCommand("NbTotalActivite");
                commande.Connection = conn;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "resultat",
                    MySqlDbType = MySqlDbType.Byte,
                    Direction = System.Data.ParameterDirection.ReturnValue
                });

                conn.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                nb = Convert.ToInt32(commande.Parameters["resultat"].Value);

                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return nb;
        }


        public static ArrayList moyenneNoteActivite()
        {
            ArrayList tabNote = new ArrayList();

            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "Select * FROM moyennenoteactivite";
                conn.Open();
                MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    tabNote.Add(r["nom"] + ": " + r["noteMoyenne"] + "/5");

                }

                r.Close();
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return tabNote;
        }

        public static string ajouterAdherent(string nom, string prenom, string adresse, DateOnly date_naissance)
        {
            string message = "";
            string date = date_naissance.ToString();

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = conn;
                commande.CommandText = "INSERT INTO adherent (nom, prenom, adresse, date_naissance) VALUES ('" + nom + "', '" + prenom + "', '" + adresse + "', '" + date + "');";
                conn.Open();
                commande.ExecuteReader();
                conn.Close();
                return "Adherent ajouté avec succès";
            }

            catch (Exception e)
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                return e.Message;
            }
            return message;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

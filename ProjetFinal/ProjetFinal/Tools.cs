using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFinal
{
    internal static class Tools
    {
        #region Obsolete

        /// <summary>
        /// Cours appris : CSV
        /// Fonctionne uniquement dans une Window (Pas dans une Page)
        /// </summary>
        /// <param name="TexteAEnregistrer">Texte à enregistrer</param>
        /// <param name="nomFichier">Le nom du fichier à enregistrer sans extension.</param>
        /// <param name="extensionFichier">Extension du fichier à enregistrer.</param>
        /// <param name="target">Mettre this</param>
        public async static void EnregistrerString_VersTXT(string TexteAEnregistrer, string nomFichier, string extensionFichier, object target)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(target);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = nomFichier;

            switch (extensionFichier)
            {
                case ".txt":
                    picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".txt" });
                    break;
                default:
                    picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".txt" });
                    break;
            }

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            //écrit dans le fichier
            if (monFichier != null)
                await Windows.Storage.FileIO.WriteTextAsync(monFichier, TexteAEnregistrer);
        }


        /// <summary>
        /// Cours appris : CSV
        /// Fonctionne uniquement dans une Window (Pas dans une Page)
        /// </summary>
        /// <param name="TexteAEnregistrer">Liste à enregistrer</param>
        /// <param name="nomFichier">Le nom du fichier à enregistrer sans extension.</param>
        /// <param name="extensionFichier">Extension du fichier à enregistrer.</param>
        /// <param name="target">Mettre this</param>
        public async static void EnregistrerListe_VersTXT(List<string> TexteAEnregistrer, string nomFichier, string extensionFichier, object target)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(target);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = nomFichier;

            switch (extensionFichier)
            {
                case ".txt":
                    picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".txt" });
                    break;
                default:
                    picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".txt" });
                    break;
            }

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            //écrit dans le fichier
            if (monFichier != null)
            {
                //écrit dans le fichier chacune des lignes du tableau
                //une boucle sera faite sur la collection et prendra chacun des objets de celle-ci
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, TexteAEnregistrer.ConvertAll(x => x.ToString()));
            }
        }

        #endregion

        /// <summary>
        /// Cours appris : CSV
        /// Fonctionne uniquement dans une Window (Pas dans une Page)
        /// </summary>
        /// <param name="TexteAEnregistrer">Texte à enregistrer</param>
        /// <param name="nomFichier">Le nom du fichier à enregistrer sans extension.</param>
        /// <param name="extensionFichier">Extension du fichier à enregistrer.</param>
        /// <param name="target">Mettre this</param>
        public async static void EnregistrerString_VersCSV(string TexteAEnregistrer, string nomFichier, string extensionFichier, object target)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(target);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = nomFichier;

            switch (extensionFichier)
            {
                case ".txt":
                    picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".txt" });
                    break;
                case ".csv":
                    picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });
                    break;
                default:
                    picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".txt" });
                    break;
            }

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            //écrit dans le fichier
            if (monFichier != null)
                await Windows.Storage.FileIO.WriteTextAsync(monFichier, TexteAEnregistrer, Windows.Storage.Streams.UnicodeEncoding.Utf8);

        }


        /// <summary>
        /// Cours appris : CSV
        /// Fonctionne uniquement dans une Window (Pas dans une Page)
        /// </summary>
        /// <param name="TexteAEnregistrer">Liste à enregistrer</param>
        /// <param name="nomFichier">Le nom du fichier à enregistrer sans extension.</param>
        /// <param name="extensionFichier">Extension du fichier à enregistrer.</param>
        /// <param name="target">Mettre this</param>
        public async static void EnregistrerListe_VersCSV(List<string> TexteAEnregistrer, string nomFichier, string extensionFichier, object target)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(target);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = nomFichier;

            switch (extensionFichier)
            {
                case ".txt":
                    picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".txt" });
                    break;
                case ".csv":
                    picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });
                    break;
                default:
                    picker.FileTypeChoices.Add("Fichier texte", new List<string>() { ".txt" });
                    break;
            }

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            //écrit dans le fichier
            if (monFichier != null)
            {
                //écrit dans le fichier chacune des lignes du tableau
                //une boucle sera faite sur la collection et prendra chacun des objets de celle-ci
                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, TexteAEnregistrer.ConvertAll(x => x.ToString()), Windows.Storage.Streams.UnicodeEncoding.Utf8);
            }
        }


        /// <summary>
        /// Cours appris : CSV
        /// Fonctionne uniquement vers une Window (Pas dans une Page)
        /// </summary>
        /// <param name="EndroitOuMettreLeTexte">Nom du TextBlock.</param>
        /// <param name="extensionFichier">Extension du fichier à ouvrir.</param>
        /// <param name="target">Mettre this</param>
        public async static void OuvrirTXT_VersString(TextBlock EndroitOuMettreLeTexte, string extensionFichier, object target)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(extensionFichier);

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(target);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            //sélectionne le fichier à lire
            Windows.Storage.StorageFile monFichier = await picker.PickSingleFileAsync();

            //ouvre le fichier et lit le contenu
            if (monFichier != null)
                EndroitOuMettreLeTexte.Text = await Windows.Storage.FileIO.ReadTextAsync(monFichier);
        }


        /// <summary>
        /// Cours appris : CSV
        /// Fonctionne uniquement vers une Window (Pas dans une Page)
        /// </summary>
        /// <param name="EndroitOuMettreLeTexte">Nom du ListView.</param>
        /// <param name="extensionFichier">Extension du fichier à ouvrir.</param>
        /// <param name="target">Mettre this</param>
        public async static void OuvrirTXT_VersList(ListView EndroitOuMettreLeTexte, string extensionFichier, object target)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(extensionFichier);

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(target);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            //sélectionne le fichier à lire
            Windows.Storage.StorageFile monFichier = await picker.PickSingleFileAsync();

            //ouvre le fichier et lit le contenu
            if (monFichier != null)
            {
                var lignes = await Windows.Storage.FileIO.ReadLinesAsync(monFichier);

                EndroitOuMettreLeTexte.ItemsSource = lignes;
            }
        }

        /// <summary>
        /// Cours appris : CSV
        /// Fonctionne uniquement vers une Window (Pas dans une Page)
        /// </summary>
        /// <param name="EndroitOuMettreLeTexte">Nom du ListView.</param>
        /// <param name="extensionFichier">Extension du fichier à ouvrir.</param>
        /// <param name="target">Mettre this</param>
        public async static void OuvrirCSV_VersTableauString(string[,] EndroitOuMettreLeTexte, string extensionFichier, object target)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(extensionFichier);

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(target);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            //sélectionne le fichier à lire
            Windows.Storage.StorageFile monFichier = await picker.PickSingleFileAsync();

            //ouvre le fichier et lit le contenu
            if (monFichier != null)
            {
                var lignes = await Windows.Storage.FileIO.ReadLinesAsync(monFichier);

                int nbTemp = 0;
                string[,] tabTemp = new string[lignes[0].Split(";").Length, lignes.Count];

                /*boucle permettant de lire chacune des lignes du fichier
                * et de remplir un tableau d'objets de type string
                */
                foreach (var ligne in lignes)
                {
                    var v = ligne.Split(";");

                    tabTemp[nbTemp++, 0] = v[0];
                    tabTemp[nbTemp++, 0] = v[1];
                    tabTemp[nbTemp++, 0] = v[2];
                }
                EndroitOuMettreLeTexte = tabTemp;
            }
        }


        #region PasToujoursBon

        /// <summary>
        /// Cours appris : CSV
        /// Fonctionne uniquement vers une Window (Pas dans une Page)
        /// </summary>
        /// <param name="EndroitOuMettreLeTexte">Nom du ListView.</param>
        /// <param name="extensionFichier">Extension du fichier à ouvrir.</param>
        /// <param name="target">Mettre this</param>
        public async static void OuvrirCSV_VersList(ListView EndroitOuMettreLeTexte, string extensionFichier, object target)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(extensionFichier);

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(target);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            //sélectionne le fichier à lire
            Windows.Storage.StorageFile monFichier = await picker.PickSingleFileAsync();

            //ouvre le fichier et lit le contenu
            if (monFichier != null)
            {
                var lignes = await Windows.Storage.FileIO.ReadLinesAsync(monFichier);

                List<Produit> liste = new List<Produit>();   // Il faut changer Client En fonction de la classe voulu dans la liste. 

                /*boucle permettant de lire chacune des lignes du fichier
                * et de remplir une liste d'objets de type Produit
                */
                foreach (var ligne in lignes)
                {
                    var v = ligne.Split(";");
                    liste.Add(new Produit(v[0], Convert.ToDouble(v[1]), v[2]));  // Il faut changer Client En fonction de la classe voulu dans la liste. 
                }

                EndroitOuMettreLeTexte.ItemsSource = liste;
            }
        }

        #endregion

    }
}

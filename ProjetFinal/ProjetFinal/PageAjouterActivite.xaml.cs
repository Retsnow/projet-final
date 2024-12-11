using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAjouterActivite : Page
    {

        NavigationViewItem nv_activite;
        Activite activite;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            object[] objects = new object[2];

            if (e.Parameter is not null)
            {
                objects = (object[])e.Parameter;
                nv_activite = objects[0] as NavigationViewItem;
                if (objects.Count() == 2)
                    activite = objects[1] as Activite;
            }

            if (activite != null)
            {
                tbx_nom_activite.Text = activite.Nom;
                tbx_nom_activite.IsEnabled = false;
                tbx_cout_organisation.Text = activite.Cout_organisation.ToString();
                tbx_prix_vente.Text = activite.Prix_vente.ToString();

                foreach (var item in cbxCategorie.Items)
                    if ((item as Categorie).Id == activite.Id_categorie)
                        cbxCategorie.SelectedItem = item;

            }
        }

        public PageAjouterActivite()
        {
            this.InitializeComponent();

            cbxCategorie.ItemsSource = SingletonRequete.getListeCategorie();
        }

        private async void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            PermissionDialog dialog = new PermissionDialog();
            dialog.XamlRoot = stk_aj_activite.XamlRoot;
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;


            double prix_vente;
            double cout_organisation;

            bool validation_prix_vente = double.TryParse(tbx_prix_vente.Text, out prix_vente);
            bool validation_cout_organisation = double.TryParse(tbx_cout_organisation.Text, out cout_organisation);

            if (validation_cout_organisation && validation_prix_vente)
            {
                if (prix_vente - cout_organisation <= 0)
                {
                    dialog.Message = "Voulez-vous vraiment ajouter cette activité ?\nElle ne rapportera pas ou vous feras perdre de l'argent.";
                }
                else if (prix_vente < 0)
                {
                    dialog.Message = "Voulez-vous vraiment ajouter cette activité ?\nLe prix de vente est négatif.";
                }
                else if (cout_organisation < 0)
                {
                    dialog.Message = "Voulez-vous vraiment ajouter cette activité ?\nLe cout d'organisation est négatif.";
                }
                else
                {
                    dialog.Message = "Voulez-vous vraiment ajouter cette activité ?";
                }
            }
            else if (!validation_prix_vente && !validation_cout_organisation)
            {

                return;
            }
            else if (!validation_prix_vente)
            {
                dialog.Message = "Le prix de vente n'est pas un nombre";
                await dialog.ShowAsync();
                return;
            }
            else if (!validation_cout_organisation)
            {
                return;
            }



            if (activite != null)
            {
                dialog.Title = "Modifier Activité";
                dialog.PrimaryButtonText = "Modifier";
                ContentDialogResult resultat = await dialog.ShowAsync();
                if (dialog.Resultat)
                    SingletonRequete.modifierActivite(tbx_nom_activite.Text, Convert.ToDouble(tbx_cout_organisation.Text), Convert.ToDouble(tbx_prix_vente.Text), (cbxCategorie.SelectedItem as Categorie).Id.ToString());

            }

            else
            {
                dialog.Title = "Ajouter Activité";
                dialog.PrimaryButtonText = "Ajouter";
                ContentDialogResult resultat = await dialog.ShowAsync();
                if (dialog.Resultat)
                    SingletonRequete.ajouterActivite(tbx_nom_activite.Text, Convert.ToDouble(tbx_cout_organisation.Text), Convert.ToDouble(tbx_prix_vente.Text), (cbxCategorie.SelectedItem as Categorie).Id.ToString());
            }

            nv_activite.IsSelected = false;
            nv_activite.IsSelected = true;
        }

    }
}


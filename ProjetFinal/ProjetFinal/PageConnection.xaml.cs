using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
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
    public sealed partial class PageConnection : Page
    {
        


        public PageConnection()
        {
            this.InitializeComponent();
            

        }


        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {

            if (SingletonRequete.connexionAdherant(tbx_id_adherent.Text))
            {
                txt_validation.Text = "Connexion en tant qu'adhérent réussie";
                txt_validation.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Green);
                
               
            }
            else
            {
                txt_validation.Text = "Échec de la connexion en tant qu'adhérent";
                txt_validation.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
            }

            txt_validation.Visibility = Visibility.Visible;
        }

        private async void btn_admin_Click(object sender, RoutedEventArgs e)
        {
            ConnexionAdminDialog dialog = new ConnexionAdminDialog();
            dialog.XamlRoot = stck_conn.XamlRoot;
            dialog.Title = "Connexion Admnisatrateur";
            dialog.CloseButtonText = "Annuler";
            dialog.PrimaryButtonText = "Se connecter";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();


           

                if (dialog.Administrateur)
                {
                    txt_validation.Text = "Connexion en tant qu'administrateur réussie";
                    txt_validation.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Green);


                }
                else
                {
                    txt_validation.Text = "Échec de la connexion en tant qu'administrateur";
                    txt_validation.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Red);
                }

                txt_validation.Visibility = Visibility.Visible;
           

        }

    }
}

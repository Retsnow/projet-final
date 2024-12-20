using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Org.BouncyCastle.Asn1.X509;
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
    public sealed partial class PageDeconnexion : Page
    {
        NavigationViewItem nv_activite;

        public PageDeconnexion()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                nv_activite = (NavigationViewItem)e.Parameter;
            }

        }
        private void btn_deconn_Click(object sender, RoutedEventArgs e)
        {
            if (RoleUtilisateur.UtilisateurConnecte != null)
            {
                RoleUtilisateur.UtilisateurConnecte = null;
            }
            else
            {
                RoleUtilisateur.Admin = false;
            }

            txt_validation.Text = "D�connexion r�ussie";
            txt_validation.Foreground = new SolidColorBrush(Microsoft.UI.Colors.Green);
            txt_validation.Visibility = Visibility.Visible;
            nv_activite.IsSelected = true;
        }
    }
}

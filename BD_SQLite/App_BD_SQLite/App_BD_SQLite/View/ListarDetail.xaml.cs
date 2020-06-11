using App_BD_SQLite.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_BD_SQLite.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_BD_SQLite.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListarDetail : ContentPage
    {
        public ListarDetail()
        {
            InitializeComponent();
            AtualizaLista();
        }
        public void AtualizaLista()
        {
            ServicesBDNota dbNotas = new ServicesBDNota(App.DbPath);
            ListasNotas.ItemsSource = dbNotas.Listar();
        }

        private void ListaNotas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ModelNota nota = (ModelNota)ListasNotas.SelectedItem;
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new CadastrarDetail(nota));
        }

        private void SwFavorito_Toggled(object sender, ToggledEventArgs e)
        {
            ServicesBDNota dbNotas = new ServicesBDNota(App.DbPath);

            if (swFavorito.IsToggled)
            {
                ListasNotas.ItemsSource = dbNotas.ListarFavoritos();
            }
            else
            {
                AtualizaLista();
            }
        }

        private void BtLocalizar_Clicked(object sender, EventArgs e)
        {
            String titulo = "";
            if (txtNota.Text != null) titulo = txtNota.Text;
            ServicesBDNota dbNotas = new ServicesBDNota(App.DbPath);

            ListasNotas.ItemsSource = dbNotas.Localizar(titulo);
            txtNota.Text = "";
        }

        private void BtTodos_Clicked(object sender, EventArgs e)
        {
            AtualizaLista();
        }
    }
}
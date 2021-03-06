﻿using App_BD_SQLite.Model;
using App_BD_SQLite.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_BD_SQLite.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastrarDetail : ContentPage
    {
        int id;
       
        public CadastrarDetail()
        {
            InitializeComponent();
        }
        
        
        public CadastrarDetail(ModelNota nota)
        {
            InitializeComponent();

            btSalvar.Text = "Alterar";
            btExcluir.IsVisible = true;

            id = nota.Id;
            txtTitulo.Text = nota.Titulo;
            txtDados.Text = nota.Dados;
            swFavorito.IsToggled = nota.Favorito;
        }

        private void BtCancelar_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new HomeDetail());
        }

        private void BtSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                ModelNota nota = new ModelNota();
                nota.Titulo = txtTitulo.Text;
                nota.Dados = txtDados.Text;
                nota.Favorito = swFavorito.IsToggled;
                ServicesBDNota dbNotas = new ServicesBDNota(App.DbPath);
                if (btSalvar.Text == "Inserir")
                {
                    dbNotas.Inserir(nota);
                    DisplayAlert("Resultado da operação", dbNotas.StatusMessage, "OK");
                }
                else
                {
                    nota.Id = id;
                    dbNotas.Alterar(nota);
                    DisplayAlert("Resultado da operação", dbNotas.StatusMessage, "OK");

                }

                Voltar();
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "OK");
            }
        }
        public void Voltar()
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new HomeDetail());
        }

        private async void btExcluir_Clicked(object sender, EventArgs e)
        {

            bool resp = await DisplayAlert("Excluir Registro",
                "Deseja excluir a nota atual?",
                "Sim", "Não");

            if (resp)
            {
                ServicesBDNota dbNotas = new ServicesBDNota(App.DbPath);
                dbNotas.Excluir(id);
                await DisplayAlert("Resultado da operação", dbNotas.StatusMessage, "OK");
            }
        }

    }
}

using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MIUCSHA
{
    public partial class SelAsignaturas : ContentPage
    {
        public List<string> Asignaturas;
        private string nmat = "0";
        private string anyo = "2020";
        private string sem = "1";
        private List<CargaClass> carga;
        private PeriodosClass periodo;
        private string Aurl = "127.0.0.1";
        public SelAsignaturas(string Durl, List<CargaClass> cargaAca, PeriodosClass period, string numat)
        {
            Aurl = Durl;
            InitializeComponent();
            Asignaturas = new List<string>();
            anyo = period.anyo;
            sem = period.sem;
            nmat = numat;
            carga = new List<CargaClass>();
            periodo = period;

            for (int ty = 0; ty < cargaAca.Count; ty++)
            {
                string contenido = cargaAca[ty].desc_asig;
               if ( cargaAca[ty].cod_func == "01")
                {
                    carga.Add(cargaAca[ty]);
                 Asignaturas.Add(contenido+" Seccion 2");
               }
            }

            picker.ItemsSource = Asignaturas;
            Sela.ItemsSource = carga;
            
        }
        protected override void OnAppearing()
        {
            string an = periodo.anyo;
            string se = periodo.sem;
            string tit = "NOTAS (" + an + " - " + se + ")";
            titulo.Text = tit;
            base.OnAppearing();
        }

        async void picker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            if (picker.SelectedItem != null)
            {
                String bus = picker.SelectedItem.ToString();
                await Navigation.PushModalAsync(new CalificacionPage(Aurl, bus, carga, periodo, nmat));
            }
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {

            await Navigation.PopModalAsync();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (picker.SelectedItem != null)
            {
                string bus = picker.SelectedItem.ToString();
             //   await Navigation.PushModalAsync(new DestinosPage(Aurl));
             //   await Navigation.PushModalAsync(new CalificacionPage( Aurl, bus, carga, periodo, nmat));
            }
        }

        async void Sela_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            CargaClass bus = (CargaClass) Sela.SelectedItem;
            await Navigation.PushModalAsync(new CalificacionPage(Aurl, bus.desc_asig, carga, periodo, nmat));
        }
    }
}

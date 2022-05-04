using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MIUCSHA
{
    public partial class CaliMeuPage : ContentPage
    {
        private List<cursosClass> dcursos;
        private PeriodosClass period;
        private string Aurl;
        private string minombre;
        private string nmat;
        private string Run;
        public CaliMeuPage(List<cursosClass> cursos, string run, string Nmat, string Minombre, string rurl, PeriodosClass peri)
        {
            dcursos = cursos;
            Run = run;
            nmat = Nmat;
            minombre = Minombre;
            Aurl = rurl;
            period = peri;
            InitializeComponent();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NotaFinalPage(dcursos, Run, nmat, minombre, Aurl, period));
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NotaParcialPage(dcursos, Run, nmat, minombre, Aurl, period));
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}

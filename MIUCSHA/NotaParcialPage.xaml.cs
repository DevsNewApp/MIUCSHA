using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MIUCSHA
{
    public partial class NotaParcialPage : ContentPage
    {
        private List<NotasParcialesClass> Notas;
        private List<cursosClass> dcursos;
        private string Aurl;
        private PeriodosClass periodo;
        private int rate = 0;
        private string rut;
        private string materia;
        private string nombre;
        private string notNum = "0";
        private string codMat = "";
        private readonly HttpClient client = new HttpClient();
        public NotaParcialPage(List<cursosClass> cursos, string Run, string nmat, string minombre, string rurl, PeriodosClass peri)
        {
            dcursos = cursos;
            rate = 0;
            periodo = peri;
            Aurl = rurl;
            rut = Run;
            materia = nmat;
            nombre = minombre;
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {

            var monkeyList = new List<string>();
            for (int r = 0; r < dcursos.Count; r++)
            {
                string alfa = ReplaceAt(dcursos[r].codigo, 6, '-');
                monkeyList.Add(alfa);
            }

            Xpicker.Title = monkeyList[rate];
            Xpicker.ItemsSource = monkeyList;
            Mtitulo.Text = dcursos[rate].asig_desc;
            Periodod.Text = periodo.anyo + "-" + periodo.sem;

            string any = periodo.anyo;

            string vep = periodo.sem;
            string asi = dcursos[rate].hora_asig;
            string sec = dcursos[rate].hora_secc;
            codMat = asi;
            notNum = "1";
            string URL = "http://" + Aurl + ":8020/aca/getNotasPar?anyo=" + any + "&vepe=" + vep +
                       "&asig=" + asi + "&secc=" + sec + "&version=" + dcursos[rate].hora_vers +
                       "&func=" + dcursos[rate].hora_func + "&nrgr=" + dcursos[rate].hora_nrgr + "&numnota=1" + "&numcontrol=0" + "&tipocredito=" + dcursos[rate].carr_tpro;
            //  client.Timeout = TimeSpan.FromSeconds(120);
            string content2 = await client.GetStringAsync(URL);
            Notas = JsonConvert.DeserializeObject<List<NotasParcialesClass>>(content2);
         
            Asistencias.ItemsSource = Notas;
        }
        private async void actualizaList(string codigo, string numero)
        {
            int rate = 0;
            for (int u=0; u< dcursos.Count; u++)
            {
                if (dcursos[u].hora_asig.Equals(codigo.Substring(0,6))) rate = u;
            }
            string any = periodo.anyo;
            
            string vep = periodo.sem;
            string asi = dcursos[rate].hora_asig;
            string sec = dcursos[rate].hora_secc;
            codMat = asi;
            notNum = numero;
            string URL = "http://" + Aurl + ":8020/aca/getNotasPar?anyo=" + any + "&vepe=" + vep +
                      "&asig=" + asi + "&secc=" + sec + "&version=" + dcursos[rate].hora_vers +
                      "&func=" + dcursos[rate].hora_func + "&nrgr=" + dcursos[rate].hora_nrgr + "&numnota="+numero + "&numcontrol=0" + "&tipocredito=" + dcursos[rate].carr_tpro;
            //  client.Timeout = TimeSpan.FromSeconds(120);
            string content2 = await client.GetStringAsync(URL);
            Notas = JsonConvert.DeserializeObject<List<NotasParcialesClass>>(content2);

            Asistencias.ItemsSource = Notas;

        }
        private static string ReplaceAt( string value, int index, char newchar)
        {
            if (value == null)
            {
                throw new ArgumentNullException("input");
            }
            char[] chars = value.ToCharArray();
            chars[index] = newchar;
            return new string(chars);
        }
        async void Asistencias_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            NotasParcialesClass selec = (NotasParcialesClass)Asistencias.SelectedItem;
            string ruts = selec.rut;
            string nmats = selec.nmat;


            await Navigation.PushModalAsync(new DetalleAlumno(Aurl, ruts, nmats));
        }

        async void CancelButtonClicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void Xpicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            string valoro = (string)Xpicker.SelectedItem;
            codMat = valoro;
            actualizaList(codMat, notNum);
        }

        void Xpicker_SelectedIndexChanged2(System.Object sender, System.EventArgs e)
        {
            string valoro = (string)Xpicker2.SelectedItem;
            notNum = valoro;
            actualizaList(codMat, notNum);
        }
    }
}

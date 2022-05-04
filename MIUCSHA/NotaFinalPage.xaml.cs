using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MIUCSHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotaFinalPage : ContentPage
    {
        private List<NotaFinalClass> Notas;
        private List<cursosClass> dcursos;
        private string Aurl;
        private PeriodosClass periodo;
        private int rate = 0;
        private string rut;
        private string materia;
        private string nombre;
        private readonly HttpClient client = new HttpClient();
        public NotaFinalPage(List<cursosClass> cursos, string Run, string nmat, string minombre, string rurl, PeriodosClass peri)
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
            try
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
            string URL = "http://" + Aurl + ":8020/aca/notasFinales?anyo=" + any + "&vepe=" + vep + "&asig=" + asi + "&secc=" + sec;
          //  client.Timeout = TimeSpan.FromSeconds(120);
           
               
                string content2 = await client.GetStringAsync(URL);

                Notas = JsonConvert.DeserializeObject<List<NotaFinalClass>>(content2);

                Asistencias.ItemsSource = Notas;
            } catch (Exception p)
            {
                Msg("error", p.ToString());
            }
        }
        async void updData(string codigo)
        {
           
            for (int kr=0; kr< dcursos.Count;kr++)
            {
                if (dcursos[kr].codigo == codigo) rate = kr;
            }
            string any = periodo.anyo;
            // any = "2019";
            string vep = periodo.sem;
            string asi = dcursos[rate].hora_asig;
            string sec = dcursos[rate].hora_secc;
            string URL = "http://" + Aurl + ":8020/aca/notasFinales?anyo=" + any + "&vepe=" + vep + "&asig=" + asi + "&secc=" + sec;
           // client.Timeout = TimeSpan.FromSeconds(120);
           try { 
            string content2 = await client.GetStringAsync(URL);

            Notas = JsonConvert.DeserializeObject<List<NotaFinalClass>>(content2);
            Asistencias.ItemsSource = Notas;
            }
            catch (Exception p)
            {
                Msg("error 2", p.ToString());
            }
        }
        private static string ReplaceAt(string value, int index, char newchar)
        {
            if (value == null)
            {
                throw new ArgumentNullException("input");
            }
            char[] chars = value.ToCharArray();
            chars[index] = newchar;
            return new string(chars);
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async private void Asistencias_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            NotaFinalClass selec = (NotaFinalClass)Asistencias.SelectedItem;
            string ruts = selec.alum_rut;
            string nmats = selec.nmat;


            await Navigation.PushModalAsync(new DetalleAlumno(Aurl, ruts, nmats));
        }
        private void Msg(string asunto, string mensaje)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                string titulo = asunto;
                string cuerpo = mensaje;
                await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
                // await this.DisplayAlert("Notificacion", "Rut o password incorrectos", "OK");
            });
        }

        void Xpicker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            string cambio = (string)Xpicker.SelectedItem;
            updData(cambio);
        }
    }
}
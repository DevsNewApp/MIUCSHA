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
    public partial class DetalleAlumno : ContentPage
    {
        private string Aurl;
        private string Run;
        private string Nmat;
        private readonly HttpClient client = new HttpClient();
        public DetalleAlumno(string Url, string Rut, string Nmats)
        {
            Aurl = Url;
            Run = Rut;
            Nmat = Nmats;
          
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {

          
            

            //  Device.BeginInvokeOnMainThread(async () => {
            //     string titulo = "Error Url " ;
            //    string cuerpo = "Este es el url que enviamos " + Url;

            //      await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
            //     await DisplayAlert("Error", "URL= " + Aurl + "  -Se presento un error que no ha podido controlar = " + e.ToString(), "OK");
            //  });

            string Url = "http://" + Aurl + ":8020/aca/datosa";
            Url = Url + "?rut=" + Run + "&nmat=" + Nmat;

            string content = await client.GetStringAsync(Url);

            List<EstudianteClass> datosA = JsonConvert.DeserializeObject<List<EstudianteClass>>(content);
           

           
            RutA.Text = datosA[0].alum_rut;
            XNmat.Text =  Nmat;
            NombreA.Text = datosA[0].nombre;
            CarreraA.Text = datosA[0].carr_desc;
            EmailA.Text = datosA[0].mail_mail;
            FotoA.Source = datosA[0].foto;
            base.OnAppearing();
        }

            async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}
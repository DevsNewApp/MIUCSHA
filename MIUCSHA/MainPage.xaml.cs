using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;


namespace MIUCSHA
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string Aurl = "app.ucsh.cl";  // "192.168.1.129";  "app.ucsh.cl"
        private string Url = "http://app.ucsh.cl:8020/aca/valida";
       

        private readonly HttpClient client = new HttpClient();
       
       
        public MainPage()
        {                           
            Url = Aurl;
            InitializeComponent();
           
        }
        /*
        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText.Text = "Sorry, this device is not supported";
                   // Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available.";
                return true;
            }
        }
     */
        async void OkButtonClicked(object sender, EventArgs e)
        {
            string usua = usuario.Text;
            string pass = password.Text;
            if (usua.ToCharArray().All(Char.IsLetter) || usua.Length > 8)
            {
                string titulo = "Error Login";
                string cuerpo = "El usuario es la parte numerica de su Rut sin el numero digito";
                await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));

            }
            else
            {
                // string Test = "http://" + Url + ":8020/aca/valida?rut=" + usua + "&password=" + pass;
                string Test = "http://" + Url + ":8020/aca/datos?rut=" + usua ;
                try
                {
                    string content = await client.GetStringAsync(Test);
                    List<datosDocentesClass> datos = JsonConvert.DeserializeObject<List<datosDocentesClass>>(content);
                    string Url2 = "http://" + Aurl + ":8020/periodo";
                    String periodos = await client.GetStringAsync(Url2);
                    PeriodosClass period = JsonConvert.DeserializeObject<PeriodosClass>(periodos);

                    
                    string URL = "http://" + Aurl + ":8020/aca/cursos?anyo=" + period.anyo + "&vepe=" + period.sem + "&rut=" + usua;
                    //  client.Timeout = TimeSpan.FromSeconds(120);
                    string content2 = await client.GetStringAsync(URL);

                    List<cursosClass> dcursos = JsonConvert.DeserializeObject<List<cursosClass>>(content2);
                    if (dcursos.Count() < 1)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            string titulo = "Error Carga Academica";
                            string cuerpo = "Usted no tiene carga academica para este periodo con el rut: " + usua;
                            await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
                            // await this.DisplayAlert("Notificacion", "Rut o password incorrectos", "OK");
                        });

                    }
                    else
                    {
                        if (datos.Count() < 1)
                        {

                            Device.BeginInvokeOnMainThread(async () =>
                             {
                                 string titulo = "Error Login";
                                 string cuerpo = "Usted no tiene acceso a este sistema con el rut: " + usua;
                                 await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
                             // await this.DisplayAlert("Notificacion", "Rut o password incorrectos", "OK");
                         });
                        }
                        else
                        {
                            Page p = new MenuPage(Aurl, usua);

                            await Navigation.PushModalAsync(p);
                        }
                    }
                }
                catch (Exception t)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        string titulo = "Conexion";
                        string cuerpo = "El servidor no responde posible problema de conexion";
                        cuerpo = t.ToString();
                        await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
                        //  await this.DisplayAlert("Conexión", "En este momento tienes problemas de conectividad, intentalo mas tarde." + t.ToString(), "OK");
                    });

                }
            }
        }

        void CancelButtonClicked(object sender, EventArgs e)
        {
            
        }
    }
}

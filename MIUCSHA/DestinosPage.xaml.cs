using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MIUCSHA
{
    public partial class DestinosPage : ContentPage
    {
        private string destinoProperty;
        private string Aurl = "127.0.0.1";
        private string mnmat = "";
        private readonly HttpClient client = new HttpClient();
        private List<GruposClass> desty;
        private async void Inicializa(string grup, string mat)
        {
            string rUrl = "http://" + Aurl + ":8020/aca/listaUsers";
            string content = await client.GetStringAsync(rUrl);
            List<UsuarioClass> usu = JsonConvert.DeserializeObject<List<UsuarioClass>>(content);
            desty = new List<GruposClass>();
         
            for (int rt = 0; rt < usu.Count; rt++)
            {
                if (mat != usu[rt].login)
                {
                    desty.Add(new GruposClass
                    {
                        grupo = grup,
                        nombre = usu[rt].nombre,
                        nmat = usu[rt].login
                    });
                   
                }
            }
          
            

        }

        public string remplazaDestino(string mat)
        {
            string valor = mat;
            for (int x = 0; x < desty.Count; x++)
            {
                if (desty[x].nombre == mat) valor = desty[x].nmat;
            }
            return valor;
        }
        public DestinosPage(string Durl, string aquien,string  dequien, List<GruposClass> des)
        {
            
            Aurl = Durl;
               desty = des;
               mnmat = dequien;
          //  Inicializa("General", "0");
            if (aquien == null) aquien = "OK Nada";
            destinoProperty = aquien;
           
            InitializeComponent();
            BindingContext = this;
        }
        protected override  void OnAppearing()
        {
            Etiq.Text = destinoProperty;

        }
        async void ButtonClicked(object sender, EventArgs e)
        {
            HttpClient client;
            string titulo = "";
            string cuerpo = "";
            client = new HttpClient();
            //  var json = new JavaScriptSerializer().Serialize(obj);
            string sub = txtSubject.Text;
            string body = txtBody.Text;
            int io = 1;
            if (sub.Length < 2)
            {
                titulo = "Atencion";
                cuerpo = "El Asunto no debe estar en blanco o ser de un caracter";

                io = 0;
                await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
            }
            if (body.Length < 2)
            {
                titulo = "Atencion";
                cuerpo = "El Asunto no debe estar en blanco o tener menos de dos caracteres";
                io = 0;
                await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
            }
       
            if (io == 1)
            {
                Msg item = new Msg();
                item.destinatario = remplazaDestino(destinoProperty);
                item.recepcion = "N";
                item.remitente = mnmat;
                item.asunto = sub;
                item.cuerpo = body;
                item.create_date = DateTime.Now;
                string url = "http://" + Aurl + ":8020/api/msgs";
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // var content = new FormUrlEncodedContent(json);
                //  content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var respuesta = responseString;
                // await DisplayAlert("Atencion", destinoProperty, "OK");
                titulo = "Atencion";
                cuerpo = "La notificacion ha sido enviada a " + destinoProperty;
                await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
                //  await DisplayAlert("Atencion", "La notificacion ha sido enviada a "+ destinoProperty, "OK");
                await Navigation.PopModalAsync();
            }
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}

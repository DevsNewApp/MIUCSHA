using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Newtonsoft.Json;
using System.Text;
using Rg.Plugins.Popup.Services;

namespace MIUCSHA
{
    public partial class Notifica : ContentPage
    {
        private string Url = "http://localhost:8020/api/msgs";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<TMsg> _msg;
        public IList<TMsg> Msgs { get; private set; }
        private List<string> carga;
        private List<Msg> msgs;
        private List<UsuariosClass> users;
        private string mnmat = "";
        private string Aurl = "127.0.0.1";
        // private string _someImage = "Mensajeo.jpg";
        public bool imagen1 = false;
        private List<GruposClass> desty;
        private string minombre;
        private async void Inicializa(string grup, string mat)
        {
            string rUrl = "http://" + Aurl + ":8020/aca/listaUsers";
            string content = await client.GetStringAsync(rUrl);
            List<UsuarioClass> usu = JsonConvert.DeserializeObject<List<UsuarioClass>>(content);
            desty = new List<GruposClass>();
            desty.Add(new GruposClass
            {
                grupo = grup,
                nombre = minombre,
                nmat = mat
            });
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
                    carga.Add(usu[rt].nombre);
                }
            }
        }
        public string remplazaOrigen(string mat)
        {
            string valor = mat;
            for (int x =0; x < desty.Count; x++)
            {
                if (desty[x].nmat == mat) valor = desty[x].nombre;
            }
            return valor;
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
        private List<GruposClass> cargaUsuarios()
        {

            List<GruposClass> desty = new List<GruposClass>();
            try
            {

                String url = "http://" + Aurl + ":8020/api/getUsers";
                string content = System.Threading.Tasks.Task.Run(async () => await client.GetStringAsync(url)).Result;
                users = JsonConvert.DeserializeObject<List<UsuariosClass>>(content);

                for (int r = 0; r < users.Count; r++)
                {
                    //  notifica("ATENCION", users[r].nombre);
                    desty.Add(new GruposClass
                    {
                        grupo = users[r].tipo,
                        nmat = users[r].login,
                        nombre = users[r].nombre
                    });

                }
            }
            catch (Exception r) { notifica("Error", r.ToString()); }
            return desty;
        }
        public Notifica(string Durl, string nmat, List <cursosClass> cargaAca,string nomb)
        {   
            Aurl = Durl;
            minombre = nomb;
            desty = new List<GruposClass>();
            desty = cargaUsuarios();
            carga = new List<string>();
            for (int x = 0; x < desty.Count; x++)
                carga.Add(desty[x].nombre);
            for (int d = 0; d < cargaAca.Count; d++)
            {
                carga.Add(cargaAca[d].asig_nomc);
            }

            //  titulo.Text = "Notificacion (Sin leer)";
           // Inicializa("General", nmat);
          
        
           

            // Imag.Source = "Mensaje.jpg";
            imagen1 = false;
           
            mnmat = nmat;
            Url = "http://" + Aurl + ":8020/api/msg?destino="+mnmat;
            InitializeComponent();

            Msgs = new List<TMsg>();
            Msgs.Add(new TMsg
            {
                destinatario = "Baboon",
                asunto = "Esta es una prueba",
                imagen = "Mensaje.jpg",
             });
            BindingContext = this;
        }
        async void actualiza()
        {
            titulo.Text = "Notificaciones (Sin leer)";
            string content = await client.GetStringAsync(Url);
            msgs = JsonConvert.DeserializeObject<List<Msg>>(content);
            List<TMsg> nlmsgs = new List<TMsg>();
            for (var r = 0; r < msgs.Count; r++)
            {
                if (msgs[r].recepcion.Equals("N"))
                {
                    msgs[r].remitente = remplazaOrigen(msgs[r].remitente);
                    nlmsgs.Add(new TMsg
                    {
                        _id = msgs[r]._id,
                        asunto = msgs[r].asunto,
                        create_date = msgs[r].create_date,
                        cuerpo = msgs[r].cuerpo,
                        destinatario = msgs[r].remitente,
                        recepcion = msgs[r].recepcion,
                        remitente = msgs[r].remitente,
                        imagen = "Mensaje.jpg"
                    });
                }
            }
            _msg = new ObservableCollection<TMsg>(nlmsgs);
            Noticas.ItemsSource = _msg;
            picker.ItemsSource = carga;
        }

        protected override async void OnAppearing()
        {
            titulo.Text = "Notificaciones (Sin leer)";
            string content = await client.GetStringAsync(Url);
            msgs = JsonConvert.DeserializeObject< List < Msg >>(content);
            List<TMsg> nlmsgs = new List<TMsg>();
            for(var r=0;r < msgs.Count; r++)
            {
               if (msgs[r].recepcion.Equals("N"))
                {
                    msgs[r].remitente = remplazaOrigen(msgs[r].remitente);
                    nlmsgs.Add(new TMsg
                    {
                        _id = msgs[r]._id,
                         asunto = msgs[r].asunto,
                          create_date = msgs[r].create_date,
                           cuerpo = msgs[r].cuerpo,
                            destinatario = msgs[r].remitente,
                             recepcion = msgs[r].recepcion,
                              remitente = msgs[r].remitente,
                         imagen = "Mensaje.jpg"
                    }) ;
                }
            }
            _msg = new ObservableCollection<TMsg>(nlmsgs);
            Noticas.ItemsSource = _msg;
            picker.ItemsSource = carga;
            base.OnAppearing();

        }
        async void ButtonClicked(object sender, EventArgs e)
        {
            if (picker.SelectedItem!=null)
            { 
                string seleccionado = picker.SelectedItem.ToString();
                picker.Title = "Enviar notificación";
                picker.SelectedItem = "Enviar notificación";
                await Navigation.PushModalAsync(new DestinosPage(Aurl, seleccionado, mnmat, desty));
                // await Navigation.PushModalAsync(new DestinosPage(Aurl,seleccionado, mnmat, desty));
            }
        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        void ButtonClickedLeido(object sender, EventArgs e)
        {
            titulo.Text = "Notificacion (Leidas)";
            List<TMsg> nlmsgs = new List<TMsg>();
            for (var r = 0; r < msgs.Count; r++)
            {
               if (msgs[r].recepcion.Equals("S"))
               {
                    //     msgs[r].destinatario = msgs[r].remitente;
                    msgs[r].remitente = remplazaOrigen(msgs[r].remitente);

                    nlmsgs.Add(new TMsg {
                        _id = msgs[r]._id,
                        asunto = msgs[r].asunto,
                        create_date = msgs[r].create_date,
                        cuerpo = msgs[r].cuerpo,
                        destinatario = msgs[r].remitente,
                        recepcion = msgs[r].recepcion,
                        remitente = msgs[r].remitente,
                        imagen = "Mensajeo.jpg"

                    });
               }
            }
            _msg = new ObservableCollection<TMsg>(nlmsgs);
            Noticas.ItemsSource = _msg;

        }
        void ButtonClickedNoLeido(object sender, EventArgs e)
        {
            titulo.Text = "Notificacion (Sin leer)";
          
            List<TMsg> nlmsgs = new List<TMsg>();
            for (var r = 0; r < msgs.Count; r++)
            {
                if (msgs[r].recepcion.Equals("N"))
                {
                    //     msgs[r].destinatario = msgs[r].remitente;
                    msgs[r].remitente = remplazaOrigen(msgs[r].remitente);
                    nlmsgs.Add(new TMsg
                    {
                        _id = msgs[r]._id,
                        asunto = msgs[r].asunto,
                        create_date = msgs[r].create_date,
                        cuerpo = msgs[r].cuerpo,
                        destinatario = msgs[r].remitente,
                        recepcion = msgs[r].recepcion,
                        remitente = msgs[r].remitente,
                        imagen = "Mensaje.jpg"

                    });
                }
            }
            _msg = new ObservableCollection<TMsg>(nlmsgs);
            Noticas.ItemsSource = _msg;

        }

        async void ButtonClickedEnviado(object sender, EventArgs e)
        {
            string ff = "00";
            try
            {
                titulo.Text = "Notificacion (Enviadas)";
                string RUrl = "http://" + Aurl + ":8020/api/msgs?remite=" + mnmat;
                ff = "240";
                string content = await client.GetStringAsync(RUrl);
                ff = "241";
                List<Msg> nmsgs = JsonConvert.DeserializeObject<List<Msg>>(content);
                ff = "242";
                List<TMsg> nlmsgs = new List<TMsg>();
                ff = "242.1";
                if (nmsgs.Count > 0)
                for (var r = 0; r < nmsgs.Count; r++)
                {
                    ff = "242.2";
                    //     msgs[r].destinatario = msgs[r].remitente;
                    nmsgs[r].destinatario = remplazaOrigen(nmsgs[r].destinatario);
                    ff = "242.3";
                    nmsgs[r].remitente = remplazaOrigen(nmsgs[r].destinatario);
                    ff = "242.4";
                    nlmsgs.Add(new TMsg
                    {
                       
                        _id = nmsgs[r]._id,
                        asunto = nmsgs[r].asunto,
                        create_date = nmsgs[r].create_date,
                        cuerpo = nmsgs[r].cuerpo,
                        destinatario = nmsgs[r].destinatario,
                        recepcion = nmsgs[r].recepcion,
                        remitente = nmsgs[r].remitente,
                        imagen = "smensaje.png"

                    });
                    ff = "242.5";
                }
                ff = "265";
                _msg = new ObservableCollection<TMsg>(nlmsgs);
                Noticas.ItemsSource = _msg;
            }
            catch(Exception t)
            {
                string titulo = "Error "+ff;
                string cuerpo = t.ToString();

                await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));


            }
        }

        async void Noticas_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            TMsg sel = (TMsg) Noticas.SelectedItem;
            HttpClient client;
            client = new HttpClient();
           
            string url = "http://" + Aurl + ":8020/api/msgm/"+sel._id;
            var json = JsonConvert.SerializeObject(sel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // var content = new FormUrlEncodedContent(json);
            //  content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(url, content);
            
            var responseString = await response.Content.ReadAsStringAsync();
            Device.BeginInvokeOnMainThread(async () => {
                string titulo = sel.asunto;
                string cuerpo = sel.cuerpo;

                await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
              //  await DisplayAlert(sel.asunto, sel.cuerpo, "OK");
            });
            

        }
        async void notifica(string titulos, string cuerpos)
        {
            string titulo = titulos;
            string cuerpo = cuerpos;

            await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));

        }
        async void ImageButton_Clicked(System.Object boton, System.EventArgs e)
        {
           // TMsg sel = (TMsg)Noticas.SelectedItem;
            var ib = boton as ImageButton;
             string va = ib.AutomationId;
            string titulo = "Atención"; // sel.asunto;
            string cuerpo = "Se Elimino el mensaje"; // sel.cuerpo;

            await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
            object isender = new object();

            HttpClient client;
            client = new HttpClient();

            string url = "http://" + Aurl + ":8020/api/msgs/" + va;
     
            var response = await client.DeleteAsync(url);
            //   var response = await client.PutAsync(url, content);

            actualiza();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MIUCSHA
{
    
    public partial class MenuPage : ContentPage
    {
        private string Url = "http://192.168.1.129:8020/datos";
        private string Url2 = "http://192.168.1.129:8020/periodo";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<datosPersonales> _datos;
        private List<HorariosClases> horas;
        private List<CargaClass> cargaAca;
        private PeriodosClass period;
        private List<datosDocentesClass> ddatos;
        private List<cursosClass> dcursos;
        private string Run;
        private string nmat;
        private string Aurl = "127.0.0.1";
        private string bus;
        private int anyo = 0;
        private int sem = 0;
        private int ma = 2027;
        private int mp = 1;
        private int aanyo = 2027;
        private string minombre;

        public MenuPage(string Durl, string run)
        {
            Aurl = Durl;
            Url = "http://" + Aurl + ":8020/aca/datos";
            Url = Url + "?rut=" + run;
            Url2 = "http://" + Aurl + ":8020/periodo";
            UpdPeriod();
            Run = run;
          
            InitializeComponent();
        }     
            
        async void UpdPeriod()
        {
            String periodos = await client.GetStringAsync(Url2);
            period = JsonConvert.DeserializeObject<PeriodosClass>(periodos);
            aanyo = Int32.Parse(period.anyo);
            mp = Int32.Parse(period.sem);
        }
        protected override async void OnAppearing()
        {
            string ff = "00";
            try
            {
                
              
                string content = await client.GetStringAsync(Url);
                ff = "49";
                ddatos = JsonConvert.DeserializeObject<List<datosDocentesClass>>(content);
                ff = "51";
               
                minombre = ddatos[0].nomb_acad;
                Nombres.Text = ddatos[0].nomb_acad;
                // Rut.Text = "Rut:" +Run;
                string alfa = ddatos[0].carr_acad;
                nmat = alfa;
                Carrera.Text = ddatos[0].carr_acad;
                ff = "52";
                anyo = Int32.Parse(period.anyo);
                sem = Int32.Parse(period.sem);
            
                ma = anyo;
                mp = sem;
                ff = "53";
                string URL3 = "http://"+Aurl+":8020/aca/horarios?rut=" + Run + "&anyo=" + period.anyo + "&sem=" + period.sem;
               
                ff = "55";
                string content7 = await client.GetStringAsync(URL3);
                ff = "56";
                ff = "70";
               horas = JsonConvert.DeserializeObject<List<HorariosClases>>(content7);
                ff = "71";

               string URL = "http://" + Aurl + ":8020/aca/cursos?anyo=" + period.anyo + "&vepe=" + period.sem+"&rut=" + Run;
                //  client.Timeout = TimeSpan.FromSeconds(120);
               string content2 = await client.GetStringAsync(URL);
            
                dcursos = JsonConvert.DeserializeObject<List<cursosClass>>(content2);
                ff = "72";
        
                ff = "77";
                DiaSem.Text = anyo.ToString() + "-" + sem.ToString();
            
               
            
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    string titulo = "Error 77 -- "  + ff.ToString();
                    string cuerpo = "Se presento un error que no ha podido controlar " + e.ToString();

                    await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
               //     await DisplayAlert("Error", "URL= " + Aurl + "  -Se presento un error que no ha podido controlar = " + e.ToString(), "OK");
                });
               
            }
        //  await DisplayAlert("Notificacion", "Todos estan = ", "OK");
            base.OnAppearing();


        }
        async void Actualiza()
        {

            try
            {
                string ff = "0";

                string content = await client.GetStringAsync(Url);
                ff = "49";
                ddatos = JsonConvert.DeserializeObject<List<datosDocentesClass>>(content);
                ff = "51";

                minombre = ddatos[0].nomb_acad;
                Nombres.Text = ddatos[0].nomb_acad;
           //    Rut.Text = "Rut:" + Run;
                string alfa = ddatos[0].carr_acad;
                nmat = alfa;
                Carrera.Text = ddatos[0].carr_acad;
                ff = "52";
                anyo = Int32.Parse(period.anyo);
                sem = Int32.Parse(period.sem);
             //   ma = anyo;
             //   mp = sem;
                ff = "53";
                string URL3 = "http://" + Aurl + ":8020/aca/horarios?rut=" + Run + "&anyo=" + period.anyo + "&sem=" + period.sem;

                ff = "55";
                string content7 = await client.GetStringAsync(URL3);
                ff = "56";
                ff = "70";
                horas = JsonConvert.DeserializeObject<List<HorariosClases>>(content7);
                ff = "71";

                string URL = "http://" + Aurl + ":8020/aca/cursos?anyo=" + period.anyo + "&vepe=" + period.sem + "&rut=" + Run;
                //  client.Timeout = TimeSpan.FromSeconds(120);
                string content2 = await client.GetStringAsync(URL);

                dcursos = JsonConvert.DeserializeObject<List<cursosClass>>(content2);
                ff = "72";

                ff = "77";
                DiaSem.Text = anyo.ToString() + "-" + sem.ToString();




/*

                //    client.Timeout = TimeSpan.FromSeconds(120);
                string content = await client.GetStringAsync(Url);
                List<datosPersonales> datos = JsonConvert.DeserializeObject<List<datosPersonales>>(content);
                // _msg = new ObservableCollection<Msg>(msgs);

                // var dtas = datos.ToArray();
                minombre = datos[0].nombres + " " + datos[0].apellPat + " " + datos[0].apellMat;
                Nombres.Text = datos[0].nombres + " " + datos[0].apellPat + " " + datos[0].apellMat;
                Rut.Text = Run;
                string alfa = datos[0].numMat;
                nmat = alfa;
                Carrera.Text = "(" + datos[0].codCar + ")-" + datos[0].carrera;
                // client.Timeout = TimeSpan.FromSeconds(120);

                //  XXXXXXXX        cambios por pruebas debe ser eliminas las siguientes lineas

                anyo = Int32.Parse(period.anyo);
                sem = Int32.Parse(period.sem);
                //   XXXXXXXXX       Pendientes
                string URL = "http://" + Aurl + ":8020/aca/horarios?rut=" + Run + "&anyo=" + period.anyo + "&sem=" + period.sem;
                //  client.Timeout = TimeSpan.FromSeconds(120);
                string content2 = await client.GetStringAsync(URL);
                horas = JsonConvert.DeserializeObject<List<HorariosClases>>(content2);
                 URL = "http://" + Aurl + ":8020/aca/cursos?anyo=" + period.anyo + "&vepe=" + period.sem + "&rut=" + Run;
                //  client.Timeout = TimeSpan.FromSeconds(120);
                content2 = await client.GetStringAsync(URL);

                dcursos = JsonConvert.DeserializeObject<List<cursosClass>>(content2);
                DiaSem.Text = anyo.ToString() + "-" + sem.ToString();
                bus = dcursos[0].asig_desc;
*/
            }
            catch (Exception e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    string titulo = "Error 127";
                    string cuerpo = "Se presento un error que no ha podido controlar";

                    await PopupNavigation.Instance.PushAsync(new PopupNewTaskView(titulo, cuerpo));
                    //     await DisplayAlert("Error", "URL= " + Aurl + "  -Se presento un error que no ha podido controlar = " + e.ToString(), "OK");
                });
            }
        }
        async void CalendaButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new HorariosPage(Aurl, horas, dcursos, period));
          //  await Navigation.PushModalAsync(new CalendarioAcadPage(period, "1", Aurl));
           
          //  await Navigation.PushModalAsync(new MiCalendario(horas));
        }
        async void AsistenciaButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CaliAcaPage(dcursos,Run,nmat,minombre,Aurl, period));
          //  await Navigation.PushModalAsync(new MyPage(Aurl,nmat,cargaAca,period));
        }
        async void NotificaButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Notifica(Aurl,Run,dcursos,minombre));
        }
        async void CalificaButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CaliMeuPage(dcursos, Run, nmat, minombre, Aurl, period));
            //  await Navigation.PushModalAsync(new NotaFinalPage(dcursos, Run, nmat, minombre, Aurl, period));
            // await Navigation.PushModalAsync(new SelAsignaturas(Aurl,cargaAca, period, nmat));
        }
        async void OfertaButtonClicked(object sender, EventArgs e)
        {
            Page p = new FacultadesSel(Aurl);
            await Navigation.PushModalAsync(p);
        }
        async void PlanButtonClicked(object sender, EventArgs e)
        {
            //  await Navigation.PushModalAsync(new MyPlanEstudio(Aurl, nmat, cargaAca, period));
            //  await Navigation.PushModalAsync(new SemestresPage(Aurl, nmat, cargaAca, period));
          //  await Navigation.PushModalAsync(new HorariosPage(Aurl, horas, dcursos, period));
            await Navigation.PushModalAsync(new AsisteDocePage(dcursos, Aurl, Run, minombre, period));

        }
         void HistoricoButtonClicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                string t = "Histórico de Notas";
                //   await Navigation.PushModalAsync(new HorariosPage(Aurl, horas, dcursos, period));
                //   await Navigation.PushModalAsync(new AsisteDocePage(dcursos, Aurl, Run, minombre, period));
                await Navigation.PushModalAsync(new CalendarioAcadPage(period, "1", Aurl));

            });
            
           // await Navigation.PushModalAsync(new SemestresPage(Aurl, nmat, cargaAca, period));

        }
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }



         void ImageButton_Clicked_2(System.Object sender, System.EventArgs e)
        {
            if (sem == 1) { sem = 2; anyo--; }
            else
              if (sem == 2) { sem = 1; }
            
            
            string dio = anyo.ToString() + "-" + sem.ToString();



            DiaSem.Text = anyo.ToString() + "-" + sem.ToString();
            period.anyo = anyo.ToString();
            period.sem = sem.ToString();
            Actualiza();

        }

        void ImageButton_Clicked_3(System.Object sender, System.EventArgs e)
        {

            if (anyo <= aanyo)
            {
                if (sem == 1) sem++;
                else
                if (sem == 2 && anyo != aanyo) { sem = 1; anyo++; }
            }
            DiaSem.Text = anyo.ToString() + "-" + sem.ToString();
            period.anyo = anyo.ToString();
            period.sem = sem.ToString();
            Actualiza();
        }
    }
}

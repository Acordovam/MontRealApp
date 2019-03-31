using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace MontRealApp
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    /// 

        public class Modelo
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string edad { get; set; }
        public string folio { get; set; }
        public string direccion { get; set; }
        public string nacimiento { get; set; }
        public string sexo { get; set; }
        public string padre { get; set; }
        public string madre { get; set; }
        public string encargado { get; set; }
        public string seccion { get; set; }
        public string jornada { get; set; }
        public string carrera { get; set; }

    }

    public sealed partial class MainPage : Page
    {
        MySqlConnection con= new MySqlConnection();
        private string server = "localhost";
        private string port = "3306";
        private string DataBase = "montrealbd";
        private string Master = "root";
        private string pass = "alejandro198";
        string[] lista = new string[1000];
    
        //Comienzan Módulos



        void ocultar(Grid grid) //Recibe objeto Grid que no se ocultará
        {
            Estudiante.Visibility = Visibility.Collapsed;//Se ocultan todos los grid
            Empleado.Visibility = Visibility.Collapsed;
            Cobros.Visibility = Visibility.Collapsed;
            Ver_Datos.Visibility = Visibility.Collapsed;


            grid.Visibility = Visibility.Visible; //Se vuelve visible el grid que recibe la función como parámetro

        }
        public async System.Threading.Tasks.Task MessageBoxAsync(string titulo, string text)
        {
            var dialog = new MessageDialog(text);
            dialog.Title = titulo;
            await dialog.ShowAsync();
        }

        private void coneccion()
        {
           
            con.ConnectionString = "Server="+server+";Port="+port+";Database="+DataBase+";Uid="+Master+";Pwd="+pass+";";
            try
            {
                con.Open();
               // MessageBoxAsync("Conección Correcta", "!La conección ha sido un éxito!");
            }
            catch(Exception e)
            {
                MessageBoxAsync("Error", "Error en la conección. Si el problema persiste porfavor comunicarse con el administrador \n"+e);
            }

        }

        
        private void cond(string query)
        {
   
        }

        private string[] consulta(string query)
        {
            int l = 0;
            while (l < 1000)
            {
                lista[l] = null;
                l++;
            }
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader rdr = cmd.ExecuteReader();int i=0;

            

            while (rdr.Read())
            {
                lista[i] = rdr[0].ToString();
                i++;
            }
            rdr.Close();
            return lista;
        }

        private void alterar(string query)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBoxAsync("Acción Realizada", "La acción ha sido realizada en la Base de Datos Correctamente.");
            }
            catch(Exception e)
            {
                MessageBoxAsync("Error al guardar",""+query+"\n\n La acción no pudo realizarse con éxito, lamentamos el inconveniente, si el problema persiste porfavor contacte al administrador.\n"+e);
            }
            

            
        }
        private void desconectar()
        {
           
            con.Close();   
        }

        public void salir()
        {
            App.Current.Exit();
        }

        //Terminan Modulos

            void obenerData()
        {
            try
            {
                //Alumnos
                int i=0;
                string[] resul = consulta("select secciones.seccion from secciones;");
                while (lista[i]!=null)
                {
                    cbseccionEs.Items.Add(lista[i]);
                    i++;
                 
                }

                 i = 0;
                resul = consulta("select jornada.jornada from jornada;");
                while (lista[i] != null)
                {
                    cbjornadaEs.Items.Add(lista[i]);
                    cbjornada.Items.Add(lista[i]);
                    i++;

                }

                i = 0;
                resul = consulta("select carreras.nombre from carreras;");
                while (lista[i] != null)
                {
                    cbcarreraEs.Items.Add(lista[i]);
                    i++;

                }
                //Empleos
                i = 0;
                resul = consulta("select empleo.empleo from empleo;");
                while (lista[i] != null)
                {
                    cbempleoEm.Items.Add(lista[i]);
                    i++;

                }
                //Cobros
                i = 0;
                resul = consulta("select alumno.nombre from alumno;");
                while (lista[i] != null)
                {
                    cbalumn.Items.Add(lista[i]);
                    i++;

                }

                i = 0;
                resul = consulta("select conceptoPago.concepto from conceptoPago;");
                while (lista[i] != null)
                {
                    cbconcepto.Items.Add(lista[i]);
                    i++;

                }
                i = 0;
                resul = consulta("select empleados.nombre from empleados;");
                while (lista[i] != null)
                {
                    cbempleado.Items.Add(lista[i]);
                    i++;

                }
                cond("select * from alumno");

            }
            catch (Exception e)
            {
                MessageBoxAsync("Error", "Error al intentar insertar datos en el campo Secciones. si el problema persiste porfavor contactar al administrador \n"+e);
            }
            
           
        }

        void Inicializacion()   //Funcion de Inicialización de algunos valores 
        {
            cbsexoEs.Items.Add("F");
            cbsexoEs.Items.Add("M");
            cbsexoEs.SelectedIndex = 0;
            Menu.Visibility = Visibility.Collapsed;
            Estudiante.Visibility = Visibility.Collapsed;//Se ocultan todos los grid
            Empleado.Visibility = Visibility.Collapsed;
            Cobros.Visibility = Visibility.Collapsed;
            Ver_Datos.Visibility = Visibility.Collapsed;
            Login.Visibility = Visibility.Visible;
            coneccion();
            


        }
 
  

        public MainPage()
        {
            this.InitializeComponent();

            Inicializacion();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEstudiante_Click(object sender, RoutedEventArgs e)
        {
            ocultar(Estudiante);
        }

        private void BtnEmpleado_Click(object sender, RoutedEventArgs e)
        {
            ocultar(Empleado);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnCobro_Click(object sender, RoutedEventArgs e)
        {
            ocultar(Cobros);
        }

        private void BtnVer_Click(object sender, RoutedEventArgs e)
        {
            ocultar(Ver_Datos);


        }

        private void Btnsalir_Click(object sender, RoutedEventArgs e)
        {
            desconectar();
            salir();
        }

        private void Btninicio_Click(object sender, RoutedEventArgs e)
        {
            string user = txtuser.Text;
            string pass = txtpass.Password.ToString();
            string[] resultado = consulta("select 1 from usuario where usuario.alias='"+user+"'and usuario.contraseña='"+pass+"'");
            if (resultado[0]!=null)
            {
                MessageBoxAsync("!Bienvenido! " + user, "Bienvenido al Sistema");
                Login.Visibility = Visibility.Collapsed;
                ocultar(Estudiante);
                Menu.Visibility = Visibility.Visible;
                obenerData();
            }
            else
            {
                MessageBoxAsync("Error de Datos", "Usuario o Contraseña Incorrectos, porfavor revise y escriba nuevamente. Si el error persiste comuniquese con el administrador");

            }

           
            
        }

        private void Principal_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string idalumno = txtidEs.Text;
            string nombres = txtnombreEs.Text;
            string edad = txtedadEs.Text;
            string ubidoc = txtfolioEs.Text;
            string direcc = txtdireccionEs.Text;

            string fechaN = cdpnacimientoEs.Date.ToString();
            fechaN=fechaN.Substring(0, 10);
            string sexo = cbsexoEs.SelectedItem.ToString();
            string nombreP = txtpadreEs.Text;
            string nombreM = txtmadreEs.Text;
            string nombreE = txtencargadoEs.Text;
            string seccion = cbseccionEs.SelectedItem.ToString();
            string jornada = cbjornadaEs.SelectedItem.ToString();
            string carrera = cbcarreraEs.SelectedItem.ToString();


            alterar("insert into alumno values ('"+idalumno+"', '"+nombres+"', '"+edad+"', '"+ubidoc+"', '"+direcc+"', '"+fechaN+"', '"+sexo+"', '"+nombreP+"', '"+nombreM+"', '"+nombreE+"',(select secciones.idsecciones from secciones where secciones.seccion='"+seccion+"'), (select jornada.idjornada from jornada where jornada.jornada='"+jornada+"'), (select carreras.idcarreras from carreras where carreras.nombre='"+carrera+"'))");
        }

        private void BtnguardarEm_Click(object sender, RoutedEventArgs e)
        {
            string id = txtidEm.Text;
            string nombre = txtnombreEm.Text;
            string tel = txttelefonoEm.Text;
            string correo = txtcorreoEm.Text;
            string dpi = txtdpiEm.Text;
            string emp = cbempleoEm.SelectedItem.ToString();

            alterar("insert into empleados values('"+id+"', '"+nombre+"', '"+tel+"','"+correo+"', '"+dpi+"',(select empleo.idempleo from empleo where empleo.empleo='"+emp+"' ));");
        }

        private void Btnguardar_Click(object sender, RoutedEventArgs e)
        {
            string id = txtidcobro.Text;
            string alum = cbalumn.SelectedItem.ToString();
            string cant = txtcantidad.Text;
            string fecha = cpfecha.Date.ToString();
            fecha = fecha.Substring(0,10);
            string concepto = cbconcepto.SelectedItem.ToString();
            string jornada = cbjornada.SelectedItem.ToString();
            string emple = cbempleado.SelectedItem.ToString();

            alterar("insert into cobros values('"+id+"', (select alumno.idalumno from alumno where alumno.nombre='"+alum+"'), '"+cant+"', '"+fecha+"', (select conceptoPago.idconceptoPago from conceptoPago where conceptoPago.concepto='"+concepto+"'), (select jornada.idjornada from jornada where jornada.jornada='"+jornada+"'), (select empleados.idempleados from empleados where empleados.nombre='"+emple+"'))");
        }
    }
}

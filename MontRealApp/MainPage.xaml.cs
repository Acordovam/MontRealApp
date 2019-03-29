using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    public sealed partial class MainPage : Page
    {
        private MySqlConnection con;
        private string server = "localhost";
        private string port = "3306";
        private string DataBase = "montrealbd";
        private string Master = "root";
        private string pass = "alejandro198";
        private MySqlDataReader resultado;
        private MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
       
        
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
            con=new MySqlConnection();
            con.ConnectionString = "Server="+server+";Port="+port+";Database="+DataBase+";Uid="+Master+";Pwd="+pass+";";
            try
            {
                con.Open();
               // MessageBoxAsync("Conección Correcta", "!La conección ha sido un éxito!");
            }
            catch(Exception e)
            {
                MessageBoxAsync("Error", "Error en la conección");
            }
            
        }

        private MySqlDataReader consulta(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader rdr = cmd.ExecuteReader();

            return rdr;
        }

        private void alterar(string query)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBoxAsync("Acción Realizada", "La acción ha sido realizada en la Base de Datos Correctamente.")
            }
            catch(Exception e)
            {
                MessageBoxAsync("La acción no pudo realizarse con éxito, lamentamos el inconveniente, si el problema persiste porfavor contacte al administrador.");
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
            salir();
        }

        private void Btninicio_Click(object sender, RoutedEventArgs e)
        {
            string user = txtuser.Text;
            string pass = txtpass.Password.ToString();
            resultado = consulta("select 1 from usuario where usuario.alias='"+user+"'and usuario.contraseña='"+pass+"'");
            if (resultado.HasRows)
            {
                MessageBoxAsync("!Bienvenido! " + user, "Bienvenido al Sistema");
                Login.Visibility = Visibility.Collapsed;
                ocultar(Estudiante);
            }
            else
            {
                MessageBoxAsync("Error de Datos", "Usuario o Contraseña Incorrectos, porfavor revise y escriba nuevamente. Si el error persiste comuniquese con el administrador");

            }
            resultado.Close();
        }

       
    }
}

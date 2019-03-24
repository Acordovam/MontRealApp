using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        //Comienzan Módulos
        void ocultar(Grid grid) //Recibe objeto Grid que no se ocultará
        {
            Estudiante.Visibility = Visibility.Collapsed;//Se ocultan todos los grid
            Empleado.Visibility = Visibility.Collapsed;
            Cobros.Visibility = Visibility.Collapsed;
            Ver_Datos.Visibility = Visibility.Collapsed;

            grid.Visibility = Visibility.Visible; //Se vuelve visible el grid que recibe la función como parámetro

        }

        //Terminan Modulos
        void Inicializacion()   //Funcion de Inicialización de algunos valores 
        {
            cbsexoEs.Items.Add("F");
            cbsexoEs.Items.Add("M");
            cbsexoEs.SelectedIndex = 0;
         
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
    }
}

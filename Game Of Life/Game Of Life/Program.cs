using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 * Conway's Game of Life
 * Juego de la vida de Conway
 * 
 * Proyecto de Laboratorio de Programacion
 * ID:1069835   JUAN DANIEL OZUNA ESPINAL
 */
namespace JuegoDeLaVidaINTENTO
{
    static class Program
    {
        public static GOFMain gof = new GOFMain();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            gof.ShowDialog();
        }
    }
}

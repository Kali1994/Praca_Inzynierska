using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
  ///      [STAThread]
    //    [DllImport("C:\\Users\\Kali\\Desktop\\TestDLL\\Debug\\TestDLL.dll", CallingConvention = CallingConvention.Cdecl)]
   //     public static extern int decryption(int b);
        // public static extern int encryption(string a);

        [STAThread] static void Main()
        {
            AlgorithmWrapper.WrapperClass cppClass = new AlgorithmWrapper.WrapperClass();
            //    int b = 5;
            // Program.decryption(b);
            //encryption("ss");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainScreen());
        }
    }
}

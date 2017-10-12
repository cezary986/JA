using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using RotateHL;
using System.Drawing;


namespace MainProgram
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Form1 form = new Form1();

            Bitmap bmp =  RotateHL.AddClass.RotateImage(form.getPictureBoxImage(), 60);
            form.setPictureBoxImage(bmp);
            bmp.Save("C:\\Users\\cezary\\Desktop\\STUDIA PROJEKTY\\JA\\Repo\\images\\result.bmp");
            //            Application.Run(form);

            int x = MyProc1(3, 3);

            Application.Run(form);

    }
        [DllImport("MASM_DLL.dll")]
        static extern int MyProc1(int x, int y);
    }
}

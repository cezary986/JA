using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainProgram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public Bitmap getPictureBoxImage()   
        {
            Rectangle rect = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bitmap , rect);
            return bitmap;
        }

        public void setPictureBoxImage(Bitmap bitmap)
        {
            pictureBox2.Image = bitmap;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;


namespace Projekt
{
    public partial class Form1 : Form
    {
        Bitmap m;     
        int X, Y, X1, Y1;
        bool Cleaked = false;
        bool AlgorytmBresenhama;
        bool AlgorytmPrzyrostowy;
        public Form1()
        {
            InitializeComponent();
            m = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Cleaked = true;
            X = e.X;
            Y = e.Y;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            m.Dispose();
            m = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            X = 0;
            Y = 0;
            X1 = 0;
            Y1 = 0;
            if (AlgorytmBresenhama == true)
            {
                AlgorytmBresenhama = true;
            }
            else
            {
                AlgorytmBresenhama = false;
                AlgorytmPrzyrostowy = true;
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            AlgorytmBresenhama = true;
            AlgorytmPrzyrostowy = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Cleaked == true)
            {
                X1 = e.X;
                Y1 = e.Y;
                if (X1 < 0)
                {
                    X1 = 0;
                }
                if (Y1 < 0)
                {
                    Y1 = 0;
                }
                if (X1 > pictureBox1.Width)
                {
                    X1 = pictureBox1.Width - 1;
                }
                if (Y1 > pictureBox1.Height)
                {
                    Y1 = pictureBox1.Height - 1;
                }

                Bitmap bitm = new Bitmap(m);
                if (AlgorytmBresenhama == true)
                {
                    bitm = DrawLineBlack(bitm, X, Y, X1, Y1);
                }
                if (AlgorytmPrzyrostowy == true)
                {
                    bitm = Przyrostowy(bitm, X, Y, X1, Y1);
                }
                pictureBox1.Image = bitm;
            }
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            AlgorytmPrzyrostowy = true;
            AlgorytmBresenhama = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Cleaked = false;
            Bitmap m1 = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            if (AlgorytmBresenhama == true)
            {
                 m1 = DrawLineBlack(m, X, Y, X1, Y1);
            }
           
            if (AlgorytmPrzyrostowy == true)
            {
                 m1 = Przyrostowy(m, X, Y, X1, Y1);
            }
             
            pictureBox1.Image = m1;
          
        }
        private Bitmap DrawLineBlack( Bitmap n ,int x1, int y1, int x2, int y2)
        {
            int deltax, deltay, krokx, kroky, błęd;
            deltax = x2 - x1;
            if (deltax > 0)
            {
                krokx = 1;
            }
            else
            {
                krokx = -1;
            }              
            deltax = Math.Abs(deltax);
            deltay = y2 - y1;
            if (deltay > 0)
            {
                kroky = 1;
            }
            else
            {
                kroky = -1;
            }
            deltay = Math.Abs(deltay);
            if (deltax > deltay)
            {

                błęd = -deltax;
                while (x1 != x2)
                {                  
                    n.SetPixel(x1, y1, System.Drawing.Color.Black);
                    błęd += 2 * deltay;
                    if (błęd > 0) 
                    { 
                        y1 += kroky;
                        błęd -= 2 * deltax;
                    }
                    x1 += krokx;
                }
            }
            else
            {
                błęd = -deltay;
                while (y1 != y2)
                {
                    n.SetPixel(x1, y1, System.Drawing.Color.Black);

                    błęd += 2 * deltax;
                    if (błęd > 0) 
                    { 
                        x1 += krokx;
                        błęd -= 2 * deltay; 
                    }
                    y1 += kroky;
                }
            }
            return n;
        }
        private Bitmap Przyrostowy(Bitmap n, int x1, int y1, int x2, int y2)
        {
            int x;
            int y;
            float deltax, deltay,Y,X,m;           
            deltax = x2 - x1;
            deltay = y2 - y1;          
            m = deltay / deltax;
            Y = y1;
            X = x1;
            if (Math.Abs(m) >= 1)
            {
                for (y = y1; y <= y2; y++)
                {
                    n.SetPixel((int)Math.Floor(X + 0.5), y, Color.Red);
                    X += (1 / m);
                }
            }
            else
            {
                for (x = x1; x <= x2; x++)
                {
                    n.SetPixel(x, (int)Math.Floor(Y + 0.5), Color.Red);
                    Y += m;
                }
            }
            if ( deltay < 0 || deltax < 0)
            {
                if (Math.Abs(m) >= 1) 
                {
                    X = x2;
                    for (y = y2; y <= y1; y++)
                    {
                        n.SetPixel((int)Math.Floor(X + 0.5), y, Color.Red);
                        X += (1 / m);
                    }
                   
                }
                else
                {
                    Y = y2;
                    for (x = x2; x <= x1; x++) 
                    {
                        n.SetPixel(x, (int)Math.Floor(Y + 0.5), Color.Red);
                        Y += m;
                    }
                }
            }
          
                
            
            return n;
          
        }


    }
}

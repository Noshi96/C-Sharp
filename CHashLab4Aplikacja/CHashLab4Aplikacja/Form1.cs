using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CHashLab4Aplikacja
{

    public partial class Form1 : Form
    {
        class Figure
        {
            virtual public void draw(Graphics g, Pen pen, int startX, int startY, int endY, int endX, int pX, int pY)
            {

            }
        }
        class Rectangle : Figure
        {          
            override public void draw(Graphics g, Pen pen, int startX, int startY, int endY, int endX, int pX, int pY)
            {              
                g.DrawLine(pen, new Point(startX, startY), new Point(endX, startY));
                g.DrawLine(pen, new Point(endX, startY), new Point(endX, endY));
                g.DrawLine(pen, new Point(endX, endY), new Point(startX, endY));
                g.DrawLine(pen, new Point(startX, endY), new Point(startX, startY));
            }
        }
        class Circle : Figure
        {
            float radius;
            double temp;
            override public void draw(Graphics g, Pen pen, int startX, int startY, int endY, int endX, int pX, int pY)
            {
                temp = Math.Sqrt(Math.Pow((startX - endX), 2) + Math.Pow((startY - endY), 2));
                radius = Math.Abs((float)temp);
                g.DrawEllipse(pen, startX - radius, startY - radius, radius + radius, radius + radius);
            }
        }
        class Line : Figure
        {
            override public void draw(Graphics g, Pen pen, int startX, int startY, int endY, int endX, int pX, int pY)
            {
                g.DrawLine(pen, new Point(startX, startY), new Point(endX, endY));
            }
        }
        class Free : Figure
        {
            override public void draw(Graphics g, Pen pen, int startX, int startY, int endY, int endX, int pX, int pY)
            {
                g.DrawLine(pen, new Point(pX, pY), new Point(pX+1, pY+1));
                g.DrawLine(pen, new Point(pX, pY), new Point(pX-1, pY+1));
                g.DrawLine(pen, new Point(pX, pY), new Point(pX+1, pY-1));
                g.DrawLine(pen, new Point(pX, pY), new Point(pX-1, pY-1));
            }
        }


        Graphics g;
        int posX = -1, posY = -1;
        bool move = false, freeB = false;
        Pen pen;
        int sX = 0, sY = 0, eX = 0, eY = 0;
        Figure figure = new Figure();

        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            pen = new Pen(Color.Black, 5);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            pen.Color = p.BackColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            figure = new Rectangle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            figure = new Circle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            figure = new Line();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            figure = new Free();
            freeB = true;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            posX = e.X;
            posY = e.Y;
            sX = e.X;
            sY = e.Y;
            panel1.Cursor = Cursors.Cross;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(move && posX != -1 && posY != -1)
            {
                if (freeB)
                {
                    panel1_Paint(this, null);
                }
                posX = e.X;
                posY = e.Y;            
            }
        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {
            figure.draw(g, pen, sX, sY, eY, eX, posX, posY);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            eX = e.X;
            eY = e.Y;
            move = false;
            posX = -1;
            posY = -1;
            panel1.Cursor = Cursors.Default;
            panel1_Paint(this, null);
            freeB = false;
        }
    }
}

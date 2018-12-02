using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics;

namespace GraphicsOpenGL
{
    public partial class Form1 : Form
    {
        List<KeyValuePair<double, double>> points_for_drawing;
        OpenGL gl;
        List<PointF> figure = new List<PointF>();
        double rotation = 0;
        public Form1()
        {
            InitializeComponent();
            points_for_drawing = new List<KeyValuePair<double, double>>();
            gl = this.openGLControl1.OpenGL;
        }

        private void _pause(int value)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
                Application.DoEvents();
        }

        private void BuildFigure(List<PointF> points)
        {
            var randomCol = GetRandomColor();
            gl.Color(randomCol.Item1, randomCol.Item2, randomCol.Item3);
            foreach (var p in points)
            {
                figure.Add(new PointF(p.X, p.Y));
                gl.Vertex(p.X, p.Y);
            }
        }

        private Tuple<float, float, float> GetRandomColor()
        {
            var rand = new Random();
            return new Tuple<float, float, float>((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
        }


        private void PrepareToDraw()
        {
            rotation = 0;
            figure.Clear();
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -20.0f);
        }

        private void BuildTriangle()
        {
            PrepareToDraw();
            gl.Begin(OpenGL.GL_TRIANGLES);
            var shift = new Random().Next(2, 7);
            BuildFigure(new List<PointF>() { new PointF(-2 + shift , 0), new PointF(0, 2 + shift), new PointF(2 + shift, 0) });
            gl.End();
            openGLControl1.Focus();
        }

        private void triangleButtonClick(object sender, EventArgs e) =>
            BuildTriangle();

        private void BuildSegment()
        {
            PrepareToDraw();
            gl.Begin(OpenGL.GL_LINES);

            var shift = new Random().Next(2, 5);
            BuildFigure(new List<PointF>() { new PointF(-shift, 0), new PointF(0, shift) });

            gl.End();
            openGLControl1.Focus();
        }

        private void segmentButtonClick(object sender, EventArgs e) => BuildSegment();

        private void BuildQuadrangle()
        {
            var shift = new Random().Next(2, 7);
            PrepareToDraw();
            gl.Begin(OpenGL.GL_QUADS);

            BuildFigure(new List<PointF>() { new PointF(-shift, -shift), new PointF(-shift, shift), new PointF(shift, shift), new PointF(shift, -shift) });
            gl.End();
            openGLControl1.Focus();
        }

        private void quadrangleButtonClick(object sender, EventArgs e) => BuildQuadrangle();

        private void BuildPentagon()
        {
            PrepareToDraw();
            gl.Begin(OpenGL.GL_POLYGON);
            var shift = new Random().Next(2, 7);
            BuildFigure(new List<PointF>() { new PointF(-shift, -shift), new PointF(-shift, 0), new PointF(0, 0), new PointF(0, -shift), new PointF(shift/2, -shift/2), new PointF(0, 0) });
            gl.End();
            openGLControl1.Focus();
        }

        private void pentagonButtonClick(object sender, EventArgs e) => BuildPentagon();
        

        private void openGLControl1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (figure.Count)
            {
                case 2:
                    BuildSegment();
                    break;
                case 3:
                    BuildTriangle();
                    break;
                case 4:
                    BuildQuadrangle();
                    break;
                default:
                    BuildPentagon();
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "Построить")
            {
                rotation = 0;
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.PushMatrix();
                gl.LoadIdentity();
                gl.Ortho2D(0, openGLControl1.Width, openGLControl1.Height, 0);
                gl.Begin(OpenGL.GL_POLYGON);
                for (int i = 0; i < points_for_drawing.Count; i++)
                {
                    switch (i % 3)
                    {
                        case 0:
                            gl.Color(0f, 0f, 1f);
                            break;
                        case 1:
                            gl.Color(0f, 1f, 0f);
                            break;
                        case 2:
                            gl.Color(1f, 0f, 0f);
                            break;
                    }

                    gl.Vertex(points_for_drawing[i].Key, points_for_drawing[i].Value);
                }
                gl.End();
                gl.PopMatrix();
                button5.Text = "Очистить";
            }
            else
            {

                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                points_for_drawing.Clear();
                button5.Text = "Построить";
            }
            openGLControl1.Focus();
        }


        private void RotateFigure(double rotationDiff)
        {
            rotation += rotationDiff;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -20.0f);
            gl.Rotate(rotation, 0, 0, 1);
            gl.Begin(OpenGL.GL_POLYGON);

            for (int i = 0; i < figure.Count; i++)
                gl.Vertex(figure[i].X, figure[i].Y);
            gl.End();
            openGLControl1.Focus();
        }

        private void openGLControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'a')
                RotateFigure(5);
            if (e.KeyChar == 'd')
                RotateFigure(-5);
        }



        void Cube()
        {
            gl.Begin(OpenGL.GL_QUADS);
            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f); 
            gl.TexCoord(1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);  
            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex(1.0f, 1.0f, 1.0f);
            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex(-1.0f, 1.0f, 1.0f);  

            gl.TexCoord(1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex(-1.0f, 1.0f, -1.0f); 
            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex(1.0f, 1.0f, -1.0f);
            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f); 

            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex(-1.0f, 1.0f, -1.0f);
            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex(-1.0f, 1.0f, 1.0f); 
            gl.TexCoord(1.0f, 0.0f);
            gl.Vertex(1.0f, 1.0f, 1.0f); 
            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex(1.0f, 1.0f, -1.0f); 

            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f); 
            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.TexCoord(1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f); 

            gl.TexCoord(1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f); 
            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex(1.0f, 1.0f, -1.0f);  
            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex(1.0f, 1.0f, 1.0f);
            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f); 

            gl.TexCoord(0.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.TexCoord(1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f); 
            gl.TexCoord(1.0f, 1.0f);
            gl.Vertex(-1.0f, 1.0f, 1.0f);  
            gl.TexCoord(0.0f, 1.0f);
            gl.Vertex(-1.0f, 1.0f, -1.0f); 

            gl.End();
        }



        private void button7_Click(object sender, EventArgs e)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity(); 
            if (radioButton1.Checked)
                gl.Perspective(45.0f, openGLControl1.Width / openGLControl1.Height, 0.1f, 100.0f);
            else
                gl.Ortho(-8, 8, -8, 8, 0.1, 100);
            

            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            double angle = Math.PI / 18;
            double rad = 4;
            for (int i = 0; i < 100; i++)
            {
                _pause(100);
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                gl.Color(0.8f, 0.498039f, 0.196078f);

                gl.LoadIdentity();
                gl.Translate(0.0f, 0.3f, -20.0f);
                gl.Translate(rad * Math.Cos(angle), 0, rad * Math.Sin(angle));
                gl.Scale(1.5, 1.5, 0);
                Cube();

                gl.Color((float)0.207, (float)0.194, (float)0.194);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(-3f, 0.0f, -20.0f);
                gl.Translate(rad * Math.Cos(angle), 0, rad * Math.Sin(angle));
                gl.Scale(1.25, 1.25, 0);
                Cube();

                gl.Color(0.5f, 0.35f, 0.05f);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(3.0f, -0.2f, -20.0f);
                gl.Translate(rad * Math.Cos(angle), 0, rad * Math.Sin(angle));
                Cube();

                angle += Math.PI / 18;
            }


            openGLControl1.Focus();
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            if (radioButton1.Checked)
                gl.Perspective(45.0f, openGLControl1.Width / openGLControl1.Height, 0.1f, 100.0f);
            else
                gl.Ortho(-8, 8, -8, 8, 0.1, 100);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            gl.Color(0.8f, 0.498039f, 0.196078f);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(2.0f, 0.3f, -19.0f);
            gl.Scale(1.5, 1.5, 0);
            Cube();

            gl.Color((float)0.207, (float)0.194, (float)0.194);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(-0.5f , 0.0f, -18.0f);
            gl.Scale(1.25, 1.25, 0);
            Cube();

            gl.Color(0.5f, 0.35f, 0.05f);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(4.5f, -0.2f, -20.0f);
            Cube();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity(); 
            if (radioButton1.Checked)
                gl.Perspective(45.0f, openGLControl1.Width / openGLControl1.Height, 0.1f, 100.0f);
            else
                gl.Ortho(-8, 8, -8, 8, 0.1, 100);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            double angle = Math.PI / 18;
            double rad = 3.5;
            for (int i = 0; i < 100; i++)
            {
                _pause(100);
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                gl.Color(0.8f, 0.498039f, 0.196078f);

                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(2f, 0.3f, -20.0f);
                gl.Scale(1.5, 1.5, 0);
                gl.Rotate(0, -(float)angle * (float)rad * 12, 0);
                Cube();

                gl.Color((float)0.207, (float)0.194, (float)0.194);

                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(0, 0.0f, -20.0f);
                gl.Translate(2 - rad * Math.Cos(angle), 0, -rad * Math.Sin(angle));
                gl.Scale(1.25, 1.25, 0);
                Cube();

                gl.Color(0.5f, 0.35f, 0.05f);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(0f, -0.2f, -20.0f);
                gl.Translate(2 + rad * Math.Cos(angle), 0, rad * Math.Sin(angle));
                Cube();

                angle += Math.PI / 18;
            }


            openGLControl1.Focus();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            if (radioButton1.Checked)
                gl.Perspective(45.0f, openGLControl1.Width / openGLControl1.Height, 0.1f, 100.0f);
            else
                gl.Ortho(-8, 8, -8, 8, 0.1, 100);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            double angle = Math.PI / 18;
            double rad = 3;
            for (int i = 0; i < 100; i++)
            {
                _pause(100);
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                gl.Color(0.8f, 0.498039f, 0.196078f);

                gl.LoadIdentity();
                gl.Translate(2f, 0.3f, -20.0f);
                gl.Scale(1.5, 1.5, 0);
                gl.Rotate(0, -(float)angle * (float)rad * 10, 0);
                Cube();

                gl.Color((float)0.207, (float)0.194, (float)0.194);

                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(2 - 3f, 0.0f, -20.0f);
                gl.Scale(1.25, 1.25, 0);
                gl.Rotate(0, -(float)angle * (float)rad * 10, 0);
                Cube();

                gl.Color(0.5f, 0.35f, 0.05f);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(2 + 2.5f, -0.2f, -20.0f);
                gl.Rotate(0, -(float)angle * (float)rad * 10, 0);
                Cube();

                angle += Math.PI / 18;
            }


            openGLControl1.Focus();
        }
    }
}

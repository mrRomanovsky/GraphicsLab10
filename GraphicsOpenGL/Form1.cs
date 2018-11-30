﻿using System;
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
        double x, y, z = 0;
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
            //BuildTriangle();
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

            // Завершаем работу
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
            // Завершаем работу
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


        void LoadGLTextures(uint[] Textures, string path)
        {
            const int FC_TextureCount = 1;
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            // Загрузка текстурки (про альфу еще не думал. рано мне)
            gl.GenTextures(FC_TextureCount, Textures);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);

            Bitmap bitmap = new Bitmap(path);
            BitmapData data = bitmap.LockBits(
                            new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                            ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0,
                            OpenGL.GL_RGB, data.Width, data.Height, 0,
                            OpenGL.GL_BGR, OpenGL.GL_UNSIGNED_BYTE, data.Scan0);
            bitmap.UnlockBits(data);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
        }



        void Cube()
        {
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(1, 1.0, 1.0, 0.0);
            // Передняя грань
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, 1.0f);  // Низ лево
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(1.0f, -1.0f, 1.0f);  // Низ право
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(1.0f, 1.0f, 1.0f);  // Верх право
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, 1.0f);  // Верх лево

            // Задняя грань
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, -1.0f);  // Низ право
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, -1.0f);  // Верх право
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(1.0f, 1.0f, -1.0f);  // Верх лево
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(1.0f, -1.0f, -1.0f);  // Низ лево

            // Верхняя грань
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, -1.0f);  // Верх лево
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(-1.0f, 1.0f, 1.0f);  // Низ лево
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(1.0f, 1.0f, 1.0f);  // Низ право
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(1.0f, 1.0f, -1.0f);  // Верх право

            // Нижняя грань
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(-1.0f, -1.0f, -1.0f);  // Верх право
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(1.0f, -1.0f, -1.0f);  // Верх лево
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(1.0f, -1.0f, 1.0f);  // Низ лево
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, 1.0f);  // Низ право

            // Правая грань
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(1.0f, -1.0f, -1.0f);  // Низ право
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(1.0f, 1.0f, -1.0f);  // Верх право
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(1.0f, 1.0f, 1.0f);  // Верх лево
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(1.0f, -1.0f, 1.0f);  // Низ лево

            // Левая грань
            gl.TexCoord(0.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, -1.0f);  // Низ лево
            gl.TexCoord(1.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, 1.0f);  // Низ право
            gl.TexCoord(1.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, 1.0f);  // Верх право
            gl.TexCoord(0.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, -1.0f);  // Верх лево

            gl.End();
        }



        private void button7_Click(object sender, EventArgs e)
        {
            const int FC_TextureCount = 1;  // извращенец, да. 
            uint[] Textures = new uint[FC_TextureCount];

            LoadGLTextures(Textures, "1.jpg");
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);// Загрузка текстур
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.DepthFunc(OpenGL.GL_LESS);   // Тип теста глубины
            gl.Enable(OpenGL.GL_DEPTH_TEST);// разрешить тест глубины
            gl.ShadeModel(OpenGL.GL_SMOOTH);// разрешить плавное цветовое сглаживание
            gl.MatrixMode(OpenGL.GL_PROJECTION);// Выбор матрицы проекции
            gl.LoadIdentity();       // Сброс матрицы проекции
            if (radioButton1.Checked)
                gl.Perspective(45.0f, openGLControl1.Width / openGLControl1.Height, 0.1f, 100.0f);
            else
                gl.Ortho(-8, 8, -8, 8, 0.1, 100);
            

            gl.MatrixMode(OpenGL.GL_MODELVIEW);// Выбор матрицы просмотра модели

            double angle = Math.PI / 18;
            double rad = 4;
            for (int i = 0; i < 100; i++)
            {
                _pause(100);
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                LoadGLTextures(Textures, "10.jpeg");
                gl.LoadIdentity();
                gl.Translate(0.0f, 0.0f, -20.0f);
                gl.Translate(rad * Math.Cos(angle), 0, rad * Math.Sin(angle));
                gl.Scale(1.5, 1.5, 0);
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);
                Cube();


                LoadGLTextures(Textures, "silver.png");
                // Вычислить соотношение геометрических размеров для окна
                gl.MatrixMode(OpenGL.GL_MODELVIEW);// Выбор матрицы просмотра модели
                gl.LoadIdentity();
                gl.Translate(-3f, 0.0f, -20.0f);
                gl.Translate(rad * Math.Cos(angle), 0, rad * Math.Sin(angle));
                gl.Scale(1.25, 1.25, 0);
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);
                Cube();


                LoadGLTextures(Textures, "16.jpg");
                gl.MatrixMode(OpenGL.GL_MODELVIEW);// Выбор матрицы просмотра модели
                gl.LoadIdentity();
                gl.Translate(3.0f, 0.0f, -20.0f);
                gl.Translate(rad * Math.Cos(angle), 0, rad * Math.Sin(angle));
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);
                Cube();

                angle += Math.PI / 18;
            }


            openGLControl1.Focus();
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            const int FC_TextureCount = 1;  // извращенец, да. 
            uint[] Textures = new uint[FC_TextureCount];
            
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.Enable(OpenGL.GL_COLOR_MATERIAL);
            gl.DepthFunc(OpenGL.GL_LESS);   // Тип теста глубины
            gl.Enable(OpenGL.GL_DEPTH_TEST);// разрешить тест глубины
            gl.ShadeModel(OpenGL.GL_SMOOTH);// разрешить плавное цветовое сглаживание
            gl.Viewport(0, 0, openGLControl1.Width, openGLControl1.Height);
            gl.MatrixMode(OpenGL.GL_PROJECTION);// Выбор матрицы проекции
            gl.LoadIdentity();       // Сброс матрицы проекции
            if (radioButton1.Checked)
                gl.Perspective(45.0f, openGLControl1.Width / openGLControl1.Height, 0.1f, 100.0f);
            else
                gl.Ortho(-8, 8, -8, 8, 0.1, 100);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            
            LoadGLTextures(Textures, "10.jpeg");
            // Вычислить соотношение геометриче
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(2.0f, 0.0f, -20.0f + 1.5);
            gl.Scale(1.5, 1.5, 0);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);
            Cube();


            LoadGLTextures(Textures, "silver.png");
            // Вычислить соотношение геометрических размеров для окна
            gl.MatrixMode(OpenGL.GL_MODELVIEW);// Выбор матрицы просмотра модели
            gl.LoadIdentity();
            gl.Translate(-2.5f + 2, 0.0f, -20.0f + 1.25);
            gl.Scale(1.25, 1.25, 0);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);
            Cube();


            LoadGLTextures(Textures, "16.jpg");
            gl.MatrixMode(OpenGL.GL_MODELVIEW);// Выбор матрицы просмотра модели
            gl.LoadIdentity();
            gl.Translate(2.5f + 2, 0.0f, -20.0f);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);
            Cube();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            const int FC_TextureCount = 1;  // извращенец, да. 
            uint[] Textures = new uint[FC_TextureCount];
            
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);// Загрузка текстур
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.DepthFunc(OpenGL.GL_LESS);   // Тип теста глубины
            gl.Enable(OpenGL.GL_DEPTH_TEST);// разрешить тест глубины
            gl.ShadeModel(OpenGL.GL_SMOOTH);// разрешить плавное цветовое сглаживание
            gl.MatrixMode(OpenGL.GL_PROJECTION);// Выбор матрицы проекции
            gl.LoadIdentity();       // Сброс матрицы проекции
            if (radioButton1.Checked)
                gl.Perspective(45.0f, openGLControl1.Width / openGLControl1.Height, 0.1f, 100.0f);
            else
                gl.Ortho(-8, 8, -8, 8, 0.1, 100);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);// Выбор матрицы просмотра модели

            double angle = Math.PI / 18;
            double rad = 3.5;
            for (int i = 0; i < 100; i++)
            {
                _pause(100);
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                LoadGLTextures(Textures, "10.jpeg");
                // Вычислить соотношение геометриче
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(2f, 0.0f, -20.0f);
                gl.Scale(1.5, 1.5, 0);
                gl.Rotate(0, -(float)angle * (float)rad * 12, 0);
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);
                Cube();


                LoadGLTextures(Textures, "silver.png");
                // Вычислить соотношение геометрических размеров для окна
                gl.MatrixMode(OpenGL.GL_MODELVIEW);// Выбор матрицы просмотра модели
                gl.LoadIdentity();
                gl.Translate(0, 0.0f, -20.0f);
                gl.Translate(2 - rad * Math.Cos(angle), 0, -rad * Math.Sin(angle));
                gl.Scale(1.25, 1.25, 0);
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);
                Cube();
                
                LoadGLTextures(Textures, "16.jpg");
                gl.MatrixMode(OpenGL.GL_MODELVIEW);// Выбор матрицы просмотра модели
                gl.LoadIdentity();
                gl.Translate(0f, 0.0f, -20.0f);
                gl.Translate(2 + rad * Math.Cos(angle), 0, rad * Math.Sin(angle));
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);
                Cube();

                angle += Math.PI / 18;
            }


            openGLControl1.Focus();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            const int FC_TextureCount = 1;  // извращенец, да. 
            uint[] Textures = new uint[FC_TextureCount];
            
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);// Загрузка текстур
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.DepthFunc(OpenGL.GL_LESS);   // Тип теста глубины
            gl.Enable(OpenGL.GL_DEPTH_TEST);// разрешить тест глубины
            gl.ShadeModel(OpenGL.GL_SMOOTH);// разрешить плавное цветовое сглаживание
            gl.MatrixMode(OpenGL.GL_PROJECTION);// Выбор матрицы проекции
            gl.LoadIdentity();       // Сброс матрицы проекции
            if (radioButton1.Checked)
                gl.Perspective(45.0f, openGLControl1.Width / openGLControl1.Height, 0.1f, 100.0f);
            else
                gl.Ortho(-8, 8, -8, 8, 0.1, 100);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);// Выбор матрицы просмотра модели

            double angle = Math.PI / 18;
            double rad = 3;
            for (int i = 0; i < 100; i++)
            {
                _pause(100);
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                LoadGLTextures(Textures, "10.jpeg");
                gl.LoadIdentity();
                gl.Translate(2f, 0.0f, -20.0f);
                gl.Scale(1.5, 1.5, 0);
                gl.Rotate(0, -(float)angle * (float)rad * 10, 0);
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);
                Cube();


                LoadGLTextures(Textures, "silver.png");
                // Вычислить соотношение геометрических размеров для окна
                gl.MatrixMode(OpenGL.GL_MODELVIEW);// Выбор матрицы просмотра модели
                gl.LoadIdentity();
                gl.Translate(2 - 3f, 0.0f, -20.0f);
                gl.Scale(1.25, 1.25, 0);
                gl.Rotate(0, -(float)angle * (float)rad * 10, 0);
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);
                Cube();


                LoadGLTextures(Textures, "16.jpg");
                gl.MatrixMode(OpenGL.GL_MODELVIEW);// Выбор матрицы просмотра модели
                gl.LoadIdentity();
                gl.Translate(2 + 2.5f, 0.0f, -20.0f);
                gl.Rotate(0, -(float)angle * (float)rad * 10, 0);
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, Textures[0]);
                Cube();

                angle += Math.PI / 18;
            }


            openGLControl1.Focus();
        }
    }
}

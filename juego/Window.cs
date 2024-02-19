using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace juego
{
    internal class Window
    {
        /// <summary>
        /// ancho de la consola
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// largo de la consola
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// cambiar de color la consola
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// limite superieur pour le jeu
        /// </summary>
        public Point MaxHeight { get; set; }

        /// <summary>
        /// limite inferieur pour le jeu
        /// </summary>
        public Point MinHeight { get; set; }


        /// <summary>
        /// constructor por defecto
        /// </summary>
        public Window() 
        {

        }

        /// <summary>
        /// constructor con la altura y el ancho de la consola
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Window(int width, int height, ConsoleColor color,Point maxHeight,Point minHeight)
        {
            // actualizamos el ancho dado en el constructor
            Width = width;
            // actualizamos la altura dada en el constructor
            Height = height;
            // actualizar el color
            Color = color;
            // actualizar limite superieur pour le jeu
            MaxHeight = maxHeight;
            // actualizar limite inferior pour le jeu
            MinHeight = minHeight;
            // lamamos el metodo que inicia la ventana con el ancho y la latura y el titulo 
            Init();
        }

        /// <summary>
        /// metodo para crear la consola con el ancho la altura y el titulo 
        /// </summary>
        private void Init()
        {
            Console.SetWindowSize(Width, Height);
            Console.Title = "Space Invader";
            Console.BackgroundColor = Color;
            Console.CursorVisible = false;
            Console.Clear();
        }

        /// <summary>
        /// dibujar el marco del juego
        /// </summary>
        public void DrawFrame()
        {
            for (int i = MaxHeight.X; i <= MinHeight.X; i++)
            {
                Console.SetCursorPosition(i,MaxHeight.Y);
                Console.Write("═");
                Console.SetCursorPosition(i,MinHeight.Y);
                Console.Write("═");
            }
            for (int i = MaxHeight.Y; i <= MinHeight.Y; i++)
            {
                Console.SetCursorPosition(MaxHeight.X, i);
                Console.Write("║");
                Console.SetCursorPosition(MinHeight.X, i);
                Console.Write("║");

            }

            Console.SetCursorPosition(MaxHeight.X,MaxHeight.Y);
            Console.Write("╔");
            Console.SetCursorPosition(MaxHeight.X,MinHeight.Y);
            Console.WriteLine("╚");
            Console.SetCursorPosition(MinHeight.X,MaxHeight.Y);
            Console.WriteLine("╗");
            Console.SetCursorPosition(MinHeight.X, MinHeight.Y);
            Console.WriteLine("╝");
        }

    }
}

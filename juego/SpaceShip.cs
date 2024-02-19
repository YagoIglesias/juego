using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Configuration;
using juego;

namespace juego
{
    internal class SpaceShip
    {
        /// <summary>
        /// vida de la nave
        /// </summary>
        public float Live {  get; set; }

        /// <summary>
        /// velocidad de desplazamiento
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// posicion de la nave
        /// </summary>
        public Point Position { get; set; } 

        /// <summary>
        /// distance parcourru
        /// </summary>
        public Point Range { get; set; }

        /// <summary>
        /// color de la nave
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// constructor de la ventana
        /// </summary>
        public Window WindowSpaceShip { get; set; }

        /// <summary>
        /// stocker les positions du vaisseau apres chaque mouvement
        /// </summary>
        public List<Point> SpaceShipListPosition { get; set; }

        /// <summary>
        /// list pour stocker les misile
        /// </summary>
        public List<Missile> Missile { get; set; }

        /// <summary>
        /// contructor de la nave espacial del jugador 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="window"></param>
        public SpaceShip(Point position, ConsoleColor color, Window window)
        {
            // actualizar la position
            Position = position;
            // actualizar el color
            Color = color;
            // actualizar la ventana
            WindowSpaceShip = window;
            // la valor de la variable vida es de 100 puntos
            Live = 100;
            //declarer la nouvelle list
            SpaceShipListPosition = new List<Point>();
            // declaration de la list des misile
            Missile = new List<Missile>();
            
        }

        /// <summary>
        /// metodo para dibujar la nave
        /// </summary>
        public void DrawSpaceShip()
        {
            // damos el color de la nave
            Console.ForegroundColor = Color;
            // posiciones de los ejes X e Y que corresponden a la posicion que se declara al principio
            int positionX = Position.X;
            int positionY = Position.Y;

            // dibujar la nave con las cordenadas de los ejes
            Console.SetCursorPosition(positionX + 3,positionY);
            Console.Write("A");
            Console.SetCursorPosition(positionX + 1,positionY + 1);
            Console.Write("<{X}>");
            Console.SetCursorPosition(positionX, positionY + 2);
            Console.WriteLine("± W W ±");

            // vaciar la lista 
            SpaceShipListPosition.Clear();

            // guardar en la lista la posicion del caracter A
            SpaceShipListPosition.Add(new Point(positionX+3,positionY));
            // guardar en la lista la posicion del caracter <{X}>
            SpaceShipListPosition.Add(new Point(positionX + 1, positionY + 1));
            SpaceShipListPosition.Add(new Point(positionX + 2, positionY + 1));
            SpaceShipListPosition.Add(new Point(positionX + 3, positionY + 1));
            SpaceShipListPosition.Add(new Point(positionX + 4, positionY + 1));
            SpaceShipListPosition.Add(new Point(positionX + 5, positionY + 1));
            // guardar en la lista la posicion del caracter ± W W ±
            SpaceShipListPosition.Add(new Point(positionX,positionY+2));
            SpaceShipListPosition.Add(new Point(positionX+2, positionY + 2));
            SpaceShipListPosition.Add(new Point(positionX+4, positionY + 2));
            SpaceShipListPosition.Add(new Point(positionX+6, positionY + 2));

        }

        /// <summary>
        /// metodo para borar la ultima posicion de la nave en la consola 
        /// </summary>
        public void ConsoleClear()
        {
            foreach (Point charPosition in SpaceShipListPosition)
            {
                Console.SetCursorPosition(charPosition.X,charPosition.Y);
                Console.Write(" ");
            }
        }

        /// <summary>
        /// methode pour valider les touches et leur instruction
        /// </summary>
        /// <param name="range"></param>
        /// <param name="speed"></param>
        public void KeyPressed( Point range, int speed)
        {
            // variable pour verifier la touche presse
            ConsoleKeyInfo pressed = Console.ReadKey();

            if (pressed.Key == ConsoleKey.RightArrow)
            {
                range = new Point(+1, 0);
            }

            if (pressed.Key == ConsoleKey.LeftArrow)
            {
                range = new Point(-1, 0);
            }

            //distance de l'axe X * la rapidite 
            range.X *= speed;
            //distance de l'axe Y * la rapidite 
            range.Y *= speed;
            // misse a jour de la position avec la distance parcourrue par le vaiseau * la rapidite
            Position = new Point(Position.X + range.X, Position.Y + range.Y);

            // tirer avec les misile
            if (pressed.Key == ConsoleKey.DownArrow)
            {
                // instencier un misile avec les proprietes du constructeur
                Missile missile = new Missile(position:new Point(Position.X+6,Position.Y+2),
                    color:ConsoleColor.Red,type:juego.Missile.MissileType.Standar);
                //ajouter le misile a la liste
                Missile.Add(missile);
            }

            if (pressed.Key == ConsoleKey.UpArrow)
            {
                Missile missile = new Missile(position: new Point(Position.X, Position.Y + 2),
                color: ConsoleColor.Red, type: juego.Missile.MissileType.Standar);

                Missile.Add(missile);
            }

            if (pressed.Key == ConsoleKey.Spacebar)
            {
                Missile missile = new Missile(position: new Point(Position.X +2, Position.Y -2),
                color: ConsoleColor.Red, type:juego.Missile.MissileType.Special);

                Missile.Add(missile);
            }

        }

        /// <summary>
        /// methode pour pas delimiter le mouvement du vaiseau jusqu'au cadre du jeu 
        /// </summary>
        /// <param name="range"></param>
        public void Collisions(Point range)
        {
            // position sans compter le cadre du jeu 
            Point positionAuxiliary = new Point(Position.X + range.X, Position.Y + range.Y);

            //coter gauche
            // si la position sans le cadre est <= au cadre alors
            // on augmente de 1 la position pour pas l'ecraser
            if (positionAuxiliary.X <= WindowSpaceShip.MaxHeight.X)
            {
                positionAuxiliary.X = WindowSpaceShip.MaxHeight.X+1;
            }
            //coter droit
            if (positionAuxiliary.X + 6 >= WindowSpaceShip.MinHeight.X)
            {
                positionAuxiliary.X = WindowSpaceShip.MinHeight.X - 8;// car le dernier caracter
                                                                      // du vaiseau est a la position X+6
            }
            // cote superieur
            if (positionAuxiliary.Y <= WindowSpaceShip.MaxHeight.Y)
            {
                positionAuxiliary.Y = WindowSpaceShip.MaxHeight.Y + 1;
            }
            // cote inferieur
            if (positionAuxiliary.Y + 2 >= WindowSpaceShip.MinHeight.Y)
            {
                positionAuxiliary.Y = WindowSpaceShip.MinHeight.Y - 3;
            }

            // position mis ajour avec la delimitation du cadre
            Position = positionAuxiliary;

        }

        /// <summary>
        /// metodo para mover la nave 
        /// </summary>
        public void Move(int Speed)
        {
            // verifier si les touches preses sont correctes
            if (Console.KeyAvailable)
            {
                // effacer la position precedante du vaiseau 
                ConsoleClear();
                // distance de deplacement du vaiseau 
                Point range = new Point();
                // verification de la touche 
                KeyPressed( range, Speed);
                // point maximeum avant d'ecraser le cadre
                Collisions(range);
                //dessiner le vaiseau 
                DrawSpaceShip();
            }
            Information(); 
        }

        /// <summary>
        /// méthode pour tirer un misil
        /// </summary>
        public void Shot()
        {
            for (int i = 0; i < Missile.Count; i++)
            {
                if (Missile[i].MoveMissile(1, WindowSpaceShip.MaxHeight.Y))
                {
                    Missile.Remove(Missile[i]);
                }
                
            }
        }

        public void Information()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WindowSpaceShip.MaxHeight.X, WindowSpaceShip.MaxHeight.Y - 1);
            Console.Write("VIE : " + Convert.ToInt32(Live) + " %");
        }

    }
}

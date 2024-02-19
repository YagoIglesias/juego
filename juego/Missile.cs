using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace juego
{
    internal class Missile
    {
        /// <summary>
        /// position des misile
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// couleur du misile
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// sortes de misiles
        /// </summary>
        public enum MissileType
        {
            Standar,Special

        }

        /// <summary>
        /// mise a jour du type de misile
        /// </summary>
        public MissileType Type { get; set; }

        /// <summary>
        /// stocker la position des misile
        /// </summary>
        public List<Point> PositionMissile { get; set; }

        /// <summary>
        /// stocker l'heure 
        /// </summary>
        private DateTime _time;
      
        /// <summary>
        /// contructeur des misiles
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="type"></param>
        public Missile(Point position,ConsoleColor color, MissileType type) 
        {
            // misse a jour de la position 
            Position = position;
            // mise a jour de la couleur 
            Color = color;
            // mise a jour du type
            Type = type;
            // declaration de la liste
            PositionMissile = new List<Point>();
            // stocker la date et heure actuelle
            _time = DateTime.Now;

        }

        /// <summary>
        /// methode pour dessiner les missiles
        /// </summary>
        public void DrawMissile()
        {
            // axes X et Y
            int posicionX = Position.X;
            int posicionY = Position.Y; 

            // couleur 
            Console.ForegroundColor = Color;
            // vider la liste des misiles
            PositionMissile.Clear();
            // type de misile créé
            switch (Type)
            {
                // misile normal
                case MissileType.Standar:
                    // dessiner le misile a la position du vaiseau 
                    Console.SetCursorPosition(posicionX, posicionY);
                    Console.Write("o");
                    // ajouter le misile tirer dans la list
                    PositionMissile.Add(new Point(posicionX, posicionY));
                break;

                // misile special 
                case MissileType.Special:
                    //// dessiner le misile a la position du vaiseau 
                    Console.SetCursorPosition(posicionX+1,posicionY);
                    Console.Write("_");
                    Console.SetCursorPosition(posicionX,posicionY+1);
                    Console.WriteLine("( )");
                    Console.SetCursorPosition(posicionX+1,posicionY+2);
                    Console.Write("W");
                    // ajouter le misile tirer dans la list
                    PositionMissile.Add(new Point(posicionX+1,posicionY));
                    PositionMissile.Add(new Point(posicionX, posicionY + 1));
                    PositionMissile.Add(new Point(posicionX + 2, posicionY + 1));
                    PositionMissile.Add(new Point(posicionX + 1, posicionY + 2));
                break;
            }
        }

        /// <summary>
        /// methode pour effacer la position precedente des misiles
        /// </summary>
        public void ConsoleClear()
        {
            foreach (Point charMissile in PositionMissile)
            {
                Console.SetCursorPosition(charMissile.X, charMissile.Y);
                Console.Write(" ");
            }
        }

        /// <summary>
        /// methode pour le mouvement des misiles
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public bool MoveMissile(int speed,int limit)
        {
            // verifier 30 millisecondes de l'heure actuelle
            // apres 30 millisecondes du mouvement on peut rebouger
            if (DateTime.Now >_time.AddMilliseconds(30))
            {
                // efacer la position des misiles precedents
                ConsoleClear();
                // verifier le type de misiles tirer 
                switch (Type)
                {
                    // misile normal
                    case MissileType.Standar:
                        // position du misile mise ajour avec la position precendent dans l'axe X
                        // moins la vitese dans l'axe Y  
                        Position = new Point(Position.X, Position.Y - speed);
                        // si le misile attients le point de collision retourne vrais 
                        if (Position.Y <= limit)
                        {
                            return true;
                        }
                        break;

                    case MissileType.Special:
                        Position = new Point(Position.X, Position.Y - speed);
                        if (Position.Y <= limit)
                        {
                            return true;
                        }
                        break;

                }
                // dessiner le missile 
                DrawMissile();
                // actualiser l'heure actuelle
                _time = DateTime.Now;
            }

            // si n'attaint pas la limite retourne faux
            return false;

        }

    }
}

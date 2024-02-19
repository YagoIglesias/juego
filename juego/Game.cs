using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace juego
{
    internal class Game
    {
        // declarer la fenetre
        Window window;
        // declarar la nave del jugador
        SpaceShip player1;

        public Game() 
        {
            // creation de la fenetre
            window = new Window
            (width: 160, height: 45,
            color: ConsoleColor.Black,
            maxHeight: new Point(3, 5), minHeight: new Point(154, 40));
            // dibujar el marco del juego
            window.DrawFrame();

            // construction du vaiseau
            player1 = new SpaceShip(position:new Point(75,35),color: ConsoleColor.White,window:window);
            // desinner le vaiseau 
            player1.DrawSpaceShip();

            // faire bouger le vaiseau tant que 
            while (true)
            {
                // mouvement du vaiseau 
                player1.Move(2);
                player1.Shot();
            }
    
        }

        

    }
}

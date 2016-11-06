using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.Graphics;
using SFML.Window;

using Snake;

namespace SnakeSFML
{
    class Program
    {
        private static RenderWindow window;
        private static SnakeDirection direction;
        private static RenderTexture texture;
        private static List<SnakeParticle> snakePartsList;

        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
            
        }

        static void SnakeAddParticleToList(Texture texture, float x, float y)
        {
            SnakeParticle snkprt = new SnakeParticle(texture, x, y);
            snakePartsList.Add(snkprt);
        }

        static void OnKeyPressed(object window, EventArgs e)
        {
            KeyEventArgs key = (KeyEventArgs)e;

            if (key.Code.Equals(Keyboard.Key.W))
            {
                snakePartsList.Insert(0, new SnakeParticle(texture.Texture, snakePartsList[0].GetX(), snakePartsList[0].GetY() - 20));
                snakePartsList.Remove(snakePartsList[snakePartsList.Count - 1]);
                Console.WriteLine("Moved up");
            }
            else if (key.Code.Equals(Keyboard.Key.S))
            {
                snakePartsList.Insert(0, new SnakeParticle(texture.Texture, snakePartsList[0].GetX(), snakePartsList[0].GetY() + 20));
                snakePartsList.Remove(snakePartsList[snakePartsList.Count - 1]);
                Console.WriteLine("Moved down");
            }

            else if (key.Code.Equals(Keyboard.Key.A))
            {
                snakePartsList.Insert(0, new SnakeParticle(texture.Texture, snakePartsList[0].GetX() - 20, snakePartsList[0].GetY()));
                snakePartsList.Remove(snakePartsList[snakePartsList.Count - 1]);
                Console.WriteLine("Moved left");
            }

            else if (key.Code.Equals(Keyboard.Key.D))
            {
                snakePartsList.Insert(0, new SnakeParticle(texture.Texture, snakePartsList[0].GetX() + 20, snakePartsList[0].GetY()));
                snakePartsList.Remove(snakePartsList[snakePartsList.Count - 1]);
                Console.WriteLine("Moved right");
            }
        }

        static void Main()
        {
            window = new RenderWindow(new VideoMode(800, 600), "SnakeSFML");
            window.Closed += new EventHandler(OnClose);
            
            texture = new RenderTexture(20, 20);
            texture.Clear(Color.White);
            snakePartsList = new List<SnakeParticle>();
            direction = new SnakeDirection();

            SnakeAddParticleToList(texture.Texture, 20, 20);
            SnakeAddParticleToList(texture.Texture, 20, 40);
            SnakeAddParticleToList(texture.Texture, 20, 60);
            SnakeAddParticleToList(texture.Texture, 20, 80);
            SnakeAddParticleToList(texture.Texture, 20, 100);

            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);

            while (window.IsOpen)
            {
                window.Clear(Color.Black);
                foreach (SnakeParticle obj in snakePartsList)
                    obj.Draw(window);

                window.DispatchEvents();
                window.Display();
            }
        }
    }
}

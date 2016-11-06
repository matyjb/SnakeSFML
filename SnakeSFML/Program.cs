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
        private static List<SnakeParticle> snakePartsList;

        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
            
        }

        static void OnKeyPressed(object window, EventArgs e)
        {
            KeyEventArgs key = (KeyEventArgs)e;

            if (key.Code.Equals(Keyboard.Key.W))
                snakePartsList[0].MoveBy(0, 20);

            else if (key.Code.Equals(Keyboard.Key.S))
                snakePartsList[0].MoveBy(0, -20);

            else if (key.Code.Equals(Keyboard.Key.A))
                snakePartsList[0].MoveBy(20, 0);

            else if (key.Code.Equals(Keyboard.Key.D))
                snakePartsList[0].MoveBy(-20, 0);
        }

        static void Main()
        {
            window = new RenderWindow(new VideoMode(800, 600), "SnakeSFML");
            window.Closed += new EventHandler(OnClose);
            
            RenderTexture texture = new RenderTexture(20, 20);
            texture.Clear(Color.White);

            snakePartsList = new List<SnakeParticle>();
            SnakeParticle snkprt = new SnakeParticle(texture.Texture, 20, 20);
            snakePartsList.Add(snkprt);

            
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            

            foreach (SnakeParticle obj in snakePartsList)
                obj.Draw(window);
            
            
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

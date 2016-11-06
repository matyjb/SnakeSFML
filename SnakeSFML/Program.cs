using System;
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
        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
            
        }

        static void MoveSnake(Keyboard.Key key, object window, object snakepart)
        {
            SnakeParticle snkprt = (SnakeParticle)snakepart;

            if (key == Keyboard.Key.W)
                snkprt.MoveBy(0, 20);

            else if (key == Keyboard.Key.S)
                snkprt.MoveBy(0, -20);

            else if (key == Keyboard.Key.A)
                snkprt.MoveBy(20, 0);

            else if (key == Keyboard.Key.D)
                snkprt.MoveBy(-20, 0);
        }

        static void Main()
        {
            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "SnakeSFML");
            window.Closed += new EventHandler(OnClose);
            
            RenderTexture texture = new RenderTexture(20, 20);
            texture.Clear(Color.White);

            SnakeParticle snakepart = new SnakeParticle(texture.Texture, 20, 20);

            window.KeyPressed += (o, a) => MoveSnake(a.Code, window, snakepart);
            //window.KeyReleased += (o, a) => MoveSnake(a.Code, window, snakepart);

            snakepart.Draw(window);

            while (window.IsOpen)
            {
                window.Clear(Color.Black);
                snakepart.Draw(window);
                window.DispatchEvents();
                window.Display();
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;

namespace Snake
{
    enum Directions {N,S,E,W}
    
    class SnakeDirection
    {
        public Directions direction;

        public SnakeDirection(Directions directionn = Directions.S)
        {
            direction = directionn;
        }
    }

    class SnakeParticle
    {
        Sprite Part = new Sprite();

        public SnakeParticle(Texture texture, float x = 0, float y = 0)
        {
            Part.Texture = texture;
            Part.Position = new Vector2f(x,y);
        }

        public void MoveBy(float x, float y)
        {
            Part.Position = new Vector2f(Part.Position.X + x, Part.Position.Y + y);
            Console.WriteLine("Moved by x = " + x + " | y = " + y);
        }

        public void MoveTo(float x, float y)
        {
            Part.Position = new Vector2f(x, y);
        }

        public void Draw(object window)
        {
            RenderWindow windoww = (RenderWindow)window;
            windoww.Draw(Part);
        }

        public float GetX()
        {
            return Part.Position.X;
        }

        public float GetY()
        {
            return Part.Position.Y;
        }
    }

}

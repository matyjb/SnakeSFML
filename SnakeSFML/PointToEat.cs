using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;

namespace PointToEat
{
    class PointToEat
    {
        Sprite Point = new Sprite();

        public PointToEat(Texture texture, float x = 0, float y = 0)
        {
            Point.Texture = texture;
            Point.Position = new Vector2f(x, y);
        }

        public void MoveBy(float x, float y)
        {
            Point.Position = new Vector2f(Point.Position.X + x, Point.Position.Y + y);
            Console.WriteLine("Moved by x = " + x + " | y = " + y);
        }

        public void MoveTo(float x, float y)
        {
            Point.Position = new Vector2f(x, y);
        }

        public void Draw(object window)
        {
            RenderWindow windoww = (RenderWindow)window;
            windoww.Draw(Point);
        }

        public float GetX()
        {
            return Point.Position.X;
        }

        public float GetY()
        {
            return Point.Position.Y;
        }
        
    }
}

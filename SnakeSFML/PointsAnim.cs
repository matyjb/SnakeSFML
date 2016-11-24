using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace SnakeSFML
{
    class PointAnim
    {
        public Sprite PAnim = new Sprite();
        public uint state = 1;

        public PointAnim(Texture texture, float x = 0, float y = 0, uint statee = 1)
        {
            
            PAnim.Texture = texture;
            PAnim.Position = new Vector2f(x, y);
            state = statee;
            PAnim.Origin = new Vector2f(5, 5);
        }
        public void MoveBy(float x, float y)
        {
            PAnim.Position = new Vector2f(PAnim.Position.X + x, PAnim.Position.Y + y);
            Console.WriteLine("PAnim moved by x = " + x + " | y = " + y);
        }

        public void MoveTo(float x, float y)
        {
            PAnim.Position = new Vector2f(x, y);
            Console.WriteLine("PAnim moved to x = " + x + " | y = " + y);
        }

        public void Draw(object window)
        {
            RenderWindow windoww = (RenderWindow)window;

            
            PAnim.Position = PAnim.Position + new Vector2f(-0.02f, -0.02f);
            windoww.Draw(PAnim);
            PAnim.Position = PAnim.Position + new Vector2f(0.04f * state, 0);
            windoww.Draw(PAnim);
            PAnim.Position = PAnim.Position + new Vector2f(0, 0.04f * state);
            windoww.Draw(PAnim);
            PAnim.Position = PAnim.Position + new Vector2f(-0.04f * state, 0);
            windoww.Draw(PAnim);
            PAnim.Position = PAnim.Position + new Vector2f(0,-0.04f * state);
          
        }

        public float GetX()
        {
            return PAnim.Position.X;
        }

        public float GetY()
        {
            return PAnim.Position.Y;
        }
        
    }

    
}

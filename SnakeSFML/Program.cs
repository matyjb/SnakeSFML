using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

using Snake;
using PointToEat;

namespace SnakeSFML
{
    class Program
    {
        private static RenderWindow window;
        private static SnakeDirection direction;
        private static RenderTexture textureW, textureR, textureSR;
        private static List<SnakeParticle> snakePartsList;
        private static List<PointAnim> pointsAnims;
        private static bool canStillChangeDirection;
        private static bool removeParticle = true;
        private static PointToEat.PointToEat pointToEat;
        private static bool MainGameOn = true;
        private static Font arialF = new Font("arial.ttf");

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

        static void MoveSnake(bool removeLastParticle)
        {
            if (direction.direction == Directions.N)
            {
                snakePartsList.Insert(0, new SnakeParticle(textureW.Texture, snakePartsList[0].GetX(), snakePartsList[0].GetY() - 20));

                //Console.WriteLine("Moved up");
            }
            else if (direction.direction == Directions.S)
            {
                snakePartsList.Insert(0, new SnakeParticle(textureW.Texture, snakePartsList[0].GetX(), snakePartsList[0].GetY() + 20));

                //Console.WriteLine("Moved down");
            }

            else if (direction.direction == Directions.W)
            {
                snakePartsList.Insert(0, new SnakeParticle(textureW.Texture, snakePartsList[0].GetX() - 20, snakePartsList[0].GetY()));

                //Console.WriteLine("Moved left");
            }

            else if (direction.direction == Directions.E)
            {
                snakePartsList.Insert(0, new SnakeParticle(textureW.Texture, snakePartsList[0].GetX() + 20, snakePartsList[0].GetY()));

                //Console.WriteLine("Moved right");
            }
            if (removeLastParticle)
                snakePartsList.Remove(snakePartsList[snakePartsList.Count - 1]);
        }

        static void OnKeyPressed(object window, EventArgs e)
        {
            KeyEventArgs key = (KeyEventArgs)e;
            if (canStillChangeDirection)
            {
                if (!(direction.direction.Equals(Directions.S) || direction.direction.Equals(Directions.N)))
                {
                    if (key.Code.Equals(Keyboard.Key.W))
                    {
                        direction.direction = Directions.N;
                        Console.WriteLine("Changed direction to N");
                        canStillChangeDirection = false;
                    }
                    else if (key.Code.Equals(Keyboard.Key.S))
                    {
                        direction.direction = Directions.S;
                        Console.WriteLine("Changed direction to S");
                        canStillChangeDirection = false;
                    }
                }
                else if (!(direction.direction.Equals(Directions.E) || direction.direction.Equals(Directions.W)))
                {
                    if (key.Code.Equals(Keyboard.Key.A))
                    {
                        direction.direction = Directions.W;
                        Console.WriteLine("Changed direction to W");
                        canStillChangeDirection = false;
                    }
                    else if (key.Code.Equals(Keyboard.Key.D))
                    {
                        direction.direction = Directions.E;
                        Console.WriteLine("Changed direction to E");
                        canStillChangeDirection = false;
                    }
                }
            }
            if (key.Code.Equals(Keyboard.Key.Escape))
            {
                OnClose(window, null);
            }
            if (key.Code.Equals(Keyboard.Key.F))
            {
                removeParticle = false;
            }

        }

        static bool CheckPointPick()
        {
            if (snakePartsList[0].GetX().Equals(pointToEat.GetX()) && snakePartsList[0].GetY().Equals(pointToEat.GetY()))
            {
                return true;
            }
            return false;
        }

        static void PointGained()
        {
            Random rndX = new Random(); Random rndY = new Random();
            float modX = 1, modY = 1;
            bool isPointNotOnParticle = true;
            do
            {
                modX = rndX.Next(0, Convert.ToInt16(window.Size.X) / 20) * 20;
                modY = rndY.Next(0, Convert.ToInt16(window.Size.Y) / 20) * 20;

                foreach (SnakeParticle obj in snakePartsList)
                    if (!(obj.GetX().Equals(modX) && obj.GetY().Equals(modY))) isPointNotOnParticle = false;
                    else isPointNotOnParticle = true;

            } while (isPointNotOnParticle);

            pointToEat.MoveTo(modX, modY);
            removeParticle = false;
            Console.WriteLine("Point gained!");
        }

        static bool CheckOverlapingOrOutOfBorderOfSnake(List<SnakeParticle> snakePartsList, Vector2u windowSize)
        {
            for (int i = 1; i < snakePartsList.Count(); i++)
                if (snakePartsList[i].GetX() == snakePartsList[0].GetX())
                    if (snakePartsList[i].GetY() == snakePartsList[0].GetY())
                        return true;

            if (snakePartsList[0].GetX() > windowSize.X || snakePartsList[0].GetX() < 0)
                return true;
            if (snakePartsList[0].GetY() > windowSize.Y || snakePartsList[0].GetY() < 0)
                return true;

            return false;
        }

        static void DrawPointsAnim()
        {
            foreach (PointAnim pA in pointsAnims)
            {
                pA.Draw(window);
                pA.state++;
                pA.PAnim.Rotation += 1;
            }
            int i = 0;
            while (i < pointsAnims.Count)
            {
                if (pointsAnims[i].state == int.MaxValue)
                {
                    pointsAnims.RemoveAt(i);
                    Console.WriteLine("Deleted animation");
                }
                else i++;
            }
        }

        static void Main()
        {

            window = new RenderWindow(new VideoMode(800, 600), "SnakeSFML");
            window.Closed += new EventHandler(OnClose);

            textureW = new RenderTexture(20, 20); textureR = new RenderTexture(20, 20); textureSR = new RenderTexture(10, 10);
            textureW.Clear(Color.White); textureR.Clear(Color.Red); textureSR.Clear(Color.Red);

            pointToEat = new PointToEat.PointToEat(textureR.Texture, 60, 100);

            snakePartsList = new List<SnakeParticle>();
            pointsAnims = new List<PointAnim>();
            direction = new SnakeDirection();

            Clock clock = new Clock();

            SnakeAddParticleToList(textureW.Texture, 20, 40);
            SnakeAddParticleToList(textureW.Texture, 20, 20);

            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);

            while (window.IsOpen)
            {
                window.DispatchEvents();
                while (MainGameOn)
                {
                    window.Clear(Color.Black);

                    window.DispatchEvents();

                    pointToEat.Draw(window);
                    foreach (SnakeParticle obj in snakePartsList)
                        obj.Draw(window);

                    Time elapsed = clock.ElapsedTime;
                    Time speedOfSnake = Time.FromSeconds(0.1f);

                    if (elapsed >= speedOfSnake)
                    {

                        if (CheckPointPick())
                        {

                            pointsAnims.Add(new PointAnim(textureSR.Texture, pointToEat.GetX(), pointToEat.GetY()));
                            PointGained();


                        }
                        
                        MoveSnake(removeParticle);
                        clock.Restart();
                        canStillChangeDirection = true;
                        removeParticle = true;


                        if (CheckOverlapingOrOutOfBorderOfSnake(snakePartsList, window.Size))
                        {
                            Console.WriteLine("GAME OVER"); MainGameOn = false;
                        }

                    }
                    DrawPointsAnim();
                    window.Display();
                }
                window.Clear(Color.Black);

                Text gameOver = new Text("GAME OVER", arialF, 16);
                RenderTexture gameOverTexture = new RenderTexture(500, 300);
                gameOver.Color = Color.White;
                gameOver.Position = new Vector2f(window.Size.X / 4, window.Size.Y / 2);
                gameOver.Draw(window, RenderStates.Default);

                window.Display();

            }
        }
    }
}

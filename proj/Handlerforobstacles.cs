using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proj
{
    public class Handlerforobstacles
    {
        private Snake snake;
        private TextBox lives;
        private TextBox apples;
        public Handlerforobstacles(Snake s, TextBox lives, TextBox apples)
        {
            this.snake = s;
            this.lives = lives;
            this.apples = apples;
        }
        public void Apple(List<Apple> Apples, MyPoint end, Graphics board ,List<Flybane> Flybane)
        {
            foreach (var i in Apples)
            {
                if (this.snake.GetPoint(0).x <= i.x + 7 && this.snake.GetPoint(0).x >= i.x - 7 && this.snake.GetPoint(0).y <= i.y + 7 && this.snake.GetPoint(0).y >= i.y - 7)
                {
                    //zjada jabuszko
                    SolidBrush brushwhite = new SolidBrush(Color.White);
                    board.FillRectangle(brushwhite, new Rectangle(i.x, i.y, 7, 7));
                    this.snake.AddPoint(end.x, end.y);
                    Random rand = new Random();
                    Segment newapple = new Segment(rand.Next(2, 916), rand.Next(2, 506));
                    while (this.snake.IfContains(newapple) || Flybane.Contains(new Flybane(newapple.x, newapple.y)) || Apples.Contains(new Apple(newapple.x, newapple.y)))
                    {
                        newapple = new Segment(rand.Next(2, 916), rand.Next(2, 506));
                    }
                    i.Change(newapple.x, newapple.y);
                    this.snake.EatApple();
                    this.lives.Text = "Liczba żyć: " + this.snake.GetLife().ToString();
                    this.apples.Text = "Liczba zjedzonych jabłek: " + this.snake.GetApple().ToString();
                }
            }
        }
        public bool Flybane(List<Flybane> Flybane,Graphics board)
        {
            foreach (var i in Flybane)
            {
                if (this.snake.GetPoint(0).x <= i.x + 7 && this.snake.GetPoint(0).x >= i.x - 7 && this.snake.GetPoint(0).y <= i.y + 7 && this.snake.GetPoint(0).y >= i.y - 7)
                {
                    //zjada muchomorka i traci jedno zycie 
                    SolidBrush brushwhite = new SolidBrush(Color.White);
                    board.FillRectangle(brushwhite, new Rectangle(i.x, i.y, 7, 7));
                    this.snake.SetLife(this.snake.GetLife() - 1);

                    Random rand = new Random();
                    Segment newflybane = new Segment(rand.Next(2, 916), rand.Next(2, 506));
                    while (this.snake.IfContains(newflybane))
                    {
                        newflybane = new Segment(rand.Next(2, 916), rand.Next(2, 506));
                    }
                    i.Change(newflybane.x, newflybane.y);
                    this.lives.Text = "Liczba żyć: " + this.snake.GetLife().ToString();
                    this.apples.Text = "Liczba zjedzonych jabłek: " + this.snake.GetApple().ToString();
                    if (this.snake.Dead())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public bool Wall()//koniec gry =(((
        {
            if (this.snake.GetPoint(0).x < 3 || this.snake.GetPoint(0).x > 916 || this.snake.GetPoint(0).y < 3 || this.snake.GetPoint(0).y > 506)
            {
                return true;
            }
            return false;
        }
    }
}

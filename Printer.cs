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
    class Printer
    {
        public Graphics board;
        public Printer(Graphics b)
        {
            this.board = b;
        }
        public void PrintWall()
        {
            SolidBrush brush = new SolidBrush(Color.Black);//czarny pisak
            this.board.FillRectangle(brush, new Rectangle(0, 0, 3, 520));
            this.board.FillRectangle(brush, new Rectangle(0, 0, 930, 3));
            this.board.FillRectangle(brush, new Rectangle(0, 513, 930, 3));//mur na dole planszy
            this.board.FillRectangle(brush, new Rectangle(930, 0, 3, 520));
        }
        public void PrintLayout()
        {
            SolidBrush brushwhite = new SolidBrush(Color.White);//bialy pisak
            this.board.FillRectangle(brushwhite, new Rectangle(0, 0, 930, 930));
        }
        public void PrintSnake(Snake snake)
        {
            SolidBrush bluebrush = new SolidBrush(Color.Blue);
            foreach (var point in snake.List())
            {
                board.FillRectangle(bluebrush, new Rectangle(point.x, point.y, 7, 7));
            }
        }
        public void PrintApple(List<Apple> apples)
        {
            foreach (var i in apples)
            {
                board.FillRectangle(i.color, new Rectangle(i.x, i.y, 7, 7));
            }
        }
        public void PrintFlybane(List<Flybane> flybanes)
        {
            foreach (var i in flybanes)
            {
                board.FillRectangle(i.color, new Rectangle(i.x, i.y, 7, 7));
            }
        }
        public void PrintPointWhite(int x, int y)
        {
            SolidBrush blackbrush = new SolidBrush(Color.White);
            this.board.FillRectangle(blackbrush, new Rectangle(x, y, 7, 7));
        }
        public void Clear(Snake snake)
        {
            SolidBrush whitebrush = new SolidBrush(Color.White);
            foreach (var point in snake.List())
            {
                board.FillRectangle(whitebrush, new Rectangle(point.x, point.y, 7, 7));
            }
        }
    }
}

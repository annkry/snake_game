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
    public class GameLoop
    {
        private Timer timer;
        private Snake snake;
        private List<Apple> apple;
        private List<Flybane> flybane;
        private TextBox lives;
        private TextBox apples;
        private Printer printer;
        private int counter;
        private Form1 form;
        public GameLoop(PictureBox board)
        {
            this.printer= new Printer(board.CreateGraphics());
            this.counter = 0;
        }
        public void Init(TextBox textBox1, TextBox textBox2, Form1 form)
        {

            this.form = form;
            this.counter = 0;
            this.printer.PrintLayout();
            this.printer.PrintWall();
            this.snake = new Snake();
            this.snake.SetApple(0);
            Random rand = new Random();
            int los = rand.Next(0, 4);
            char[] array = new char[] { 'a', 'w', 's', 'd' };
            this.snake.SetDirection(array[los]);//losujemy kierunek poruszania sie weza
            this.snake.NewSnake();
            this.snake.AddPoint(rand.Next(2, 916), rand.Next(2, 506));
            this.printer.PrintSnake(this.snake);
            this.apple = new List<Apple>();
            for (int i = 0; i < 20; i++)
            {
                Segment newj = new Segment(rand.Next(2, 916), rand.Next(2, 506));
                while (this.snake.IfContains(newj))
                {
                    newj = new Segment(rand.Next(2, 916), rand.Next(2, 506));
                }

                this.apple.Add(new Apple(newj.x, newj.y));
            }
            this.flybane = new List<Flybane>();
            for (int i = 0; i < 75; i++)
            {
                Segment newm = new Segment(rand.Next(2, 916), rand.Next(2, 506));
                while (this.snake.IfContains(newm) || this.apple.Contains(new Apple(newm.x, newm.y)) || this.flybane.Contains(new Flybane(newm.x,newm.y)))
                {
                    newm = new Segment(rand.Next(2, 916), rand.Next(2, 506));
                }
                this.flybane.Add(new Flybane(newm.x, newm.y));
            }
            this.printer.PrintFlybane(this.flybane);
            this.printer.PrintApple(this.apple);
            this.snake.NewLife();
            this.apples = textBox2;
            this.lives = textBox1;
            this.lives.Text = "Liczba żyć: " + this.snake.GetLife().ToString();
            this.apples.Text= "Liczba zjedzonych jabłek: " + this.snake.GetApple().ToString();
            this.Loop();
        }
        public void PressAction(KeyPressEventArgs e)
        {
            Segment seg = new Segment(0,0);
            SolidBrush brush = new SolidBrush(Color.White);
            switch (e.KeyChar)
            {
                case 'w':
                    seg.Change(this.snake.GetPoint(0).x, this.snake.GetPoint(0).y - 7);
                    this.snake.SetDirection('w');
                    break;
                case 'a':
                    seg.Change(this.snake.GetPoint(0).x - 7, this.snake.GetPoint(0).y);
                    this.snake.SetDirection('a');
                    break;
                case 's':
                    seg.Change(this.snake.GetPoint(0).x, this.snake.GetPoint(0).y + 7);
                    this.snake.SetDirection('s');
                    break;
                case 'd':
                    seg.Change(this.snake.GetPoint(0).x + 7, this.snake.GetPoint(0).y);
                    this.snake.SetDirection('d');
                    break;
                default:
                    return;
            }
            this.snake.Insert(0, seg);//zmiana wspolrzednych glowy weza
            this.printer.PrintPointWhite(this.snake.GetPoint(this.snake.Count() - 1).x, this.snake.GetPoint(this.snake.Count() - 1).y);
            this.snake.Remove(this.snake.Count() - 1);//usuwa ostatni punkt
        }
        public void Loop()
        {
            this.timer = new Timer();
            this.timer.Tick += Game;
            this.timer.Interval = 100;
            this.timer.Start();
        }
        public void Game(object sender, EventArgs e)
        {
            this.counter += 1;
            if(this.counter>=2400)
            {
                //koniec gry
                this.timer.Stop();
                string message = "Upłynął czas, czy chcesz zagrać jeszcze raz?";
                string caption = "Przegrana.";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Init(this.lives, this.apples, this.form);
                }
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    this.form.Dispose();
                }
            }
            this.printer.PrintWall();
            Segment end = this.snake.GetPoint(this.snake.Count() - 1);
            int id;
            this.printer.Clear(this.snake);
            Segment pnt = new Segment(0,0);
            if (this.snake.GetDirection() == 'a')
            {
                pnt = new Segment(this.snake.GetPoint(0).x - 7, this.snake.GetPoint(0).y);
            }
            else if (this.snake.GetDirection() == 's')
            {
                pnt = new Segment(this.snake.GetPoint(0).x, this.snake.GetPoint(0).y + 7);
            }
            else if (this.snake.GetDirection() == 'w')
            {
                pnt = new Segment(this.snake.GetPoint(0).x, this.snake.GetPoint(0).y - 7);
            }
            else
            {
                pnt = new Segment(this.snake.GetPoint(0).x + 7, this.snake.GetPoint(0).y);
            }
            this.snake.Insert(0, pnt);
            id = this.snake.Count() - 1;
            this.snake.Remove(id);
            if(this.snake.Collision())
            {
                //koniec gry
                this.timer.Stop();
                string message = "Wąż zderzył się z samym sobą, czy chcesz zagrać jeszcze raz?";
                string caption = "Przegrana.";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

               
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Init(this.lives, this.apples, this.form);
                }
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    this.form.Dispose();
                }
            }
            Handlerforobstacles check = new Handlerforobstacles(this.snake, this.lives, this.apples);
            check.Apple(this.apple, end, this.printer.board, this.flybane);
            this.printer.PrintApple(this.apple);
            if (check.Flybane(this.flybane, this.printer.board))
            {
                this.timer.Stop();
                string message = "Nastąpiło stracenie pięciu żyć, czy chcesz zagrać jeszcze raz?";
                string caption = "Przegrana.";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Init(this.lives, this.apples, this.form);
                }
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    this.form.Dispose();
                }
            }
            this.printer.PrintFlybane(this.flybane);
            if (check.Wall())
            {
                this.timer.Stop();
                string message = "Wąż uderzył w mur, czy chcesz zagrać jeszcze raz?";
                string caption = "Przegrana.";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Init(this.lives, this.apples, this.form);
                }
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    this.form.Dispose();
                }
            }
            if (this.snake.Win())
            {
                this.timer.Stop();
                string message = "Wygrałeś/aś grę, czy chcesz zagrać jeszcze raz?";
                string caption = "Wygrana.";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Init(this.lives, this.apples, this.form);
                }
                if (result == System.Windows.Forms.DialogResult.No)
                {
                    this.form.Dispose();
                }
            }
            this.printer.PrintSnake(this.snake);
        }
    }
}

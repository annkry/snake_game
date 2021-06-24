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
    public class Snake
    {
        private List<Segment> co_ordinates;//wspolrzedne ciala snake'a
        private Life tilldead;
        private char dir;
        private int EatenApple;
        public void AddPoint(int x, int y)
        {
            this.co_ordinates.Add(new Segment(x, y));
        }
        public void SetDirection(char dir)
        {
            this.dir = dir;
        }
        public char GetDirection()
        {
            return this.dir;
        }

        public void NewSnake()
        {
            this.co_ordinates = new List<Segment>();
            this.EatenApple = 0;
        }
        public void EatApple()
        {
            this.EatenApple += 1;
        }
        public void SetApple(int eatenapple)
        {
            this.EatenApple = eatenapple;
        }
        public int GetApple()
        {
            return this.EatenApple;
        }
        public bool Win()
        {
            if (this.GetApple() == 50)
                return true;
            else
                return false;
        }
        public void NewLife()
        {
            this.tilldead = new Life();
        }
        public void Insert(int id, Segment seg)
        {
            this.co_ordinates.Insert(id, seg);
        }
        public void Remove(int id)
        {
            this.co_ordinates.RemoveAt(id);
        }
        public int Count()
        {
            return this.co_ordinates.Count;
        }
        public Segment GetPoint(int id)
        {
            return this.co_ordinates[id];
        }
        public int GetLife()
        {
            return this.tilldead.Get();
        }
        public bool IfContains(Segment pnt)
        {
            return this.co_ordinates.Contains(pnt);
        }
        public void SetLife(int count)
        {
            this.tilldead.Set(count);
        }
        public bool Dead()
        {
            return this.tilldead.IfDead();
        }
        public List<Segment> List()
        {
            return this.co_ordinates;
        }
        public bool Collision()
        {
            Segment[] wsp = new Segment[this.Count()];      
            this.co_ordinates.CopyTo(wsp);
            for(int i=1;i<this.Count();i++)
            {
                if (this.co_ordinates[0].x == wsp[i].x && this.co_ordinates[0].y== wsp[i].y)
                    return true;
            }
            return false;

        }
    }
}

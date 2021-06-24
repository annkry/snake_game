using System;
using System.Collections.Generic;
using System.Text;

namespace proj
{
    public class Life
    {
        public int life;

        public Life()
        {
            this.life = 5;
        }
        public bool IfDead()
        {
            if (this.life == 0)
                return true;
            return false;
        }
        public void Set(int newlife)
        {
            this.life = newlife;
        }
        public int Get()
        {
            return this.life;
        }
    }
}

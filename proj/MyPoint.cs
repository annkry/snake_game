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
    abstract public class MyPoint
    {
        public int x;
        public int y;
        public MyPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public MyPoint()
        {
            this.x = 0;
            this.y = 0;
        }

        public void Change(int xnew, int ynew)
        {
            this.x = xnew;
            this.y = ynew;
        }
    }
}

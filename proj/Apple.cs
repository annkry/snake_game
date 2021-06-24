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
    public class Apple : MyPoint
    {
        public SolidBrush color; 
        public Apple(int x, int  y)
        {
            this.x = x;
            this.y = y;
            this.color = new SolidBrush(Color.Green);
        }
    }
}

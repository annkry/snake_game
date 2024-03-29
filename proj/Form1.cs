﻿using System;
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
    
    public partial class Form1 : Form
    {
        private GameLoop game;
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
            this.game = new GameLoop(this.plansza);
            
        }
        private void Startbutton_Click(object sender, EventArgs e)
        {
            this.game.Init(textBox1, textBox2, this);
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.game.PressAction(e);
        }
    }
}

﻿using InstantMovement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstantMovement
{


    public partial class Form2 : Form
    {
        private CenterButton center;
        private TargetBall ball;
        private Score score;

        public Form2()
        {
            InitializeComponent();
            Init();
            Invalidate();
        }

        public void Init()
        {
            center = new CenterButton(ClientSize.Width / 2 - 25, ClientSize.Height / 2 - 25, true);
            ball = new TargetBall(10, 10, 20, false);
            score = new Score();
        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            var leftBorderCenter = new Point(center.X, center.Y);
            var rightBorderCenter = new Point(center.X + center.Size, center.Y + center.Size);
            var leftBorderBall = new Point(ball.X, ball.Y);
            var rightBorderBall = new Point(ball.X + ball.Size, ball.Y + ball.Size);

            if (e.Button == MouseButtons.Left && e.X >= leftBorderCenter.X
                && e.X <= rightBorderCenter.X && e.Y >= leftBorderCenter.Y
                && e.Y <= rightBorderCenter.Y && !ball.State)
            {
                ball.State = true;
                center.State = false;
                var rnd = new Random();
                ball.Size = rnd.Next(15, 40);
                ball.X = rnd.Next(0, ClientSize.Width - ball.Size);
                ball.Y = rnd.Next(0, ClientSize.Height - ball.Size);
                Invalidate();// таймер, чтобы перерисовывать
            }

            if (e.Button == MouseButtons.Left && e.X >= leftBorderBall.X
                && e.X <= rightBorderBall.X && e.Y >= leftBorderBall.Y
                && e.Y <= rightBorderBall.Y)
            {
                ball.State = false;
                center.State = true;
                score.Counter += 1;

                Invalidate();
            }

        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            if (score.Counter < 1 && center.State != false)
                graphics.DrawImage(center.StartButtonImg, center.X, center.Y, center.Size, center.Size);
            else
                graphics.DrawImage(center.ButtonImg, center.X, center.Y, center.Size, center.Size); 

            if (ball.State)
                graphics.DrawImage(ball.NewColorTargetBall(), ball.X, ball.Y, ball.Size, ball.Size);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            var form = new Form1();
            form.ShowDialog();
            Close();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrueHeroSimulator.Buttons
{
    internal class FightButton : UndertaleButton
    {
        public FightButton()
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            Font font = GameResources.DeterminationFont.ToFont((int)(this.Height / 2));

            string text = "FIGHT";

            var textSize = g.MeasureString(text, font);
            g.DrawString(text, font, new SolidBrush(this.isSelected ? this.hoverColor : this.normalColor), this.Width - textSize.Width + this.Width / 40, (this.Height - textSize.Height) / 2);

            if (this.isSelected)
                g.DrawImage(TrueHeroSimulatorUI.Instance.soulColor == SoulColour.Green ? GameResources.GreenHeartIcon : GameResources.HeartIcon, this.Width / 15, this.Height / 5, this.Width / 5, this.Height / 2);
            else
                g.DrawImage(GameResources.SwordIcon, this.Width / 15, this.Height / 5, this.Width / 7, (int)(this.Height / 1.5));
        }
    }
}

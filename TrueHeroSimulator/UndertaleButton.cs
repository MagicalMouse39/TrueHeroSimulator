using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrueHeroSimulator
{
    class UndertaleButton : Panel
    {
        private Color normalColor = Color.FromArgb(255, 255, 127, 39);
        private Color hoverColor = Color.FromArgb(255, 255, 255, 64);

        private Bitmap sprite;
        private string text;

        private bool isSelected;

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
                this.Refresh();
            }
        }

        public UndertaleButton(string text, Bitmap sprite)
        {
            this.text = text;
            this.sprite = sprite;

            this.isSelected = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.Height = 84 * this.Width / 220;
            Graphics g = e.Graphics;

            Pen p = new Pen(this.isSelected ? this.hoverColor : this.normalColor);
            p.Width = this.Height / 15;
            int t = 20;
            g.DrawRectangle(p, new Rectangle(this.Height / t, this.Height / t, this.Width - (this.Height / t * 2), this.Height - (this.Height / t * 2)));

            Font font = GameResources.DeterminationFont.ToFont((int)(this.Height / 1.75));

            var textSize = g.MeasureString(this.text, font);
            g.DrawString(this.text, font, new SolidBrush(this.isSelected ? this.hoverColor : this.normalColor), this.Width - textSize.Width + this.Width / 40, (this.Height - textSize.Height) / 2);

            if (this.isSelected)
                g.DrawImage(TrueHeroSimulatorUI.Instance.soulColor == SoulColour.Green ? GameResources.GreenHeartIcon : GameResources.HeartIcon, 10, 10, this.Width - textSize.Width, this.Height - 20);
            else
                g.DrawImage(this.sprite, 10, 10, this.Width - textSize.Width, this.Height - 20);

            base.OnPaint(e);
        }
    }
}

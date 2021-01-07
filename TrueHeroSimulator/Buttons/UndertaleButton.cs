using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrueHeroSimulator.Buttons
{
    class UndertaleButton : Panel
    {
        protected Color normalColor = Color.FromArgb(255, 255, 127, 39);
        protected Color hoverColor = Color.FromArgb(255, 255, 255, 64);

        protected bool isSelected;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            this.Height = 84 * this.Width / 220;
            Graphics g = e.Graphics;

            Pen p = new Pen(this.isSelected ? this.hoverColor : this.normalColor);
            p.Width = this.Height / 15;
            int t = 20;
            g.DrawRectangle(p, new Rectangle(this.Height / t, this.Height / t, this.Width - (this.Height / t * 2), this.Height - (this.Height / t * 2)));

            base.OnPaint(e);
        }
    }
}

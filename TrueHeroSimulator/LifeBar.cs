using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrueHeroSimulator
{
    class LifeBar : Panel
    {
        private int hp;

        public int HP
        {
            get
            {
                return this.hp;
            }
            set
            {
                this.Refresh();
                this.hp = value;
            }
        }
        public int MaxHP { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(Brushes.Red, this.ClientRectangle);
            g.FillRectangle(Brushes.Yellow, new Rectangle(0, 0, this.ClientRectangle.Width * this.hp / this.MaxHP, this.ClientRectangle.Height));

            base.OnPaint(e);
        }
    }
}

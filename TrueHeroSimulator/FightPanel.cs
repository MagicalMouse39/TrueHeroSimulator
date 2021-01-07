using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrueHeroSimulator
{
    class FightPanel : Panel
    {
        private Rectangle screen;

        public Label Dialog;
        private FightView currentView;

        public FightPanel()
        {
            this.screen = Screen.PrimaryScreen.Bounds;

            this.Width = this.screen.Width / 2;
            this.Height = this.screen.Height / 4;

            //Dialog Setup
            Font font = GameResources.DeterminationFont.ToFont(32);
            this.Dialog = new Label() { Left = 5, Top = 5, Font = font, Width = this.Width - 20, Height = this.Height - 206, ForeColor = Color.White };
            this.Controls.Add(Dialog);
        }

        public void SetView(FightView view)
        {
            this.currentView = view;
            
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.FillRectangle(Brushes.Black, this.ClientRectangle);
            Pen p = new Pen(Brushes.White);
            p.Width = 10;
            g.DrawRectangle(p, this.ClientRectangle);
        }
    }
}

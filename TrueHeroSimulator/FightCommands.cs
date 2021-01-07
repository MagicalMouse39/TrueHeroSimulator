using System.Drawing;
using System.Windows.Forms;
using TrueHeroSimulator.Buttons;

namespace TrueHeroSimulator
{
    class FightCommands : Panel
    {
        private LifeBar lifeBar;

        internal FightButton fightBtn;
        internal ActButton actBtn;
        internal ItemButton itemBtn;
        internal MercyButton mercyBtn;

        private Rectangle screen;

        public FightCommands()
        {
            this.screen = Screen.PrimaryScreen.Bounds;

            this.Width = this.screen.Width / 2;
            this.Height = this.screen.Height / 8;

            this.lifeBar = new LifeBar() { MaxHP = 60, HP = 60, Width = this.Width / 10, Height = this.Height / 5, Top = 5 };
            this.Controls.Add(this.lifeBar);

            int l = this.Width / 4;
            int t = l - this.Width / 15;

            this.fightBtn = new FightButton() { Left = 0, Top = this.Height / 3, Width = t };
            this.actBtn = new ActButton() { Left = l, Top = this.Height / 3, Width = t };
            this.itemBtn = new ItemButton() { Left = 2 * l, Top = this.Height / 3, Width = t };
            this.mercyBtn = new MercyButton() { Left = 3 * l, Top = this.Height / 3, Width = t };
            this.Controls.Add(this.fightBtn);
            this.Controls.Add(this.actBtn);
            this.Controls.Add(this.itemBtn);
            this.Controls.Add(this.mercyBtn);
        }

        public void SelectedButtonRight()
        {
            if (this.fightBtn.IsSelected)
            {
                this.fightBtn.IsSelected = false;
                this.actBtn.IsSelected = true;
                this.fightBtn.Refresh();
                this.actBtn.Refresh();
            }
            else if (this.actBtn.IsSelected)
            {
                this.actBtn.IsSelected = false;
                this.itemBtn.IsSelected = true;
                this.actBtn.Refresh();
                this.itemBtn.Refresh();
            }
            else if (this.itemBtn.IsSelected)
            {
                this.itemBtn.IsSelected = false;
                this.mercyBtn.IsSelected = true;
                this.itemBtn.Refresh();
                this.mercyBtn.Refresh();
            }
            else if (this.mercyBtn.IsSelected)
            {
                this.mercyBtn.IsSelected = false;
                this.fightBtn.IsSelected = true;
                this.fightBtn.Refresh();
                this.mercyBtn.Refresh();
            }
        }

        public void SelectedButtonLeft()
        {
            if (this.mercyBtn.IsSelected)
            {
                this.mercyBtn.IsSelected = false;
                this.itemBtn.IsSelected = true;
                this.fightBtn.Refresh();
                this.actBtn.Refresh();
            }
            else if (this.itemBtn.IsSelected)
            {
                this.itemBtn.IsSelected = false;
                this.actBtn.IsSelected = true;
                this.actBtn.Refresh();
                this.itemBtn.Refresh();
            }
            else if (this.actBtn.IsSelected)
            {
                this.actBtn.IsSelected = false;
                this.fightBtn.IsSelected = true;
                this.itemBtn.Refresh();
                this.mercyBtn.Refresh();
            }
            else if (this.fightBtn.IsSelected)
            {
                this.fightBtn.IsSelected = false;
                this.mercyBtn.IsSelected = true;
                this.fightBtn.Refresh();
                this.mercyBtn.Refresh();
            }
        }

        public void DecreaseLife(int dmg)
        {
            this.lifeBar.HP -= dmg;
            this.Refresh();
            this.lifeBar.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Font textFont = GameResources.MarsNeedsCunnilingusFont.ToFont(this.Height / 7);
            string text = "Magical";
            var textSize = g.MeasureString(text, textFont);
            g.DrawString(text, textFont, Brushes.White, 0, 0);

            text = "LV 10";
            float w = textSize.Width;
            g.DrawString(text, textFont, Brushes.White, w + this.Width / 25, 0);
            w += g.MeasureString(text, textFont).Width + (int)(this.Width / 12.5);

            text = "HP";
            g.DrawString(text, textFont, Brushes.White, w + this.Width / 25, 0);
            w += g.MeasureString(text, textFont).Width + this.Width / 25;

            this.lifeBar.Left = (int)w;
            w += this.lifeBar.Width + this.Width / 25;

            text = this.lifeBar.HP + " / " + this.lifeBar.MaxHP;
            g.DrawString(text, textFont, Brushes.White, w, 0);

            base.OnPaint(e);
        }

        public void UpdateComponentsPosition()
        {
            int l = this.Width / 4;
            int t = l - this.Width / 15;

            this.fightBtn.Width = t;
            this.fightBtn.Top = this.Height / 3;
            this.actBtn.Left = l;
            this.actBtn.Top = this.Height / 3;
            this.actBtn.Width = t;
            this.itemBtn.Left = l * 2;
            this.itemBtn.Top = this.Height / 3;
            this.itemBtn.Width = t;
            this.mercyBtn.Left = l * 3;
            this.mercyBtn.Top = this.Height / 3;
            this.mercyBtn.Width = t;

            this.lifeBar.Width = this.Width / 10;
            this.lifeBar.Height = this.Height / 5;

            this.Refresh();
        }
    }
}

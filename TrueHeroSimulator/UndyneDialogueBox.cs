using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace TrueHeroSimulator
{
    public partial class UndyneDialogueBox : Panel
    {
        private string content = string.Empty;
        private Label textLbl;
        private Font font;

        public UndyneDialogueBox(string content)
        {
            InitializeComponent();

            this.content = content;
            PrivateFontCollection pfc = new PrivateFontCollection();
            var fontLength = GameResources.DeterminationFont.Length;
            byte[] fontData = GameResources.DeterminationFont;
            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontData, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);
            this.font = new Font(pfc.Families[0], 16);
            this.textLbl = new Label() { Text = this.content, Width = 280, Height = 80, Left = 35, Top = 10, Font = this.font, BackColor = Color.White };
            this.Controls.Add(this.textLbl);
        }

        public void UpdateContent(string content)
        {
            this.content = content;
            this.textLbl.Text = this.content;
            this.Visible = true;
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //Boring settings
            this.Width = 325;
            this.Height = 100;

            //Main "rectangle"
            g.FillPath(Brushes.White, new Rectangle(25, 0, 300, 100).Round(15));
            
            //Lil triangle thing
            var points = new List<PointF>();
            points.Add(new PointF(0, 25));
            points.Add(new PointF(25, 15));
            points.Add(new PointF(25, 35));
            g.FillPolygon(Brushes.White, points.ToArray());


            base.OnPaint(e);
        }
    }
}

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
using System.Threading;

namespace TrueHeroSimulator
{
    public partial class UndyneDialogueBox : Panel
    {
        private string content = string.Empty;
        private Label textLbl;
        private Font font;
        private Task writeThread;
        private bool threadStopped;
        public bool HasFinishedWriting { get; private set; }

        public UndyneDialogueBox(string content)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            this.content = content;
            PrivateFontCollection pfc = new PrivateFontCollection();
            var fontLength = GameResources.DeterminationFont.Length;
            byte[] fontData = GameResources.DeterminationFont;
            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontData, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);
            this.font = new Font(pfc.Families[0], 16);
            this.textLbl = new Label() { Width = 280, Height = 80, Left = 35, Top = 10, Font = this.font, BackColor = Color.White };
            this.HasFinishedWriting = false;
            this.threadStopped = false;
            
            this.UpdateContent(this.content);
            this.Controls.Add(this.textLbl);
        }

        public void UpdateContent(string content)
        {
            this.content = content;
            this.threadStopped = false;
            this.HasFinishedWriting = false;
            this.writeThread = Task.Factory.StartNew(() =>
            {
                int index = 0;
                while (this.textLbl.Text != this.content && !this.threadStopped)
                {
                    this.textLbl.Text += this.content[index++];
                    Thread.Sleep(40);
                    this.textLbl.Refresh();
                }
                this.HasFinishedWriting = true;
            });
            this.Visible = true;
            this.Refresh();
        }

        public void StopWriteThread()
        {
            this.threadStopped = true;
            this.textLbl.Text = this.content;
            this.HasFinishedWriting = true;
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

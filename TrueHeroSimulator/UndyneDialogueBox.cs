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
using System.Runtime.ExceptionServices;

namespace TrueHeroSimulator
{
    public partial class UndyneDialogueBox : Panel
    {
        private string content = string.Empty;
        private Label textLbl;
        private System.Windows.Forms.Timer writer;
        private int writerIndex = 0;
        public bool HasFinishedWriting { get; private set; }


        public UndyneDialogueBox(string content)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            this.textLbl = new Label() { Width = 280, Height = 80, Left = 35, Top = 10, Font = GameResources.DeterminationFont.ToFont(16), BackColor = Color.White };
            this.HasFinishedWriting = false;
            this.writer = new System.Windows.Forms.Timer();
            this.writer.Interval = 40;
            this.writer.Tick += (s, e) =>
            {
                if (this.textLbl.Text != this.content)
                    this.textLbl.Text += this.content[this.writerIndex++];
                else
                    this.HasFinishedWriting = true;
            };

            this.UpdateContent(content);
            this.Controls.Add(this.textLbl);
        }

        public void UpdateContent(string content)
        {
            this.content = content;
            this.HasFinishedWriting = false;
            this.writerIndex = 0;
            this.Visible = true;
            if (!this.writer.Enabled)
                this.writer.Start();
            this.Refresh();
        }

        public void StopWriteThread()
        {
            if (this.writer.Enabled)
                this.writer.Stop();
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

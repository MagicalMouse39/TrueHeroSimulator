using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace TrueHeroSimulator
{
    class TrueHeroSimulatorUI : Form
    {
        private PictureBox undyneSprite;
        private UndyneDialogueBox dialogueBox;

        private WindowsMediaPlayer musicPlayer;
        private string musicTmpFile = string.Empty;

        public TrueHeroSimulatorUI()
        {
            Rectangle screen = Screen.PrimaryScreen.Bounds;
            this.Width = 1000;
            this.Height = 800;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;
            this.Text = "True Hero Simulator";
            Directory.CreateDirectory($@"C:\Users\{Environment.UserName}\AppData\Local\Temp\TrueHeroSimulator");
            this.musicTmpFile = $@"C:\Users\{Environment.UserName}\AppData\Local\Temp\TrueHeroSimulator\theme.mp3";
            this.FormClosing += (s, e) => File.Delete(musicTmpFile);

            this.SizeChanged += (s, e) => UpdateComponentsPosition();

            //Undyne Sprite
            this.undyneSprite = new PictureBox();
            this.undyneSprite.Image = GameResources.UndyneSprite;
            this.undyneSprite.Width = GameResources.UndyneSprite.Width;
            this.undyneSprite.Height = GameResources.UndyneSprite.Height;
            this.WindowState = FormWindowState.Maximized;
            this.undyneSprite.Top = 100;
            this.Controls.Add(this.undyneSprite);

            //Music Controller
            this.musicPlayer = new WindowsMediaPlayer();
            File.WriteAllBytes(this.musicTmpFile, GameResources.MusicTheme);
            this.musicPlayer.URL = this.musicTmpFile;
            this.musicPlayer.settings.autoStart = true;
            this.musicPlayer.settings.setMode("loop", true);
            this.musicPlayer.controls.play();

            //Dialogue manager
            this.dialogueBox = new UndyneDialogueBox("...");
            this.dialogueBox.Top = 200;
            this.Controls.Add(dialogueBox);
            this.dialogueBox.BringToFront();

            //Key Manager
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Z)
                {

                }
            };

            UpdateComponentsPosition();
        }

        private void UpdateComponentsPosition()
        {
            this.undyneSprite.Left = (this.Width - this.undyneSprite.Width) / 2;

            this.dialogueBox.Left = this.undyneSprite.Left + 200;
        }
    }
}

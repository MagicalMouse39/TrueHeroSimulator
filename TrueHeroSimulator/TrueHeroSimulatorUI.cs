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

        private int playerX, playerY;
        private List<string> lastDialogueWritings;

        private GamePhase phase;

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

            this.lastDialogueWritings = new List<string>();
            this.lastDialogueWritings.Add("");
            this.phase = GamePhase.InitialDialogue;

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
            this.dialogueBox = new UndyneDialogueBox("You're gonna have to try a little harder than THAT");
            this.dialogueBox.Top = 200;
            this.Controls.Add(dialogueBox);
            this.dialogueBox.BringToFront();

            //Movement Manager
            this.PreviewKeyDown += (s, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        this.playerY++;
                        break;
                    case Keys.Right:
                        this.playerX++;
                        break;
                    case Keys.Down:
                        this.playerY--;
                        break;
                    case Keys.Left:
                        this.playerX--;
                        break;
                }
            };

            //Key Manager
            this.KeyDown += (s, e) =>
            {
                Debug.WriteLine(e.KeyCode);
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Z)
                {
                    if (this.phase == GamePhase.InitialDialogue)
                    {
                        this.UpdateGamePhase();
                        return;
                    }
                }
                if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.X || e.KeyCode == Keys.ShiftKey)
                {
                    if (this.dialogueBox.Visible)
                        this.dialogueBox.StopWriteThread();
                }
            };

            UpdateComponentsPosition();
        }

        private void UpdateGamePhase()
        {
            switch (this.phase)
            {
                case GamePhase.InitialDialogue:
                    if (this.dialogueBox.HasFinishedWriting)
                        this.dialogueBox.Visible = false;
                    break;
            }

            switch (this.phase.Next())
            {
                case GamePhase.LastDialogue:
                    this.dialogueBox.UpdateContent("");
                    break;
            }
        }

        private void UpdateComponentsPosition()
        {
            this.undyneSprite.Left = (this.Width - this.undyneSprite.Width) / 2;

            this.dialogueBox.Left = this.undyneSprite.Left + 200;
        }
    }
}

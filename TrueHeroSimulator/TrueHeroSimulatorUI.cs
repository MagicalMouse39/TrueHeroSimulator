using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace TrueHeroSimulator
{
    class TrueHeroSimulatorUI : Form
    {
        public static TrueHeroSimulatorUI Instance = new TrueHeroSimulatorUI();

        private PictureBox undyneSprite;
        private UndyneDialogueBox dialogueBox;
        private FightCommands fightCommands;
        private FightPanel fightPanel;

        private WindowsMediaPlayer musicPlayer;
        private string musicTmpFile = string.Empty;

        private bool buttonsSelectable;
        private int playerX, playerY;

        private Rectangle screen;

        internal GamePhase phase;
        internal SoulColour soulColor;

        private TrueHeroSimulatorUI()
        {
            this.screen = Screen.PrimaryScreen.Bounds;
            this.Width = 1000;
            this.Height = 800;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;
            this.Text = "True Hero Simulator";
            Directory.CreateDirectory($@"C:\Users\{Environment.UserName}\AppData\Local\Temp\TrueHeroSimulator");
            this.musicTmpFile = $@"C:\Users\{Environment.UserName}\AppData\Local\Temp\TrueHeroSimulator\theme.mp3";
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(500, 500);
            this.FormClosing += (s, e) => File.Delete(musicTmpFile);

            this.SizeChanged += (s, e) => UpdateComponentsPosition();

            this.playerX = this.playerY = 0;
            this.buttonsSelectable = false;

            this.phase = GamePhase.InitialDialogue;
            this.soulColor = SoulColour.Green;

            //Undyne Sprite
            this.undyneSprite = new PictureBox();
            this.undyneSprite.Image = GameResources.UndyneSprite;
            this.undyneSprite.SizeMode = PictureBoxSizeMode.StretchImage;
            this.undyneSprite.Width = this.screen.Width / 5;
            this.undyneSprite.Height = this.screen.Height / 2;
            this.undyneSprite.Top = 20;
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
            this.dialogueBox.Top = 300;
            this.Controls.Add(dialogueBox);
            this.dialogueBox.BringToFront();

            //Fight Panel
            this.fightPanel = new FightPanel() { Left = this.screen.Width / 4, Top = this.screen.Height / 2 };
            this.Controls.Add(this.fightPanel);
            this.fightPanel.BringToFront();

            //Fight Commands
            this.fightCommands = new FightCommands() { Left = this.screen.Width / 4, Top = this.screen.Height - this.screen.Height / 8 - 50 };

            this.Controls.Add(this.fightCommands);
            this.fightCommands.BringToFront();

            //Movement Manager
            this.PreviewKeyDown += (s, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        this.playerY++;
                        break;
                    case Keys.Right:
                        if (this.buttonsSelectable)
                            this.fightCommands.SelectedButtonRight();
                        this.playerX++;
                        break;
                    case Keys.Down:
                        this.playerY--;
                        break;
                    case Keys.Left:
                        if (this.buttonsSelectable)
                            this.fightCommands.SelectedButtonLeft();
                        this.playerX--;
                        break;
                }
            };

            //Key Manager
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Z)
                {
                    if (this.phase == GamePhase.InitialDialogue)
                    {
                        this.UpdateGamePhase();
                        return;
                    }
                    if (this.buttonsSelectable)
                    {
                        if (this.fightCommands.fightBtn.IsSelected)
                        {
                            this.fightPanel.SetView(FightView.Fight);
                        }
                        else if (this.fightCommands.actBtn.IsSelected)
                        {
                            this.fightPanel.SetView(FightView.Act);
                        }
                        else if (this.fightCommands.itemBtn.IsSelected)
                        {
                            this.fightPanel.SetView(FightView.Item);
                        }
                        else if (this.fightCommands.mercyBtn.IsSelected)
                        {
                            this.fightPanel.SetView(FightView.Mercy);
                        }
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
                    {
                        this.dialogueBox.Visible = false;
                        this.buttonsSelectable = true;
                        this.fightCommands.fightBtn.IsSelected = true;
                        this.fightPanel.Dialog.Text = "* The heroine appears.";
                    }
                    break;
            }

            switch (this.phase.Next())
            {
                case GamePhase.LastDialogue:
                    this.dialogueBox.UpdateContent("You won!");
                    break;
            }

            this.phase = this.phase.Next();
        }

        private void UpdateComponentsPosition()
        {
            this.undyneSprite.Height = (int)(this.Height / 2.5);
            this.undyneSprite.Width = this.undyneSprite.Height * 346 / 240;
            this.undyneSprite.Left = (this.Width - this.undyneSprite.Width) / 2;

            this.dialogueBox.Top = this.undyneSprite.Height / 2;
            this.dialogueBox.Left = (int)(this.undyneSprite.Left + this.undyneSprite.Width / 1.75);

            this.fightCommands.Width = this.Width / 2;
            this.fightCommands.Height = this.Height / 8;
            this.fightCommands.Top = this.Height - this.Height / 8 - 50;
            this.fightCommands.Left = this.Width / 4;
            this.fightCommands.UpdateComponentsPosition();
        }
    }
}

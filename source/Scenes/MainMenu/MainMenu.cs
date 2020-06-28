using Annex;
using Annex.Audio;
using Annex.Graphics;
using Annex.Graphics.Events;
using Annex.Scenes.Components;

namespace Game.Scenes.MainMenu
{
    public class MainMenu : Scene
    {
        private readonly Menu _menu;
        private readonly Button _startButton;
        private readonly Button _musicButton;
        private readonly Button _devButton;
        private bool musicPlaying; //a flag to tell if music is on or off

        public MainMenu()
        {

            this._menu = new Menu();

            this._devButton = new MenuButton("ENTER DEV MODE")
            {
                OnClickHandler = this.EnterDevMode
            };

            this._musicButton = new MenuButton("MUSIC ON/OFF")
            {
                OnClickHandler = this.ToggleMusic
            };

            this._startButton = new MenuButton("START")
            {
                OnClickHandler = this.StartGame
            };

            this._startButton.Position.Set(450, 200);
            this._devButton.Position.Set(450, 250);
            this._musicButton.Position.Set(450, 300);

            this.AddChild(this._startButton);
            this.AddChild(this._devButton);
            this.AddChild(this._musicButton);
        }

        public void EnterDevMode ( MouseButtonPressedEvent e)
        {
            var scenes = ServiceProvider.SceneService;
            var log = ServiceProvider.Log;
            log.WriteLineClean("Entering Level Development Mode");
            scenes.LoadNewScene<Scenes.DevMode.DevMode>();
        }

        public void StartGame(MouseButtonPressedEvent e)
        {
            var log = ServiceProvider.Log;
            log.WriteLineClean("Entering game");
            ServiceProvider.SceneService.LoadNewScene<NewGame>();
        }

        public void ToggleMusic( MouseButtonPressedEvent e)
        {
            if (musicPlaying)
            {
                var audioService = ServiceProvider.AudioService;
                audioService.StopPlayingAudio("start_music");
                musicPlaying = false;
            }
            else
            {
                var audioService = ServiceProvider.AudioService;
                var tune = new AudioContext()
                {
                    BufferMode = BufferMode.Buffered,
                    ID = "start_music",
                    Loop = true,
                    Volume = 100
                };
                var playingAudio = audioService.PlayAudio("pufferfish.wav", tune);
                musicPlaying = true;
            }
        }

        public override void HandleCloseButtonPressed() {
            ServiceProvider.SceneService.LoadGameClosingScene();
        }

        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {
            if (e.Key == Annex.Scenes.KeyboardKey.Tilde) {
                Debug.ToggleDebugOverlay();
                return;
            }
            base.HandleKeyboardKeyPressed(e);
        }

        public override void Draw(ICanvas canvas)
        {
            this._menu.Draw(canvas);
            base.Draw(canvas);
        }
    }
}

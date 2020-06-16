namespace Game.Scenes.Backgrounds
{
    public class TextureImage
    {
        public string Filename { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public TextureImage(string filename, int width, int height)
        {
            this.Filename = filename;
            this.Width = width;
            this.Height = height;
        }
    }
}

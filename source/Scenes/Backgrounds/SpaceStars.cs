using Annex.Graphics;
using Annex.Graphics.Contexts;
using System;
using System.Collections.Generic;

namespace Game.Scenes.Backgrounds
{
    public class SpaceStars : IDrawableObject
    {
        private readonly BatchTextureContext _batchTextureContext;

        private readonly TextureImage _image;

        private readonly int _xSpaceSize;
        private readonly int _ySpaceSize;

        private readonly int _numStars;
        private readonly int _maxStarSize;

        public SpaceStars(TextureImage image, int xSpaceSize, int ySpaceSize, int numStars, int maxStarSize = 16)
        {
            this._image = image;
            this._xSpaceSize = xSpaceSize;
            this._ySpaceSize = ySpaceSize;
            this._numStars = numStars;
            this._maxStarSize = maxStarSize;

            var random = new Random();

            var positions = new List<(float, float)>();
            var rect = new List<(int, int, int, int)>();
            var sizes = new List<(float, float)>();

            for (var i = 0; i < this._numStars; i++)
            {
                positions.Add((random.Next(0, this._xSpaceSize), random.Next(0, this._ySpaceSize)));

                var starSize = random.Next(2, this._maxStarSize);
                sizes.Add((starSize, starSize));
                rect.Add((0, 0, this._image.Width, this._image.Height));
            }

            _batchTextureContext = new BatchTextureContext(this._image.Filename, positions.ToArray(), sizes.ToArray(), rect.ToArray());
        }

        public void Draw(ICanvas canvas)
        {
            canvas.Draw(_batchTextureContext);
        }
    }
}

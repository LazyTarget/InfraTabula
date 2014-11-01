using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Xna
{
    public class MultiStateSpriteTexture2D<TState> : ISpriteTexture
    {
        private readonly Dictionary<TState, ISpriteTexture> _textures;


        public MultiStateSpriteTexture2D(Dictionary<TState, ISpriteTexture> textures)
        {
            if (!textures.Any())
                throw new ArgumentException("textures");
            _textures = textures;
            State = textures.FirstOrDefault().Key;
        }




        public TState State { get; set; }

        
        public Rectangle Bounds
        {
            get
            {
                var spriteTexture = GetSpriteTexture();
                return spriteTexture.Bounds;
            }
        }



        public virtual void Update(GameTime gameTime)
        {
            var spriteTexture = GetSpriteTexture();
            if (spriteTexture != null)
                spriteTexture.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            var spriteTexture = GetSpriteTexture();
            if (spriteTexture != null)
                spriteTexture.Draw(spriteBatch, position);
        }





        private ISpriteTexture GetSpriteTexture()
        {
            ISpriteTexture spriteTexture;
            if (_textures.TryGetValue(State, out spriteTexture))
                return spriteTexture;
            if (_textures.Any())
                return _textures.Select(x => x.Value).FirstOrDefault();
            return null;
        }


    }
}

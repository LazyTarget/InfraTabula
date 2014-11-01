using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Xna
{
    public class Sprite : ISprite
    {
        public event EventHandler<MouseDownEventArgs> OnClicked;
        public event EventHandler<MouseMoveEventArgs> OnMouseEntered;
        public event EventHandler<MouseMoveEventArgs> OnMouseLeft;


        public Sprite()
        {

        }

        public Sprite(ISpriteTexture spriteTexture)
            : this()
        {
            if (spriteTexture == null)
                throw new ArgumentNullException("spriteTexture");
            SpriteTexture = spriteTexture;
        }

        public Game Game { get; internal set; }

        public ISpriteTexture SpriteTexture { get; internal set; }

        public Vector2 Position { get; set; }

        public Rectangle Bounds
        {
            get
            {
                var bounds = new Rectangle(
                    (int) Math.Round(Position.X),
                    (int) Math.Round(Position.Y),
                    SpriteTexture.Bounds.Width,
                    SpriteTexture.Bounds.Height);
                return bounds;
            }
        }


        public virtual void Update(GameTime gameTime)
        {
            SpriteTexture.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            SpriteTexture.Draw(spriteBatch, Position);
        }



        #region Events


        internal void _InvokeClick(MouseDownEventArgs args)
        {
            OnClick(args);

            if (OnClicked != null)
                OnClicked(this, args);
        }

        protected virtual void OnClick(MouseDownEventArgs args)
        {
            
        }



        internal void _InvokeMouseEnter(MouseMoveEventArgs args)
        {
            OnMouseEnter(args);

            if (OnMouseEntered != null)
                OnMouseEntered(this, args);
        }

        protected virtual void OnMouseEnter(MouseMoveEventArgs args)
        {

        }



        internal void _InvokeMouseLeave(MouseMoveEventArgs args)
        {
            OnMouseLeave(args);

            if (OnMouseLeft != null)
                OnMouseLeft(this, args);
        }

        protected virtual void OnMouseLeave(MouseMoveEventArgs args)
        {

        }

        #endregion

    }
}

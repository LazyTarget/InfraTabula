namespace InfraTabula.Xna
{
    public static class SpriteExtensions
    {
        public static void TrySetTextureState<TState>(this ISpriteTexture spriteTexture, TState state)
        {
            var t = spriteTexture;

            var multiStateSpriteTexture = t as MultiStateSpriteTexture2D<TState>;
            if (multiStateSpriteTexture != null)
            {
                multiStateSpriteTexture.State = state;
            }
        }

    }
}

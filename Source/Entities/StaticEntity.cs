using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Unknown1.Source.Core;
using Unknown1.Source.Rendering;

namespace Unknown1.Source.Entities;

internal class StaticEntity : IEntity
{
    public Transform Transform { get; }

    public Sprite Sprite { get; }


    public StaticEntity(Game game, Texture2D texture, Rectangle srcRect, int renderOrder, Vector2 position, Vector2? scale = null, float rotation = 0)
    {
        Transform = new(position, scale, rotation);
        Sprite = new(game.Services.GetService<SpriteBatch>(), texture, Transform, srcRect, renderOrder);
    }

    public void Update(float deltaTime) { }

    public void Draw()
    {
        Sprite.Draw();
    }
}

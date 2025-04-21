using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using Unknown1.Source.Core;

namespace Unknown1.Source.Rendering;

internal class Sprite : IComparable<Sprite>
{    
    private readonly Transform _transform;
    private readonly SpriteBatch _spriteBatch;


    public Texture2D Tex { get; set; }

    public int RenderOrder { get; set; }

    public Vector2 Origin { get; set; }

    protected Rectangle SrcRect { get; set; }


    public Sprite(SpriteBatch spriteBatch, Texture2D tex, Transform transform, Rectangle srcRect, int renderOrder)
    {
        _spriteBatch = spriteBatch;
        RenderOrder = renderOrder;
        SrcRect = srcRect;
        Origin = new(SrcRect.Width / 2, SrcRect.Height / 2);
        Tex = tex;
        _transform = transform;
    }

    public void Draw(Vector2 offset = default, Vector2 scaleOffset = default, float rotationOffset = 0f)
    {
        Vector2 position = _transform.Position + offset;
        Vector2 scale = _transform.Scale + scaleOffset;
        float rotation = _transform.Rotation + rotationOffset;

        _spriteBatch.Draw(Tex, position, SrcRect, Color.White, MathHelper.ToRadians(rotation), Origin, scale, SpriteEffects.None, 0f);
    }

    /// <summary>
    /// Lower render order values appear on top. For example, 0 is drawn above 1.<br></br>
    /// If two sprites have the same render order, the one with the greater Y position is rendered on top.
    /// </summary>
    /// <param name="other">The sprite to compare to this.</param>
    /// <returns>
    /// <b>Greater than zero</b> if current is on top of other<br></br>
    /// <b>Zero</b> if current is at the same level of other<br></br>
    /// <b>Lesser than zero</b> if current is below of other
    /// </returns>
    public int CompareTo(Sprite other)
    {
        int order = other.RenderOrder.CompareTo(RenderOrder);
        if (order != 0) return order;

        return (_transform.Position.Y + Origin.Y).CompareTo(other._transform.Position.Y + other.Origin.Y);
    }
}

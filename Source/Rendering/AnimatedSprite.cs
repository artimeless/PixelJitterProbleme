using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Unknown1.Source.Core;

namespace Unknown1.Source.Rendering;

internal class AnimatedSprite : Sprite
{
    private readonly Point _offset;

    private float _frameTimer;
    private float _cooldownTimer;
    private bool _isOnCooldown;

    public uint ColumnCount { get; }

    public uint FrameCount { get; }

    public uint CurrentFrame { get; set; }

    public uint Fps { get; set; }

    public float Cooldown { get; set; }


    public AnimatedSprite(SpriteBatch spriteBatch, Texture2D tex, Transform transform, Rectangle srcRect, int renderOrder, uint columnCount, uint frameCount, uint fps, float cooldown = 0)
    : base(spriteBatch, tex, transform, srcRect, renderOrder)
    {
        _offset = new(srcRect.X, srcRect.Y);
        ColumnCount = columnCount;
        FrameCount = frameCount;
        Cooldown = cooldown;
        Fps = fps;
    }

    public void Update(float deltaTime)
    {
        if (Fps == 0) return;

        if ((Cooldown > 0) && _isOnCooldown)
        {
            _cooldownTimer += deltaTime;
            if (_cooldownTimer >= Cooldown)
            {
                _isOnCooldown = false;
                _cooldownTimer = 0;
            }
        }
        else
        {
            _frameTimer += deltaTime;
            if (_frameTimer >= ((float)1 / Fps))
            {
                _frameTimer = 0;
                CurrentFrame = (CurrentFrame + 1) % FrameCount;
                
                // Every frame has been shown, so we are on cooldown again
                if (CurrentFrame == 0) _isOnCooldown = true;

                RefreshSrcRect();
            }
        }
    }

    public void ResetAnimation(bool resetCooldown = false)
    {
        if (resetCooldown)
        {
            _isOnCooldown = false;
            _cooldownTimer = 0;
        }

        _frameTimer = 0;
        CurrentFrame = 0;
        RefreshSrcRect();
    }

    private void RefreshSrcRect()
    {
        int column = (int)(CurrentFrame % ColumnCount);
        int row = (int)(CurrentFrame / ColumnCount);

        SrcRect = new(_offset.X + (SrcRect.Width * column), _offset.Y + (SrcRect.Height * row), SrcRect.Width, SrcRect.Height);
    }
}

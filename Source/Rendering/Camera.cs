using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Unknown1.Source.Core;
using Unknown1.Source.Entities;

namespace Unknown1.Source.Rendering;

internal class Camera
{
    private readonly GraphicsDeviceManager _graphics;
    private readonly SpriteBatch _spriteBatch;
    private readonly List<IEntity> _entities = new();

    private Matrix _tranformMatrix;


    public Transform Target { get; set; }

    public Vector2 Offset { get; set; }

    public float FollowSpeed { get; set; }

    public Transform Transform { get; }


    public Camera(Game game, Transform target, Vector2 offset = default, float followSpeed = 1000)
    {
        _graphics = game.Services.GetService<GraphicsDeviceManager>();
        _spriteBatch = game.Services.GetService<SpriteBatch>();
        FollowSpeed = followSpeed;
        Offset = offset;
        Target = target;
        Transform = new(Target.Position, new(4, 4));
    }

    public void AddEntity(IEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _entities.Add(entity);
    }

    public void RemoveSprite(IEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _entities.Remove(entity);
    }

    public void Update(float deltaTime)
    {
        float amount = 1 - MathF.Pow(0.5f, deltaTime * FollowSpeed);
        Vector2 lerped = Vector2.Lerp(Transform.Position, Target.Position, amount);

        Vector2 snapped = new(Snap(lerped.X), Snap(lerped.Y));

        // Problem: Pixel jitter
        Transform.Position = lerped;

        //// Problem: Choppy, blocky effect but no pixel jitter
        //Transform.Position = snapped;

        _tranformMatrix =
            Matrix.CreateTranslation(new Vector3(-Transform.Position, 0f)) *                    
            Matrix.CreateRotationZ(MathHelper.ToRadians(-Transform.Rotation)) *
            Matrix.CreateScale(new Vector3(Transform.Scale, 1f)) *
            Matrix.CreateTranslation(new Vector3(_graphics.PreferredBackBufferWidth * 0.5f,
                                                 _graphics.PreferredBackBufferHeight * 0.5f, 0f));
    }

    public void Draw()
    {
        // We place the priority sprite at the end so it renders on top.
        _entities.Sort((e1, e2) => e1.Sprite.CompareTo(e2.Sprite));

        _spriteBatch.Begin(transformMatrix: _tranformMatrix, samplerState: SamplerState.PointClamp);

        foreach (IEntity e in _entities)
            e.Sprite.Draw();

        _spriteBatch.End();
    }

    private float Snap(float x)
    {
        float ratio = 1f / Transform.Scale.X;
        return ((int)x) + MathF.Round((x % 1) / ratio) * ratio;
    }
}

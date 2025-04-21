using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unknown1.Source.Core;
using Unknown1.Source.Input;
using Unknown1.Source.Rendering;

namespace Unknown1.Source.Entities;

internal class Player : IEntity
{
    private readonly SpriteBatch _spriteBatch;
    private readonly InputManager _inputManager;


    public Animator<PlayerState> Animator { get; }

    public Transform Transform { get; }

    public Sprite Sprite { get => Animator.CurrentAnimation; }

    public float Speed { get; set; } = 50f;


    public Player(Game game, Texture2D texture, Vector2 position)
    {
        _spriteBatch = game.Services.GetService<SpriteBatch>();
        _inputManager = game.Services.GetService<InputManager>();

        Transform = new(position, new(1, 1), 0);

        // Walk animations
        AnimatedSprite walkDown  = new(_spriteBatch, texture, Transform, new Rectangle(0, 0,  16, 32), 0, 4, 4, 8);
        AnimatedSprite walkRight = new(_spriteBatch, texture, Transform, new Rectangle(0, 32, 16, 32), 0, 4, 4, 8);
        AnimatedSprite walkUp    = new(_spriteBatch, texture, Transform, new Rectangle(0, 64, 16, 32), 0, 4, 4, 8);
        AnimatedSprite walkLeft  = new(_spriteBatch, texture, Transform, new Rectangle(0, 96, 16, 32), 0, 4, 4, 8);

        // Idle animations
        AnimatedSprite idleDown  = new(_spriteBatch, texture, Transform, new Rectangle(64, 0,  16, 32), 0, 3, 3, 12, 4);
        AnimatedSprite idleRight = new(_spriteBatch, texture, Transform, new Rectangle(64, 32, 16, 32), 0, 3, 3, 12, 4);
        AnimatedSprite idleUp    = new(_spriteBatch, texture, Transform, new Rectangle(64, 64, 16, 32), 0, 3, 3, 12, 4);
        AnimatedSprite idleLeft  = new(_spriteBatch, texture, Transform, new Rectangle(64, 96, 16, 32), 0, 3, 3, 12, 4);

        Animator = new(new KeyValuePair<PlayerState, AnimatedSprite>[]
        {
            // Idle animation pairs
            new(PlayerState.IdleDown,  idleDown), // The first is the default one
            new(PlayerState.IdleRight, idleRight),
            new(PlayerState.IdleUp,    idleUp),
            new(PlayerState.IdleLeft,  idleLeft),

            // Walk animation pairs
            new(PlayerState.WalkDown,  walkDown),
            new(PlayerState.WalkRight, walkRight),
            new(PlayerState.WalkUp,    walkUp),
            new(PlayerState.WalkLeft,  walkLeft),
        });
    }

    public void Update(float deltaTime)
    {
        Vector2 dir = _inputManager.Direction;

        // Logic part //

        Transform.Position += dir * Speed * deltaTime;

        // Rendering part //

        // Walk animations
        if      (dir.X > 0) Animator.SetCurrentAnimation(PlayerState.WalkRight);
        else if (dir.X < 0) Animator.SetCurrentAnimation(PlayerState.WalkLeft);
        else if (dir.Y > 0) Animator.SetCurrentAnimation(PlayerState.WalkDown);
        else if (dir.Y < 0) Animator.SetCurrentAnimation(PlayerState.WalkUp);

        // Idle animations
        else if (Animator.CurrentState == PlayerState.WalkDown)  Animator.SetCurrentAnimation(PlayerState.IdleDown);
        else if (Animator.CurrentState == PlayerState.WalkUp)    Animator.SetCurrentAnimation(PlayerState.IdleUp);
        else if (Animator.CurrentState == PlayerState.WalkRight) Animator.SetCurrentAnimation(PlayerState.IdleRight);
        else if (Animator.CurrentState == PlayerState.WalkLeft)  Animator.SetCurrentAnimation(PlayerState.IdleLeft);

        Animator.Update(deltaTime);
    }

    public void Draw()
    {
        Sprite.Draw();
    }
}

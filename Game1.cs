using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using Unknown1.Source.Data;
using Unknown1.Source.Entities;
using Unknown1.Source.Input;
using Unknown1.Source.Rendering;

namespace Unknown1;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Camera _camera;
    private Player _player;
    private StaticEntity _balloon;
    private InputManager _inputManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Services.AddService(typeof(GraphicsDeviceManager), _graphics);

        _graphics.PreferredBackBufferHeight = 1080 / 2;
        _graphics.PreferredBackBufferWidth = 1920 / 2;
        _graphics.ApplyChanges();

        Content.RootDirectory = "Content";
        Window.Title = "Unknown1";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {   // TODO: Add your initialization logic here

        SettingsManager settingsManager = new();
        Services.AddService(settingsManager);

        _spriteBatch = new(GraphicsDevice);
        Services.AddService(_spriteBatch);

        _inputManager = new(settingsManager.Data.Input);
        Services.AddService(_inputManager);

        _inputManager.OnExit += () => Exit();
        _inputManager.OnInteract += () => { _camera.Target = (_camera.Target == _player.Transform) ? _balloon.Transform : _player.Transform; };

        base.Initialize();
    }

    protected override void LoadContent()
    {   // TODO: use this.Content to load your game content here

        Texture2D playerSheet = Content.Load<Texture2D>("Sprites/PlayerSheet");
        Texture2D balloonTex = Content.Load<Texture2D>("Sprites/balloon");

        _player = new(this, playerSheet, new(0, 0));
        _balloon = new(this, balloonTex, new(0, 0, 16, 24), 0, new(50, 50), new(1, 1));

        _camera = new(this, _player.Transform, _player.Sprite.Origin, 2);
        _camera.AddEntity(_player);
        _camera.AddEntity(_balloon);
    }

    protected override void Update(GameTime gameTime)
    {   // TODO: Add your update logic here

        float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

        _inputManager.Update(deltaTime);
        _player.Update(deltaTime);
        _balloon.Update(deltaTime);
        _camera.Update(deltaTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {   // TODO: Add your drawing code here

        GraphicsDevice.Clear(Color.Black);

        _camera.Draw();

        base.Draw(gameTime);
    }
}

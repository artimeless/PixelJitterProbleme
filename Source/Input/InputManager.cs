using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Unknown1.Source.Data;

namespace Unknown1.Source.Input;

internal class InputManager
{
    public event Action OnLeft;
    public event Action OnRight;
    public event Action OnUp;
    public event Action OnDown;
    public event Action OnInteract;
    public event Action OnExit;

    private Vector2 _direction;
    private KeyboardState _previousKs;
    private GamePadState _previousGs;


    public InputSettings InputSettings { get; }

    public Vector2 Direction { get => _direction; private set => _direction = float.IsNaN(value.X) ? Vector2.Zero : value; }


    public InputManager(InputSettings inputSettings)
    {
        InputSettings = inputSettings;
        _previousKs = Keyboard.GetState();
        _previousGs = GamePad.GetState(PlayerIndex.One);
    }

    public void Update(float deltaTime)
    {
        KeyboardState ks = Keyboard.GetState();
        GamePadState gs = GamePad.GetState(PlayerIndex.One);
        Vector2 ThumbStickDir = Vector2.Round(gs.ThumbSticks.Left);
        int dirX = 0, dirY = 0;

        // Directionnal inputs
        if ((Direction.X != 1) && (ks.IsKeyDown(InputSettings.LeftKey) || ks.IsKeyDown(Keys.Left) ||
                                   gs.IsButtonDown(InputSettings.LeftButton) || (ThumbStickDir.X == -1)))
        {
            OnLeft?.Invoke();
            dirX = -1;
        }
        if ((Direction.X != -1) && (ks.IsKeyDown(InputSettings.RightKey) || ks.IsKeyDown(Keys.Right) ||
                                    gs.IsButtonDown(InputSettings.RightButton) || (ThumbStickDir.X == 1)))
        {
            OnRight?.Invoke();
            dirX = 1;
        }
        if ((Direction.Y != -1) && (ks.IsKeyDown(InputSettings.DownKey) || ks.IsKeyDown(Keys.Down) ||
                                    gs.IsButtonDown(InputSettings.DownButton) || (ThumbStickDir.Y == -1)))
        {
            OnDown?.Invoke();
            dirY = 1;
        }
        if ((Direction.Y != 1) && (ks.IsKeyDown(InputSettings.UpKey) || ks.IsKeyDown(Keys.Up) ||
                                   gs.IsButtonDown(InputSettings.UpButton) || (ThumbStickDir.Y == 1)))
        {
            OnUp?.Invoke();
            dirY = -1;
        }
        Direction = Vector2.Normalize(new(dirX, dirY));

        // Basic inputs
        if (ks.IsKeyDown(Keys.Escape))
            OnExit?.Invoke();

        if (IsKeyFirstTimePressed(ks, InputSettings.InteractKey) || IsButtonFirstTimePressed(gs, InputSettings.InteractButton))
            OnInteract?.Invoke();

        _previousKs = ks;
        _previousGs = gs;
    }

    private bool IsKeyFirstTimePressed(KeyboardState ks, Keys key) => ks.IsKeyDown(key) && _previousKs.IsKeyUp(key);

    private bool IsButtonFirstTimePressed(GamePadState gs, Buttons button) => gs.IsButtonDown(button) && _previousGs.IsButtonUp(button);
}
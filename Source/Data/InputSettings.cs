using Microsoft.Xna.Framework.Input;
using System;

namespace Unknown1.Source.Data;

internal class InputSettings
{
    // Keyboard
    private Keys _upKey = Keys.Z;
    private Keys _leftKey = Keys.Q;
    private Keys _downKey = Keys.S;
    private Keys _rightKey = Keys.D;
    private Keys _interactKey = Keys.E;
    // GamePad
    private Buttons _upButton = Buttons.DPadUp;
    private Buttons _leftButton = Buttons.DPadLeft;
    private Buttons _downButton = Buttons.DPadDown;
    private Buttons _rightButton = Buttons.DPadRight;
    private Buttons _interactButton = Buttons.A;


    // Keyboard

    public Keys UpKey { get => _upKey; set { if (Enum.IsDefined(typeof(Keys), value)) _upKey = value; } } 

    public Keys LeftKey { get => _leftKey; set { if (Enum.IsDefined(typeof(Keys), value)) _leftKey = value; } }

    public Keys DownKey { get => _downKey; set { if (Enum.IsDefined(typeof(Keys), value)) _downKey = value; } }

    public Keys RightKey { get => _rightKey; set { if (Enum.IsDefined(typeof(Keys), value)) _rightKey = value; } }

    public Keys InteractKey { get => _interactKey; set { if (Enum.IsDefined(typeof(Keys), value)) _interactKey = value; } }

    // GamePad

    public Buttons UpButton { get => _upButton; set { if (Enum.IsDefined(typeof(Buttons), value)) _upButton = value; } }

    public Buttons LeftButton { get => _leftButton; set { if (Enum.IsDefined(typeof(Buttons), value)) _leftButton = value; } }

    public Buttons DownButton { get => _downButton; set { if (Enum.IsDefined(typeof(Buttons), value)) _downButton = value; } }

    public Buttons RightButton { get => _rightButton; set { if (Enum.IsDefined(typeof(Buttons), value)) _rightButton = value; } }

    public Buttons InteractButton { get => _interactButton; set { if (Enum.IsDefined(typeof(Buttons), value)) _interactButton = value; } }
}

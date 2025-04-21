using Microsoft.Xna.Framework;

namespace Unknown1.Source.Core;

internal class Transform
{
    private float _rotation;


    public Vector2 Position { get; set; }

    public Vector2 Scale { get; set; }

    /// <summary> Rotation in degree </summary>
    public float Rotation { get => _rotation; set => _rotation = value % 360; }


    public Transform(Vector2 position = default, Vector2? scale = null, float rotation = 0)
    {
        Position = position;
        Rotation = rotation;
        Scale = (scale == null) ? Vector2.One : (Vector2) scale;
    }
}

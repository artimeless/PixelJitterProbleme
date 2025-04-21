using System;
using Unknown1.Source.Core;
using Unknown1.Source.Rendering;

namespace Unknown1.Source.Entities;

internal interface IEntity
{
    public Transform Transform { get; }

    public Sprite Sprite { get; }

    public void Update(float deltaTime);

    public void Draw();
}

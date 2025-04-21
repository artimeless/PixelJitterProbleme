using System;
using System.Collections.Generic;
using System.Linq;

namespace Unknown1.Source.Rendering;

internal class Animator<T> where T : Enum
{
    private readonly Dictionary<T, AnimatedSprite> _animations = new();
    private AnimatedSprite _currentAnimation;

    /// <remarks>If the animation given is the same as the current one, it will be ignored.</remarks>
    public AnimatedSprite CurrentAnimation
    {
        get => _currentAnimation;
        private set
        {
            if (_currentAnimation == value) return;

            // Make sure to reset the previous animation. We could also reset the new one instead.
            _currentAnimation?.ResetAnimation();

            PreviousState = CurrentState;
            _currentAnimation = value;
        }
    }

    public AnimatedSprite PreviousAnimation => GetAnimation(PreviousState);

    public T CurrentState => _animations.FirstOrDefault(p => p.Value == CurrentAnimation).Key;

    public T PreviousState { get; private set; }


    public Animator(KeyValuePair<T, AnimatedSprite>[] animationPairs)
    {
        foreach (var p in animationPairs)
            if (!_animations.TryAdd(p.Key, p.Value))
                throw new InvalidOperationException($"Animation for state {p.Key} already exists.");
        
        // Set the first animation as the current one
        if (animationPairs.Length > 0) CurrentAnimation = animationPairs[0].Value;
    }

    public AnimatedSprite GetAnimation(T state)
    {
        if (!_animations.TryGetValue(state, out AnimatedSprite animatedSprite))
            throw new KeyNotFoundException($"Animation for state {state} doesn't exists.");

        return animatedSprite;
    }

    public void SetCurrentAnimation(T state)
    {
        CurrentAnimation = GetAnimation(state);
    }

    public void ForEachAnimation(Action<AnimatedSprite> action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        foreach (var a in _animations.Values)
            action(a);
    }

    public void Update(float deltaTime)
    {
        CurrentAnimation.Update(deltaTime);
    }
}

using System;

public class NoActions : IInputActionController
{
    public IInputActionController Down(Action onDown) => this;
    public IInputActionController Left(Action onLeft) => this;
    public IInputActionController Right(Action onRight) => this;
    public IInputActionController Up(Action onUp) => this;
    public IInputActionController Fire(Action onFire) => this;

    public void SlideIn() { }
    public void SlideOut() { }

    public void Enable() { }
    public void Disable() { }

    public void ObstacleDetected() { }

    public void Reset() { }
}

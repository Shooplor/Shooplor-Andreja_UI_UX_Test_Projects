using System;

public interface IInputActionController
{
    IInputActionController Up(Action onUp);
    IInputActionController Down(Action onDown);
    IInputActionController Left(Action onLeft);
    IInputActionController Right(Action onRight);
    IInputActionController Fire(Action onFire);

    void SlideIn();
    void SlideOut();
    void ObstacleDetected();
    
    void Reset();

    void Disable();
    void Enable();
}

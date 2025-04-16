using System;
using UnityEngine;

public class RandomInputActions : IInputActionController
{
    private bool isEnabled = true;
    private int moveDirection = -1;
    private float maxTime = 5f;
    private float CountDown;

    private bool isSliding = false;

    public RandomInputActions()
    {
        CountDown = maxTime;
    }

    private void Update()
    {
        CountDown -= Time.deltaTime;
        if (CountDown <= 0)
        {
            moveDirection = Probability.GenerateRandomNumberBetween(0, 4);
            CountDown = maxTime;
        }
    }
    
    public IInputActionController Up(Action onUp)
    {
        Update();
        if (isEnabled && moveDirection ==0)
        {
            if (!isSliding)
                Update();
            onUp();
        }
        //if (isEnabled && moveDirection==0)
            //onUp();
        return this;
    }

    public IInputActionController Down(Action onDown)
    {
        if (isEnabled && moveDirection==1)
        {
            if (!isSliding)
                Update();
            onDown();
        }
        return this;
    }

    public IInputActionController Left(Action onLeft)
    {
        if (isEnabled && moveDirection==2)
        {
            if (!isSliding)
                Update();
            onLeft();
        }
        return this;
    }

    public IInputActionController Right(Action onRight)
    {
        if (isEnabled && moveDirection == 3)
        {
            if (!isSliding)
                Update();
            onRight();
        }
        return this;
    }

    public IInputActionController Fire(Action onFire)
    {
        if (!isEnabled) return this;
            onFire();
        return this;
    }

    public void SlideIn() => isSliding = true;
    public void SlideOut()
    {
        Update();
        isSliding= false;
    }
    
    public void Enable() => isEnabled = true;
    public void Disable() => isEnabled = false;

    public void ObstacleDetected() => Update();

    public void Reset() { }
}

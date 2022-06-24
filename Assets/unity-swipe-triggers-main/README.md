Why is it so hard to find a great swipe manager for Unity ?

# unity-swipe-triggers
A simple swipe event triggers that also works with mouse movement.

# Usage
Attach the **SwipeEventTrigger** component to a gameobject, link its **OnSwipe** event to your scripts function.
![SwipeEventTrigger Component](https://i.imgur.com/zeYALLH.png)

Function prototype should take a [SwipeData](SwipeEventTrigger.cs#L9-L17) argument, defined in **UnityUtility.Events**, it contains:
```c#
public struct SwipeData
{
  public float distance; //The distance of the swipe
  public float angle; //The angle of the swipe, 0 is right
  public Vector2 direction; //The directional vector of the swipe
  public Vector2 start; //The start screen position of the swipe
  public Vector2 end; //The end screen position of the swipe
}
```
# Static Methods


Method | Description
| :------------- | :----------:
[SwipeEventTrigger.GetMoveDirection](SwipeEventTrigger.cs#L88-L109) | Returns the up/down/left/right direction

# Expand it !

If you want to use different event behaviour, like calling a separate event for each direction, you can override **OnSwipeDetected** in inherited class :
```c#
protected override void OnSwipeDetected(SwipeData swipe)
{
    base.OnSwipeDetected(swipe);

    switch (GetMoveDirection(swipe))
    {
        case MoveDirection.Up:
            OnUpSwipe?.Invoke();
            break;
        case MoveDirection.Down:
            OnDownSwipe?.Invoke();
            break;
        case MoveDirection.Left:
            OnLeftSwipe?.Invoke();
            break;
        case MoveDirection.Right:
            OnRightSwipe?.Invoke();
            break;
        default:
            break;
    }
}
```
Example class [SwipeDirectionEventTrigger](SwipeDirectionEventTrigger.cs) is provided.

Hope you found what you needed, use it as you'd like ! 

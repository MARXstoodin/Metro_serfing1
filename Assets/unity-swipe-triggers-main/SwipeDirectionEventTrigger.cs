using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityUtility.Events;

namespace UnityUtility.EventTriggers
{
    public class SwipeDirectionEventTrigger : SwipeEventTrigger
    {
        [Header("Events")]
        public UnityEvent OnUpSwipe;
        public UnityEvent OnDownSwipe;
        public UnityEvent OnLeftSwipe;
        public UnityEvent OnRightSwipe;

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
    }
}

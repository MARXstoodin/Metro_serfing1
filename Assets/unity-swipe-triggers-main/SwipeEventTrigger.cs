using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityUtility.Events
{
    [System.Serializable]
    public struct SwipeData
    {
        public float distance;
        public float angle;
        public Vector2 direction;
        public Vector2 start;
        public Vector2 end;
    }

    public class SwipeEventTrigger : MonoBehaviour
    {
        [Tooltip("How much drag distance recquired to detect input as swipe")]
        public float swipeDetectionThreshold = 100f;

        public UnityEvent<SwipeData> onSwipe;

        private SwipeData currentSwipe;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnStartSwipe();
            }
            else if (Input.GetMouseButton(0))
            {
                OnSwiping();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnEndSwipe();
            }
        }

        /// <summary>
        /// Called when a swipe is detected, triggers events
        /// </summary>
        /// <param name="swipe"> </param>
        protected virtual void OnSwipeDetected(SwipeData swipe)
        {
            onSwipe?.Invoke(swipe);
        }

        /// <summary>
        /// Called when input starts, initializes swipe data
        /// </summary>
        private void OnStartSwipe()
        {
            currentSwipe = new SwipeData
            {
                start = Input.mousePosition,
                end = currentSwipe.start
            };
        }

        /// <summary>
        /// Called on update when input swiping, updates swipe data
        /// </summary>
        private void OnSwiping()
        {
            currentSwipe.end = Input.mousePosition;

            currentSwipe.direction = currentSwipe.end - currentSwipe.start;
            currentSwipe.angle = (Mathf.Atan2(currentSwipe.direction.y, currentSwipe.direction.x) / (Mathf.PI));
            currentSwipe.distance = Vector2.Distance(currentSwipe.start, currentSwipe.end);
        }

        /// <summary>
        /// Called when input ends, sends swipe events
        /// </summary>
        private void OnEndSwipe()
        {
            if (currentSwipe.direction.magnitude >= swipeDetectionThreshold)
            {
                OnSwipeDetected(currentSwipe);
            }
        }

        /// <summary>
        /// Converts swipe data to Up/Down/Left/Right direction
        /// </summary>
        /// <param name="data"></param>
        public static MoveDirection GetMoveDirection(SwipeData swipe)
        {
            return (GetMoveDirection(swipe.direction));
        }

        /// <summary>
        /// Converts direction vector to Up/Down/Left/Right direction
        /// </summary>
        /// <param name="data"></param>
        public static MoveDirection GetMoveDirection(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return (MoveDirection.None);
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                return (direction.x >= 0 ? MoveDirection.Right : MoveDirection.Left);
            else
                return (direction.y >= 0 ? MoveDirection.Up : MoveDirection.Down);
        }
    }
}
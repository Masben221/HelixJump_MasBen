using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelixJump
{
    public abstract class BallEvent : MonoBehaviour
    {
        [SerializeField] private BallController ballController;

        protected virtual void Awake()
        {
            ballController.CollisionSegment.AddListener(OnBallCollisionSegment);
        }

        private void OnDestroy()
        {
            ballController.CollisionSegment.RemoveListener(OnBallCollisionSegment);
        }

        protected virtual void OnBallCollisionSegment(SegmentType type)
        { }

    }
}


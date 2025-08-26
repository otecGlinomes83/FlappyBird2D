using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CollisionDetector : MonoBehaviour
    {
        public event Action Detected;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Detected?.Invoke();
        }
    }
}
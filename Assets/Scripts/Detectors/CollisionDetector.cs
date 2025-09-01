using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class CollisionDetector : MonoBehaviour
    {
        public event Action<Collision2D> Detected;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Detected?.Invoke(collision);
        }
    }
}
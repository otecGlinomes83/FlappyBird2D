using UnityEngine;

namespace Assets.Scripts.Player
{
    public class BirdTracker : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _offsetX;

        private void Update()
        {
            Vector3 position = transform.position;
            position.x = _target.transform.position.x + _offsetX;

            transform.position = position;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}
using UnityEngine;

namespace Assets.Scripts
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private float _damage;

        public void Shoot()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);

            if (hit.collider.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, transform.right * 10f);
        }
    }
}
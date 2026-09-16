using UnityEngine;

namespace ATTILA.CameraSystem
{
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new(0f, 17f, -14f);
        [SerializeField] private float followSmoothTime = 0.18f;
        private Vector3 followVelocity;
        public void SetTarget(Transform value) => target = value;
        private void LateUpdate()
        {
            if (target == null) return;
            transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref followVelocity, followSmoothTime);
            transform.rotation = Quaternion.Euler(52f, 0f, 0f);
        }
    }
}

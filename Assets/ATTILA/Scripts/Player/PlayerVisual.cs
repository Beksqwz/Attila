using UnityEngine;

namespace ATTILA.Player
{
    public enum FacingDirection { N, NE, E, SE, S, SW, W, NW }
    public sealed class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private Transform visualRoot;
        public FacingDirection Facing { get; private set; } = FacingDirection.S;
        public bool IsMoving { get; private set; }
        private void Awake() => visualRoot ??= transform;
        public void SetMovement(Vector3 direction, bool isMoving)
        {
            IsMoving = isMoving;
            float angle = Mathf.Repeat(Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 22.5f, 360f);
            Facing = (FacingDirection)Mathf.FloorToInt(angle / 45f);
            // This component owns direction state. Replace visualRoot with a future 8-direction sprite renderer.
            if (visualRoot != null) visualRoot.localScale = Vector3.one * (isMoving ? 1.03f : 1f);
        }
    }
}

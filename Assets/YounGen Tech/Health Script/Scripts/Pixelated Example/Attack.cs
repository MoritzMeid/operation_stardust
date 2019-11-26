using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Other/Attack")]
    public class Attack : MonoBehaviour {

        [SerializeField]
        private LayerMask _raycastLayer;

        [SerializeField]
        private float _damage = 1;

        public UnityEvent OnAttack;
        public UnityEvent OnEndAttack;

        #region Properties
        /// <summary>
        /// The damage amount done when firing the raycast.
        /// </summary>
        public float Damage {
            get { return _damage; }
            set { _damage = value; }
        }

        /// <summary>
        /// The layer mask to use when firing the raycast.
        /// </summary>
        public LayerMask RaycastLayer {
            get { return _raycastLayer; }
            set { _raycastLayer = value; }
        }
        #endregion

        public void OnMouseUp() {
            StartAttack();
        }

        /// <summary>
        /// Invokes the OnAttack event if this component is enabled.
        /// </summary>
        public void StartAttack() {
            if(enabled)
                if(OnAttack != null) OnAttack.Invoke();
        }

        /// <summary>
        /// Fires a raycast to do damage and invokes the OnEndAttack event.
        /// </summary>
        public void EndAttack() {
            RaycastDamage(Damage);
            if(OnEndAttack != null) OnEndAttack.Invoke();
        }

        /// <summary>
        /// Fires a raycast that does damage to an object within 2 units in front of this object.
        /// </summary>
        /// <param name="damageAmount"></param>
        public void RaycastDamage(float damageAmount) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * .5f, transform.right * Mathf.Sign(transform.localScale.x), 2, RaycastLayer);

            Debug.DrawRay(transform.position, transform.right, Color.red, 5);

            if(hit.collider) {
                Health health = hit.collider.GetComponentInParent<Health>();

                if(health)
                    health.Damage(new HealthEvent(gameObject, damageAmount));
            }
        }
    }
}
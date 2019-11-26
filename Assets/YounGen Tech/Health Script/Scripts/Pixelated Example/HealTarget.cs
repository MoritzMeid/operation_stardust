using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Other/Heal Target")]
    public class HealTarget : MonoBehaviour {

        [SerializeField]
        GameObject _target;

        [SerializeField]
        float _healAmount = 1;

        [SerializeField]
        float _delay = .25f;

        public UnityEvent OnHeal;

        bool canHeal = true;

        #region Properties
        public float Delay {
            get { return _delay; }
            set { _delay = value; }
        }

        public float HealAmount {
            get { return _healAmount; }
            set { _healAmount = value; }
        }

        public GameObject Target {
            get { return _target; }
            set { _target = value; }
        }
        #endregion

        public void OnMouseUp() {
            Heal();
        }

        public void Heal() {
            if(!Target || !canHeal) return;

            Health health = Target.GetComponentInParent<Health>();

            if(health)
                health.Heal(new HealthEvent(gameObject, HealAmount));

            canHeal = false;

            if(OnHeal != null) OnHeal.Invoke();

            StartCoroutine("EnableHealTimer");
        }

        IEnumerator EnableHealTimer() {
            yield return new WaitForSeconds(Delay);

            canHeal = true;
        }
    }
}
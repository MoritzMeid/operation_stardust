using UnityEngine;
using System.Collections;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Effects/Temporary Health Buff")]
    public class TemporaryHealthBuff : MonoBehaviour {

        public GameObject caster;
        public int repitions = 1;
        public float amount;
        public float delayTime;

        [Range(0, 100)]
        public int criticalHitChance = 20;
        public float criticalHitMultiplier = 2;

        [SerializeField]
        bool castOnStart;

        void Start() {
            if(castOnStart) CastBuff();
        }

        public void CastBuff() {
            StartCoroutine("UpdateTime", repitions);
        }

        public IEnumerator UpdateTime(int repeats) {
            for(int i = 0; i < repeats; i++) {
                float deal = amount;

                if(criticalHitChance > 0 && Random.value <= criticalHitChance / 100f)
                    deal *= criticalHitMultiplier;

                SendMessage("ChangeHealth", new HealthEvent(caster, deal), SendMessageOptions.DontRequireReceiver);

                yield return new WaitForSeconds(delayTime);
            }

            Destroy(this);
        }
    }
}
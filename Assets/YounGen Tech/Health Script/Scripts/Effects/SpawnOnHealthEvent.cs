using UnityEngine;
using System.Collections;

namespace YounGenTech.HealthScript {

    [AddComponentMenu("YounGen Tech/Health/Effects/Spawn On Health Event")]
    public class SpawnOnHealthEvent : MonoBehaviour {

        public GameObject prefab;
        public Transform spawnPosition;

        public bool spawnOnHealed = true;
        public bool spawnOnRestored = true;
        public bool spawnOnDamaged = true;
        public bool spawnOnDeath = true;

        public void OnHealed(HealthEvent health) {
            if(spawnOnHealed) Spawn("OnHealed", health);
        }

        public void OnRestored(HealthEvent health) {
            if(spawnOnRestored) Spawn("OnRestored", health);
        }

        public void OnDamaged(HealthEvent health) {
            if(spawnOnDamaged) Spawn("OnDamaged", health);
        }

        public void OnDeath(HealthEvent health) {
            if(spawnOnDeath) Spawn("OnDeath", health);
        }

        void Spawn(string method, HealthEvent health) {
            if(prefab) {
                GameObject go = Instantiate(prefab, spawnPosition ? spawnPosition.position : transform.position, transform.rotation) as GameObject;
                go.SendMessage(method, health, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
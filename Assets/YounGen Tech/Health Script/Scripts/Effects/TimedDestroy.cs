using System.Collections.Generic;
using UnityEngine;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Effects/Timed Destroy")]
    public class TimedDestroy : MonoBehaviour {

        public float time;

        void Start() {
            Destroy(gameObject, time);
        }
    }
}
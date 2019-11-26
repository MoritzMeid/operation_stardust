using UnityEngine;
using System.Collections;

namespace YounGenTech.HealthScript {

    [AddComponentMenu("YounGen Tech/Health/Effects/Destroy Event")]
    public class DestroyEvent : MonoBehaviour {

        [SerializeField, UnityEngine.Serialization.FormerlySerializedAs("destroyThis")]
        /// <summary>The object that will be destroyed on death.</summary>
        GameObject _destroyThis;

        public GameObject DestroyThis {
            get { return _destroyThis; }
            set { _destroyThis = value; }
        }

        public void Destroy() {
            Destroy(DestroyThis);
        }

        void Reset() {
            DestroyThis = gameObject;
        }
    }
}
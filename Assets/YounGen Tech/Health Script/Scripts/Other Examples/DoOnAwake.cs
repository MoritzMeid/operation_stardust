using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Other/Do On Awake")]
    public class DoOnAwake : MonoBehaviour {

        public UnityEvent OnAwake;

        void Awake() {
            if(OnAwake != null) OnAwake.Invoke();
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Other/Toggle Text Info")]
    public class ToggleTextInfo : MonoBehaviour {

        [TextArea, SerializeField]
        string _text;

        [SerializeField]
        Text _textObject;

        Toggle toggleComponent;

        void Awake() {
            toggleComponent = GetComponent<Toggle>();

            if(!toggleComponent) {
                Debug.LogError("No Toggle component found on object '" + gameObject.name + "'. Destroying.");
                Destroy(gameObject);
            }

            toggleComponent.onValueChanged.AddListener(ToggleText);
        }

        public void ToggleText(bool isOn) {
            _textObject.text = toggleComponent.isOn ? _text : "";
        }
    }
}
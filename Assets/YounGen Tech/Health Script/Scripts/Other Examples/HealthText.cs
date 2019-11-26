using UnityEngine;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Other/Health Text")]
    public class HealthText : MonoBehaviour {
        public Health healthComponent;

        public void SetText(float health) {
            GetComponent<TextMesh>().text = "<color=red> Health: " + health.ToString("0") + "/" + healthComponent.MaxValue + "</color>";
        }
    }
}
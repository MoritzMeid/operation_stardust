using UnityEngine;
using System.Collections;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Other/Shoot")]
    public class Shoot : MonoBehaviour {

        public GameObject crosshairs;
        public float damage = 1;

        [Range(0, 100)]
        public int criticalHitChance = 20;
        public float criticalHitMultiplier = 2;

        public MouseButton mouseButton = MouseButton.MouseLeft;

        void Update() {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
                if(!crosshairs.activeSelf) crosshairs.SetActive(true);

                crosshairs.transform.position = hit.point;
            }
            else
                if(crosshairs.activeSelf) crosshairs.SetActive(false);

            if(Input.GetMouseButtonDown((int)mouseButton))
                if(crosshairs.activeSelf) {
                    float deal = damage;

                    if(criticalHitChance > 0 && Random.value <= criticalHitChance / 100f)
                        deal *= criticalHitMultiplier;

                    Health health = hit.collider.GetComponentInParent<Health>();

                    if(health)
                        if(hit.collider.CompareTag("Enemy") || mouseButton == MouseButton.MouseRight)
                            health.Damage(new HealthEvent(gameObject, deal));
                        else if(hit.collider.CompareTag("Friendly"))
                            health.Heal(new HealthEvent(gameObject, deal));
                }
        }

        public enum MouseButton {
            MouseLeft = 0,
            MouseRight = 1
        }
    }
}
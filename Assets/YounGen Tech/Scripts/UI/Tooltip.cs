using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace YounGenTech {
    [AddComponentMenu("YounGen Tech/Scripts/UI/Tooltip")]
    public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        public Canvas tooltipCanvas;

        [Multiline]
        public string info;
        public RectTransform tooltip;
        bool mouseOver;

        void Start() {
            tooltip.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData data) {
            mouseOver = true;

            if(tooltip)
                tooltip.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData) {
            mouseOver = false;

            if(tooltip)
                tooltip.gameObject.SetActive(false);
        }
    }
}
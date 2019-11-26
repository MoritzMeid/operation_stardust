using UnityEngine;
using System.Collections;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Other/Restart Scene")]
    public class RestartScene : MonoBehaviour {
        public void Restart() {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}

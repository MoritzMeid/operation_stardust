using UnityEngine;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Other/Scene Manager")]
    public class SceneManager : MonoBehaviour {

        public void Load(string sceneName) {
            Application.LoadLevel(sceneName);
        }
        public void Load(int sceneNumber) {
            Application.LoadLevel(sceneNumber);
        }

        public void Restart() {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
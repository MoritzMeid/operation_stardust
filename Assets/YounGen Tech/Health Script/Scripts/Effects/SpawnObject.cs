using UnityEngine;
using System.Collections;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Effects/Spawn Object")]
    public class SpawnObject : MonoBehaviour {

        public Transform spawnPosition;
        public Vector3 Position {
            get {
                return spawnPosition ? spawnPosition.position : transform.position;
            }
        }

        public void Instantiate(GameObject gameObject) {
            GameObject go = (GameObject)Object.Instantiate(gameObject);

            go.transform.position = Position;
        }
        public void InstantiateDefaultRotation(GameObject gameObject) {
            Instantiate(gameObject, Position, Quaternion.identity);
        }
        public void InstantiateUseRotation(GameObject gameObject) {
            Instantiate(gameObject, Position, spawnPosition.rotation);
        }
    }
}
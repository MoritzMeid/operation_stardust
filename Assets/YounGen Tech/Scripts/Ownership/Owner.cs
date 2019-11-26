using UnityEngine;
using System.Collections;

namespace YounGenTech {
    [AddComponentMenu("YounGen Tech/Scripts/Other/Owner")]
    /// <summary>
    /// Stores a GameObject that acts as an owner
    /// Also re-routes health events to the owner which "should" have a Health component
    /// </summary>
    public class Owner : MonoBehaviour {

        public GameObject owner;

        public void SetOwner(GameObject owner) {
            this.owner = owner;
        }
    }
}
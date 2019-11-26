using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace YounGenTech.HealthScript {

    /// <summary>This component should go on the killable object. Any object that damages this one will be added to the kill assist list.</summary>
    [RequireComponent(typeof(Health)), AddComponentMenu("YounGen Tech/Health/Assist/Assist")]
    public class Assist : MonoBehaviour {

        [SerializeField]
        float _maxAssistTime = 1;

        /// <summary>This object has died and returns a list of GameObjects that killed it along with this GameObject.</summary>
        public AssistEvent OnKillAssist;

        Health _health;
        Dictionary<GameObject, AssistTimestamp> killAssisters = new Dictionary<GameObject, AssistTimestamp>();
        float lowestTimestamp = float.MaxValue;

        #region Properties
        /// <summary>Health component located on this GameObject.</summary>
        public Health Health {
            get {
                if(!_health) _health = GetComponent<Health>();

                return _health;
            }
        }

        /// <summary>Maximum amount of time to store an assistant.</summary>
        public float MaxAssistTime {
            get { return _maxAssistTime; }
            set { _maxAssistTime = value; }
        }
        #endregion

        void Awake() {
            if(Health) {
                Health.OnDamaged.AddListener(healthEvent => AddAssist(healthEvent.EventObject));
                Health.OnDeath.AddListener(healthEvent => OnDeath());
            }
        }

        void Update() {
            if(Time.time > lowestTimestamp) {
                while(true) {
                    bool found = false;
                    GameObject removeObject = null;

                    foreach(AssistTimestamp timestamp in killAssisters.Values)
                        if((Time.time - timestamp.Time) > MaxAssistTime) {
                            found = true;
                            removeObject = timestamp.AssistObject;
                            lowestTimestamp = timestamp.Time;
                            break;
                        }

                    if(found) killAssisters.Remove(removeObject);
                    else break;
                }
            }
        }

        /// <summary>Adds an object as an assistant and stores its timestamp.</summary>
        public void AddAssist(GameObject assistant) {
            if(!assistant) return;

            float time = Time.time;

            killAssisters[assistant] = new AssistTimestamp(assistant, time);

            if(time < lowestTimestamp)
                lowestTimestamp = time;
        }

        /// <summary>Remove all assisting GameObjects.</summary>
        public void ClearAssists() {
            killAssisters.Clear();
            lowestTimestamp = float.MaxValue;
        }

        void OnDeath() {
            List<GameObject> allAssists = GetAssists();

            if(allAssists.Count > 0)
                if(OnKillAssist != null) OnKillAssist.Invoke(allAssists, gameObject);

            ClearAssists();
        }

        /// <summary>Get all assisting GameObjects.</summary>
        public List<GameObject> GetAssists() {
            return GetAssists(Time.time);
        }

        /// <summary>Get all assisting GameObjects at a time that hasn't passed the MaxAssistTime.</summary>
        public List<GameObject> GetAssists(float time) {
            List<GameObject> list = new List<GameObject>();

            foreach(KeyValuePair<GameObject, AssistTimestamp> killAssister in killAssisters)
                if(killAssister.Key && (time - killAssister.Value.Time) <= MaxAssistTime) list.Add(killAssister.Key);

            return list;
        }

        [Serializable]
        public class AssistEvent : UnityEvent<List<GameObject>, GameObject> { }
    }

    [Serializable]
    public struct AssistTimestamp {
        [SerializeField]
        GameObject _assistObject;

        [SerializeField]
        float _time;

        #region Properties
        public GameObject AssistObject {
            get { return _assistObject; }
            set { _assistObject = value; }
        }
        public float Time {
            get { return _time; }
            set { _time = value; }
        }
        #endregion

        public AssistTimestamp(GameObject gameObject, float time) {
            _assistObject = gameObject;
            _time = time;
        }
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Assist/Display Kills")]
    public class DisplayKills : MonoBehaviour {

        public Queue<Kill> killList = new Queue<Kill>();

        public float displayTime = 5;

        /// <summary>
        /// This is called by the ConnectWithKillDisplay component
        /// </summary>
        public void AddKill(GameObject[] killers, GameObject killed) {
            killList.Enqueue(new Kill(Time.time, killers, killed));
        }

        public void AddKill(IEnumerable<GameObject> killers, GameObject killed) {
            killList.Enqueue(new Kill(Time.time, killers.ToArray(), killed));
        }

        void Update() {
            List<Kill> list = new List<Kill>();

            foreach(Kill message in killList)
                if(Time.time - message.time < displayTime)
                    list.Add(message);

            killList = new Queue<Kill>(list);
        }

        void OnGUI() {
            foreach(Kill message in killList) {
                string killers = "";

                for(int i = 0; i < message.killers.Length; i++) {
                    killers += message.killers[i].name;

                    if(i < message.killers.Length - 1)
                        killers += "+";
                }

                string text = killers + " - " + message.killed.name;
                Rect boxRect = GUILayoutUtility.GetRect(new GUIContent(text), GUI.skin.box);

                boxRect.center = new Vector2(Screen.width * .5f, boxRect.center.y);

                Color boxColor = GUI.color;
                boxColor.a = 1 - (Mathf.InverseLerp(message.time, message.time + displayTime, Time.time) - .3f);
                GUI.color = boxColor;

                GUI.Box(boxRect, text);
                GUI.color = Color.white;
            }
        }

        [System.Serializable]
        public class Kill {
            public float time;
            public GameObject[] killers;
            public GameObject killed;

            public Kill(float time, GameObject[] killers, GameObject killed) {
                this.time = time;
                this.killers = killers;
                this.killed = killed;
            }
        }
    }
}
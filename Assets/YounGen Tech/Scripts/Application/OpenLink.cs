using UnityEngine;

namespace YounGenTech {
	[AddComponentMenu("YounGen Tech/Scripts/Application/Open URL")]
	public class OpenLink : MonoBehaviour {
		public void GoToWebsite(string url) {
			Application.OpenURL(url);
		}
	}
}
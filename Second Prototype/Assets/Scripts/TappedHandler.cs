using System;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
	public class TappedHandler : MonoBehaviour
	{

		public GameObject PrefabButton;

		// Use this for initialization
		void Start ()
		{
			var tg = gameObject.GetComponent<TapGesture>();
			tg.UseUnityEvents = true;
			tg.Tapped += ChangeColor;
			tg.Tapped += ShowActionMenu;
		}

		private void ChangeColor(object sender, EventArgs e)
		{
			var rend = gameObject.GetComponent<Renderer>();
			rend.material.SetColor("_Color", new Color(Random.value, Random.value, Random.value));
		}

		private void ShowActionMenu(object sender, EventArgs e)
		{
			var cam = GameObject.Find("Main Camera").GetComponent<Camera>();
			var screenPosition = cam.WorldToScreenPoint(gameObject.transform.position);
			Debug.Log("Screen pos: " + screenPosition.x + ", " + screenPosition.y);

			var canvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();

			var button = Instantiate(PrefabButton);
			button.transform.SetParent(canvas.transform);
			button.transform.position = screenPosition + new Vector3(50, 50);

			var text = button.GetComponentInChildren<Text>();
			text.text = "Remove";

			//TODO: Connect button to cube somehow. Then make the button remove/destroy the cube.
		}

		// Update is called once per frame
		void Update () {
		
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
	public class TappedHandler : MonoBehaviour
	{
	
		private readonly Vector3 _menuOffset = new Vector3(50, 50);

		public Camera MainCamera;
		public Canvas MainUiCanvas;

		void Start ()
		{
			var tg = gameObject.GetComponent<TapGesture>();
			tg.UseUnityEvents = true;
			tg.Tapped += ShowActionMenu;
		}

		private void ShowActionMenu(object sender, EventArgs e)
		{
			//get scene objects
			var screenPosition = MainCamera.WorldToScreenPoint(gameObject.transform.position);
			//var canvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
			var buttons = new List<Button>();

			//create background button first (as it should be behind the other buttons)
			CreateBackgroundButton(MainUiCanvas, buttons);
			CreateRemoveButton(MainUiCanvas, screenPosition, buttons);
			CreateNewColorButton(MainUiCanvas, screenPosition, buttons);
			CreateDuplicateButton(MainUiCanvas, screenPosition, buttons);
		}

		private void CreateDuplicateButton(Canvas canvas, Vector3 screenPosition, List<Button> buttons)
		{
			var copyButton = CreateButton(canvas, buttons, "Duplicate");
			copyButton.transform.position = screenPosition + _menuOffset + new Vector3(0, 80);
			copyButton.onClick.AddListener(DuplicateGameObject);
			copyButton.onClick.AddListener(() => DestroyActionMenu(buttons));
		}

		private void DuplicateGameObject()
		{
			var copy = Instantiate(gameObject);
			copy.transform.parent = gameObject.transform.parent;
			copy.transform.localScale = gameObject.transform.localScale;

			var oldPos = gameObject.transform.localPosition;
			var oldScale = gameObject.transform.localScale;
			var newPos = new Vector3(oldScale.x, 0, oldScale.z) / -2;
			copy.transform.localPosition = oldPos + newPos;
		}

		private void CreateNewColorButton(Component canvas, Vector3 screenPosition, ICollection<Button> buttons)
		{
			var newColorButton = CreateButton(canvas, buttons, "New Color");
			newColorButton.transform.position = screenPosition + _menuOffset + new Vector3(0, 40);
			newColorButton.onClick.AddListener(ChangeColor);
		}

		private void ChangeColor()
		{
			var rend = gameObject.GetComponent<Renderer>();
			rend.material.SetColor("_Color", new Color(Random.value, Random.value, Random.value));
		}

		private void CreateRemoveButton(Component canvas, Vector3 screenPosition, ICollection<Button> buttons)
		{
			var removeButton = CreateButton(canvas, buttons, "Remove");
			removeButton.transform.position = screenPosition + _menuOffset;
			removeButton.onClick.AddListener(() => DestroyActionMenu(buttons));
			removeButton.onClick.AddListener(() => Destroy(gameObject));
		}

		private static Button CreateButton(Component canvas, ICollection<Button> buttons, string buttonText)
		{
			var button = Instantiate(Resources.Load<GameObject>("Prefabs/Button")).GetComponent<Button>();
			buttons.Add(button);
			button.transform.SetParent(canvas.transform);
			button.GetComponentInChildren<Text>().text = buttonText;
			return button;
		}

		private static void CreateBackgroundButton(Component canvas, ICollection<Button> buttons)
		{
			var backgroundButton = Instantiate(Resources.Load<GameObject>("Prefabs/Background Button")).GetComponent<Button>();
			buttons.Add(backgroundButton);
			backgroundButton.transform.SetParent(canvas.transform);
			backgroundButton.onClick.AddListener(() => DestroyActionMenu(buttons));
		}

		private static void DestroyActionMenu(IEnumerable<Button> buttons)
		{
			buttons
				.Select(b => b.gameObject)
				.ToList()
				.ForEach(Destroy);
		}
	}
}

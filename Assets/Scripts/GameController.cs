﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class GameController : MonoBehaviour {
	
	public Text text_box;
	public Image image;

	DataController data;
	List<Chapter> chapter_list = new List<Chapter>();
	Interaction current_interaction;

	public Sprite sprite1;


	// Use this for initialization
	void Start () {
		data = (DataController)FindObjectOfType(typeof(DataController));
		text_box.text = "Error loading interactions";

		foreach (Interaction interaction in data.chapter_1.interaction_list) {
			if (interaction.tag == "firstText") {
				current_interaction = interaction;
			}
		}

		LoadCurrentInteraction ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadCurrentInteraction() {
		text_box.text = current_interaction.text;

		if (current_interaction.pic == null)
			image.rectTransform.sizeDelta = new Vector2 (0, 0);
		else {
			image.overrideSprite = Resources.Load<Sprite> (current_interaction.pic);
			if (current_interaction.type == Interaction.InteractionType.Paragraph)
				image.rectTransform.sizeDelta = new Vector2 (100, 400);
		}
	}

	public void changeInteraction() {
		
		foreach (var interaction in data.chapter_1.interaction_list) {
			if (interaction.tag == current_interaction.next) {
				current_interaction = interaction;
				break;
			}
		}

		LoadCurrentInteraction ();
	}
}
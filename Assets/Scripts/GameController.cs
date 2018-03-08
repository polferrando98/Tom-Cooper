using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class GameController : MonoBehaviour {
	
	public Text text_box;
	DataController data;
	List<Chapter> chapter_list = new List<Chapter>();


	// Use this for initialization
	void Start () {
		data = (DataController)FindObjectOfType(typeof(DataController));
		text_box.text = "Error loading interactions";
		LoadCurrentInteraction ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadCurrentInteraction() {
		text_box.text = data.chapter_1.current_interaction.text;
	}
}

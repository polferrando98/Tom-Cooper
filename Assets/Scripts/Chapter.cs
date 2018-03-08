using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Chapter{

	public List<Interaction> interaction_list;
	public Interaction current_interaction;

	public Chapter ()
	{

	}

	public void Awake ()
	{
		foreach (Interaction interaction in interaction_list) {
			if (interaction.tag == "firstText") {
				current_interaction = interaction;
			}
		}
	}
		
}

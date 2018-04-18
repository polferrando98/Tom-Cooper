using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Interaction {
	public enum InteractionType {Paragraph, Question, Line, Picture};
	public InteractionType type;
	public Question question;
	public string text;
	public string tag;
	public string next;
	public string pic;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Interaction : MonoBehaviour {
	public enum InteractionType {Paragraph, Question, Line, Picture};
	public InteractionType type;
	public Question question;
}

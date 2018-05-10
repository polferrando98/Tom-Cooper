using UnityEngine;
using System.Collections;

[System.Serializable]

public class Answer {
	public string text;
	public bool is_correct;
	public string next;

    public Answer(string _text, string _next)
    {
        this.text = _text;
        this.next = _next;
    }
}
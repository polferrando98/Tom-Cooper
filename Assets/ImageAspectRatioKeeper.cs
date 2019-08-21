using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAspectRatioKeeper : MonoBehaviour {

    private AspectRatioFitter ar_fitter;
    private Image image;
    private GameController gc;

	// Use this for initialization
	void Start () {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        ar_fitter = gameObject.GetComponent<AspectRatioFitter>();


    }
	
	// Update is called once per frame
	void Update () {

        Sprite display = gc.current_pic;

        float ar = display.rect.width / display.rect.height;
        Debug.Log(display.rect.width);
        ar_fitter.aspectRatio = ar;
    }
}

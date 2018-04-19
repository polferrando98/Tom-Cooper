using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class GameController : MonoBehaviour
{



    public GameObject ParagraphPanel;
    public Text text_box;
    public Image paragraph_image;

    public GameObject FullscreenPicPanel;
    public Image fullscreen_image;

    DataController data;
    List<Chapter> chapter_list = new List<Chapter>();
    Interaction current_interaction;



    // Use this for initialization
    void Start()
    {
        data = (DataController)FindObjectOfType(typeof(DataController));
        text_box.text = "Error loading interactions";

        foreach (Interaction interaction in data.chapter_1.interaction_list)
        {
            if (interaction.tag == "intro_pic_plane")
            {
                current_interaction = interaction;

            }
        }

        LoadCurrentInteraction();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadCurrentInteraction()
    {

        hideAllPanels();

        switch (current_interaction.type)
        {
            case Interaction.InteractionType.FullscreenPicture:
                FullscreenPicPanel.SetActive(true);
                fullscreen_image.overrideSprite = Resources.Load<Sprite>(current_interaction.pic);
                break;
            case Interaction.InteractionType.Paragraph:
                ParagraphPanel.SetActive(true);
                text_box.text = current_interaction.text;
                paragraph_image.overrideSprite = Resources.Load<Sprite>(current_interaction.pic);
                break;
        }
    }

    public void changeInteraction()
    {

        foreach (var interaction in data.chapter_1.interaction_list)
        {
            if (interaction.tag == current_interaction.next)
            {
                current_interaction = interaction;
                break;
            }
        }

        LoadCurrentInteraction();
    }

    public void hideAllPanels()
    {
        ParagraphPanel.SetActive(false);
        FullscreenPicPanel.SetActive(false);
    }
}
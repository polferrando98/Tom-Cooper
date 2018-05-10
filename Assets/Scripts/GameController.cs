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
    public Text paragraph_text_box;
    public Image paragraph_image;

    public GameObject FullscreenPicPanel;
    public Image fullscreen_image;

    public GameObject QuestionPanel;
    public Text question_text_box;
    public Button opt1;
    public Button opt2;
    public Image question_image;

    DataController data;
    List<Chapter> chapter_list = new List<Chapter>();
    Interaction current_interaction;



    // Use this for initialization
    void Start()
    {
        data = (DataController)FindObjectOfType(typeof(DataController));
        paragraph_text_box.text = "Error loading interactions";

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
                paragraph_text_box.text = current_interaction.text;
                paragraph_image.overrideSprite = Resources.Load<Sprite>(current_interaction.pic);
                break;
            case Interaction.InteractionType.Question:
                QuestionPanel.SetActive(true);
                question_text_box.text = current_interaction.question.questionText;
                opt1.GetComponentInChildren<Text>().text = current_interaction.question.answers[0].text;
                opt2.GetComponentInChildren<Text>().text = current_interaction.question.answers[1].text;
                question_image.overrideSprite = Resources.Load<Sprite>(current_interaction.pic);

                //Calls the Opt1OnClick method when you click the Button
                opt1.onClick.AddListener(OnOpt1Click);
                opt2.onClick.AddListener(OnOpt2Click);
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
        QuestionPanel.SetActive(false);
    }

    public void OnOpt1Click()
    {
        foreach (var interaction in data.chapter_1.interaction_list)
        {
            if (interaction.tag == current_interaction.question.answers[0].next)
            {
                current_interaction = interaction;
                break;
            }
        }

        LoadCurrentInteraction();
    }

    public void OnOpt2Click()
    {
        foreach (var interaction in data.chapter_1.interaction_list)
        {
            if (interaction.tag == current_interaction.question.answers[1].next)
            {
                current_interaction = interaction;
                break;
            }
        }

        LoadCurrentInteraction();
    }
}
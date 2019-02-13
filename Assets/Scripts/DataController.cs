using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

 //Needed for Lists
using System.Xml;

 //Needed for XML functionality
using System.Xml.Serialization;

 //Needed for XML Functionality
using System.IO;
using System.Xml.Linq;

using UnityEditor;

//Needed for XDocument


public class DataController : MonoBehaviour
{
    public Interaction[] all_interactions_data;

    TextAsset myText;
    XDocument xmlDoc;
    //create Xdocument. Will be used later to read XML file

    IEnumerable<XElement> chapters;

    IEnumerable<XElement> interactions;

    //Create an Ienumerable list. Will be used to store XML Items.

    public Chapter chapter_1;
    //Initialize List of XMLData objects.

    bool finishedLoading = false;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        chapter_1 = new Chapter();

        //Allows Loader to carry over into new scene 
        LoadXML(); //Loads XML File. Code below. 
        StartCoroutine("AssignData"); //Starts assigning XML data to data List. Code below
    }

    void Update()
    {

        if (finishedLoading)
        {
            chapter_1.Awake();
            SceneManager.LoadScene("MenuScreen"); //Only happens if coroutine is finished 
            finishedLoading = false;
        }

    }

    void LoadXML()
    {
        string path = Application.dataPath + "/Tom-cooper-data.xml";
        myText = (TextAsset)AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));

        //Assigning Xdocument xmlDoc. Loads the xml file from the file path listed. 
        xmlDoc = XDocument.Parse(myText.text);

        //This basically breaks down the XML Document into XML Elements. Used later. 
        chapters = xmlDoc.Descendants("chapter");
    }


    //this is our coroutine that will actually read and assign the XML data to our List
    IEnumerator AssignData()
    {

        /*foreach allows us to look at every Element of our XML file and do something with each one. Basically, this line is saying “for each element in the xml document, do something.*/
        foreach (var chapter_node in chapters)
        {
            interactions = chapter_node.Descendants("interaction");
            List<Interaction> interactions_list = new List<Interaction>();
            //Debug.Log (chapter);

            foreach (var interaction_node in interactions)
            {

                Interaction interaction = new Interaction();

                interaction.tag = interaction_node.Attribute("tag").Value;

                if (interaction_node.Attribute("type").Value == "paragraph")
                {
                    interaction.type = Interaction.InteractionType.Paragraph;

                    IEnumerable<XElement> interaction_childs = interaction_node.Descendants();

                    foreach (var descendant in interaction_childs)
                    {
                        if (descendant.Name == ("text"))
                        {
                            interaction.text = descendant.Value.ToString();
                        }
                        if (descendant.Name == ("next"))
                        {
                            interaction.next = descendant.Value.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", ""); ;
                        }
                        if (descendant.Name == ("pic"))
                        {
                            interaction.pic = descendant.Value.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
                        }
                    }
                }

                if (interaction_node.Attribute("type").Value == "fullscreen_pic")
                {
                    interaction.type = Interaction.InteractionType.FullscreenPicture;

                    IEnumerable<XElement> interaction_childs = interaction_node.Descendants();

                    foreach (var descendant in interaction_childs)
                    {
                        if (descendant.Name == ("next"))
                        {
                            interaction.next = descendant.Value.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", ""); ;
                        }
                        if (descendant.Name == ("pic"))
                        {
                            interaction.pic = descendant.Value.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
                        }
                    }
                }

                if (interaction_node.Attribute("type").Value == "question")
                {
                    interaction.type = Interaction.InteractionType.Question;
                    interaction.question = new Question();
                    interaction.question.answers = new List<Answer>();

                    IEnumerable<XElement> interaction_childs = interaction_node.Descendants();

                    foreach (var descendant in interaction_childs)
                    {
                        if (descendant.Name == ("pic"))
                        {
                            interaction.pic = descendant.Value.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
                        }
                        if (descendant.Name == ("question"))
                        {

                            IEnumerable<XElement> question_nodes = descendant.Descendants();
                            foreach (var question_child in question_nodes)
                            {
                                if (question_child.Name == ("question_text"))
                                {
                                    interaction.question.questionText = question_child.Value.ToString();
                                }
                                if (question_child.Name == ("answer"))
                                {
                                    interaction.question.answers.Add(new Answer(question_child.Value, question_child.Attribute("next").Value));
                                }
                            }
                        }
                    }
                }

                interactions_list.Add(interaction);

                chapter_1.interaction_list = interactions_list;
            }

            finishedLoading = true; //tell the program that we’ve finished loading data. 
            yield return null;
        }
    }

}
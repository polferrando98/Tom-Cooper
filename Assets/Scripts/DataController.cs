using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
	public Interaction[] all_interactions_data;


	// Use this for initialization
	void Start()
	{
		DontDestroyOnLoad(gameObject);

		SceneManager.LoadScene("MenuScreen");
	}

	public Interaction GetCurrentRoundData()
	{
		return all_interactions_data[0];
	}

	// Update is called once per frame
	void Update()
	{

	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class JSONData : MonoBehaviour
{
	string jsonData;
	string _name;
	string _pictureURL;
	string _displayName;
	string _language;
	string _interestID;


	public ScrollRect scrollview;
	public GameObject scrollContent;
	public GameObject scrollObject;
	public GameObject fetchButton;
	public Text messageText;
	string temp1,temp2;
	string temp;


	// Use this for initialization

	//[System.Obsolete]
	public void FetchData()
    {
		StartCoroutine(RequestData());
    }

	[System.Obsolete]
	IEnumerator RequestData()
	{
		// implememt WWW to get json data from any url
		string url = "https://testinterest.s3.amazonaws.com/interest.json";
		WWW www = new WWW(url);
		yield return www;

		// store text in www to json string
		if (string.IsNullOrEmpty(www.error))
		{
			jsonData = www.text;
		}

		// use simpleJSON to get values stored in JSON data for different key value pair
		JSONNode jsonNode = JSON.Parse(jsonData);
		for (int i=0;i<8; i++)
        {

			_name = jsonNode[i][0].ToString();
			_pictureURL = jsonNode[i][1].ToString();
			_displayName = jsonNode[i][2].ToString();
			_language = jsonNode[i][3].ToString();
			_interestID = jsonNode[i][4].ToString();
			showData();

		}

		fetchButton.SetActive(false);
		messageText.text = "Data Fetched Successfully";
	}

	public void showData()
    {
		GameObject scrollItemObj = Instantiate(scrollObject);
		scrollItemObj.transform.SetParent(scrollContent.transform, false);
		scrollItemObj.transform.Find("Name").gameObject.GetComponent<Text>().text = _name.ToString();
		scrollItemObj.transform.Find("URL").gameObject.GetComponent<Text>().text = _pictureURL.ToString();
		scrollItemObj.transform.Find("Display").gameObject.GetComponent<Text>().text = _displayName.ToString();
		scrollItemObj.transform.Find("Lang").gameObject.GetComponent<Text>().text = _language.ToString();
		scrollItemObj.transform.Find("ID").gameObject.GetComponent<Text>().text = _interestID.ToString();
	}
}

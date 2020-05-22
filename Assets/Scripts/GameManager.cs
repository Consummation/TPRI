using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public ActList levelAct;
    public GameObject positiveContent;
    public GameObject negativeContent;
    public GameObject characterContent;
    public GameObject actPrefab;
    public GameObject characterActPrefab;
    public TextMeshProUGUI positiveActsNumberText;
    public TextMeshProUGUI negativeActsNumberText;
    public static int positiveActsNumber;
    public static int negativeActsNumber;
    // Start is called before the first frame update
    void Start()
    {
        positiveActsNumber = 0;
        negativeActsNumber = 0;
        //foreach(var act in levelAct.positiveActs)
        //{
        //    var textObject = Instantiate(actPrefab, positiveContent.transform, false) as GameObject;
        //    textObject.GetComponent<TextMeshProUGUI>().text = act;
        //}
        //foreach(var act in levelAct.negativeActs)
        //{
        //    var textObject = Instantiate(actPrefab, negativeContent.transform, false) as GameObject;
        //    textObject.GetComponent<TextMeshProUGUI>().text = act;
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        positiveActsNumberText.text = positiveActsNumber.ToString();
        negativeActsNumberText.text = negativeActsNumber.ToString();
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    
}

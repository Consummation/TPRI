using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Level_3 : MonoBehaviour, ILevel
{
    public int startCharacterActNumber;
    public int startActNumber;
    public ActList levelAct;
    public GameObject characterContent;
    public GameObject characterActPrefab;
    public GameObject actPrefab;
    public GameObject positiveContent;
    public GameObject negativeContent;
    public int charactersToWin;
    public Image completeScreen;
    public Image character;
    public Sprite[] sprites;
    public AudioSource goodSound;
    public AudioSource badSound;
    public static int positiveNumber;
    public static int negativeNumber;
    private List<string> allActs = new List<string>();
    private int actNumber = 0;
    private int failureTries = 3;
    private int currentPositiveActs = 0;
    private int currentNegativeActs = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var act in levelAct.positiveActs)
        {
            var textObject = Instantiate(actPrefab, positiveContent.transform, false) as GameObject;
            textObject.GetComponent<TextMeshProUGUI>().text = act;
        }
        foreach (var act in levelAct.negativeActs)
        {
            var textObject = Instantiate(actPrefab, negativeContent.transform, false) as GameObject;
            textObject.GetComponent<TextMeshProUGUI>().text = act;
        }
        currentPositiveActs = levelAct.positiveActs.Count;
        currentNegativeActs = levelAct.negativeActs.Count;
        GenerateNewCharacter();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Accept()
    {
        GameManager.positiveActsNumber = 0;
        GameManager.negativeActsNumber = 0;
        if (positiveNumber >= negativeNumber)
        {
            goodSound.Play();
            charactersToWin--;
            Debug.Log("Cool");
        }
        else
        {
            badSound.Play();
            failureTries--;
            Debug.Log("Bad");
        }
        if (failureTries == 0)
        {
            Fail();
        }
        if (charactersToWin == 0)
        {
            Win();
        }
        ClearContents();
        startCharacterActNumber += 2;
        startActNumber += 2;
        character.rectTransform.DOAnchorPosX(-500f, 1f).OnComplete(GenerateNewCharacter);
       // UpdateContents();
    }

    public void Decline()
    {
        GameManager.positiveActsNumber = 0;
        GameManager.negativeActsNumber = 0;
        if (negativeNumber > positiveNumber)
        {
            goodSound.Play();
            charactersToWin--;
            Debug.Log("Cool");
        }
        else
        {
            badSound.Play();
            failureTries--;
            Debug.Log("Bad");
        }
        if (failureTries == 0)
        {
            Fail();
        }
        if (charactersToWin == 0)
        {
            Win();
        }
        ClearContents();
        startCharacterActNumber += 2;
        startActNumber += 2;
        character.rectTransform.DOAnchorPosX(-500f, 1f).OnComplete(GenerateNewCharacter);
        //UpdateContents();
    }

    public void GenerateNewCharacter()
    {
        if (failureTries > 0 && charactersToWin > 0)
        {
            int index = Random.Range(0, sprites.Length);
            character.sprite = sprites[index];
            character.rectTransform.DOAnchorPosX(-150f, 1f).OnComplete(CalculateCharacter);
        }


    }

    public void ClearContents()
    {
        //for (int i = 0; i < startActNumber; i++)
        //{
        //    Destroy(positiveContent.transform.GetChild(i).gameObject);
        //    Destroy(negativeContent.transform.GetChild(i).gameObject);
        //}
        for (int i = 0; i < actNumber; i++)
        {
            for (int j = 0; j < currentPositiveActs; j++)
            {
                if (positiveContent.transform.GetChild(j).GetComponent<TextMeshProUGUI>().text == characterContent.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text)
                {
                    currentPositiveActs--;
                    Destroy(positiveContent.transform.GetChild(j).gameObject);
                    break;
                }
            }
            for (int j = 0; j < currentNegativeActs; j++)
            {
                if (negativeContent.transform.GetChild(j).GetComponent<TextMeshProUGUI>().text == characterContent.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text)
                {
                    currentNegativeActs--;
                    Destroy(negativeContent.transform.GetChild(j).gameObject);
                    break;
                }
            }
        }
    }
    public void UpdateContents()
    {



        for (int i = 0; i < startActNumber; i++)
        {
            var textObject = Instantiate(actPrefab, positiveContent.transform, false) as GameObject;
            textObject.GetComponent<TextMeshProUGUI>().text = levelAct.positiveActs[i];
        }
        for (int i = 0; i < startActNumber; i++)
        {
            var textObject = Instantiate(actPrefab, negativeContent.transform, false) as GameObject;
            textObject.GetComponent<TextMeshProUGUI>().text = levelAct.negativeActs[i];
        }
    }

    public void CalculateCharacter()
    {
        allActs = new List<string>();
        if (actNumber > 0)
        {
            for (int i = 0; i < actNumber; i++)
            {
                Destroy(characterContent.transform.GetChild(i).gameObject);
            }
        }
        foreach (var text in levelAct.positiveActs)
        {
            allActs.Add(text);
        }
        foreach (var text in levelAct.negativeActs)
        {
            allActs.Add(text);
        }
        positiveNumber = 0;
        negativeNumber = 0;
        actNumber = Random.Range(startCharacterActNumber, startCharacterActNumber + 2);
        for (int i = 0; i < actNumber; i++)
        {
            int index = Random.Range(0, allActs.Count);
            var textObject = Instantiate(characterActPrefab, characterContent.transform, false) as GameObject;
            textObject.GetComponent<TextMeshProUGUI>().text = allActs[index];
            if (levelAct.positiveActs.Contains(allActs[index]))
            {
                positiveNumber++;
            }
            if (levelAct.negativeActs.Contains(allActs[index]))
            {
                negativeNumber++;
            }
            allActs.RemoveAt(index);
        }
    }

    public void Fail()
    {
        completeScreen.gameObject.SetActive(true);
        completeScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Поражение";
        completeScreen.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void Win()
    {
        completeScreen.gameObject.SetActive(true);
        completeScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Вы прошли игру";
    }
}

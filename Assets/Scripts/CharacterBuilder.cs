using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class CharacterBuilder : MonoBehaviour, ILevel
{
    public ActList levelAct;
    public GameObject characterContent;
    public GameObject characterActPrefab;
    public GameObject actPrefab;
    public GameObject positiveContent;
    public GameObject negativeContent;
    public Image character;
    public Sprite[] sprites;
    public int charactersToWin;
    public Image completeScreen;
    public AudioSource goodSound;
    public AudioSource badSound;
    public static int positiveNumber;
    public static int negativeNumber;
    private List<string> allActs = new List<string>();
    private int actNumber = 0;
    private int failureTries = 3;

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
        GenerateNewCharacter();
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
        character.rectTransform.DOAnchorPosX(-500f, 1f).OnComplete(GenerateNewCharacter);
        
    }

    public void Decline()
    {
        GameManager.positiveActsNumber = 0;
        GameManager.negativeActsNumber = 0;
        if (negativeNumber > positiveNumber)
        {
            charactersToWin--;
            goodSound.Play();
            Debug.Log("Cool");
        }
        else
        {
            failureTries--;
            badSound.Play();
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
        character.rectTransform.DOAnchorPosX(-500f, 1f).OnComplete(GenerateNewCharacter);
        
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
        actNumber = Random.Range(5, 8);
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
        completeScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Победа";
    }

    public void NewLevel()
    {
        SceneManager.LoadScene("Level2");
    }
    
}

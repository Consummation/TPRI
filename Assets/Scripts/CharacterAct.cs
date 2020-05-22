using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CharacterAct : MonoBehaviour, IPointerClickHandler
{
    private int modeIndex;
    // Start is called before the first frame update
    void Start()
    {
        modeIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData data)
    {
        modeIndex++;
        if (modeIndex > 2)
        {
            modeIndex = 0;
        }
        switch(modeIndex)
        {
            case 0:
                GetComponent<TextMeshProUGUI>().color = Color.black;
                GameManager.negativeActsNumber--;
                break;
            case 1:
                GetComponent<TextMeshProUGUI>().color = Color.green;
                GameManager.positiveActsNumber++;
                break;
            case 2:
                GetComponent<TextMeshProUGUI>().color = Color.red;
                GameManager.positiveActsNumber--;
                GameManager.negativeActsNumber++;
                break;

        }
    }
    
}

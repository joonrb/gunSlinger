using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<floatingText> floatingTexts = new List<floatingText>();

    public void show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration){
        floatingText floatingTxt = getFloatingText();
        floatingTxt.txt.text = msg;
        floatingTxt.txt.fontSize = fontSize;
        floatingTxt.txt.color = color;
        
        floatingTxt.go.transform.position = Camera.main.WorldToScreenPoint(position); //Transfer world space to screen space
        floatingTxt.motion = motion;
        floatingTxt.duration = duration;

        floatingTxt.show();
    }

    private void Update(){
        foreach(floatingText txt in floatingTexts){
            txt.updateFloatingText();
        }
    }

    private floatingText getFloatingText(){
        floatingText txt = floatingTexts.Find(t => !t.active);

        if(txt == null){
            txt = new floatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }
        return txt;
    }
}

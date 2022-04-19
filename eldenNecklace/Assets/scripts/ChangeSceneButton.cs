using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void changeScene(string sceneName){
        LevelManager.Instance.LoadScene(sceneName);
    }
}

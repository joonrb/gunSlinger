using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null){
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += loadState;
        DontDestroyOnLoad(this.gameObject);
    }

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;

    public player player;
    public floatingTextManager floatingTextManager;

    public int exp;

    public void showText(string msg, int fontsize, Color color, Vector3 position, Vector3 motion, float duration){
        floatingTextManager.show(msg, fontsize, color, position, motion, duration);
    }

    public void SaveState(){
        string saving = "|";

        saving += exp.ToString() + "|";

        PlayerPrefs.SetString("SaveState", saving);
    }

    public void loadState(Scene s, LoadSceneMode mode){
        if(!PlayerPrefs.HasKey("SaveState"))
            return;
        
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        exp = int.Parse(data[0]);
    }
}

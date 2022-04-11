using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    public GameObject myPrefab;
    private float coolDown = 0.1f;
    private float lastShoot;
    private GameObject playerObject;
    private Vector3 barrel;
    public player player;

    protected void Start(){
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerObject = GameObject.Find("player");
    }
    
    protected void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(Time.time - lastShoot > coolDown){
                lastShoot = Time.time;
                shoot();
            }
        }
    }

    private void shoot(){
        if(playerObject.transform.localScale.x == 1){
            barrel = new Vector3(0.13f,0,0);
        }
        else if(playerObject.transform.localScale.x == -1){
            barrel = new Vector3(-0.13f,0,0);
        }    
        Instantiate(myPrefab, transform.position + barrel, transform.rotation);
        Debug.Log(transform.rotation);
    }
}


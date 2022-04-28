using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : character
{
    //experience
    public int xpValue = 1;

    //logic
    private bool playerCollision;
    private Transform playerTransform;
    private Vector3 startingPosition;
    private Vector3 barrel = new Vector3(-0.2f,0,0);
    public GameObject placeBulletHere;

    //random motion
    private int nextMotion = 0;
    private Vector3 randomMovement;

    //hitbox
    private BoxCollider2D hitBox;
    public ContactFilter2D filter;
    private Collider2D [] hits = new Collider2D[10];

    protected override void Start(){
        base.Start();
        playerTransform = GameObject.Find("player").transform; //Or can use GameObject.Find("player").transform   || gameManager.instance.player.transform
        startingPosition = transform.position;

        //in order avoid confusion in which boxcollider component to get, use GetChild function to get hitBox(child of small enemy) boxcollider
        hitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private float getRandomXMovementValue(){
        int dice = Random.Range(0,2);
        float result = 0;
        if(playerTransform.position.x > this.transform.position.x) result = .32f;
        else if(playerTransform.position.x < this.transform.position.x) result = -.32f;

        return result;
    }

    private float getRandomYMovementValue(){
        int dice = Random.Range(0,2);
        float result = 0;
        if(playerTransform.position.y > this.transform.position.x) result = .32f;
        else if(playerTransform.position.y < this.transform.position.x) result = -.32f;

        return result;
    }

    //Random movement function
    private Vector3 randomMovementExec(){
        Vector3 temp = new Vector3(0,0,0);
        int motionSwitch = Random.Range(0,6);

        if(motionSwitch < 4){
            temp = new Vector3(0,0,0);
        }
        else if(motionSwitch == 4){
            temp = new Vector3(0,getRandomYMovementValue(),0);
        }
        else if(motionSwitch == 5){
            temp = new Vector3(getRandomXMovementValue(),0,0);
        }
        
        return temp;
    }

    private void Update(){
        //random movements
        if(Time.time >=nextMotion){
            nextMotion = Mathf.FloorToInt(Time.time) + 1;
            randomMovement = randomMovementExec();
            shoot();
        }
        UpdateMotor(randomMovement);
        transform.rotation = playerTransform.position.x < this.transform.position.x ? Quaternion.Euler(0,180,0) : Quaternion.identity;
        if(playerTransform.position.x > this.transform.position.x){
            barrel = new Vector3(0.15f,0,0);
            //transform.rotation = Quaternion.identity;
        }
        else if(playerTransform.position.x < this.transform.position.x){
            barrel = new Vector3(-0.15f,0,0);
        }
    }

    protected override void death(){
        Destroy(gameObject);
        GameManager.instance.exp += xpValue;
        GameManager.instance.showText("+" + xpValue + "xp", 30, Color.blue, transform.position, Vector3.up * 20, 0.75f);
    }

    private void shoot(){
        Instantiate(placeBulletHere, transform.position + barrel, transform.rotation);
    }


}

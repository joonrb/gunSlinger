using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : character
{
    //experience
    public int xpValue = 1;

    //logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool playerCollision;
    private Transform playerTransform;
    private Vector3 startingPosition;

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

    private void FixedUpdate(){
        //checking if player is in range
        
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength){
            //start chasing
            
            if(Vector3.Distance(playerTransform.position, startingPosition) < triggerLength){
                chasing = true;
                
            }

            if(chasing){
                if(!playerCollision){
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                    
                }
            }
            else{
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else{
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }
        
        //check for collision
        playerCollision = false;
        boxCollider.OverlapCollider(filter, hits);
        for(int i = 0; i < hits.Length; i++){
            if(hits[i] == null)
                continue;

            if(hits[i].tag == "Character" && hits[i].name == "player"){
                playerCollision = true;
            }
            hits[i] = null;
        }
    }

    protected override void death(){
        Destroy(gameObject);
        GameManager.instance.exp += xpValue;
        GameManager.instance.showText("+" + xpValue + "xp", 30, Color.blue, transform.position, Vector3.up * 20, 0.75f);
    }
}

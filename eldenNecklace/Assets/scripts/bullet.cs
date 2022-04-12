using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : collidable
{
    private SpriteRenderer spriteRenderer;
    public int damagePoint = 1;
    public float pushForce = 1.0f;
    private float bulletSpeed = 3.0f;
    private Transform playerTransform;
    private bool onMotionL = false;
    private bool onMotionR = false;
    public player player;
    

    protected override void Start(){
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //ani = GetComponent<Animator>();
        playerTransform = GameObject.Find("player").transform;
    }

    private void FixedUpdate(){
        
        if(playerTransform.localScale.x == 1){
            onMotionR = true;
            if(onMotionR && !onMotionL){
                transform.Translate(bulletSpeed * Time.deltaTime, 0, 0);
            }
            if(onMotionL){
                transform.Translate(-bulletSpeed * Time.deltaTime, 0, 0);
            }
        }
        else if(playerTransform.localScale.x == -1){
            onMotionL = true;
            if(onMotionL && !onMotionR){
                transform.Translate(-bulletSpeed * Time.deltaTime, 0, 0);
            }
            if(onMotionR){
                transform.Translate(bulletSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    protected override void onCollide(Collider2D coll){
        if(coll.tag == "Character"){
            if(coll.name == "Player"){
                return;
            }

            //create a damage object and send to enemy
            damage dmg = new damage{
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("receiveDamage", dmg);
            Destroy(gameObject);
        }

        if(coll.tag == "wall"){
            Destroy(gameObject);
        }
        if(coll.tag == "shield"){
            Destroy(gameObject);
        }
    }
}

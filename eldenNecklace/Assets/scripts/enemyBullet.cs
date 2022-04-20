using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : collidable
{
    private SpriteRenderer spriteRenderer;
    public int damagePoint = 1;
    public float pushForce = 1.0f;
    private float bulletSpeed = 3.0f;
    private Transform playerTransform;
    

    protected override void Start(){
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //ani = GetComponent<Animator>();
        playerTransform = GameObject.Find("player").transform;
    }

    private void FixedUpdate(){
        
        /*if(playerTransform.position.x > transform.position.x){
            transform.position += -tranform.right * Time.deltaTime * bulletSpeed;
        }
        else if(playerTransform.position.x < transform.position.x){
            transform.Translate(-bulletSpeed * Time.deltaTime, 0, 0);
        }*/
        transform.position += -transform.right * Time.deltaTime * bulletSpeed;
    }

    protected override void onCollide(Collider2D coll){
        if(coll.tag == "Character"){
            if(coll.name == "player"){
                //create a damage object and send to enemy
                damage dmg = new damage{
                    damageAmount = damagePoint,
                    origin = transform.position,
                    pushForce = pushForce
                };

                coll.SendMessage("receiveDamage", dmg);
                Destroy(gameObject);
            }
        }

        if(coll.tag == "wall"){
            Destroy(gameObject);
        }
        if(coll.tag == "shield"){
            Destroy(gameObject);
        }
    }
}
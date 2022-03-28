using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : collidable
{
    private SpriteRenderer spriteRenderer;
    public int damagePoint = 1;
    public float pushForce = 1.0f;
    private float bulletSpeed = 3.0f;
    

    protected override void Start(){
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //ani = GetComponent<Animator>();
    }

    private void FixedUpdate(){
        transform.Translate(-bulletSpeed * Time.deltaTime, 0, 0);
    }

    protected override void onCollide(Collider2D coll){
        if(coll.tag == "Character"){
            if(coll.name == "enemy"){
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
    }
}
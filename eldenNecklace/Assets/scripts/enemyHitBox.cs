using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHitBox : collidable
{
    public int damage = 1;
    public float pushForce = 2.0f;

    protected override void onCollide(Collider2D coll){
        if(coll.name == "Player"){
            //create a new damage object to send it to player
            damage dmg = new damage{
                damageAmount = damage,
                pushForce = pushForce,
                origin = transform.position
            };

            coll.SendMessage("receiveDamage",dmg);
        }
    }
}

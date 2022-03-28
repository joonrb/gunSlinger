using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
//----------Combat Area
    //public field
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //immunity
    protected float immuneTime = 0.3f;
    protected float lastImmune;

    //push
    protected Vector3 pushDirection;
    
    //intake damage
    
    protected virtual void receiveDamage(damage dmg){
        if(Time.time - lastImmune > immuneTime){
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            GameManager.instance.showText(dmg.damageAmount.ToString(),30,Color.red, transform.position,Vector3.up * 10, 0.5f);
        }

        

        if(hitPoint <= 0){
            hitPoint = 0;
            death();
        }
    }

    //death.
    protected virtual void death(){

    }

//----------Motion Area
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected float xSpeed = 0.3525f;
    protected float ySpeed = 0.5f;
    protected RaycastHit2D hit;
    

    protected virtual void Start(){
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input){
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        if(moveDelta.x > 0){
            transform.localScale = Vector3.one;
        }
        else if(moveDelta.x < 0){
            transform.localScale = new Vector3(-1,1,1);
        }

        moveDelta += pushDirection;

        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);
        
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null){
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null){
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : character
{
    protected float playerXspeed = 1.0f;
    protected float playerYspeed = 0.75f;

    public SpriteRenderer spriteRenderer;
    public Sprite upSprite;
    public Sprite normalSprite;
    public bool faceUp = false;
    
    protected override void Start(){
        base.Start();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected override void UpdateMotor(Vector3 input){
        moveDelta = new Vector3(input.x * playerXspeed, input.y * playerYspeed, 0);

        if(moveDelta.x > 0){
            spriteRenderer.sprite = normalSprite;
            transform.localScale = Vector3.one;
        }
        else if(moveDelta.x < 0){
            spriteRenderer.sprite = normalSprite;
            transform.localScale = new Vector3(-1,1,1);
        }
        else if(moveDelta.y > 0){
            spriteRenderer.sprite = upSprite;
            faceUp = true;
        }
        else if(moveDelta.y < 0){
            spriteRenderer.sprite = normalSprite;
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

    private void FixedUpdate(){
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x,y,0));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : character
{
    protected float playerXspeed = 0.6f;
    protected float playerYspeed = 0.8f;

    //public float MovementSpeed = 1;

    public SpriteRenderer spriteRenderer;
    public Sprite upSprite;
    public Sprite normalSprite;
    public bool faceUp = false;
    public GameObject gun;
    public GameObject shield;
    
    protected override void Start(){
        base.Start();
        gun = transform.GetChild(0).gameObject;
        shield = transform.GetChild(1).gameObject;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        shield.SetActive(false);
    }

    protected override void UpdateMotor(Vector3 input){
        
        moveDelta = new Vector3(input.x * playerXspeed, input.y * playerYspeed, 0);
        /*
        if(moveDelta.x > 0){
            spriteRenderer.sprite = normalSprite;
            transform.localScale = Vector3.one;
        }
        else if(moveDelta.x < 0){
            spriteRenderer.sprite = normalSprite;
            transform.localScale = new Vector3(-1,1,1);
        }*/
        
        moveDelta += pushDirection;

        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        
        if(!Mathf.Approximately(0,input.x))
            transform.rotation = input.x < 0 ? Quaternion.Euler(0,180,0) : Quaternion.identity;
        
        /*
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null){
            transform.position += moveDelta * Time.deltaTime;
            
            //transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null){
            transform.position += moveDelta * Time.deltaTime;
            
            //transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
        */
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, moveDelta.y), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null){
            transform.position += moveDelta * Time.deltaTime;
            //transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    private void Update(){
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        UpdateMotor(new Vector3(x,y,0));
        
        if(Input.GetKeyDown(KeyCode.C)){
            shield.SetActive(true);
            gun.SetActive(false);
        }
        if(Input.GetKeyUp(KeyCode.C)){
            shield.SetActive(false);
            gun.SetActive(true);
        }
    }

    protected override void death(){
        Destroy(gameObject);
        Debug.Log("You Died");
    }

}

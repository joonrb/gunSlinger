using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;
    
    private void LateUpdate(){
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        if(deltaX > boundX || deltaX < -boundX){
            if(transform.position.x < lookAt.position.x){
                deltaX = deltaX - boundX;
            }
            else{
                deltaX = deltaX + boundX;
            }
        }

        float deltaY = lookAt.position.y - transform.position.y;
        if(deltaY > boundY || deltaY < -boundY){
            if(transform.position.y < lookAt.position.y){
                deltaY = deltaY - boundY;
            }
            else{
                deltaY = deltaY + boundY;
            }
        }

        transform.position += new Vector3(deltaX, deltaY, 0);
    }
}

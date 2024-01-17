using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsFrog : MonoBehaviour {
    bool isJumping = false;
    private Vector3 velocity;

    private Vector3 jumpStartPoint;
    private Vector3 jumpFinalPoint;

    private int stepIndex;
    // Start is called before the first frame update
    void Start() {
        Vector3 position = transform.position;
        stepIndex = -1;
        position.x = GameManager.instance.xCoordinate(stepIndex);
        position.y = -1.5f;
        transform.position = position;        
    }

    // Update is called once per frame
    void Update() {

        if( ! isJumping)  {
            if(Input.GetKeyDown(KeyCode.Space)) {
                if(stepIndex == 9) {
                    stepIndex = -1;
                    Vector3 position = transform.position;                
                    position.x = GameManager.instance.xCoordinate(stepIndex);
                    transform.position = position;
                    GameManager.instance.EndTrial();                
                } else {
                    stepIndex = Random.Range(stepIndex +1, 10);
                    GameManager.instance.AddJump();
                    MoveFrog();
                }
            }
        } else {
            velocity += Physics.gravity * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
        
    }

    private void MoveFrog() {
        isJumping = true;

        jumpStartPoint = transform.position;
        jumpFinalPoint = new Vector3(GameManager.instance.xCoordinate(stepIndex), jumpStartPoint.y, jumpStartPoint.z);

        float jumpHeight = 2f;
        float upMovementTime = Mathf.Sqrt(2 * jumpHeight / Physics.gravity.magnitude);

        velocity.x = (jumpFinalPoint.x - jumpStartPoint.x) / (upMovementTime*2);
        velocity.y = - Physics.gravity.y * upMovementTime;
        velocity.z = 0;
    }
}

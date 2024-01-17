using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicFrog : MonoBehaviour {
    private int stepIndex;
    private Vector3 jumpStartPoint;
    private Vector3 jumpCentralPoint;
    private Vector3 jumpFinalPoint;

    
    private float T; //El tiempo total de viaje calculado para uno de los tramos de movimiento
    private float t; //Fraccion del tramo de movimiento que se ha movido la rana, entre 0 y 1 sirve para hacer la interpolacion
    private float speed = 5; //La velocidad constante a la que se mueve la rana en sus saltos

    private bool isGoingUp = false;
    private bool isGoingDown = false;


    // Start is called before the first frame update

    void Start() {
        //La posicion inicial de la rana es un lugar a la izquierda respecto a la piedra de índice 0
        //es decir, como si fuera la posicion de la piedra -1
        Vector3 position = transform.position;
        stepIndex = -1;
        t = 0f;
        position.x = GameManager.instance.xCoordinate(stepIndex);
        position.y = -1.5f;
        transform.position = position;
    }

    // Update is called once per frame
    void Update() {

        if(isGoingUp) {
            t += Time.deltaTime / T;
            transform.position = InterpolatePosition(jumpStartPoint, jumpCentralPoint, t);
            if(t >= 1) {
                isGoingUp = false;
                isGoingDown = true;
                t = 0;            
            }
            Debug.Log("Subiendo");
        } else if(isGoingDown) {
            t += Time.deltaTime / T;
            transform.position = InterpolatePosition(jumpCentralPoint, jumpFinalPoint, t);
            if(t >= 1) {
                isGoingDown = false;
                t = 0;
            }
        } else if(Input.GetKeyDown(KeyCode.Space)) {
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
    }

    private void MoveFrog() {
        jumpStartPoint = transform.position;
        jumpFinalPoint = new Vector3(GameManager.instance.xCoordinate(stepIndex), jumpStartPoint.y, jumpStartPoint.z);
        jumpCentralPoint = new Vector3((jumpStartPoint.x + jumpFinalPoint.x) /2, jumpStartPoint.y+2,jumpStartPoint.z);

        T = (jumpCentralPoint - jumpStartPoint).magnitude / speed;
        t = 0;
        isGoingUp = true;
        Debug.Log("StartJumping");

    }

    private Vector3 InterpolatePosition(Vector3 startPosition, Vector3 endPosition, float t) {
        return Vector3.Lerp(startPosition, endPosition, t);
    }
}

using UnityEngine;

public class KinematicFrog : MonoBehaviour
{
    private int stepIndex = -1;
    private Vector3 jumpStartPoint;
    private Vector3 jumpCentralPoint;
    private Vector3 jumpFinalPoint;

    private float T;
    private float t = 0;
    private float speed = 2;
    private bool isGoingUp = false;
    private bool isGoingDown = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = transform.position;
        position.x = GameManager.instance.xCoordinate(stepIndex);
        position.y = -1.5f;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoingUp) {
            t += Time.deltaTime / T;
            Debug.Log(t);
            if (t > 1) {
                isGoingUp = false;
                isGoingDown = true;
                t = 0;
            }
            transform.position = InterpolatePosition(jumpStartPoint, jumpCentralPoint, t);
        } else if (isGoingDown) {
            t += Time.deltaTime / T;
            if (t > 1) {
                isGoingDown = false;
                t = 0;
            }
            transform.position = InterpolatePosition(jumpCentralPoint, jumpFinalPoint, t);
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            if (stepIndex == 0) {
                stepIndex = -1;
                Vector3 position = transform.position;
                position.x = GameManager.instance.xCoordinate(stepIndex);
                transform.position = position;
            } else {
                stepIndex = Random.Range(stepIndex + 1, 10);
                MoveFrog();
            }
        }
    }

    private void MoveFrog()
    {
        jumpStartPoint = transform.position;
        jumpFinalPoint = new Vector3(GameManager.instance.xCoordinate(stepIndex), jumpStartPoint.y, jumpStartPoint.z);
        jumpCentralPoint = new Vector3((jumpStartPoint.x + jumpStartPoint.y) / 2, jumpStartPoint.y + 2, jumpStartPoint.z);

        T = (jumpCentralPoint - jumpStartPoint).magnitude / speed;
        isGoingUp = true;
    }

    private Vector3 InterpolatePosition(Vector3 startPosition, Vector3 endPosition, float t)
    {
        return Vector3.Lerp(startPosition, endPosition, t);
    }
}

using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Playercontroller player;
    public Transform focusPoint;
    public float height = 1.4f;
    public float distance = 6.4f;
    public float rotationDamping = 3;
    public float heightDamping = 2;
    public float zoomRacio = 0.5f;
    public float defaultFOV = 60;
    private Vector3 rotationVector;
    public bool canRot = true;

    void Start() {
        focusPoint.transform.parent.transform.parent = null;
    }

    void Update()
    {
        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    void LateUpdate() {
        float wantedAngle = rotationVector.y;
        float wantedHeight = focusPoint.position.y + height;
        float myAngle = transform.eulerAngles.y;
        float myHeight = transform.position.y;
        myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);
        myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime);
        Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);
        transform.position = focusPoint.position + new Vector3(0, wantedHeight, 0);
        transform.position -= currentRotation * Vector3.forward * distance;
        transform.LookAt(focusPoint);
    }
    void FixedUpdate() {
        if (canRot && player != null)
        {
            if (player.input.GetAxis("Gas") < 0)
            {
                rotationVector.y = focusPoint.eulerAngles.y + 180;
            }
            else if (player.input.GetAxis("Gas") > 0)
            {
                rotationVector.y = focusPoint.eulerAngles.y;
            }
            GetComponent<Camera>().fieldOfView = defaultFOV + player.input.GetAxis("Gas") * zoomRacio;
        }
    }
}

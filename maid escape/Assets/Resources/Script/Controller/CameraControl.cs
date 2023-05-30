using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 show;

    private Vector3 FirstPos = new Vector3(0,4,-10);

    public float cameraSpeed = 4f;
    //카메라 중심부 위치
    public Vector2 offset;

    [SerializeField] 
    private float limitMinX, limitMinY, limitMaxX, limitMaxY;
    private float cameraHalfWidth, cameraHalfHeight;

    void Init()
    {
        this.transform.position = FirstPos;    
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),   // X
            Mathf.Clamp(target.position.y + offset.y, limitMinY + cameraHalfHeight, limitMaxY - cameraHalfHeight), // Y
            -10);                                                                                                  // Z
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * cameraSpeed);
    }
}

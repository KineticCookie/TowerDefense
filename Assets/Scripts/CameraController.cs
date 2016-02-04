using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float HorizontalSpeed = 40;
    public float VerticalSpeed = 40;
    public float CameraDistance = 30;

    public float Boundary = 1;

    private float ScreenWidth;
    private float ScreenHeight;

    // Use this for initialization
    void Start () {
        ScreenHeight = Screen.height;
        ScreenWidth = Screen.width;
        transform.rotation = Quaternion.Euler(45, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.mousePosition.x > ScreenWidth - Boundary)
        {
            transform.position += new Vector3( Time.deltaTime * HorizontalSpeed, 0.0f, 0.0f);
        }

        if (Input.mousePosition.x < 0 + Boundary)
        {
            transform.position -= new Vector3( Time.deltaTime * HorizontalSpeed, 0.0f, 0.0f);
        }

        if (Input.mousePosition.y > ScreenHeight - Boundary)
        {
            transform.position += new Vector3(0.0f, 0.0f, Time.deltaTime * VerticalSpeed);
        }

        if (Input.mousePosition.y < 0 + Boundary)
        {
            transform.position -= new Vector3(0.0f, 0.0f, Time.deltaTime * VerticalSpeed);
        }
    }
}

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    #region Fields
    /// <summary>
    /// Horizontal scrolling speed
    /// </summary>
    public float horizontalSpeed = 20;

    /// <summary>
    /// Vertical scrolling speed
    /// </summary>
    public float verticalSpeed = 20;

    /// <summary>
    /// Offset from screen borders to trigger scrolling
    /// </summary>
    public float boundary = 1;

    /// <summary>
    /// Screen width
    /// </summary>
    private float screenWidth;

    /// <summary>
    /// Screen height
    /// </summary>
    private float screenHeight;

    private float minFOV = 20;
    private float maxFOV = 60;
    private float deltaFOV = 5;
    #endregion

    #region Behaviour
    /// <summary>
    /// Camera initialization
    /// </summary>
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        transform.rotation = Quaternion.Euler(60, 0, 0);
    }

    /// <summary>
    /// RTS-like camera logic is implemented here.
    /// </summary>
    void Update()
    {
        if (Input.mousePosition.x > screenWidth - boundary)
        {
            transform.position += new Vector3(Time.deltaTime * horizontalSpeed, 0.0f, 0.0f);
        }
        else if (Input.mousePosition.x < 0 + boundary)
        {
            transform.position -= new Vector3(Time.deltaTime * horizontalSpeed, 0.0f, 0.0f);
        }

        if (Input.mousePosition.y > screenHeight - boundary)
        {
            transform.position += new Vector3(0.0f, 0.0f, Time.deltaTime * verticalSpeed);
        }
        else if (Input.mousePosition.y < 0 + boundary)
        {
            transform.position -= new Vector3(0.0f, 0.0f, Time.deltaTime * verticalSpeed);
        }

        if(Input.GetAxis(Constants.Axes.MouseScrollWheel) > 0) // zoom out
        {
            Camera.main.fieldOfView = Mathf.Max(Camera.main.fieldOfView - deltaFOV, minFOV);
        }
        else if (Input.GetAxis(Constants.Axes.MouseScrollWheel) < 0) // zoom in
        {
            Camera.main.fieldOfView = Mathf.Min(Camera.main.fieldOfView + deltaFOV, maxFOV);
        }
    } 
    #endregion
}

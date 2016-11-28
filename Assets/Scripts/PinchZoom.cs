using UnityEngine;
using System.Collections;

public class PinchZoom : MonoBehaviour {
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

    public float minZoom = 20.0f;
    public float maxZoom = 250.0f;


    Vector3 bottomLeft;
    Vector3 topRight;

    float cameraMaxY;
    float cameraMinY;
    float cameraMaxX;
    float cameraMinX;

    Camera camera;
    
    void Start()
    {
        camera = GetComponent<Camera>();
    
        //set max camera bounds (assumes camera is max zoom and centered on Start)
        topRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, -transform.position.z));
        bottomLeft = camera.ScreenToWorldPoint(new Vector3(0, 0, -transform.position.z));
        cameraMaxX = topRight.x;
        cameraMaxY = topRight.y;
        cameraMinX = bottomLeft.x;
        cameraMinY = bottomLeft.y;
    }

    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (camera.orthographic)
            {
                    ZoomOrthoCamera(Camera.main.ScreenToWorldPoint(touchZero.position), deltaMagnitudeDiff);

                // Make sure the orthographic size never drops below zero.
                //camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
        }
    }
    void ZoomOrthoCamera(Vector3 zoomTowards, float deltaDiff)
    {
        // Calculate how much we will have to move towards the zoomTowards position
        float multiplier = (1.0f / camera.orthographicSize * -deltaDiff);

        if(camera.orthographicSize < 200)
        {
            Debug.Log(multiplier);
            // Move camera
            transform.position += (zoomTowards - transform.position) * multiplier;
            CalcMinMax();
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.z = Mathf.Clamp(pos.z, minY, maxY);
            transform.position = pos;
        }

        // Zoom camera
        camera.orthographicSize += deltaDiff * orthoZoomSpeed;

        // Limit zoom
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minZoom, maxZoom);
    }

    void CalcMinMax()
    {
        float height = camera.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        maxX = (mapWidth - width) / 2.0f;
        maxY = (mapHeight - height) / 2.0f;
        minY = -maxY;
        minX = -maxX;
    }
}

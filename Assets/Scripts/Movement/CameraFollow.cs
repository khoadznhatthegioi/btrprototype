using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    [Range(0, 1)]
    public float targetDistance;
    [HideInInspector] public bool edgeRight, edgeLeft;
    public bool usesClamp = true;
    public float clampLeft, clampRight;
    [HideInInspector] public bool right, left;
    [HideInInspector] public Camera cam;
    Vector3 initialOffset;
    [HideInInspector] public bool viewportHitLeft, viewportHitRight;
    public float smoothError = 0.05f;
    float height = Screen.height;
    float width = Screen.width;

    float offsetError;

    void Start()
    {
        cam = GetComponent<Camera>();
        initialOffset = cam.ViewportToWorldPoint(new Vector3(targetDistance+0.5f, 0, 0));

        //offsetError = Mathf.Abs((width - height) / height + height/((width-height)*10));
        offsetError =/* Mathf.Abs((width - height) / height + (2*height-width)/(height*height));*/ width / (2 * height);
        offset.y = -target.position.y;
    }

    private void Update()
    {
        //offset.y = -target.position.y;
        //offsetError = ((Screen.width - Screen.height) / Screen.height);
        var viewportPoint = Camera.main.WorldToViewportPoint(target.position);
        var distanceFromCenter = /*Vector2.Distance(viewportPoint, Vector2.one * 0.5f)*/ Mathf.Abs(0.5f-viewportPoint.x)+smoothError;
        var movement = Input.GetAxis("Horizontal");
        if(cam.WorldToViewportPoint(new Vector3(clampLeft, 0, 0)).x>=0)
        {
            viewportHitLeft = true;
        }
        if(cam.WorldToViewportPoint(new Vector3(clampRight, 0, 0)).x <=1)
        {
            viewportHitRight = true;
        }
        if (cam.WorldToScreenPoint(target.position).x > Screen.width / 2)
        {
            right = true;
            left = false;
        }
        
        else if (cam.WorldToScreenPoint(target.position).x < Screen.width / 2)
        {
            left = true;
            right = false;
        }
        else { right = false; left = false; }

        if(distanceFromCenter>= targetDistance && right && movement > 0)
        {
            edgeRight = true;
            viewportHitLeft = false;
            edgeLeft = false;
        }
        else if (distanceFromCenter >= targetDistance && right && movement < 0)
        {
            edgeRight = false;
            edgeLeft = false;
        }
        if(distanceFromCenter>= targetDistance && left && movement < 0)
        {
            edgeLeft = true;
            viewportHitRight = false;
            edgeRight = false;
        }
        else if (distanceFromCenter >= targetDistance && left && movement > 0)
        {
            edgeLeft = false;
            edgeRight = false;
        }
        
        if (edgeRight && movement > 0)
        {
            offset.x = -initialOffset.x+offsetError;
        }
        else if (edgeLeft && movement < 0)
        {
            offset.x = initialOffset.x-offsetError;
        }
        if ((distanceFromCenter < targetDistance))
        {
            edgeRight = false;
            edgeLeft = false;
        }

        //if(distanceFromCenter>= targetDistance && Input.GetAxis("Horizontal")>0)
        //{
        //    edgeRight = true;
        //}
        //else if((distanceFromCenter < targetDistance) && Input.GetAxis("Horizontal") > 0)
        //{
        //    edgeRight = false;
        //}
        //if(distanceFromCenter >= targetDistance && Input.GetAxis("Horizontal") < 0)
        //{
        //    edgeLeft = true;
        //}
        //else if ((distanceFromCenter < targetDistance) && Input.GetAxis("Horizontal") < 0)
        //{
        //    edgeLeft = false;
        //}
        //if (edgeRight && Input.GetAxis("Horizontal") > 0)
        //{
        //    offset.x = -offsetX;
        //}
        //else if (edgeLeft && Input.GetAxis("Horizontal") <= 0)
        //{
        //    offset.x = offsetX;
        //}
        if (((edgeRight || edgeLeft) && (!viewportHitLeft && !viewportHitRight)) || ((edgeRight || edgeLeft) && !usesClamp))
        {
            Follow();
        }
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPostition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);
        transform.position = smoothPostition;
    }
}

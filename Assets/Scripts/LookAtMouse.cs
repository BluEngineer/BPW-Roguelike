using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public GameObject target;
    public Camera CastCam;
    public Vector3 mousePos;
    public Vector2 mouseScreenPos;
    public float screenspaceMultiplier;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    public void RotateTowardsMouse()
    {
        mouseScreenPos = Input.mousePosition / 7.5f;

        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPos);
        //Vector3 temp = CastCam.ScreenToWorldPoint(mouseScreenPos);
        Debug.DrawRay(ray.origin, ray.direction * 10);

        RaycastHit hit; 
        if (Physics.Raycast(ray, out hit))
        {

            mousePos = new Vector3(hit.point.x, gameObject.transform.position.y, hit.point.z);
            //Debug.DrawLine(CastCam.transform.position, hit.point, Color.blue);

            transform.LookAt(mousePos);
            Vector3 pos = gameObject.transform.position;
            Vector3 dir = (gameObject.transform.position - mousePos).normalized;
           // Debug.DrawLine(pos, hit.point, Color.green, Mathf.Infinity);
        }
    }


    void Update()
    {
            RotateTowardsMouse();
    }

}

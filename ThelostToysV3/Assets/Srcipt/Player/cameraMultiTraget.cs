using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMultiTraget : MonoBehaviour {
    public List<Transform> tragets;
    public Vector3 offset;
    public Vector3 player_cam_offset;
    public float smoothTime = .5f;
    public float zoomLimiter = 40;
    public float minZoom = 10;
    public float maxZoom = 50;
    private Vector3 velocity;
    private Camera cam;
    public bool islock = false;

    [Header("Look At")]
    public float degree_PreS;

	// Use this for initialization
	void Start ()
    {
        cam = this.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if(islock)
        {
            return;
        }

        remove_null_tragets();
        //move and zoom camera
        if(tragets.Count == 0)
        {
            return;
        }

        cameraMove();
        cameraZoom();

        transform.LookAt(getCenter_Point());
    }

    //function
    #region
    //if have null traget in lst 
    //remove it form lst
    private void remove_null_tragets()
    {
        for (int i = tragets.Count - 1; i > -1; i--)
        {
            if(tragets[i] == null)
            {
                tragets.RemoveAt(i);
            }
        }
    }

    //zoom in zoom out by distance
    private void cameraZoom()
    {
        float newZoom;
        newZoom = Mathf.Lerp(maxZoom, minZoom, getGreatestDistance() / zoomLimiter);
        //Debug.Log("newZoom : " + newZoom);
        //Debug.Log("Graetest Distance : " + getGreatestDistance());
        if(tragets.Count < 2)
        {
            cam.fieldOfView = 60;
            return;
        }

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    private float getGreatestDistance()
    {
        var bounds = new Bounds(tragets[0].position, Vector3.zero);
        for (int i = 0; i < tragets.Count; i++)
        {
            bounds.Encapsulate(tragets[i].position);
        }

        float current_Point_Distance = (bounds.size.x + bounds.size.z) / 2;
        return current_Point_Distance;

    }

    //move camera by center point
    private void cameraMove()
    {
        Vector3 centerPoint = getCenter_Point();
        Vector3 newPos = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);

    }

    //Return vector3 by find center point
    //use by set camera
    private Vector3 getCenter_Point()
    {
        if(tragets.Count < 2)
        {
            return tragets[0].position;
        }

        //Vector3 centerPoint = (tragets[0].position + tragets[1].position) / 2;
        var bound = new Bounds(tragets[0].position, Vector3.zero);
        for (int i = 0; i < tragets.Count; i++)
        {
            bound.Encapsulate(tragets[i].position);
        }
        
        return (bound.center + player_cam_offset);

        //Vector3 centerPoint = (tragets[0].position + tragets[1].position) / 2;
        //return centerPoint;
    }
    #endregion
}

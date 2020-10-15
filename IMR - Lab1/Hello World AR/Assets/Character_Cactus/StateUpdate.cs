using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using System.Diagnostics;
using UnityEngine;

public class StateUpdate : MonoBehaviour{
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }

    void FixedUpdate(){
        GameObject camera = GameObject.Find("ARCamera");
        Transform cameraTransform = camera.transform;
        Vector3 cameraPosition = cameraTransform.position;

        Vector3 cactusPosition = transform.position;
        float distance = Vector3.Distance(cameraPosition, cactusPosition);

        if (Input.GetKeyDown("space"))
        {
            Debug.Log(distance);
        }

        bool isTooClose = distance < 5;
        animator.SetBool("isTooClose", isTooClose);
    }
}

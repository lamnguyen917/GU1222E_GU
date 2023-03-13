using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = target.transform.position.x;
        transform.position = position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Character target = null;
    [SerializeField] Vector3 offsetPosition = default;
    [SerializeField] float depthPosition = -5f;




    void LateUpdate()
    {
        UpdateSetPositionTarget();
    }

    void UpdateSetPositionTarget()
    {
        transform.position = target.TransEye.position;
        transform.rotation = target.TransEye.rotation;

        var offsetPos = target.TransEye.position;
        offsetPos.y += offsetPosition.y;

        depthPosition += Input.GetAxis("Mouse ScrollWheel") * 10;

        transform.position = (target.TransEye.forward * depthPosition) + offsetPos;
    }
}

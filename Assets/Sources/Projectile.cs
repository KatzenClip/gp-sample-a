using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] Transform pivot = null;

    [SerializeField] float speedForwardBase = 1f;
    [SerializeField] float speedForwardAcc = 0.01f;
    [SerializeField] float durationRotate = 2f;
    [SerializeField] float speedRolling = 500.0f;
    [SerializeField] float hitRange = 1f;
    [SerializeField] float timeToActivateHomingSystem = 0.5f;

    [SerializeField] public bool IsHoming = false;

    [SerializeField] Transform explode_Origin = null;

    Vector3 startPosition = default;
    Vector3 firstFirePosition = default;
    Vector3 lastPosition = default;

    bool isHit = false; public bool IsHit => isHit;
    float time = 0f;
    float timeAcc = 0f;
    float timeRotate = 0f;


    void Update()
    {
        if (isHit) return;

        UpdateCheckHit();
        UpdateTranslateForward();
        UpdateActivateHoming();
        UpdateHomingSystem();
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
        startPosition = transform.position;
        firstFirePosition = target.position;
    }

    void UpdateHomingSystem()
    {
        if (!IsHoming) return;
        if (null == target) return;

        var direction = target.position - transform.position;
        var targetRotation = Quaternion.LookRotation(direction);
        var delta = Quaternion.identity;

        if (timeRotate < durationRotate)
        {
            timeRotate += Time.deltaTime;
            var progress = timeRotate / durationRotate;
            delta = Quaternion.Lerp(transform.localRotation, targetRotation, progress);
        }
        else
            delta = Quaternion.Lerp(transform.localRotation, targetRotation, 1f);

        transform.localRotation = Quaternion.Euler(delta.eulerAngles);
    }

    void UpdateTranslateForward()
    {
        UpdateRolling();

        if (!IsHoming)
        {

            if (lastPosition != default)
            {
                var direction = transform.position - lastPosition;
                if (float.Epsilon < direction.magnitude)
                    transform.localRotation = Quaternion.LookRotation(direction);
            }

            lastPosition = transform.position;
        }

        var dir = transform.forward * Time.deltaTime * speedForwardBase;
        dir.y -= (Time.deltaTime * WorldEnvironment.Gravity);
        transform.Translate(dir, Space.World);
    }

    void UpdateCheckHit()
    {
        if (null == target) return;

        var distance = Vector3.Distance(transform.position, target.position);
        if (distance <= hitRange)
        {
            isHit = true;
            ProcessHit();
        }
    }

    void ProcessHit()
    {
        var go = Instantiate(explode_Origin);
        go.position = transform.position;
    }

    void UpdateActivateHoming()
    {
        if (IsHoming) return;

        time += Time.deltaTime;
        if (timeToActivateHomingSystem <= time)
        {
            time = 0f;
            IsHoming = true;
        }
    }

    void UpdateRolling()
    {
        pivot.Rotate(new Vector3(0, 0, 1) * (Time.deltaTime * speedRolling));
    }
}

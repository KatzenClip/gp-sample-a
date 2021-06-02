using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float moveRangeMax = 20f;
    [SerializeField] float moveRangeMin = -20f;
    [SerializeField] float moveSpeed = 1f;

    float time = 0f;
    bool isLeft = true;

    void Update()
    {
        //UpdateLookAtTarget();
        UpdateMove();
    }

    void UpdateLookAtTarget()
    {
        if (null == target) return;

        transform.LookAt(target);
    }

    void UpdateMove()
    {
        if (moveRangeMax <= transform.localPosition.x)
        {
            isLeft = true;
        }
        else if (moveRangeMin >= transform.localPosition.x)
            isLeft = false;

        transform.Translate(new Vector3(isLeft ? -1 : 1, 0, 0) * moveSpeed);
    }
}

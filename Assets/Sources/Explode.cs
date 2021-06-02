using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] float removeTime = 0.4f;
    [SerializeField] float rangeMultiplier = 1.0f;

    float time = 0f;

    void Update()
    {
        UpdateRemove();
    }

    void UpdateRemove()
    {
        time += Time.deltaTime;
        if (time <= removeTime)
        {
            var t = time / removeTime;
            var factor = Mathf.Lerp(1f, 0f, t);
            transform.localScale *= factor;
            return;
        }

        Destroy(gameObject);
    }
}

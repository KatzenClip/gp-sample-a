using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float moveRangeMax = 20f;
    [SerializeField] float moveRangeMin = -20f;
    [SerializeField] float moveSpeed = 1f;

    [Space]
    [SerializeField] NavMeshAgent agent = null;

    bool isMoving = false;

    void Update()
    {
        UpdateMove();
    }

    void UpdateMove()
    {
        if (!isMoving)
        {
            var path = new NavMeshPath();
            agent.CalculatePath(new Vector3(Random.Range(-WorldEnvironment.MapSize.x * 0.5f, WorldEnvironment.MapSize.x * 0.5f), 0f, Random.Range(-WorldEnvironment.MapSize.z * 0.5f, WorldEnvironment.MapSize.z * 0.5f)), path);
            agent.SetPath(path);
            isMoving = true;
        }

        if (agent.velocity.magnitude <= 0.1f)
            isMoving = false;
    }
}

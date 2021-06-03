using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] Enemy target = null;
    [SerializeField] Projectile projectile_Origin = null;

    [SerializeField] List<Projectile> listProjectile = null;

    [Space]
    [SerializeField] int additionalJumpCount = 0;
    [Space]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpWeight = 1f;

    [Space]
    [SerializeField] Transform transEye = null;                 public Transform TransEye => transEye;
    [SerializeField] CharacterController controller = null;
    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] float sensitivityCamera = 3f;
    [SerializeField] float maximumAngleX = 60f;
    [SerializeField] float minimumAngleX = -90f;

    Vector3 moveDir = default;
    Vector3 rotationAxis = default;
    Vector3 lastPressPosition = default;


    void Start()
    {
        rotationAxis = transEye.rotation.eulerAngles;
    }

    void Update()
    {
        UpdateCheckIsGround();
        UpdateInput();
        UpdateGetMove();
        UpdateGetRotate();
        UpdateCheckDestroyProjectile();
    }

    void FindTarget()
    {
        var center = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        var worldPoint = Camera.main.ScreenToWorldPoint(center);
        var minDistance = float.MaxValue;
        var targetIndex = 0;
        var targetPoint = Camera.main.transform.forward * WorldEnvironment.MapSize.magnitude * 0.5f;
        if (Physics.Raycast(worldPoint, Camera.main.transform.forward, out var hit, WorldEnvironment.MapSize.magnitude * 0.5f))
            targetPoint = hit.point;

        var list = WorldEnvironment.ListEnemy;
        for (int n = 0, cnt = list.Count; n < cnt; ++n)
        {
            var enemy = list[n];
            if (null == enemy) continue;

            var distance = Vector3.Distance(enemy.transform.position, targetPoint);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetIndex = n;
            }
        }

        target = list[targetIndex];
    }

    void FireProjectileToTarget()
    {
        FindTarget();
        if (null == target) return;

        var projectile = Instantiate(projectile_Origin);
        if (null == projectile) return;

        AimToTarget(projectile);

        //projectile.transform.position = transform.position;
        projectile.transform.position = transform.position;
        projectile.SetTarget(target.transform);

        listProjectile.Add(projectile);
    }

    void AimToTarget(Projectile _projectile)
    {
        _projectile.transform.rotation = Quaternion.LookRotation(transform.forward);
        _projectile.transform.Rotate(-60f, 0, 0);
    }

    void UpdateCheckDestroyProjectile()
    {
        for (int n = listProjectile.Count - 1, zero = 0; zero <= n; --n)
        {
            var go = listProjectile[n];
            if (go.IsHit)
            {
                Destroy(go.gameObject);
                listProjectile.Remove(go);
                break;
            }
        }
    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            moveDir.y = jumpWeight;
        }
        else if (0 < additionalJumpCount)
        {
            UpdateMoveAxis();

            --additionalJumpCount;
            moveDir.y = jumpWeight;
        }
    }

    void UpdateCheckIsGround()
    {
        if (controller.isGrounded)
            additionalJumpCount = 1;
    }

    void UpdateGetMove()
    {
        moveDir.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void UpdateGetRotate()
    {
        transEye.rotation = Quaternion.Euler(rotationAxis);
        var angle = transform.rotation.eulerAngles;
        angle.y = transEye.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(angle);
        transEye.rotation = Quaternion.Euler(rotationAxis);
    }

    void UpdateInput()
    {
        if (controller.isGrounded)
            UpdateMoveAxis();

        UpdateRotationAxis();

        if (Input.GetButtonDown("Fire1"))
        {
            FireProjectileToTarget();
        }
        else if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void UpdateMoveAxis()
    {
        moveDir = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDir *= moveSpeed;
    }

    void UpdateRotationAxis()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            lastPressPosition = Input.mousePosition;
        }
        else if (Input.GetButton("Fire2"))
        {
            var delta = Input.mousePosition - lastPressPosition;
            rotationAxis.y += delta.x * sensitivityCamera * Time.deltaTime;
            rotationAxis.x -= delta.y * sensitivityCamera * Time.deltaTime;

            if (rotationAxis.x < minimumAngleX)
                rotationAxis.x = minimumAngleX;
            else if (maximumAngleX < rotationAxis.x)
                rotationAxis.x = maximumAngleX;

            lastPressPosition = Input.mousePosition;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            lastPressPosition = default;
        }
    }
}

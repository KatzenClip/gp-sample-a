using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEnvironment : MonoBehaviour
{
    static WorldEnvironment instance = null;
    [SerializeField] float gravity = 0.981f;        public static float Gravity => instance.gravity;

    [Space]
    [SerializeField] Transform plane = null;
    [Space]
    [SerializeField] List<Enemy> listEnemy = null;  public static List<Enemy> ListEnemy => instance.listEnemy;
    [Space]
    [SerializeField] Enemy enemy_origin = null;


    public static Vector3 MapSize => new Vector3(150f, 0f, 150f);


    void Awake()
    {
        SpawnEnemies();
        instance = this;
    }

    void SpawnEnemies()
    {
        listEnemy = new List<Enemy>();

        {
            var prefab = Instantiate(enemy_origin);
            var pos = prefab.transform.position;
            pos.y = plane.position.y;
            pos.x = Random.Range(0f, plane.localScale.x);
            pos.z = Random.Range(0f, plane.localScale.z);
            prefab.transform.position = pos;
            listEnemy.Add(prefab);
        }
        {
            var prefab = Instantiate(enemy_origin);
            var pos = prefab.transform.position;
            pos.y = plane.position.y;
            pos.x = Random.Range(0f, plane.localScale.x);
            pos.z = Random.Range(0f, plane.localScale.z);
            prefab.transform.position = pos;
            listEnemy.Add(prefab);
        }
        {
            var prefab = Instantiate(enemy_origin);
            var pos = prefab.transform.position;
            pos.y = plane.position.y;
            pos.x = Random.Range(0f, plane.localScale.x);
            pos.z = Random.Range(0f, plane.localScale.z);
            prefab.transform.position = pos;
            listEnemy.Add(prefab);
        }
        {
            var prefab = Instantiate(enemy_origin);
            var pos = prefab.transform.position;
            pos.y = plane.position.y;
            pos.x = Random.Range(0f, plane.localScale.x);
            pos.z = Random.Range(0f, plane.localScale.z);
            prefab.transform.position = pos;
            listEnemy.Add(prefab);
        }
    }
}

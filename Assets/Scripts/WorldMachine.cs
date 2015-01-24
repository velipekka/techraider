using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WorldMachine : MonoBehaviour 
{
    public List<GameObject> obstaclePrefabs;
    public List<GameObject> obstacleInstances;

    public int propCountMin = 10;
    public int propCountMax = 10;

    public int clusterSizeMin = 5;
    public int clusterSizeMax = 5;
    public Rect propBounds;

    void Start()
    {
        obstacleInstances = new List<GameObject>();
        MakeWorld();
    }

    public void MakeWorld()
    {
        foreach(GameObject go in obstacleInstances)
        {
            Destroy(go.gameObject);
        }

        obstacleInstances = new List<GameObject>();

        int propCount = Random.Range(propCountMin, propCountMax);

        int clusterCount = 0;

        int currentClusterN = 1000;

        for (int i = 0; i < propCount; i++)
        {
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];

            if(currentClusterN > clusterCount)
            {
                clusterCount = Random.Range(clusterSizeMin, clusterSizeMax);
                currentClusterN = 0;
            }

            bool clear = false;
            Vector2 pos = Vector2.zero;
            while (!clear)
            {


                pos = currentClusterN == 0 ? new Vector2(Random.Range(propBounds.x, propBounds.x + propBounds.width), Random.Range(propBounds.y, propBounds.height)) : 
                    ((Vector2)obstacleInstances[obstacleInstances.Count - 1].transform.position + new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)));

                //Collider2D c = Physics2D.OverlapPoint(pos, 1 << LayerMask.NameToLayer("obstacles"));
                clear = true;
                //if(c == null)
                //{
                //    clear = true;
                //}
                //else
                //{
                //}

            }

            GameObject instance = Instantiate(prefab, pos, Quaternion.Euler(0f,Random.Range(0f, 1f) < .5f ? 180f : 0f, 0f)) as GameObject;
            instance.transform.parent = transform;
            obstacleInstances.Add(instance);
            currentClusterN++;

        }

    }

	
}

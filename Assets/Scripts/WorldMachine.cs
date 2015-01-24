using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WorldMachine : MonoBehaviour 
{
    public List<GameObject> obstaclePrefabs;
    public List<GameObject> obstacleInstances;

    public int propCountMin = 10;
    public int propCountMax = 10;

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

        for (int i = 0; i < propCount; i++)
        {
            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];

           
            bool clear = false;
            Vector2 pos = Vector2.zero;
            while (!clear)
            {
                pos = new Vector2(Random.Range(propBounds.x, propBounds.x + propBounds.width), Random.Range(propBounds.y, propBounds.height));

                Collider2D c = Physics2D.OverlapPoint(pos, 1 << LayerMask.NameToLayer("obstacles"));

                if(c == null)
                {
                    clear = true;
                }
                else
                {
                }

            }

            GameObject instance = Instantiate(prefab, pos, prefab.transform.rotation) as GameObject;
            obstacleInstances.Add(instance);

        }

    }

	
}

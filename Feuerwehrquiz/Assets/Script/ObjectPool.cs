using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour //Spwan our buttons Answers
{
    public GameObject prefab;
    private Stack<GameObject> inactiveInstances = new Stack<GameObject>();

    public GameObject GetObject()
    {
         GameObject spawnedGameObject;
        if (inactiveInstances.Count > 0)
        {
            spawnedGameObject = inactiveInstances.Pop();
        }
        else
        {
            spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);

            //Add the pooledObject component to the prefab so we know it came from thes pool
            PooledObject pooledObject = spawnedGameObject.AddComponent<PooledObject>();
            pooledObject.pool = this;
        }
        //enable this instance
        spawnedGameObject.SetActive(true);
        return spawnedGameObject;
    }

    //Return an instance of the prefab to the pool (when unloading an object add unused object back to pool
    public void ReturnObject(GameObject toReturn)
    {
        PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

        // If the Instance comes from the pool list, it returns this from the pool 
        if (pooledObject != null && pooledObject.pool == this)
        {
            //disable the instance
            toReturn.SetActive(false);
            //add the instance to the collection of inactive instace
            inactiveInstances.Push(toReturn);
        }
        else
        {
            Debug.LogWarning(toReturn.name + " was returned to a pool it wasnt spawned from! Detroying.");
            Destroy(toReturn);
        }
    }
}

public class PooledObject : MonoBehaviour
{
    public ObjectPool pool;
}
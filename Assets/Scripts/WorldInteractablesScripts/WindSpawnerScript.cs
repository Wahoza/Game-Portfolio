using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WindSpawnerScript : MonoBehaviour
{
    [SerializeField] Transform myLineHolder;
    [SerializeField] Transform myLoopHolder;

    private int windIndex;

    private float spawnTimer;

    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    private float currentSpawnTime;

    [SerializeField] Transform transformToSpawnAround;
    [SerializeField] Vector3 spawnDirection;
    [SerializeField] Vector3 minBoundsSpawnDist;
    [SerializeField] Vector3 maxBoundsSpawnDist;

    private Vector3 nextSpawnDist;

    private Vector3 spawnBetweenVec;

    private void Awake()
    {
        for (int i = 0; i < myLineHolder.childCount; i++) { 
            myLineHolder.GetChild(i).eulerAngles = spawnDirection;
        }         
        for (int i = 0; i < myLoopHolder.childCount; i++) {
            myLoopHolder.GetChild(i).eulerAngles = spawnDirection;
        }    

        spawnBetweenVec = maxBoundsSpawnDist - minBoundsSpawnDist;
    }

    private void SpawnWind()
    {
        if (windIndex % 3 == 0)
        {
            //do loop spawn
            DoWindSetup(myLineHolder.GetChild(windIndex % myLineHolder.childCount));
        }
        else
        {
            //do regular
            DoWindSetup(myLoopHolder.GetChild(windIndex % myLoopHolder.childCount));
        }

        windIndex++;
        
        StartCoroutine(PrepareNextWind());
    }

    private void DoWindSetup(Transform wind)
    {
        wind.position = transformToSpawnAround.position + nextSpawnDist + minBoundsSpawnDist;
        wind.GetComponent<ParticleSystem>().Play();
    }
    private IEnumerator PrepareNextWind()
    {
        //Debug.Log(UnityEngine.Random.ColorHSV().a);
        Color Vals = UnityEngine.Random.ColorHSV();
        //yield return new WaitForEndOfFrame();
        nextSpawnDist = new Vector3(spawnBetweenVec.x * Vals.r, spawnBetweenVec.y * Vals.g, spawnBetweenVec.z * Vals.b);

        spawnTimer = Time.time + ((maxSpawnTime - minSpawnTime) * Vals.a);

        yield return null;
    }

    private void Update()
    {
        if(spawnTimer < Time.time)
        {
            SpawnWind();
        }
    }
}

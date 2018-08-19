using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHazards : MonoBehaviour
{
    public GameObject PlayerObject;
    [Space]
    public GameObject[] Hazards;
    [Space]
    public Collider Colliders;

    public float Radius;
    public float TimeBetweenSpawns = 2f;


    private const float _minX = -8f;
    private const float _maxX = 8f;

    private int _minHazardsToSpawn = 1;
    private int _maxHazardsToSpawn = 6;
    public int _amountOfHazardsToSpawn = 5;
    private int _HazardToSpawn = 0;
    private int _HazardSpawnCap = 8;

    private bool _canSpawn = false;

    void Start()
    {
        Instantiate(PlayerObject);
        _canSpawn = true;  //Start spawning hazards
    }

    void Update()
    {
        if (_canSpawn)
        {
            StartCoroutine(GenerateHazard());
        }


        //Debug.Log(_HazardToSpawn);
    }//(YPos:15.21)

    private IEnumerator GenerateHazard()
    {
        _canSpawn = false;
        TimeBetweenSpawns = Random.Range(0.5f, 2.0f);    //Testing Values
                                                         /* _amountOfHazardsToSpawn = 5; */    //Testing Values

        for (int i = 0; i < _amountOfHazardsToSpawn; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(_minX, _maxX), transform.position.y /*8.5f*/, 0.0f); //Generate A spawn position
            _HazardToSpawn = Random.Range(0, Hazards.Length);
            Instantiate(Hazards[_HazardToSpawn], spawnPos, Quaternion.identity, parent: transform); //Spawn the Hazard
            yield return new WaitForSeconds(spawnWait);
        }

        yield return new WaitForSeconds(TimeBetweenSpawns);
        TryMakeGameHarder();
        _canSpawn = true;
    }


    public int increaseDifficultyTime;
    public int increaseDifficultyHazards;
    public float spawnWait = 0.5f;

    private System.DateTime startTime;

    private void TryMakeGameHarder()
    {
        int timePassed = (System.DateTime.Now - startTime).Seconds;

        bool makeGameHarder = timePassed > increaseDifficultyTime;

        if (makeGameHarder)
        {
            spawnWait = spawnWait - (spawnWait * 0.8f);

            _amountOfHazardsToSpawn += increaseDifficultyHazards;

            startTime = System.DateTime.Now;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHazards : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameObject PlayerTrailEffect;
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
        Instantiate(PlayerTrailEffect);
        _canSpawn = true;  //Start spawning hazards
    }

    void Update()
    {
        if (TimeBetweenSpawns <= 0f)
        {
            StartCoroutine(SpawnWave());
            TimeBetweenSpawns += 6f;
        }

        TimeBetweenSpawns -= Time.deltaTime;


        //Debug.Log(_HazardToSpawn);
    }//(YPos:15.21)

    private IEnumerator GenerateHazard()
    {
        _canSpawn = false;
        //TimeBetweenSpawns = Random.Range(0.5f, 2.0f);    //Testing Values
        TimeBetweenSpawns = 2.0f;
        /* _amountOfHazardsToSpawn = 5; */    //Testing Values

        for (int i = 0; i < _amountOfHazardsToSpawn; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(_minX, _maxX), transform.position.y /*8.5f*/, 0.0f); //Generate A spawn position
            _HazardToSpawn = Random.Range(0, Hazards.Length);
            Instantiate(Hazards[_HazardToSpawn], spawnPos, Quaternion.identity, parent: transform); //Spawn the Hazard
            yield return new WaitForSeconds(spawnWait);
        }

        yield return new WaitForSeconds(TimeBetweenSpawns);
        _amountOfHazardsToSpawn += increaseDifficultyHazards;
        //TryMakeGameHarder();
        _canSpawn = true;
    }

    private IEnumerator SpawnWave()
    {
        _amountOfHazardsToSpawn += 1;
        //spawnWait += 0.5f;

        for (int i = 0; i < _amountOfHazardsToSpawn; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(_minX, _maxX), transform.position.y /*8.5f*/, 0.0f); //Generate A spawn position
            _HazardToSpawn = Random.Range(0, Hazards.Length);
            Instantiate(Hazards[_HazardToSpawn], spawnPos, Quaternion.identity, parent: transform); //Spawn the Hazard
            yield return new WaitForSeconds(spawnWait);
        }
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


















    #region OverlapFunctionNotWorking
    /* public GameObject PlayerObject;
    [Space]
    public GameObject[] Hazards;
    [Space]
    public Collider[] Colliders;

    public float Radius;
    public float TimeBetweenSpawns = 2f;


    private const float _minX = -8f;
    private const float _maxX = 8f;

    private int _minHazardsToSpawn = 1;
    private int _maxHazardsToSpawn = 6;
    public int _amountOfHazardsToSpawn = 5;
    private int _HazardToSpawn = 0;
    private int _HazardSpawnCap = 8;
    public int safetyNet;

    private bool _canSpawn = false;
    private bool _canSpawnHere = false;

    private Vector3 spawnPos;

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
        Debug.Log(spawnPos);

        //Debug.Log(_HazardToSpawn);
    }//(YPos:15.21)

    private IEnumerator GenerateHazard()
    {
        _canSpawn = false;
        TimeBetweenSpawns = Random.Range(0.5f, 2.0f);    //Testing Values
                                                         /* _amountOfHazardsToSpawn = 5;     //Testing Values
        for (int i = 0; i<_amountOfHazardsToSpawn; i++)
        {
            _HazardToSpawn = Random.Range(0, Hazards.Length);
            while (!_canSpawnHere)
            {
                float x = Random.Range(_minX, _maxX);
    float y = transform.position.y;/*8.5f
    float z = 0.0f;

    spawnPos = new Vector3(x, y, z); //Generate A spawn position 
    _canSpawnHere = PreventSpawnOverlap(spawnPos);

                if (_canSpawnHere)
                {
                    break;
                }

safetyNet++;

                bool TookTooManyAttemps = safetyNet >= 50;
                if (TookTooManyAttemps)
                {
                    Debug.Log("Took To Many Attemps");
                    break;
                }
            }

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

private bool PreventSpawnOverlap(Vector3 spawnPos)
{
    Colliders = Physics.OverlapSphere(transform.position, Radius);

    for (int i = 0; i < Colliders.Length; i++)
    {
        Vector3 centerpoint = Colliders[i].bounds.center;

        float width = Colliders[i].bounds.extents.x;
        float height = Colliders[i].bounds.extents.y;

        float rightExtent = centerpoint.x + width;
        float leftExtent = centerpoint.x - width;
        float lowerExtent = centerpoint.y + height;
        float upperExtent = centerpoint.y - height;

        if (spawnPos.x >= leftExtent && spawnPos.x <= rightExtent)
        {
            if (spawnPos.y >= lowerExtent && spawnPos.y <= upperExtent)
            {
                return false;
            }
        }
    }
    return true;
}*/
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Rock rockLoot;
    public GameObject bowLoot;
    public int numberOfRocks;
    public int numberOfBows;
    public float lootMinRange;
    public float lootMaxRange;
    private Rock newRock;
    private GameObject newBow;

    // Start is called before the first frame update
    void Start()
    {

        InstantiateRockLoot();
        InstantiateBowLoot();
    }

    public void InstantiateRockLoot()
    {
       for (int i = 0; i < numberOfRocks; i++)
        {
            var rockLootPosition = new Vector3(Random.Range(lootMinRange, lootMaxRange), 0.1f, Random.Range(lootMinRange, lootMaxRange));
            newRock = Instantiate(rockLoot, rockLootPosition, Quaternion.identity);
            newRock.transform.parent = GameObject.Find("LootSpawner").transform;
        }
    }

    public void InstantiateBowLoot()
    {
        for (int i = 0; i < numberOfBows; i++)
        {
            var bowLootPosition = new Vector3(Random.Range(lootMinRange, lootMaxRange), 0.5f, Random.Range(lootMinRange, lootMaxRange));
            newBow = Instantiate(bowLoot, bowLootPosition, Quaternion.identity);
            newBow.transform.parent = GameObject.Find("LootSpawner").transform;
        }
    }
}

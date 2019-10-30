using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] BlockPrefabs;

    public bool spawncheck;

    [SerializeField]
    private static GameObject nextBlock;

    // Start is called before the first frame update
    void Start()
    {
        nextBlock = BlockPrefabs[Random.Range(0, BlockPrefabs.Length)];
        spawncheck = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawncheck)
        {
            Spawn();
            ShowNextBlock();
            spawncheck = false;
        }
    }

    private void Spawn()
    {
        Instantiate(nextBlock, transform.position, Quaternion.identity);
    }

    private void ShowNextBlock()
    {
        Destroy(nextBlock.gameObject);
        nextBlock = Instantiate(BlockPrefabs[Random.Range(0, BlockPrefabs.Length)], new Vector3(25,10,0), Quaternion.identity);
        
    }
}

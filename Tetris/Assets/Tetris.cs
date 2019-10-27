using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    private GameObject[ , ] bars = new GameObject[12, 22];

    // Start is called before the first frame update
    void Start()
    {
        SetMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetMap()
    {
        for (int x = 0; x <= 11; x++)
        {
             for (int y = 0; y <= 21; y++)
             {
                if (x == 0 || x == 11 || y == 0 || y == 21)
                {
                    var bar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    bar.transform.position = new Vector3(x, y);

                    bars[x, y] = bar;
                }
             }
           
        }
    }
}

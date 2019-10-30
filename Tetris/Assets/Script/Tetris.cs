using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    float fallTime;
    float speed;
    public 
    bool comboactive;

    public static int height = 20;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
        fallTime = 1.0f;
        speed = 1.0f;
        comboactive = false;
    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
        Falling();
    }

    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
             if(!MoveableCheck())
                transform.position -= new Vector3(-1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!MoveableCheck())
                transform.position += new Vector3(-1, 0, 0);

        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            speed = 10.0f;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            speed = 1.0f;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 90);
            if (!MoveableCheck())
                transform.Rotate(0, 0, 90);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            while(true)
            {
                if (MoveableCheck())
                    transform.position -= new Vector3(0, 1, 0);

                else if (!MoveableCheck())
                {
                    transform.position += new Vector3(0, 1, 0);
                    break;
                }
            }
        }
    }

    void Falling()
    {
        fallTime -= Time.deltaTime * speed;

        if (fallTime <= 0.0f)
        {
            transform.position += new Vector3(0, -1, 0);
            if (!MoveableCheck())
            {
                transform.position += new Vector3(0, 1, 0);
                AddGrid();
                CheckLines();
                this.enabled = false;
                FindObjectOfType<Spawner>().spawncheck = true;
            }

            fallTime = 0.5f;
        }
    }

    void AddGrid()
    {
        foreach (Transform children in transform)
        {
            int X = Mathf.RoundToInt(children.transform.position.x);
            int Y = Mathf.RoundToInt(children.transform.position.y);

            grid[X,Y] = children;
        }
    }

    bool MoveableCheck()
    {
        foreach (Transform children in transform)
        {
            int X = Mathf.RoundToInt(children.transform.position.x);
            int Y = Mathf.RoundToInt(children.transform.position.y);

            if(X < 0 || X >= width || Y < 0 || Y >= height)
            {
                return false;
            }

            if (grid[X, Y] != null)
                return false;
           
        }
        return true;
    }

    void CheckLines()
    {
        for (int i = 0; i <= height - 1; i++)
        {
            if (FullLine(i))
            {
                FindObjectOfType<Score>().combo++;
                comboactive = true;
                DeleteLine(i);
                DownLine(i);
            }
        }

        if(!comboactive)
        {
            FindObjectOfType<Score>().combo = 0;
        }

        else if(comboactive)
        {
            comboactive = false;
        }
    }

    bool FullLine(int Linenumber)
    {
        for (int i = 0; i < width; i++)
        {
            if (grid[i, Linenumber] == null)
            {
                return false;
            }
        }

        return true;
    }

    void DeleteLine(int Linenumber)
    {
        for (int i = 0; i < width; i++)
        {
            Destroy(grid[i, Linenumber].gameObject);
            grid[i, Linenumber] = null;
        }
    }

    void DownLine(int Linenumber)
    {
        for (int i = Linenumber; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, i] != null)
                {
                    grid[j, i - 1] = grid[j, i];
                    grid[j, i] = null;
                    grid[j, i - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }

        }
    }
}

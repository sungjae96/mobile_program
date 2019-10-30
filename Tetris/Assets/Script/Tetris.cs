using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    public Vector3 rotationpoint;
    float falltime;
    float speed;

    bool leftmovecheck;
    bool rightmovecheck;
    bool downmovecheck;
    bool rotationmovecheck;

    static int height = 20;
    static int width = 10;

    private static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
        leftmovecheck = true;
        rightmovecheck = true;
        downmovecheck = true;
        rotationmovecheck = true;
        speed = 1.0f;
        falltime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Falling();
        InputKey();
    }

    private void Falling()
    {
        falltime -= Time.deltaTime * speed;

        foreach (Transform children in transform)
        {
            if (grid[(int)transform.position.x, (int)transform.position.y - 1] != null || transform.position.y == 0)            
            downmovecheck = false;
        }

        if (falltime <= 0.0f && downmovecheck)
        {
            transform.position += new Vector3(0, -1, 0);
            
            falltime = 0.5f;
        }

        if (!downmovecheck)
        {
            AddToGrid();
            CheckLines();
            this.enabled = false;
            FindObjectOfType<Spawner>().spawncheck = true;
        }
    }

    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && leftmovecheck)
        {          
            transform.position += new Vector3(-1, 0, 0);
            if(!MoveableCheck())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && rightmovecheck)
        {           
            transform.position += new Vector3(1, 0, 0);
            if (!MoveableCheck())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
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
            {
                transform.Rotate(0, 0, -90);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = height-1; i>=0; i--)
            {
                foreach (Transform children in transform)
                {
                    if (grid[(int)transform.position.x, i] != null)
                    {
                        transform.position = new Vector3(transform.position.x, i + 1, 0);
                        break;
                    }
                }
            }
        }
    }

    bool MoveableCheck()
    {
        foreach(Transform children in transform)
        {
            if (grid[(int)children.transform.position.x, (int)children.transform.position.y] != null)
                return false;
        }

        return true;
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            grid[(int)children.transform.position.x, (int)children.transform.position.y] = children;
        }
    }

    void CheckLines()
    {
        for(int i = 0; i <= height-1; i++)
        {
            if(FullLine(i))
            {
                DeleteLine(i);
                DownLine(i);
            }
        }
    }

    bool FullLine(int Linenumber)
    {
        for(int i = 0; i < width; i++)
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
            for(int j = 0; j<width; j++)
            {
                if(grid[j,i] != null)
                {
                    grid[j, i - 1] = grid[j, i];
                    grid[j, i] = null;
                    grid[j, i - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "leftwall")
            leftmovecheck = false;

        else if (other.tag == "rightwall")
            rightmovecheck = false;

        else if (other.tag == "bottom")
        {
            downmovecheck = false;
            rightmovecheck = false;
            leftmovecheck = false;
            rotationmovecheck = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "leftwall")
            leftmovecheck = true;

        else if (other.tag == "rightwall")
            rightmovecheck = true;
    }
}

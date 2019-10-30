using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text combotext;
    public int combo;
    // Start is called before the first frame update
    void Start()
    {
        combotext = GetComponent<Text>();
        combo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        combotext.text = "combo : " + combo;
    }
}

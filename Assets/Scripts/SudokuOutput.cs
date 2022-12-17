using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SudokuOutput : MonoBehaviour
{
    public GameObject SuccessScreen;
    private int[,] answer = new int[9, 9];

    public void LoadAndShowData()
    {
        answer = GameObject.Find("Sudoku UI").GetComponent<Sudoku>().arr;
        GameObject.Find("OutputRC00").GetComponentInChildren<Text>().text = "1";

        // Show Success Screen in 0.5 seconds
        Invoke("ShowSuccessScreen", 0.5f);

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                // Initialize to zero
                string prefix = "OutputRC";
                string name = prefix + j + i;
                GameObject.Find(name).GetComponentInChildren<Text>().text = answer[i, j].ToString();
            }
        }
    }

    private void ShowSuccessScreen()
    {
        SuccessScreen.SetActive(true);
    }

}

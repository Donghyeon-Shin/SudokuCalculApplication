using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sudoku : MonoBehaviour
{
    public GameObject InputButton, NumberButton, EmptyButton, ResetButton;
    public Image image;
    private Animator animator;

    public int[,] arr = new int[9, 9]; // Sudoku array
    public bool ResetCheck = false;

    private void Update()
    {
        // Empty Button Activation
        if (InputButton != null)
        {
            string value = InputButton.GetComponentInChildren<Text>().text;
            if (value != "") EmptyButton.SetActive(true);
            else EmptyButton.SetActive(false);
        }
        else
            EmptyButton.SetActive(false);

        // Reset Button Activation
        if (ResetCheck) ResetButton.SetActive(true);
        else ResetButton.SetActive(false);
    }

    public void ClickEmptyButton()
    {
        if (InputButton != null)
        {
            InputButton.GetComponentInChildren<Text>().text = "";
            // Find row, column in Curr Table Number Button.
            string name = InputButton.name;
            int r = name[2] - '0';
            int c = name[3] - '0';
            // Set array[column][row] 
            arr[c, r] = 0;

            // Verify Number Table Has number
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (arr[i, j] != 0)
                    {
                        ResetCheck = true;
                        return;
                    }
                }
            }
            ResetCheck = false;
        }
    }

    // Sudoku Table Number Button UI 
    public void SetCurrBtn()
    {
        // Change the color of a button that was previously specified
        if (InputButton != null)
        {
            image = InputButton.GetComponent<Image>();
            image.color = Color.black;
        }
        // Setting Curr Button
        InputButton = EventSystem.current.currentSelectedGameObject;

        // Change the color of a Curr button
        image = InputButton.GetComponent<Image>();
        image.color = Color.yellow;
    }

    public void ClickNumberBtn()
    {
        // Check if the Curr Table Number Button is enabled
        if (InputButton != null)
        {
            NumberButton = EventSystem.current.currentSelectedGameObject;
            // Get numberButton value
            string n = NumberButton.GetComponentInChildren<Text>().text;
            // Set Curr Table Number Button
            InputButton.GetComponentInChildren<Text>().text = n;

            // Find row, column in Curr Table Number Button.
            string name = InputButton.name;
            int r = name[2] - '0';
            int c = name[3] - '0';
            // Set array[column][row] 
            arr[c, r] = n[0] - '0';
            ResetCheck = true;
        }
    }

    // Click Reset Button
    public void ClickResetButton()
    {
        ResetTableAndArray();
    }

    // Start Calculation Button
    public void ClickStartCalculationButton()
    {
        bool check = calcultaion();
    }

    // temporary button
    private GameObject Button;

    private void ResetTableAndArray()
    {
        // Check if the Curr Table Number Button is enabled
        if (InputButton != null)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Initialize to zero
                    string prefix = "RC";
                    string name = prefix + j + i;
                    Button = GameObject.Find(name);
                    Button.GetComponentInChildren<Text>().text = "";
                    arr[i, j] = 0;
                }
            }
            // Unassign Curr Button
            ResetCheck = false;
            image = InputButton.GetComponent<Image>();
            image.color = Color.black;
            InputButton = null;
        }
    }

    /* Sudoku Calculation */
    List<Pos> blank = new List<Pos>();
    int cnt = 0; // blank count
    bool flag; // flag = Calculation Complete

    // Coordinate structure
    struct Pos
    {
        public int x;
        public int y;

        public Pos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    private bool calcultaion()
    {
        // Initialize
        blank.Clear();
        cnt = 0;
        flag = false;

        // Check blank(0)
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (arr[i, j] == 0)
                {
                    cnt++;
                    blank.Add(new Pos(i, j));
                }
            }
        }

        // Backtracking
        if (cnt > 0) bt(0);
        return true;
    }

    // Backtracking function
    private void bt(int n)
    {
        if (cnt == n)
        {
            flag = true;
            return;
        }

        int x = blank[n].x;
        int y = blank[n].y;
        for (int i = 1; i <= 9; i++)
        {
            arr[x, y] = i;
            // Check the number entered is correct
            if (check(x, y)) bt(n + 1);
            if (flag) return;
        }
        arr[x, y] = 0;
        return;
    }

    // Check row, column, 3*3 
    private bool check(int x, int y)
    {
        for (int i = 0; i < 9; i++)
        {
            if (arr[i, y] == arr[x, y] && i != x) return false;
            if (arr[x, i] == arr[x, y] && i != y) return false;
        }
        int vert_index = (x / 3) * 3;
        int horiz_index = (y / 3) * 3;

        for (int i = vert_index; i < vert_index + 3; i++)
        {
            for (int j = horiz_index; j < horiz_index + 3; j++)
            {
                if (arr[x, y] == arr[i, j] && x != i && y != j) return false;
            }
        }
        return true;
    }
}

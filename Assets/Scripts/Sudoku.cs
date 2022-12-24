using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sudoku : MonoBehaviour
{
    public GameObject OutputScreen, ErrorScreen, EmptyButton, ResetButton;
    private GameObject InputButton, NumberButton;
    private Image image;
    private Animator animator;

    public int[,] arr = new int[9, 9]; // Sudoku array
    public bool[,] UserInputArr = new bool[9, 9]; // User Input Sudoku array (User input : gray)
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

            arr[c, r] = 0; // Set array[column][row] 
            UserInputArr[c, r] = false; // User Input Sudoku array

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

            arr[c, r] = n[0] - '0'; // Set array[column][row] 
            UserInputArr[c, r] = true; // Set User Input array[column][row] 
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
        // Check array before calculation
        bool check = arrCheck();

        if (!check)
        {
            // Show ErrorScreen
            ErrorScreen.SetActive(true);
            return;
        }

        // Start Sudoku calculation
        calcultaion();
        // Check array after calculation
        check = arrCheck();

        if (!check)
        {
            // Show ErrorScreen
            ErrorScreen.SetActive(true);
            return;
        }

        // Show Output Screen
        OutputScreen.SetActive(true);
        OutputScreen.GetComponent<SudokuOutput>().LoadAndShowData();
    }

    // Check the correct number in array
    private bool arrCheck()
    {
        bool userInputCheck = false;
        // Row and Column Check
        for (int i = 0; i < 9; i++)
        {
            bool[] rowVisited = new bool[10];
            bool[] columnVisited = new bool[10];

            for (int j = 0; j < 9; j++)
            {
                if (UserInputArr[i, j]) userInputCheck = true;
                if (arr[i, j] != 0 && rowVisited[arr[i, j]]) return false;
                if (arr[j, i] != 0 && columnVisited[arr[j, i]]) return false;

                rowVisited[arr[i, j]] = true;
                columnVisited[arr[j, i]] = true;
            }
        }

        // 3*3 Check
        for (int i = 0; i < 3; i++)
        {
            int x = i * 3;
            for (int j = 0; j < 3; j++)
            {
                int y = j * 3;
                bool[] x33Visited = new bool[10];

                for (int nx = x; nx < x + 3; nx++)
                {
                    for (int ny = y; ny < y + 3; ny++)
                    {
                        if (arr[nx, ny] != 0 && x33Visited[arr[nx, ny]]) return false;

                        x33Visited[arr[nx, ny]] = true;
                    }
                }
            }
        }
        return true & userInputCheck;
    }

    // Temporary button
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

                    UserInputArr[i, j] = false;
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

    // Sudoku calculation
    private void calcultaion()
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

    // Check Row, Column, 3*3 
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

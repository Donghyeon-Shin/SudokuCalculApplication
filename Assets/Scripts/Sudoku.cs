using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sudoku : MonoBehaviour
{
    public GameObject InputButton, NumberButton, EmptyButton;
    public Image image;
    private Animator animator;

    int[,] arr = new int[9, 9]; // Sudoku array

    private void Update()
    {
        if (InputButton != null)
        {
            string value = InputButton.GetComponentInChildren<Text>().text;
            if (value != "")
            {
                EmptyButton.SetActive(true);
            }
            else
            {
                EmptyButton.SetActive(false);
            }
        }
        else EmptyButton.SetActive(false);
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
        }
    }

    // Start Calculation Button
    public void StartCalculation()
    {
        // Test Print out Sudoku array
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Debug.Log(arr[i, j]);
            }
        }
    }
}

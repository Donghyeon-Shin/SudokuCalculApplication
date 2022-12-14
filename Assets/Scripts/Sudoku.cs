using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sudoku : MonoBehaviour
{
    public GameObject InputButton, NumberButton;
    public Image image;
    private Animator animator;

    int[,] arr = new int[9, 9];

    public void SetCurrBtn()
    {
        if (InputButton != null)
        {
            image = InputButton.GetComponent<Image>();
            image.color = Color.white;
        }
        InputButton = EventSystem.current.currentSelectedGameObject;
        image = InputButton.GetComponent<Image>();
        image.color = Color.yellow;
    }

    public void ClickNumberBtn()
    {
        if (InputButton != null)
        {
            NumberButton = EventSystem.current.currentSelectedGameObject;
            string n = NumberButton.GetComponentInChildren<Text>().text;
            InputButton.GetComponentInChildren<Text>().text = n;

            string name = InputButton.name;
            int r = name[2] - '0';
            int c = name[3] - '0';
            arr[c, r] = n[0] - '0';
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }

    public void StartCalculation()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                Debug.Log(arr[i, j]);
            }
        }
    }
}

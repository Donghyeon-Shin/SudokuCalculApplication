using System.Collections;
using UnityEngine;

public class ClickAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Close Button
    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    // Close Animation
    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }
}

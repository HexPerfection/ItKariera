using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public Image dashBar;
    public PlayerMovement playerMovement;

    private Coroutine fillCoroutine;

    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        dashBar.fillAmount = 1;
    }

    // Update is called once per frame
    public void EmptyBar()
    {
        // Add smooth transition
        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);

        fillCoroutine = StartCoroutine(ChangeFillAmount(0, playerMovement.dashLength));
    }

    public void FillBar()
    {
        if (fillCoroutine != null)
            StopCoroutine(fillCoroutine);

        fillCoroutine = StartCoroutine(ChangeFillAmount(1, playerMovement.dashCooldown));
    }

    IEnumerator ChangeFillAmount(float targetFillAmount, float duration)
    {
        float currentFillAmount = dashBar.fillAmount;
        float elapsedTime = 0;

        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime / duration;
            dashBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime);
            yield return null;
        }

        dashBar.fillAmount = targetFillAmount;
    }
}

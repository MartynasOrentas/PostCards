using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tipToShow;
    public TextMeshProUGUI cardDescTexts;
    private float timeToWait = 0.5f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateTip();
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        TipManager.OnMouseLoseFocus();
    }

    private void UpdateTip()
    {
        tipToShow = cardDescTexts.text;
    }

    private void ShowMessage()
    {
        TipManager.OnMouseHover(tipToShow, Input.mousePosition);
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);

        ShowMessage();
    }

}

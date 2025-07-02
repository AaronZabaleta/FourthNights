using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public UnityEvent onClickEvent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIAudioManager.Instance?.PlaySound(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIAudioManager.Instance?.PlaySound(clickSound);
        onClickEvent?.Invoke(); 
    }
}

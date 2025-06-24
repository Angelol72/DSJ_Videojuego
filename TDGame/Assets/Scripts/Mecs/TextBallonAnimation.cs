using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;

public class TextBallonAnimation : MonoBehaviour
{
    public GameObject problemText;
    public GameObject options;

    // Time animation duration
    public float animationDuration = 0.4f; 
    public float moveDistance = 0.9f;

    private Sequence sequence;

    void Start()
    {
        // Event handler
        TextBallon textBallon = GetComponent<TextBallon>();
        if (textBallon != null)
        {
            textBallon.onCorrectAnswerEvent += (sender, e) =>
            {
                fadeOut(0);
                if (!textBallon.enemy.isDead())
                {
                    textBallon.chargeNewProblem();
                }
            };
            textBallon.onWrongAnswerEvent += (sender, e) =>
            {
                fadeOut(textBallon.enemyPathMovement.enrageDuration);
            };
        } 
    }

    // Update is called once per frame
    void Update()
    {
        OnClickOutside(); // Check for clicks outside the options and problem text
    }

    // Animate problem text and options
    public void fadeIn()
    {
        // Show the options with a fade-in effect
        CanvasGroup canvasGroup = options.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            canvasGroup.DOFade(moveDistance, animationDuration).OnComplete(() =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        }

        // Deactivate the problem text and move it up
        CanvasGroup canvasGroup2 = problemText.GetComponent<CanvasGroup>();
        canvasGroup2.interactable = false;
        canvasGroup2.blocksRaycasts = false;

        Vector3 posicionInicial = problemText.transform.localPosition;

        problemText.transform.DOLocalMoveY(posicionInicial.y + moveDistance, animationDuration)
            .SetEase(Ease.OutCubic);
    }

    public void fadeOut(float additionalDelay)
    {
        CanvasGroup canvasGroup = options.GetComponent<CanvasGroup>(); // options CanvasGroup
        CanvasGroup canvasGroup2 = problemText.GetComponent<CanvasGroup>(); // problemText CanvasGroup
        Vector3 posicionInicial = problemText.transform.localPosition;

        sequence = DOTween.Sequence();

        // Hide the options with a fade-out effect
        if (canvasGroup != null)
        {
            sequence.AppendCallback(() => {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
            sequence.Append(canvasGroup.DOFade(0f, 0.4f));
        }

        if (canvasGroup2 != null)
        {
            // Deactivate the problem text and move it down
            sequence.Join(problemText.transform.DOLocalMoveY(posicionInicial.y - moveDistance, animationDuration)
                .SetEase(Ease.OutCubic));
            sequence.AppendCallback(() => {
                canvasGroup2.interactable = false;
                canvasGroup2.blocksRaycasts = false;
            });

            // If enemy is dead never reactivates the problem text
            if (GetComponent<TextBallon>().enemy.isDead())
            {
                return;
            }

            // If additional delay is specified, wait before reactivating the problem text
            if (additionalDelay > 0)
            {
                sequence.AppendInterval(additionalDelay);
            }

            sequence.AppendCallback(() => {
                canvasGroup2.interactable = true;
                canvasGroup2.blocksRaycasts = true;
            }); 
        }
    }

    // Check if the user clicked outside the options and problem text
    public void OnClickOutside()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && options.GetComponent<CanvasGroup>().interactable)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Mouse.current.position.ReadValue()
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            bool clickedOnOptionOrProblem = false;
            foreach (var result in results)
            {
                if (result.gameObject == options || result.gameObject == problemText ||
                    result.gameObject.transform.IsChildOf(options.transform) ||
                    result.gameObject.transform.IsChildOf(problemText.transform))
                {
                    clickedOnOptionOrProblem = true;
                    break;
                }
            }

            if (!clickedOnOptionOrProblem)
            {
                fadeOut(0);
            }
        }
    }
}

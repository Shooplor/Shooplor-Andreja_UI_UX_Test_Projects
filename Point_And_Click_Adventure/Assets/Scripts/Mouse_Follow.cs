using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CursorFollow : MonoBehaviour
{
    public RectTransform cursorRectTransform;
    public Image cursorImage;
    public Sprite cursorIdle;
    public Sprite cursorClick;
    public Sprite cursorHover;  
    public Sprite cursorDrag;  

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        
        SetCursorIdle();
    }

    void Update()
    {
        Cursor.visible = false;
        // Follow mouse
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            cursorRectTransform.parent as RectTransform,
            Input.mousePosition,
            null,
            out pos
        );
        cursorRectTransform.anchoredPosition = pos;

        // Switch cursor sprite based on mouse click
        if (Input.GetMouseButtonDown(0))
            SetCursorClick();
        if (Input.GetMouseButtonUp(0))
            SetCursorIdle();
            
    }

    void SetCursorIdle()
    {
        cursorImage.sprite = cursorIdle;
    }

    void SetCursorClick()
    {
        cursorImage.sprite = cursorClick;
    }
}
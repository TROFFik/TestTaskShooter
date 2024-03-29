using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Action<Vector2> rotateAction;
    public Action<Vector3> moveAction;

    public Action clickLeftButtonAction;
    public Action clickRightButtonAction;

    private void Awake()
    {
        CreateSingleton();
    }

    private void Update()
    {
        InputMouse();
        InputKeyboard();
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InputKeyboard()
    {
        Vector3 _moveVector = new Vector2();

        _moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        moveAction?.Invoke(_moveVector);
    }

    private void InputMouse()
    {
        Vector2 mouseScreenPosition = new Vector2();

        mouseScreenPosition = new Vector2(Input.mousePosition.x / Screen.width , Input.mousePosition.y / Screen.height);

        rotateAction?.Invoke(mouseScreenPosition);

        if (Input.GetMouseButtonDown(0))
        {
            clickLeftButtonAction?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            clickRightButtonAction?.Invoke();
        }
    }
}

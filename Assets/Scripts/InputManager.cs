using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Action<Vector2> rotateAction;
    public Action<Vector3> moveAction;

    public Action<bool> clickLeftButtonAction;
    public Action<bool> clickRightButtonAction;
    public Action jumpAction;

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

        if (Input.GetMouseButton(0))
        {
            clickLeftButtonAction?.Invoke(true);
        }

        if (Input.GetMouseButton(1))
        {
            clickRightButtonAction?.Invoke(true);
        }
    }
}

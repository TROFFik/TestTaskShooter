using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private Transform _shootPoint = null;
    private void Start()
    {
        InputManager.Instance.rotateAction += Rotation;
        InputManager.Instance.moveAction += Move;
        InputManager.Instance.clickLeftButtonAction += Shoot;
        InputManager.Instance.clickRightButtonAction += ShootRay;
    }

    private void Move(Vector3 value)
    {
        transform.position += value * _speed * Time.deltaTime;
    }

    private void Rotation(Vector2 value)
    {
        Vector3 vector = value - new Vector2(0.5f, 0.5f);
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, -angle + 90, 0);
    }

    private void Shoot()
    {
        Debug.Log("Shoot1");
    }

    private void ShootRay()
    {
        Debug.Log("ShootRay");
    }

    private void OnDestroy()
    {
        InputManager.Instance.moveAction -= Move;
    }
}

using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private Transform _shootPoint = null;
    [SerializeField] private LineRenderer _lineRenderer = null;
    [SerializeField] private Animator _animator = null;

    [Header("Values")]
    [SerializeField] private float _speed = 10.0f;

    private void Start()
    {
        InputManager.Instance.rotateAction += Rotation;
        InputManager.Instance.moveAction += Move;
        InputManager.Instance.clickLeftButtonAction += Shoot;
        InputManager.Instance.clickRightButtonAction += ShootRay;
    }

    private void Move(Vector3 value)
    {
        transform.position += transform.forward * value.z *_speed * Time.deltaTime;

        _animator.SetFloat("Speed", value.z);
        _animator.gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void Rotation(Vector2 value)
    {
        Vector3 vector = value - new Vector2(0.5f, 0.5f);

        float angle = -Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg + 90;

        transform.eulerAngles = new Vector3(0, angle, 0);
    }

    private void Shoot()
    {
        Bullet tempProjectile = Pool.Instance.GetPoolObject();

        if (tempProjectile != null)
        {
            tempProjectile.gameObject.SetActive(true);
            tempProjectile.transform.rotation = transform.rotation;
            tempProjectile.transform.position = _shootPoint.position;
        }
    }

    private void ShootRay()
    {
        _lineRenderer.positionCount = 2;

        Ray ray = new Ray(_shootPoint.position, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10))
        {
            _lineRenderer.SetPosition(0, _shootPoint.position);
            _lineRenderer.SetPosition(1, hit.point);

            Debug.Log(hit.collider.name);
        }
        else
        {
            _lineRenderer.SetPosition(0, _shootPoint.position);
            _lineRenderer.SetPosition(1, _shootPoint.position + transform.forward * 10);
        }

        StartCoroutine(DisableLines());
    }

    private IEnumerator DisableLines()
    {
        yield return new WaitForSeconds(0.01f);

        _lineRenderer.positionCount = 0;
    }

    private void OnDestroy()
    {
        InputManager.Instance.rotateAction -= Rotation;
        InputManager.Instance.moveAction -= Move;
        InputManager.Instance.clickLeftButtonAction -= Shoot;
        InputManager.Instance.clickRightButtonAction -= ShootRay;
    }
}

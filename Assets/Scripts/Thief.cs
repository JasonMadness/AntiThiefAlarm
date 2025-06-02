using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Thief : MonoBehaviour
{
    [SerializeField] private Transform _robberyTarget;
    [SerializeField] private Transform _exit;
    [SerializeField] private float _speed;
    [SerializeField] private float _flipRotationAngle = 180f;

    private Transform _target;
    private Quaternion _flippedRotation;

    private void Start()
    {
        _target = _robberyTarget;
        _flippedRotation = Quaternion.Euler(transform.rotation.x, _flipRotationAngle, transform.rotation.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Chest chest))
        {
            chest.Open();
            _target = _exit;
            transform.rotation = _flippedRotation;
        }
    }

    private void Update()
    {
        float targetPositionX = Mathf.MoveTowards(transform.position.x, _target.position.x, _speed * Time.deltaTime);
        transform.position = new Vector2(targetPositionX, transform.position.y);
    }
}

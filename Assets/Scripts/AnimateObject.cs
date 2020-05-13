using UnityEngine;

public class AnimateObject : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsRunning = true;
    public float Speed = 10f;
    public float _rotationSpeedX;
    public float _rotationSpeedY;
    public float _rotationSpeedZ;
    void Start()
    {
        _rotationSpeedX = Random.Range(-125, 125);
        _rotationSpeedY = Random.Range(-125, 125);
        _rotationSpeedZ = Random.Range(-125, 125);
    }
    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            this.transform.localPosition -= new Vector3(0, 0, Speed) * Time.deltaTime;
            transform.Rotate(_rotationSpeedX * Time.deltaTime, _rotationSpeedY * Time.deltaTime, _rotationSpeedZ * Time.deltaTime);
        }
    }
}

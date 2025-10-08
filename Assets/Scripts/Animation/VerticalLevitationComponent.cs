using UnityEngine;
using UnityEngine.UIElements;


namespace Animation
{
    public class VerticalLevitationComponent : MonoBehaviour
    {
        [SerializeField] private float _amplitude;
        [SerializeField] private float _frequency;
        [SerializeField] private bool _randomStart;
        private float _startYPosition;
        private float _startXPosition;
        private float _seed;
        private void Start()
        {
            _seed = _randomStart ? Random.Range(2 * Mathf.PI, 0) : 0;
            _startYPosition = transform.position.y;
            _startXPosition = transform.position.x;
        }
        
        private void Update()
        {
            var newYPosition = _startYPosition + Mathf.Sin(_seed * _frequency) * _amplitude;
            transform.position = new Vector3(_startXPosition ,newYPosition, transform.position.z );
            _seed += Time.deltaTime;
        }
    }
}
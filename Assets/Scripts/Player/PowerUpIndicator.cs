using UnityEngine;

public class PowerUpIndicator : MonoBehaviour
{
    private GameObject _currentIndicator;

    public void Show(GameObject indicatorPrefab, Vector3 offset, Vector3 scale)
    {
        Hide();
        if(indicatorPrefab == null) return;
        
        _currentIndicator = Instantiate(indicatorPrefab, transform.position + offset,
            Quaternion.identity);
        _currentIndicator.SetActive(true);
        _currentIndicator.transform.localScale = scale;
    }

    public void Hide()
    {
        if (_currentIndicator != null)
        {
            Destroy(_currentIndicator);
        }
    }
    
    private void Update()
    {
        if (_currentIndicator != null)
        {
            _currentIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        }
    }
}

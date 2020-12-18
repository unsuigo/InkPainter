using UnityEngine;

public class TargetChaising : MonoBehaviour
{
  [SerializeField] private Transform _targetTransform;

  private void Update()
  {
    ChaiseTarget();
  }

  private void ChaiseTarget()
  {
    transform.LookAt(_targetTransform);
  }
  
}

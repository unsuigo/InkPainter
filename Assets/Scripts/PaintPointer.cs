using UnityEngine;

public class PaintPointer : MonoBehaviour
{
   public void PointerPosition(Vector3 pos)
   {
      transform.position = pos;
   }

   public void PointerRotationAlongNormal(Vector3 normalDirection)
   {
      transform.rotation = Quaternion.FromToRotation (transform.up, normalDirection) * transform.rotation;
   }
}

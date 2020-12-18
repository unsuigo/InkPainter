using System.Collections;
using Es.InkPainter;
using Es.InkPainter.Effective;
using Es.InkPainter.Sample;
using UnityEngine;


public class MouseClipPainter : MonoBehaviour
{
   /// <summary>
   	 /// Types of methods used to paint.
   	 /// </summary>
   	 [System.Serializable]
   	 private enum UseMethodType
   	 {
   	 	RaycastHitInfo,
   	 	WorldPoint,
   	 	NearestSurfacePoint,
   	 	DirectUV,
   	 }
   
	  [SerializeField]
	  private bool grab = true;
   
   	 [SerializeField]
   	 private Brush brush;
   
     [SerializeField]
     private GrabArea.GrabTextureWrapMode wrapMode = GrabArea.GrabTextureWrapMode.Repeat;
     
   	 [SerializeField]
   	 private UseMethodType useMethodType = UseMethodType.RaycastHitInfo;
   
   	 [SerializeField]
   	 bool erase = false;
   
   	 private void Update()
   	 {
   	 	if(Input.GetMouseButton(0))
   	 	{
   	 		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   	 		bool success = true;
   	 		RaycastHit hitInfo;
   	 		if(Physics.Raycast(ray, out hitInfo))
   	 		{
   	 			var paintObject = hitInfo.transform.GetComponent<InkCanvas>();
   	 			if(paintObject != null)
   	 				switch(useMethodType)
   	 				{
   	 					case UseMethodType.RaycastHitInfo:
   	 						success = erase ? paintObject.Erase(brush, hitInfo) : paintObject.Paint(brush, hitInfo);
   	 						break;
   
   	 					case UseMethodType.WorldPoint:
   	 						success = erase ? paintObject.Erase(brush, hitInfo.point) : paintObject.Paint(brush, hitInfo.point);
   	 						break;
   
   	 					case UseMethodType.NearestSurfacePoint:
   	 						success = erase ? paintObject.EraseNearestTriangleSurface(brush, hitInfo.point) : paintObject.PaintNearestTriangleSurface(brush, hitInfo.point);
   	 						break;
   
   	 					case UseMethodType.DirectUV:
   	 						if(!(hitInfo.collider is MeshCollider))
   	 							Debug.LogWarning("Raycast may be unexpected if you do not use MeshCollider.");
   	 						success = erase ? paintObject.EraseUVDirect(brush, hitInfo.textureCoord) : paintObject.PaintUVDirect(brush, hitInfo.textureCoord);
   	 						break;
   	 				}
   	 			if(!success)
   	 				Debug.LogError("Failed to paint.");
                
                if(grab)
                {
	                GrabArea.Clip(brush.BrushTexture, brush.Scale, hitInfo.transform.GetComponent<MeshRenderer>().sharedMaterial.mainTexture, hitInfo.textureCoord, brush.RotateAngle, wrapMode, guiClipTexture);
	                brush.BrushTexture = guiClipTexture;
	                brush.ColorBlending = Brush.ColorBlendType.UseBrush;
	                grab = false;
                }
   	 		}
            
            
   	 	}
   	 }
   
      
      private RenderTexture guiClipTexture = null;
      private RaycastHit hitInfo;
   	 public void OnGUI()
   	 {
   	 	if(GUILayout.Button("Reset"))
   	 	{
   	 		foreach(var canvas in FindObjectsOfType<InkCanvas>())
   	 			canvas.ResetPaint();
   	 	}
          
	     GUI.Box(new Rect(0, 0, 300, 320), "");
          GUI.Box(new Rect(0, 0, 300, 300), "Grab Texture");
          if(guiClipTexture != null)
	          GUI.DrawTexture(new Rect(0, 0, 300, 300), guiClipTexture);
          grab = GUI.Toggle(new Rect(0, 300, 300, 20), grab, "Grab");
   	 }
}

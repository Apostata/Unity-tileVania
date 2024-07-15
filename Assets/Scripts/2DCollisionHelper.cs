using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDCollisionHelper : MonoBehaviour
{
   static bool IsCollidingAt2DPosition(Collider2D collider, Vector2 direction, float positionX, float positionY, LayerMask layerMask){
        bool isTouchingLayer = collider.IsTouchingLayers(layerMask);

        if(isTouchingLayer){
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(positionX, positionY), direction, 0.1f, layerMask);
            return hit.collider != null;
        }
        return false;
    }

   public static Dictionary<string, bool> GetCollisionsPositions(Transform transform, Collider2D collider,LayerMask layerMask){
        Dictionary<string, bool> collisions = new Dictionary<string, bool>
        {
            { "top", IsCollidingAt2DPosition(collider, Vector2.up, transform.position.x, transform.position.y + 0.3f, layerMask) },
            { "bottom", IsCollidingAt2DPosition(collider,Vector2.down, transform.position.x, transform.position.y - 0.3f, layerMask) },
            { "left", IsCollidingAt2DPosition(collider, Vector2.left, transform.position.x - 0.3f, transform.position.y, layerMask) },
            { "right", IsCollidingAt2DPosition(collider, Vector2.right, transform.position.x + 0.3f, transform.position.y, layerMask) },
        };
        return collisions;
    }
}

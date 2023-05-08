using UnityEngine;

namespace Hullbreakers
{
    public class SnakeSpawner : MonoBehaviour
    {
        public float amt;
        public float spacing;
        public SnakePart snakePartPrefab;
        public SnakePart snakeHead;
    
    
    
        public void Generate()
        {
            Transform me = transform;

            int children = me.childCount;
            for(int i = 0; i < children; i++){
                DestroyImmediate(me.GetChild(0).gameObject);
            }
        
            Vector3 pos = me.position;

            SnakePart snekHead = snakeHead;
            Hull snekHull = snakeHead.GetComponent<Hull>();

            for (int i = 0; i < amt; i++)
            {
                pos.y -= spacing;
                SnakePart snakePart = Instantiate(snakePartPrefab, pos, me.rotation, me);
                snakePart.next = snekHead.rb;
                snakePart.head = snekHull;
                snekHead.prev = snakePart;
                snekHead = snakePart;
            }
        }
    }
}

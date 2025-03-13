using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    [SerializeField] private float timeDestroy = 2f;
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}

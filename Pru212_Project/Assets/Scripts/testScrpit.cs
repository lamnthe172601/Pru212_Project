using UnityEngine;

public class testScrpit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { 
            this.gameObject.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(this.gameObject.activeSelf == false)
            {
                this.gameObject.SetActive(true);
            }
        }
    }
}

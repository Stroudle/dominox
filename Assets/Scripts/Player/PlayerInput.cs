using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerDrag playerDrag;
    private const int ROTATIONANGLE = 90;

    void Start()
    {
        playerDrag = gameObject.GetComponent<PlayerDrag>();
    }
       
    void Update()
    {
        if(playerDrag.isDrag)
        {
            if(Input.GetButtonDown("rotate"))
            {
                float axisValue = Input.GetAxisRaw("rotate");
                gameObject.transform.Rotate(Vector3.forward, ROTATIONANGLE * axisValue);
            }
        }
    }
}

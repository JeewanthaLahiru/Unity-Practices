using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Vector2 velocity;
    // Start is called before the first frame update

    bool walk, walk_left, walk_right, jump;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();
        UpdatePlayerPosision();
    }

    void CheckPlayerInput()
    {
        bool left_input = Input.GetKey(KeyCode.LeftArrow);
        bool right_input = Input.GetKey(KeyCode.RightArrow);
        bool jump_input = Input.GetKeyDown(KeyCode.Space);

        walk = left_input || right_input;
        walk_left = left_input && !right_input;
        walk_right = right_input && !left_input;
        jump = jump_input;
    }

    void UpdatePlayerPosision()
    {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;

        if (walk)
        {
            if (walk_left)
            {
                pos.x -= velocity.x * Time.deltaTime;
                scale.x = -1;
            }
            if (walk_right)
            {
                pos.x += velocity.x * Time.deltaTime;
                scale.x = 1;
            }

        }

        transform.localPosition = pos;
        transform.localScale = scale;
    }
}

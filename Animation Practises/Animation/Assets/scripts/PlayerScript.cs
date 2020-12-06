using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Vector2 velocity;
    public LayerMask wallMask;
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
        PlayerAnimation();
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
            pos = CheckWalls(pos, scale.x);
        }

        

        transform.localPosition = pos;
        transform.localScale = scale;
    }

    void PlayerAnimation()
    {
        if (walk)
        {
            GetComponent<Animator>().SetBool("isRunning", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isRunning", false);
        }
    }

    Vector3 CheckWalls(Vector3 pos, float direction)
    {
        Vector2 originTop = new Vector2(pos.x + direction * 0.4f, pos.y + 1f - 0.2f);
        Vector2 originMiddle = new Vector2(pos.x + direction * 0.4f, pos.y);
        Vector2 originBottom = new Vector2(pos.x + direction * 0.4f, pos.y - 1f);

        RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallMid = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);

        if (wallTop.collider != null || wallMid.collider != null || wallBottom.collider != null)
        {
            pos.x -= direction * Time.deltaTime * velocity.x;
        }

        return pos;

    }
}

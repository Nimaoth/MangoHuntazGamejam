using UnityEngine;

public static class InputManager
{
    public static Vector2 stickOrientation()
    {
        return new Vector2(horizontal_1(), 0);
    }

    public static float horizontal(int playerId)
    {
        float axisValue = 0.0f;
        axisValue += Input.GetAxisRaw("Horizontal_" + playerId);
        axisValue += Input.GetAxisRaw("Horizontal_DPAD_" + playerId);
        return Mathf.Clamp(axisValue, -1, 1);
    }

    public static bool a_Button(int playerId)
    {
        return Input.GetButton("A_Button_" + playerId);
    }

    public static bool b_Button(int playerId)
    {
        return Input.GetButton("B_Button_" + playerId);
    }

    public static bool x_Button(int playerId)
    {
        return Input.GetButton("X_Button_" + playerId);
    }

    public static bool y_Button(int playerId)
    {
        return Input.GetButton("Y_Button_" + playerId);
    }

    public static bool rb_Button(int playerId)
    {
        return Input.GetButton("RB_Button_" + playerId);
    }



    public static bool a_Button_down(int playerId)
    {
        return Input.GetButtonDown("A_Button_" + playerId);
    }

    public static bool b_Button_down(int playerId)
    {
        return Input.GetButtonDown("B_Button_" + playerId);
    }

    public static bool x_Button_down(int playerId)
    {
        return Input.GetButtonDown("X_Button_" + playerId);
    }

    public static bool y_Button_down(int playerId)
    {
        return Input.GetButtonDown("Y_Button_" + playerId);
    }

    public static bool rb_Button_down(int playerId)
    {
        return Input.GetButtonDown("RB_Button_" + playerId);
    }

    #region Player1Input
    public static float horizontal_1()
    {
        float axisValue = 0.0f;
        axisValue += Input.GetAxis("Horizontal_1");
        axisValue += Input.GetAxis("Horizontal_DPAD_1");
        return Mathf.Clamp(axisValue, -1, 1);
    }

    public static bool a_Button_1()
    {
        return Input.GetButton("A_Button_1");
    }

    public static bool b_Button_1()
    {
        return Input.GetButton("B_Button_1");
    }

    public static bool x_Button_1()
    {
        return Input.GetButton("X_Button_1");
    }

    public static bool y_Button_1()
    {
        return Input.GetButton("Y_Button_1");
    }

    public static bool rb_Button_1()
    {
        return Input.GetButton("RB_Button_1");
    }



    public static bool a_Button_1_down()
    {
        return Input.GetButtonDown("A_Button_1");
    }

    public static bool b_Button_1_down()
    {
        return Input.GetButtonDown("B_Button_1");
    }

    public static bool x_Button_1_down()
    {
        return Input.GetButtonDown("X_Button_1");
    }

    public static bool y_Button_1_down()
    {
        return Input.GetButtonDown("Y_Button_1");
    }

    public static bool rb_Button_1_down()
    {
        return Input.GetButtonDown("RB_Button_1");
    }

    #endregion

    #region Player2 Input

    public static float horizontal_2()
    {
        float axisValue = 0.0f;
        axisValue += Input.GetAxis("Horizontal_2");
        axisValue += Input.GetAxis("Horizontal_DPAD_2");
        return Mathf.Clamp(axisValue, -1, 1);
    }

    public static bool a_Button_2()
    {
        return Input.GetButton("A_Button_2");
    }

    public static bool b_Button_2()
    {
        return Input.GetButton("B_Button_2");
    }

    public static bool x_Button_2()
    {
        return Input.GetButton("X_Button_2");
    }

    public static bool y_Button_2()
    {
        return Input.GetButton("Y_Button_2");
    }

    public static bool rb_Button_2()
    {
        return Input.GetButton("RB_Button_2");
    }



    public static bool a_Button_2_down()
    {
        return Input.GetButtonDown("A_Button_2");
    }

    public static bool b_Button_2_down()
    {
        return Input.GetButtonDown("B_Button_2");
    }

    public static bool x_Button_2_down()
    {
        return Input.GetButtonDown("X_Button_2");
    }

    public static bool y_Button_2_down()
    {
        return Input.GetButtonDown("Y_Button_2");
    }

    public static bool rb_Button_2_down()
    {
        return Input.GetButtonDown("RB_Button_2");
    }

    #endregion
}


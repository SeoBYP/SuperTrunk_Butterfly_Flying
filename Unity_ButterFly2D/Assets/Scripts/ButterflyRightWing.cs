using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ButterflyRightWing : MonoBehaviour
{
    public float _rotateSpeed;
    public float _degreeX;
    public float _degreeY;
    public float _degreeZ;

    // Update is called once per frame
    void Update()
    {
        //이차방적식을 활용한 공식(X^ * sin(Time))을 사용해서 x,y,z의 회전값을 계산했습니다. 
        //
        float x_rot = ((Mathf.Pow(_degreeX, 2) * Mathf.Sin(Time.time * _rotateSpeed)) * -1f);
        float y_rot = ((Mathf.Pow(_degreeY, 2) * Mathf.Sin(Time.time * _rotateSpeed)) * -1f) - 15 - 15;
        float z_rot = ((Mathf.Pow(_degreeZ, 2) * Mathf.Sin(Time.time * _rotateSpeed)) * -1f) - 90 + 15;

        Quaternion newRoatation = Quaternion.Euler(x_rot, y_rot, z_rot);
        StringBuilder text = new StringBuilder();
        {
            text.Append($"RightWing\n");
            text.Append($"x_rot:{x_rot} ");
            text.Append($"y_rot:{y_rot} ");
            text.Append($"z_rot:{z_rot} ");
            //Debug.Log(text);
        }

        transform.localRotation = newRoatation;
    }
}
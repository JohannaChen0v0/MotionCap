using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectpoint : MonoBehaviour
{
    public GameObject tragetPlane; //目标平面
    public GameObject projectPoint; //投影点
    public Vector3 normal = Vector3.up;

    void Update()
    {
        var Worldpos = transform.position;
        var Localpos = tragetPlane.transform.InverseTransformPoint(Worldpos);
        // Debug.Log("Cube对应目标平面当前的Local位置 " + Localpos);

        var dis = Vector3.Dot(Localpos, normal);
        var vecN = normal * dis;
        // Debug.Log("Local位置点乘法向量 up  " + dis + "  dos*nomal =" + vecN);

        Localpos = Localpos - vecN;
        // Debug.Log("ObjLocal " + Localpos);

        //Vector3 ground_position = tragetPlane.TransformPoint(Localpos);

        //ground_position.y += tragetPlane.transform.localScale.y;
        //projectPoint.position = ground_position;

        projectPoint.transform.position = tragetPlane.transform.TransformPoint(Localpos);

        // Debug.Log("目标平面下的Local局部坐标对应在全局的坐标 " + projectPoint.position);
    }
}

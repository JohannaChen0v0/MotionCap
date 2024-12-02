using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectpoint : MonoBehaviour
{
    public GameObject tragetPlane; //Ŀ��ƽ��
    public GameObject projectPoint; //ͶӰ��
    public Vector3 normal = Vector3.up;

    void Update()
    {
        var Worldpos = transform.position;
        var Localpos = tragetPlane.transform.InverseTransformPoint(Worldpos);
        // Debug.Log("Cube��ӦĿ��ƽ�浱ǰ��Localλ�� " + Localpos);

        var dis = Vector3.Dot(Localpos, normal);
        var vecN = normal * dis;
        // Debug.Log("Localλ�õ�˷����� up  " + dis + "  dos*nomal =" + vecN);

        Localpos = Localpos - vecN;
        // Debug.Log("ObjLocal " + Localpos);

        //Vector3 ground_position = tragetPlane.TransformPoint(Localpos);

        //ground_position.y += tragetPlane.transform.localScale.y;
        //projectPoint.position = ground_position;

        projectPoint.transform.position = tragetPlane.transform.TransformPoint(Localpos);

        // Debug.Log("Ŀ��ƽ���µ�Local�ֲ������Ӧ��ȫ�ֵ����� " + projectPoint.position);
    }
}

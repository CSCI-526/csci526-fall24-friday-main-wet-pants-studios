using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    public Light detectionLight;
    public Material revealMaterial;
    private Material originalMaterial;
    private Renderer objRenderer;

    public GameObject handler;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        originalMaterial = objRenderer.material;  // save object's original material 
    }

    void Update()
    {
        if (IsLitByLight())
        {
            Debug.Log("Object " + gameObject.name + " is being lit by the light.");
            if (objRenderer.material != revealMaterial)
            {
                objRenderer.material = revealMaterial;  // change to new material
                handler.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Object " + gameObject.name + " is outside the light cone.");
            if (objRenderer.material != originalMaterial)
            {
                objRenderer.material = originalMaterial;  // back to original
                handler.SetActive(false);

            }
        }
    }


    bool IsLitByLight()
    {
        Vector3 objectWorldPosition = transform.position;
        Vector3 lightWorldPosition = detectionLight.transform.position;
        Vector3 directionToLight = objectWorldPosition - lightWorldPosition;

        // 检查物体和光源之间的最小距离，避免非常靠近时检测失败
        float distanceToLight = directionToLight.magnitude;
        float minDistance = 1f;  // 你可以根据需要调整这个最小距离
        if (distanceToLight < minDistance)
        {
            return true;  // 光源离物体非常近，直接认为物体被照亮
        }

        // 距离检查，确保物体在光源的范围内
        if (distanceToLight > detectionLight.range)
        {
            return false;  // 超出光的照射范围
        }

        // 角度检查
        float angleToLight = Vector3.Angle(detectionLight.transform.forward, directionToLight);
        if (angleToLight < detectionLight.spotAngle / 2)
        {
            // 射线检测，确保光线实际照射到物体
            Ray ray = new Ray(lightWorldPosition + detectionLight.transform.forward * 0.1f, directionToLight); // 从光源前方略偏移发射射线
            if (Physics.Raycast(ray, out RaycastHit hit, detectionLight.range))
            {
                if (hit.collider.gameObject == gameObject)  // 确保光线射中了目标物体
                {
                    return true;
                }
            }
        }

        return false;
    }





    //bool IsLitByLight()
    //{
    //    Vector3 objectWorldPosition = transform.position;  
    //    Vector3 lightWorldPosition = detectionLight.transform.position;  
    //    Vector3 directionToLight = lightWorldPosition - objectWorldPosition;

    //    float angleToLight = Vector3.Angle(detectionLight.transform.forward, -directionToLight);

    //    if (angleToLight < detectionLight.spotAngle / 2)
    //    {
    //        return true; 
    //    }
    //    else
    //    {
    //        return false; 
    //    }
    //}





}
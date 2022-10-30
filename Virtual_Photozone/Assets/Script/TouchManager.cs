using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchManager : MonoBehaviour
{
    private ARRaycastManager raycastMgr;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool Touched = false;

    private GameObject touchObj;

    [SerializeField] 
    private Camera arCamera;

    void Start()
    {
        raycastMgr = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            Ray ray;
            RaycastHit hitobj;

            ray = arCamera.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hitobj))
            {
                if (hitobj.collider.tag.Equals("Structure"))
                {
                    touchObj = hitobj.collider.gameObject;
                    StructureTouch(touchObj);
                    Touched = true;
                }
                else if (hitobj.collider.tag.Equals("Animal"))
                {
                    touchObj = hitobj.collider.gameObject;
                    FindAnimalTouch(touchObj);
                    Touched = true;
                    return;
                }
                else if (hitobj.collider.tag.Equals("Description"))
                {
                    //�Ƹ� ������ �� �� �̹����� �ش� ��ġ ������ �� ��
                    touchObj = hitobj.collider.gameObject;
                    DescriptionTouch(touchObj);
                    Touched = true;
                    return;
                }
            }
        }
        
        if (touch.phase == TouchPhase.Ended)
        {
            Touched = false;
        }
        /* ������ ��ġ ���� �ȵ�, ���� �ʿ� */
        if (raycastMgr.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            if (Touched)
            {
                touchObj.transform.position = hits[0].pose.position;
            }
        }
    }
    void StructureTouch(GameObject touchObj)
    {
        AndroidToast.I.ShowToastMessage(touchObj.gameObject.name + " �������� �߰�!");

        //������ �� �Ƹ� ��ġ ���� �����ϰ�?
    }

    void FindAnimalTouch(GameObject touchObj)
    {
        AndroidToast.I.ShowToastMessage(touchObj.gameObject.name + " �߰�!!");

        //animal ���� �߰�
    }

    void DescriptionTouch(GameObject touchObj)
    {
        AndroidToast.I.ShowToastMessage(touchObj.gameObject.name + " �� ����");

        //�ش� ��ġ�� �ǹ� ����
    }
}
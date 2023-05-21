using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WardrobePlayerAnimation : MonoBehaviour
{
    public void HideInWardrobe(Transform wardrobeTransform)
    {
        StartCoroutine(HideInWardrobeCoroutine(wardrobeTransform));
    }
    
    public void ComeOutOfWardrobe(Transform wardrobeTransform)
    {
        StartCoroutine(ComeOutOfWardrobeCoroutine(wardrobeTransform));
    }
    
    private IEnumerator HideInWardrobeCoroutine(Transform wardrobeTransform)
    {
        Vector3 firstPosition;
        Vector3 secondPosition;
        Quaternion firstRotation;
        Quaternion secondRotation;
        
        Vector3 objectPosition = wardrobeTransform.position;
        Quaternion objectRotation = wardrobeTransform.rotation;
            
        firstRotation = Quaternion.Euler(wardrobeTransform.eulerAngles + new Vector3(0f, 180f, 0f));
        Debug.Log("firstRotation = " + firstRotation.eulerAngles);
        secondRotation = Quaternion.Euler(firstRotation.eulerAngles + new Vector3(0f, 180f, 0f));
        Debug.Log("secondRotation = " + secondRotation.eulerAngles);

        Vector3 objectRotationEuler = objectRotation.eulerAngles;

            
        float yawRad = Mathf.Deg2Rad * objectRotationEuler.y;

            
        float newX = objectPosition.x + 1f * Mathf.Sin(yawRad);
        float newY = objectPosition.y;
        float newZ = objectPosition.z + 1f * Mathf.Cos(yawRad);
            
        firstPosition = new Vector3(newX, newY, newZ);
            
        newX = objectPosition.x - 0.15f * Mathf.Sin(yawRad);
        newY = objectPosition.y;
        newZ = objectPosition.z - 0.15f * Mathf.Cos(yawRad);

        secondPosition = new Vector3(newX, newY, newZ);

        Quaternion newQuaternionBody = new Quaternion(0 , firstRotation.y, 0, firstRotation.w);
        Quaternion newQuaternionCamera = new Quaternion(firstRotation.x , 0, 0, firstRotation.w);
        
        transform.DOMove(firstPosition, 0.25f);
        transform.DORotate(newQuaternionBody.eulerAngles, 0.25f);
        Player.Instance.GetPlayerCamera().transform.DOLocalRotate(newQuaternionCamera.eulerAngles, 0.25f);

        yield return new WaitForSeconds(0.25f);
        /*transform.position = firstPosition;
        transform.rotation = firstRotation;*/

        Vector3 centerPoint = (firstPosition + secondPosition) / 2f;

            
        Vector3 controlPoint = centerPoint - Vector3.Cross(secondPosition - firstPosition, Vector3.up).normalized * 0.5f;

            
        Vector3[] pathPoints = new Vector3[] { firstPosition, controlPoint, secondPosition };

            
        transform.DOPath(pathPoints, 1, PathType.CatmullRom)
            .SetEase(Ease.Linear);
        
        newQuaternionBody = new Quaternion(0 , secondRotation.y, 0, secondRotation.w);

        transform.DORotate(newQuaternionBody.eulerAngles, 1f);
        

        
        
        yield return new WaitForSeconds(1f);
        
    }

    private IEnumerator ComeOutOfWardrobeCoroutine(Transform wardrobeTransform)
    {
        Vector3 firstPosition;
        Vector3 secondPosition;

        Vector3 objectPosition = wardrobeTransform.position;
        Quaternion objectRotation = wardrobeTransform.rotation;
            
        

        Vector3 objectRotationEuler = objectRotation.eulerAngles;

            
        float yawRad = Mathf.Deg2Rad * objectRotationEuler.y;

        firstPosition = transform.position;
            
        float newX = objectPosition.x + 1f * Mathf.Sin(yawRad);
        float newY = objectPosition.y;
        float newZ = objectPosition.z + 1f * Mathf.Cos(yawRad);
            
        secondPosition = new Vector3(newX, newY, newZ);

        Vector3 centerPoint = (firstPosition + secondPosition) / 2f;

        Vector3 controlPoint = centerPoint + Vector3.Cross(secondPosition - firstPosition, Vector3.up).normalized * 0.5f;
        
        Vector3[] pathPoints = new Vector3[] { firstPosition, controlPoint, secondPosition };
        
        Debug.Log("firstPosition = " + firstPosition + " controlPoint = " + controlPoint + " secondPosition = " + secondPosition);

        yield return new WaitForSeconds(0.25f);
        
        transform.DOPath(pathPoints, 1, PathType.CatmullRom)
            .SetEase(Ease.Linear);
        
        yield return new WaitForSeconds(1f);
    }
}

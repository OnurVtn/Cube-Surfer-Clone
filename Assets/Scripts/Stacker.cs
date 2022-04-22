using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacker : MonoBehaviour
{
    [SerializeField] private Transform playerVisualTransform, kidModelTransform;
    [SerializeField] private GameObject stackedCubePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectableCube"))
        {
            var newCubeNumber = other.transform.childCount + 1;

            AddCube(newCubeNumber);
          
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Diamond"))
        {
            UIManager.Instance.UpdateDiamond();
            Destroy(other.gameObject);
        }
    }

    private void AddCube(int cubeNumber)
    {
        kidModelTransform.position = new Vector3(kidModelTransform.position.x, kidModelTransform.position.y + cubeNumber, kidModelTransform.position.z);

        var currentHeight = playerVisualTransform.childCount - 2;

        for (int i = 0; i < cubeNumber; i++)
        { 
            var newCubePosition = new Vector3(transform.position.x, transform.position.y + currentHeight, transform.position.z);
            var newCube = Instantiate(stackedCubePrefab, newCubePosition, Quaternion.identity);
            newCube.transform.SetParent(playerVisualTransform);
            currentHeight++;
        }
    }
}

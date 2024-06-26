using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using EzySlice;

public class KnifeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform knifeTransform;  // Reference to the knife's transform
    public float sliceForce = 10f;   // Force to apply to the sliced pieces
    public float slideForce = 5f;    // Force applied to slide the pieces
    private List<GameObject> slicedParts = new List<GameObject>(); // Store sliced parts
    
    GameObject fruits;
    [SerializeField] float xMax;
    [SerializeField] float xMin;
    [SerializeField] float xValue;
    

    void Update()
    {
        float moveDirection = Input.GetAxis("Vertical"); // Up and Down arrow keys
        knifeTransform.Translate(Vector3.up * moveDirection * moveSpeed * Time.deltaTime);
        float clampedy = Mathf.Clamp(knifeTransform.transform.position.y, xMin, xMax);
        knifeTransform.transform.position = new Vector3(knifeTransform.transform.position.x, clampedy, knifeTransform.transform.position.z);
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Move the sliced pieces to the left
            
            SlidePieces(Vector3.left);
            if (fruits == null) return;
            
            
            MoveFruits(fruits);
            
            fruits = null;

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Move the sliced pieces to the right
            SlidePieces(Vector3.right);
        }


        // Add slicing logic here
    }

    private void MoveFruits(GameObject fruits)
    {
        //fruits.transform.Translate(Vector3.left * Time.deltaTime * slideForce);
        fruits.transform.position = new Vector3(fruits.transform.position.x - 0.5f, 0, 0);
        
    }

    void SlidePieces(Vector3 direction)
    {
        // Apply force or translation to the sliced pieces
        foreach (GameObject part in slicedParts)
        {
            if (part != null)
            {
                // Apply a translation to move the part in the specified direction
                part.transform.Translate(direction * Time.deltaTime * slideForce);
                
                ChangeRigidbody(part);
                
            }
        }
        
        slicedParts.Clear();
    }

    void ChangeRigidbody(GameObject part)
    {      
       part.transform.position = new Vector3(-1.059f, -0.053f, 0.554f);
    }

    void OnTriggerEnter(Collider other)
    { 
            // Call slicing method
            if (other.CompareTag("Fruit"))
            {
            if (other.gameObject.transform.childCount == 0)
            {
                return;
            }
                
                fruits = other.gameObject;
                
                SliceFruit(other.gameObject);
            }
    }

    void SliceFruit(GameObject fruit)
    {

        Transform slice = fruit.transform.GetChild(0);
        print(slice.name);
        slice.SetParent(null);
        //Rigidbody rb = slice.gameObject.AddComponent<Rigidbody>();
        slicedParts.Add(slice.gameObject);
        
    }


    
}

    


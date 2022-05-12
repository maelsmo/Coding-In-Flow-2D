using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 2f;


    private void Update()
    {
        transform.Rotate(0, 0, 150 * speed * Time.deltaTime);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 200f;

    void Update()
    {
        // Judėjimas pirmyn/atgal
        float moveInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * moveInput * speed * Time.deltaTime);

        // Sukimasis
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward, -rotationInput * rotationSpeed * Time.deltaTime);
    }
}

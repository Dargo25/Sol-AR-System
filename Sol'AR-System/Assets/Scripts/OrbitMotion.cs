using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour {

    public Transform movingObject;
    public Ellipse orbitPath;

    [Range(0f, 1f)]
    public float orbitProgress = 0f;
    public float orbitPeriod = 3f;
    public bool orbitActive = true;

	// Use this for initialization
	void Start () {
		if (movingObject == null)
        {
            orbitActive = false;
            return;
        }
        SetEarthPosition();
        StartCoroutine(AnimateOrbit());
	}

    private IEnumerator AnimateOrbit()
    {
        if (orbitPeriod < 0.1f)
        {
            orbitPeriod = 0.1f;
        }
        float orbitSpeed = 1f / orbitPeriod;
        while (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetEarthPosition();
            yield return null;
        }
    }

    private void SetEarthPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        movingObject.localPosition = new Vector3(orbitPos.x, 0f, orbitPos.y);
    }
}

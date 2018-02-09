using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentCube : MonoBehaviour
{
    public GameObject TopCube;

	public void TopCubeDestroy()
    {
        Destroy(TopCube);
	}

    private void Update()
    {
        gameObject.transform.LookAt(Camera.main.transform.position);
    }
}

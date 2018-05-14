using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentCube : MonoBehaviour
{
    

	public void TopCubeDestroy()
    {
		Destroy(gameObject.transform.parent.gameObject);

	}

    private void Update()
    {
        gameObject.transform.LookAt(Camera.main.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour, IHarpoonable {

	public void OnHarpoon()
    {
        Destroy(this.gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class buttonControl_script : MonoBehaviour 
{

	Animator anim;

	void Awake ()
	{
		anim = GetComponentInChildren<Animator>();
	}

	public void Dance()
	{
		anim.SetBool ("dancing", !(anim.GetBool("dancing")));
	}
}

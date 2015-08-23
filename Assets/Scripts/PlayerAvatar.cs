using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class PlayerAvatar : MonoBehaviour {

	private const int NumSprites = 5;

	private Sprite[] _sprites;
	private int _playerNum;
	private Image _image;
	private int _numZ;
	public int NumZ
	{
		get
		{
			return _numZ;
		}
		set
		{
			_numZ = value;
			_image.sprite = _sprites[_numZ];
			if(_numZ == 3)
				StartCoroutine(Die());
		}
	}

	public int PlayerNum
	{
		set
		{
			_playerNum = value; //child.Index + 1;

			//0 - 3 - Number of Z
			//    4 - Dead
			_sprites = new Sprite[NumSprites];

			for (int i = 0; i < NumSprites; i++)
			{
				_sprites[i] = Resources.Load<Sprite>("UI_P" + _playerNum + "_" + i);
			}

			gameObject.SetActive(true);

			NumZ = _numZ;
		}
	}

	private int curr;
	private float time = 1f;

	// Use this for initialization
	void Awake () 
	{
		_image = GetComponent<Image>();
		_numZ = 0;
	}

	// Update is called once per frame
	void Update () 
	{
	}

	IEnumerator Die()
	{
		yield return new WaitForSeconds(1.2f);
		NumZ = 4;
	}

}

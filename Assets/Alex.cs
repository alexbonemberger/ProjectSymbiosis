using UnityEngine;
using System.Collections;

public class Alex : MonoBehaviour {

	public static GameObject[] createObjs (GameObject obj, int numObjs, float coordenatesObjPosX, float coordenatesObjPosY) {
		// vars
		GameObject[] objs;
		objs = new GameObject[numObjs];
		int lineSlot = 0;

		//code
		for (int i = 0; i < numObjs; i++) {

			GameObject objSlot = Instantiate (obj, new Vector3 (coordenatesObjPosX, coordenatesObjPosY, 0), Quaternion.identity) as GameObject;
			coordenatesObjPosX += 1.5f;
			objs [i] = objSlot;
			//objs [i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
			lineSlot++;
			if (lineSlot > 2) {
				lineSlot = 0;
				coordenatesObjPosX = -1.5f;
				coordenatesObjPosY -= 1.5f;
			}
		}
		return objs;
	}

	public static GameObject[] createObjs (GameObject obj, int numObjs, float coordenatesObjPosX, float coordenatesObjPosY, int color) {

		// vars
		GameObject[] objs;
		objs = new GameObject[numObjs];
		int lineSlot = 0;

		//code
		for (int i = 0; i < numObjs; i++) {

			GameObject objSlot = Instantiate (obj, new Vector3 (coordenatesObjPosX, coordenatesObjPosY, 0), Quaternion.identity) as GameObject;
			coordenatesObjPosX += 1.5f;
			objs [i] = objSlot;
			//objs [i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
			//altera a cor das bolas
			objs [i].GetComponent<Animator> ().SetInteger ("color", color);

			lineSlot++;
			if (lineSlot > 2) {
				lineSlot = 0;
				coordenatesObjPosX = -1.5f;
				coordenatesObjPosY -= 1.5f;
			}
		}
		return objs;
	}

	//teletransoporta um objeto
	public static void teleport (GameObject obj, float velX) {
		obj.transform.position = new Vector3 (obj.transform.position.x+velX, obj.transform.position.y, obj.transform.position.z);
	}

	public static void teleport (GameObject obj, float velX, float velY) {
		obj.transform.position = new Vector3 (obj.transform.position.x+velX, obj.transform.position.y+velY, obj.transform.position.z);
	}

	//empura um objecto
	public static void push (GameObject obj, float velX) {
		obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(velX,0f));
	}

	public static void push (GameObject obj, float velX, float velY) {
		obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(velX,velY));
	}

	//retorna a distancia positiva entre o objeto e uma coordenada X
	public static float distanceToCoordenateX (GameObject obj, float coordenateX) {
		if ((obj.transform.position.x - coordenateX) > 0) {
			return (obj.transform.position.x - coordenateX);
		} else {
			return ((obj.transform.position.x - coordenateX) * -1);
		}
	}

	//retorna a distancia positiva entre o objeto e uma coordenada Y
	public static float distanceToCoordenateY (GameObject obj, float coordenateY) {
		if ((obj.transform.position.y - coordenateY) >= 0) {
			return (obj.transform.position.y - coordenateY);
		} else {
			return ((obj.transform.position.y - coordenateY) * -1);
		}
	}

	//move o objeto em direção as coordenadas indicadas até encontralas
	public static void reach(GameObject obj, float coordenateX, float coordenateY)
	{
		if (obj.transform.position.x != coordenateX || obj.transform.position.y != coordenateY) {
			if ((Alex.distanceToCoordenateY (obj, coordenateY)) < 0.02) {
				obj.transform.position = new Vector3 (obj.transform.position.x, coordenateY, 0f);
			} else {
				if (obj.transform.position.y < coordenateY) {
					Alex.push (obj, 0f, 10f);
				} else {
					Alex.push (obj, 0f, -10f);
				}
			}

			if ((Alex.distanceToCoordenateX (obj, coordenateX)) < 0.02) {
				obj.transform.position = new Vector3 (coordenateX, obj.transform.position.y, 0f);
			} else {
				if (obj.transform.position.x < coordenateY) {
					Alex.push (obj, 10f, 0f);
				} else {
					Alex.push (obj, -10f, 0f);
				}
			}
		}
	}


}
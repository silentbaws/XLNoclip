using Harmony12;
using UnityEngine;

namespace XLNoclip {
	class NoclipController : MonoBehaviour{
		bool noclipping = false;
		float moveSpeed = 0.3f;
		float rotateSpeed = 1.0f;
		GameObject skaterObject;

		float oldTimeScale = 1.0f;

		private void Start() {
		}

		private void Update() {
			if(PlayerController.Instance.inputController.player.GetButtonDown("Y")) {
				if (noclipping) EndNoclip();
				else StartNoclip();
			}

			if (noclipping) {
				skaterObject.transform.position += PlayerController.Instance.inputController.player.GetAxis("LeftStickX") * moveSpeed * Camera.main.transform.right +
					PlayerController.Instance.inputController.player.GetAxis("RightStickY") * moveSpeed * Camera.main.transform.up +
					PlayerController.Instance.inputController.player.GetAxis("LeftStickY") * moveSpeed * Camera.main.transform.forward;

				PlayerController.Instance.skaterController.skaterRigidbody.transform.Rotate(Vector3.up * (-PlayerController.Instance.inputController.player.GetAxis("LT") + PlayerController.Instance.inputController.player.GetAxis("RT")) * rotateSpeed);
				PlayerController.Instance.boardController.boardRigidbody.transform.Rotate(Vector3.up * (-PlayerController.Instance.inputController.player.GetAxis("LT") + PlayerController.Instance.inputController.player.GetAxis("RT")) * rotateSpeed);
				Camera.main.transform.RotateAround(PlayerController.Instance.skaterController.skaterRigidbody.transform.position, Vector3.up, (-PlayerController.Instance.inputController.player.GetAxis("LT") + PlayerController.Instance.inputController.player.GetAxis("RT")) * rotateSpeed);
			}
		}

		private void StartNoclip() {
			if (!noclipping) {
				noclipping = true;
				oldTimeScale = Time.timeScale;
				Time.timeScale = 0.0f;
			}
		}

		private void EndNoclip() {
			if (noclipping) {
				noclipping = false;
				Time.timeScale = oldTimeScale;
				Traverse.Create(PlayerController.Instance.respawn).Method("SetSpawnPos").GetValue();
				PlayerController.Instance.respawn.DoRespawn();
			}
		}

		private void OnDestroy() {
			EndNoclip();
		}

		private void OnGUI() {
			if (noclipping) {
				GUIStyle style = new GUIStyle();
				style.fontSize = 32;
				GUI.Label(new Rect(0, 0, 100, 30), "Noclipping", style);
			}
		}
	}
}

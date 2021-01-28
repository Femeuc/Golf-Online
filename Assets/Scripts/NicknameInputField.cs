using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Femeuc.GolfOnline
{
    public class NicknameInputField : MonoBehaviour
    {
        private InputField nicknameInputField;
        const string playerNamePrefKey = "PlayerName";
        // Start is called before the first frame update
        void Start()
        {
            nicknameInputField = GetComponent<InputField>();

            string defaultName = string.Empty;
            if (nicknameInputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    nicknameInputField.text = defaultName;
                }
            }


            PhotonNetwork.NickName = defaultName;
        }

        public void SetPlayerName(string value)
        {
            // #Important
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = value;
            PlayerPrefs.SetString(playerNamePrefKey, value);
        }
    }
}
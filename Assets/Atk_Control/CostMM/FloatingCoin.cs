using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingCoin : MonoBehaviour
{
    private TextMeshProUGUI mText;
    private Vector3 mSpawnPosition;
    private float mMoveUpSpeed = 1.0f;
    private float mLifeTime = 1.0f;
    // Start is called before the first frame update
    void Awake()
    {
        mText = GetComponent<TextMeshProUGUI>();
    }

    public void SetupText(Transform target, string sText, float speed)
    {
        mSpawnPosition = target.position + Vector3.up * 1.0f;
        Debug.Log(mSpawnPosition + "---");
        mLifeTime = 2.0f;
        mMoveUpSpeed = speed;
        mText.text = sText;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (mLifeTime < 0)
        {
            Destroy(gameObject);
            return;
        }
        mSpawnPosition = mSpawnPosition + Vector3.up * mMoveUpSpeed * Time.deltaTime;
        Vector2 vOut = new Vector2(0, 0);
        Vector3 vPos = Camera.main.WorldToScreenPoint(mSpawnPosition);
        Debug.Log(mSpawnPosition + "  ::  " + vPos);
        if (vPos.z < 0)
        {
            mText.enabled = false;
        }
        else
        {
            mText.enabled = true;

            Canvas c = UiMainforCoin.Instance().GetCanvas();
            if (c.renderMode == RenderMode.ScreenSpaceCamera && c.worldCamera != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(UiMainforCoin.Instance().GetRectTransform(),
              vPos, UiMainforCoin.Instance().GetCanvas().worldCamera, out vOut);
                this.transform.localPosition = vOut;
                Debug.Log(vOut);
            }
            else
            {
                this.transform.position = vPos;
            }

        }

        mLifeTime -= Time.deltaTime;
    }
}

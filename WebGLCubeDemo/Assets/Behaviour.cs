using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Behaviour : MonoBehaviour
{

    /// <summary>
    /// カメラ
    /// </summary>
    [SerializeField] Camera _sceneCamera;
    float _cameraZOAmount = 2.0f;
    

    #region Cube
    [SerializeField] GameObject _cubePrefab;

    /// <summary>
    /// 立方体のスケール
    /// </summary>
    float _cubeScale = 1.0f;

    /// <summary>
    /// 立方体の現在の数
    /// </summary>
    [SerializeField] int _cubeCount = 0;

    /// <summary>
    /// UI: テキスト
    /// </summary>
    [SerializeField] Text _counText;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _cubeCount = 1;
    }

    // スペースキー押下で灰色の立方体を複数生成し、青い立方体を大きくして降らせる。
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _cubeScale++;
            _sceneCamera.transform.Translate(0, 0, -_cameraZOAmount);
            transform.localScale = new Vector3(_cubeScale, _cubeScale, _cubeScale);
            transform.localPosition = new Vector3(0.0f, 10.0f, 0.0f);

            GenerateCube(10);
        }
    }

    void GenerateCube(int num)
    {
        // 立方体生成
        for (int y = 0; y < num; y++)
        {
            for (int x = 0; x < num; x++)
            {
                GameObject cube = Instantiate(_cubePrefab);
                cube.transform.position = new Vector3(x, y, 0);
            }
        }

        StartCoroutine(CountUp(num * num));
    }

    /// <summary>
    /// カウンターの数字を増やしUI反映する。
    /// </summary>
    /// <returns></returns>
    IEnumerator CountUp(int increaseCount)
    {
        int step = 100;
        float countPerStep = (increaseCount / step);

        for (int i = 0; i < step; i++)
        {
            _cubeCount += (int)countPerStep;
            _counText.text = _cubeCount.ToString();
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
}

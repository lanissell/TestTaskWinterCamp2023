using TMPro;
using UnityEngine;

public class Plate : MonoBehaviour
{

    public int PlateNum
    {
        get
        { 
            return _plateNum; 
        }
        set
        {
            _plateNum = value;
            _textMesh.text = _plateNum.ToString(); 
        }
    }

    private int _plateNum;
    [SerializeField]
    private TextMeshProUGUI _textMesh;

}

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CurrentStateUI : MonoBehaviour
{
    [SerializeField] private Text _currentStateText;
    [SerializeField] private Text _controllText;

    public void SetControllState(State state)
    {
        StringBuilder text = new StringBuilder();
        {
            text.Append("Current State : ");
            text.Append(state.ToString());
            _currentStateText.text = text.ToString();
        }
        _controllText.gameObject.SetActive(true);
    }
    public void SetAutoState(State state)
    {
        StringBuilder text = new StringBuilder();
        {
            text.Append("Current State : ");
            text.Append(state.ToString());
            _currentStateText.text = text.ToString();
        }
        _controllText.gameObject.SetActive(false);
    }
}

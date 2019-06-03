using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpdateKeybind : MonoBehaviour {

    public TMPro.TMP_InputField text;
    public TMPro.TMP_Text placeHolder;
    KeyCode keycode;
    Event e;
    private bool updating;

    void Start()
    {
        updating = false;
        if (text.text == "A")
        {
            if (PlayerPrefs.HasKey("a"))
            {
                SyncData.a = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("a"));
            }
            text.text = SyncData.a.ToString();
        }
        else if (text.text == "D")
        {
            if (PlayerPrefs.HasKey("d"))
            {
                SyncData.d = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("d"));
            }
            text.text = SyncData.d.ToString();
        }
        else if (text.text == "SPACE")
        {
            if (PlayerPrefs.HasKey("space"))
            {
                SyncData.space = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("space"));
            }
            text.text = SyncData.space.ToString();
        }
        else if (text.text == "F")
        {
            if (PlayerPrefs.HasKey("f"))
            {
                SyncData.f = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("f"));
            }
            text.text = SyncData.f.ToString();
        }
        else if (text.text == "R")
        {
            if (PlayerPrefs.HasKey("r"))
            {
                SyncData.r = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("r"));
            }
            text.text = SyncData.r.ToString();
        }
        else if (text.text == "I")
        {
            if (PlayerPrefs.HasKey("i"))
            {
                SyncData.i = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("i"));
            }
            text.text = SyncData.i.ToString();
        }
    }

    void OnGUI()
    {
        e = Event.current;
        if (e.isKey)
        {
            keycode = e.keyCode;
            updating = false;
        }
    }

    public void WrapperA()
    {
        StartCoroutine("UpdateA");
    }

	IEnumerator UpdateA()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.a = keycode;
                PlayerPrefs.SetString("a", keycode.ToString());
                PlayerPrefs.Save();
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.a.ToString().ToUpper();
        }
    }
    public void WrapperS()
    {
        StartCoroutine("UpdateS");
    }

    IEnumerator UpdateS()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.s = keycode;
                PlayerPrefs.SetString("s", keycode.ToString());
                PlayerPrefs.Save();
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.s.ToString().ToUpper();
        }
    }
    public void WrapperD()
    {
        StartCoroutine("UpdateD");
    }

    IEnumerator UpdateD()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.d = keycode;
                PlayerPrefs.SetString("d", keycode.ToString());
                PlayerPrefs.Save();
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.d.ToString().ToUpper();
        }
    }
    public void WrapperSpace()
    {
        StartCoroutine("UpdateSpace");
    }

    IEnumerator UpdateSpace()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.space = keycode;
                PlayerPrefs.SetString("space", keycode.ToString());
                PlayerPrefs.Save();
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.space.ToString().ToUpper();
        }
    }
    public void WrapperR()
    {
        StartCoroutine("UpdateR");
    }

    IEnumerator UpdateR()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.r = keycode;
                PlayerPrefs.SetString("r", keycode.ToString());
                PlayerPrefs.Save();
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.r.ToString().ToUpper();
        }
    }
    public void WrapperI()
    {
        StartCoroutine("UpdateI");
    }

    IEnumerator UpdateI()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.i = keycode;
                PlayerPrefs.SetString("i", keycode.ToString());
                PlayerPrefs.Save();
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.i.ToString().ToUpper();
        }
    }
    public void WrapperF()
    {
        StartCoroutine("UpdateF");
    }

    IEnumerator UpdateF()
    {
        updating = true;
        while (updating)
        {
            yield return null;
        }
        if (text.text != "")
        {
            if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
            {
                SyncData.f = keycode;
                PlayerPrefs.SetString("f", keycode.ToString());
                PlayerPrefs.Save();
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        if (keycode.ToString() != null && keycode.ToString() != "None" && keycode.ToString() != "")
        {
            text.text = SyncData.f.ToString().ToUpper();
        }
    }

    public void Clear()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}

using UnityEngine;
using UnityEngine.UI;

static public class Utils
{
    /// <summary>
    /// Установить текст в поле UI элемента.
    /// </summary>
    /// <param name="gameObject">UI объект с элементом Text.</param>
    /// <param name="text">Текст в виде string.</param>
    static internal void SetTextOnUIObject(GameObject gameObject, string text)
    {
        if (gameObject.GetComponent<Text>() != null)
        {
            gameObject.GetComponent<Text>().text = text;
        }
    }

    /// <summary>
    /// Установить текст в поле UI элемента.
    /// </summary>
    /// <param name="gameObject">UI объект с элементом Text.</param>
    /// <param name="text">Текст в виде int.</param>
    static internal void SetTextOnUIObject(GameObject gameObject, int text)
    {
        if (gameObject.GetComponent<Text>() != null)
        {
            gameObject.GetComponent<Text>().text = text.ToString();
        }
    }

    /// <summary>
    /// Установить текст в поле UI элемента.
    /// </summary>
    /// <param name="gameObject">UI объект с элементом Text.</param>
    /// <param name="text">Текст в виде float.</param>
    static internal void SetTextOnUIObject(GameObject gameObject, float text)
    {
        if (gameObject.GetComponent<Text>() != null)
        {
            gameObject.GetComponent<Text>().text = text.ToString();
        }
    }

    /// <summary>
    /// Установить текст в поле UI элемента.
    /// </summary>
    /// <param name="gameObject">UI объект с элементом Text.</param>
    /// <param name="text">Текст в виде double.</param>
    static internal void SetTextOnUIObject(GameObject gameObject, double text)
    {
        if (gameObject.GetComponent<Text>() != null)
        {
            gameObject.GetComponent<Text>().text = text.ToString();
        }
    }

    /// <summary>
    /// Проверка на моментальное нажатие кнопки.
    /// </summary>
    /// <param name="key">Код клавиши.</param>
    /// <returns>Значение, отражающее состояние нажатия.</returns>
    static internal bool CheckKeyDown(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Проверка на зажатую клавишу.
    /// </summary>
    /// <param name="key">Код клавиши.</param>
    /// <returns>Значение, отражающее состояние нажатия.</returns>
    static internal bool CheckKeyPress(KeyCode key)
    {
        if (Input.GetKey(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Установить параметр isTrigger у Colider`а объекта.
    /// </summary>
    /// <param name="gameObject">Целевой объект.</param>
    /// <param name="boolean">Значение парметра.</param>
    static internal void SetIsTrigger(GameObject gameObject, bool boolean)
    {
        if(gameObject.GetComponent<Collider>() != null)
        {
            gameObject.GetComponent<Collider>().isTrigger = boolean;
        }
        else if (gameObject.GetComponent<Collider2D>() != null)
        {
            gameObject.GetComponent<Collider2D>().isTrigger = boolean;
        }
    }

    /// <summary>
    /// Установить родителя элементу.
    /// </summary>
    /// <param name="child">Целевой Ребенок.</param>
    /// <param name="targetParent">Целевой Родитель.</param>
    /// <returns></returns>
    static internal bool ChangeParent(Transform child, Transform targetParent)
    {
        if(targetParent.gameObject == null || child.gameObject == null)
        {
            return false;
        }

        child.parent = targetParent;
        return true;
    }

    /// <summary>
    /// Активировать элементы в иерархии.
    /// </summary>
    /// <param name="elems">Массив объектов элементов.</param>
    static internal void ActivateElemsInHierarchy(GameObject[] elems)
    {
        foreach (var elem in elems)
        {
            if(elem != null)
            {
                elem.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Деактивировать элементы в иерархии.
    /// </summary>
    /// <param name="elems">Массив объектов элементов.</param>
    static internal void DeactivateElemsInHierarchy(GameObject[] elems)
    {
        foreach (var elem in elems)
        {
            if (elem != null)
            {
                elem.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Сменить активность элемента в иерархии на противоположное.
    /// </summary>
    /// <param name="elems">Массив объектов элементов.</param>
    static internal void ReverseElemsCondition(GameObject[] elems)
    {
        foreach (var elem in elems)
        {
            if (elem != null)
            {
                if (elem.activeInHierarchy)
                {
                    elem.SetActive(false);
                }
                else
                {
                    elem.SetActive(true);
                }
            }
        }
    }

    static internal void ChangeLocalScaleTo(GameObject gameObject , float x, float y, float z)
    {
        var tempScale = gameObject.transform.localScale;
        tempScale.x = x;
        tempScale.x = y;
        tempScale.x = z;
        gameObject.transform.localScale = tempScale;
    }
}
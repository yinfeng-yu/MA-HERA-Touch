using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool _active = false;
    public void ChangeLocale(int localeID)
    {
        if (_active) return;
        StartCoroutine(SetLocale(localeID));
    }
    public void SwitchLocale()
    {
        // Only call this function when you want to switch between English and German!
        // And when index of English == 0 and index of German == 1!
        if (_active) return;
        if (LocalizationSettings.SelectedLocale.Identifier == "en") ChangeLocale(1);
        else ChangeLocale(0);
    }

    IEnumerator SetLocale(int localeID)
    {
        _active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        _active = false;
    }
}

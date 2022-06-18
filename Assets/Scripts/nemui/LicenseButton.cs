using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class LicenseButton : MonoBehaviour
{
    [SerializeField] private GameObject licensePopup = default;
    private Button licenseButton = default;
    
    private void Start()
    {
        licenseButton = this.GetComponent<Button>();
            licenseButton.OnClickAsObservable()
                       .Subscribe(_ =>
                           OnClickSceneLicenseButton()
                       )
                       .AddTo(this);
    }

    private void OnClickSceneLicenseButton()
    {
        Instantiate(licensePopup);
    }
}

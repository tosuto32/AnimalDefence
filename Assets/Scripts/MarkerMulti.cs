using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[System.Serializable]
public class MarkerInfo
{
    public string markerName;
    public GameObject prefab;
}

public class MarkerMulti : MonoBehaviour
{
    public MarkerInfo[] markerinfos;
    ARTrackedImageManager manager;

    private void Awake()
    {
        manager = GetComponent<ARTrackedImageManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        manager.trackedImagesChanged += MarkerChanged;
    }

    private void OnDisable()
    {
        manager.trackedImagesChanged -= MarkerChanged;
    }

    void MarkerChanged(ARTrackedImagesChangedEventArgs args)
    {
        // 검출한 이미정보를 다 검사해서
        for (int i = 0; i < args.updated.Count; i++)
        {
            ARTrackedImage image = args.updated[i];
            for (int j = 0; j < markerinfos.Length; j++)
            {
                // 추적된 이미지의 이름과 내가 가진 마커정보의 이름이 같다면
                if (image.referenceImage.name == markerinfos[j].markerName)
                {
                    // 그 이미지가 추적중이라면
                    if (image.trackingState == TrackingState.Tracking)
                    {
                        // 그 위치에 해당 마커의 prefab을 보이게 하고싶다.
                        markerinfos[j].prefab.SetActive(true);
                        markerinfos[j].prefab.transform.position = image.transform.position;
                        markerinfos[j].prefab.transform.up = image.transform.up;

                        
                        GameManager.instance.SetGameState(true);
                    }
                    else
                    {
                        //markerinfos[j].prefab.SetActive(false);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}

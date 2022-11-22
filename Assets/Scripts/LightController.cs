using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] PlayerController _player;
    public Light2D _Light;
    // Start is called before the first frame update
    void Start()
    {
        _Light = GetComponent<Light2D>();
    }

    private void FixedUpdate()
    {
        if (_player._isHidden && _Light.pointLightOuterRadius > 7f)
        {
            _Light.pointLightOuterRadius -= 0.1f;
        }
        else if (!_player._isHidden && _Light.pointLightOuterRadius < 18f)
        {
            _Light.pointLightOuterRadius += 0.2f;
        }
    }



}

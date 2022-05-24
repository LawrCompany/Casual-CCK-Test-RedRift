using System;
using Code.Core;
using Code.Core.Net;
using UnityEngine;


namespace Code{
    public class Root : MonoBehaviour{
        [SerializeField]
        private ResourceSettings _settings;
        [SerializeField]
        private CardsModel _model;

        private async void Awake(){
            var loader = new Loader();
            await loader.LoadModel(_model, _settings);
        }
    }
}
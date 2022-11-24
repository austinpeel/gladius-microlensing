// import React, { useState, useEffect } from 'react';
import React from 'react';
import { Unity, useUnityContext } from 'react-unity-webgl';
import './UnityApp.css';

function UnityApp(props) {
  const { unityProvider, isLoaded, loadingProgression } = useUnityContext({
    loaderUrl: props.loaderUrl,
    dataUrl: props.dataUrl,
    frameworkUrl: props.frameworkUrl,
    codeUrl: props.codeUrl,
  });

  const loadingPercentage = Math.round(loadingProgression * 100);

  return (
    <div id='unity-app' className='unity-app'>
      {!isLoaded && (
        <div className='loading-overlay'>
          <p>Loading... ({loadingPercentage}%)</p>
        </div>
      )}
      <Unity unityProvider={unityProvider} className='unity' />
    </div>
  );
}

export default UnityApp;

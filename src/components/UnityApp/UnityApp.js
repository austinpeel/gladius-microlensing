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

  // const resize = () => {
  //   const element = document.getElementById('unity-app');
  //   const width = parseFloat(window.getComputedStyle(element).width);
  //   element.setAttribute(
  //     'style',
  //     'height: ' + (width / 4) * 3 + 'px !important'
  //   );
  // };

  // const [dimensions, setDimensions] = useState({
  //   height: window.innerHeight,
  //   width: window.innerWidth,
  // });

  // const width = parseFloat(window.getComputedStyle(element).width);
  // console.log('width = ' + window.getComputedStyle(element).width);

  // const handleResize = () => {
  //   setDimensions({
  //     width: window.innerWidth,
  //     height: window.innerHeight,
  //   });
  // };
  // useEffect(() => {
  //   window.addEventListener('resize', handleResize, false);
  // }, []);

  // const element = document.getElementById('unity-app');

  return (
    <div id='unity-app' className='unity-app'>
      {!isLoaded && (
        <div className='loading-overlay'>
          <p>Loading... ({loadingPercentage}%)</p>
        </div>
      )}
      <Unity unityProvider={unityProvider} className='unity' />
      {/* <p>
        {dimensions.width}, {dimensions.height}
      </p> */}
      {/* <p>{window.getComputedStyle(element).width}</p> */}
    </div>
  );
}

// class UnityApp extends React.Component {
//   constructor(props) {
//     super(props);

//     this.state = {
//       id: 'unity-app',
//       progress: 0,
//       isLoading: !props.clickToLoad,
//       startLoading: !props.clickToLoad,
//     };

//     this.unityProvider = new UnityContext({
//       loaderUrl: props.loaderUrl,
//       dataUrl: props.dataUrl,
//       frameworkUrl: props.frameworkUrl,
//       codeUrl: props.codeUrl,
//     });

//     // this.unityContent = new UnityContent(props.json, props.unityLoader);
//     this.unityProvider.on('progress', (progress) => {
//       this.setState({ progress: progress });
//     });
//     this.unityProvider.on('loaded', () => {
//       this.setState({ isLoading: false });
//     });
//   }

//   componentDidMount() {
//     window.addEventListener('resize', this.resize);
//     this.resize();
//   }

//   componentWillUnmount() {
//     window.removeEventListener('resize', this.resize);
//   }

//   resize = () => {
//     const unityElement = document.getElementById(this.state.id);
//     const width = parseFloat(window.getComputedStyle(unityElement).width);

//     unityElement.setAttribute(
//       'style',
//       'height: ' + (width / 4) * 3 + 'px !important'
//     );
//   };

//   render() {
//     return (
//       <div className='unity-app'>
//         <p className='description'>{this.props.description}</p>
//         {this.state.isLoading ? (
//           <p id='loading'>
//             Loading... {(100 * this.state.progress).toFixed()} %
//           </p>
//         ) : (
//           <p id='not-loading'></p>
//         )}
//         <div id={this.state.id} className='unity-player'>
//           {this.state.startLoading ? (
//             <Unity unityProvider={this.unityProvider} />
//           ) : (
//             <img
//               src={this.props.image}
//               onClick={() => {
//                 this.setState({
//                   startLoading: true,
//                   isLoading: true,
//                 });
//               }}
//             ></img>
//           )}
//         </div>
//       </div>
//     );
//   }
// }

export default UnityApp;

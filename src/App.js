import './App.css';
import Header from './components/Header/Header';
import Footer from './components/Footer/Footer';
import UnityApp from './components/UnityApp/UnityApp';

const appData = {
  loaderUrl: 'UnityApp/Build/UnityApp.loader.js',
  dataUrl: 'UnityApp/Build/UnityApp.data',
  frameworkUrl: 'UnityApp/Build/UnityApp.framework.js',
  codeUrl: 'UnityApp/Build/UnityApp.wasm',
  description: 'Microlensing...',
};

function App() {
  return (
    <div className='container'>
      <Header />
      <UnityApp {...appData} />
      <Footer />
    </div>
  );
}

export default App;

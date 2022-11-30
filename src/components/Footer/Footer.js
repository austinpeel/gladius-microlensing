import './Footer.css';
import Flag from '../../images/flag.jpg';

export default function Footer() {
  return (
    <>
      <div className='footer'>
        <p>
          Made by <a href='https://github.com/austinpeel'>Austin Peel</a> &
          Giorgos Vernardos © 2022
        </p>
      </div>
      <div className='acknowledgment'>
        <img src={Flag} alt='European Flag' />
        <p>
          This project has received funding from the European Union's Horizon
          2020 research and innovation programme under the Marie
          Skłodowska-Curie grant agreement No 897124.
        </p>
      </div>
    </>
  );
}

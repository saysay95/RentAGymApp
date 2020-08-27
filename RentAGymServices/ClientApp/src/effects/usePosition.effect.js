import {useState, useEffect} from 'react';

export const usePosition = () => {

  const [position, setPosition] = useState({});
  const [error, setError] = useState(null);


  var options = {
    enableHighAccuracy: true,
    timeout: 5000,
    maximumAge: 0
  };

  const success = (pos) => {
    setPosition({
      latitude: pos.coord.latitude,
      longitude: pos.coord.longitude,
    });
    // console.log(coords);

    var crd = pos.coords;
  
    console.log('Your current position is:');
    console.log(`Latitude : ${crd.latitude}`);
    console.log(`Longitude: ${crd.longitude}`);
    console.log(`More or less ${crd.accuracy} meters.`);
  };
  const Onerror = (err) => {
    // setError(error.message);
    // console.log(error);

    console.warn(`ERROR(${err.code}): ${err.message}`);
  };

  useEffect(() => {
    const geo = navigator.geolocation;
    if (!geo) {
      setError('Geolocation is not supported');
      return;
    }

    // let watcher = geo.watchPosition(onChange, onError);
    geo.getCurrentPosition(success, Onerror, options);
    //return () => geo.clearWatch(watcher);
  }, []);
  return {...position, error};
}



// var options = {
//     enableHighAccuracy: true,
//     timeout: 5000,
//     maximumAge: 0
//   };
  
//   function success(pos) {
//     var crd = pos.coords;
  
//     console.log('Your current position is:');
//     console.log(`Latitude : ${crd.latitude}`);
//     console.log(`Longitude: ${crd.longitude}`);
//     console.log(`More or less ${crd.accuracy} meters.`);
//   }
  
//   function error(err) {
//     console.warn(`ERROR(${err.code}): ${err.message}`);
//   }
  
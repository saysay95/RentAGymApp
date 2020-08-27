// import React, {useState , useEffect} from 'react'
// import {Dropdown} from '../../components/dropdown/dropdown.js'
// import {HomePageContainer} from './Homepage.styles.js'
// import {usePosition} from '../../effects/usePosition.effect.js'
// import {TransformSpaceTypeJSON, TransformCityJSON, onCityChange} from '../../util/api-processing.js'

// const Homepage = () => {

//     const [gymType, setGymType] = useState([]);
//     useEffect(() => {
//         fetch("/api/spacetypes")
//         .then(res => res.json())
//         .then((result) => {TransformSpaceTypeJSON(result); setGymType(result)})
//     }, []);

//     const [city, setCity] = useState([]);
//     useEffect(() => {
//         fetch("/api/cities")
//         .then(res => res.json())
//         .then((result) => {TransformCityJSON(result); setCity(result);})
//     }, []);

//     let onCityChange = event =>  {console.log(event);
//         fetch("/api/cities/name="+event.target.value)
//         .then(res => res.json())
//         .then((result) => {TransformCityJSON(result); setCity(result);})
//       };

// //     const {latitude, longitude, error} = usePosition();
// // console.log(longitude);
// // console.log(latitude);
// // console.log(error);
// // if(!error)
// // console.log(navigator.geolocation.getCurrentPosition);

// //     const distance = [
// //         { label: '5 miles', value: '5' },
// //         { label: '10 miles', value: '10' },
// //         { label: '25 miles', value: '25' }
// //       ]

//     return (
//             // <HomePageContainer>
//             <div>
//                 <Dropdown options={gymType} isMulti='true'></Dropdown>
//                 <Dropdown options={city} onChange={onCityChange}></Dropdown>
//             </div>
//             // </HomePageContainer>
//     );
// }


// export default Homepage;
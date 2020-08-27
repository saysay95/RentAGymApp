import React, {useState , useEffect} from 'react'
// import {Dropdown} from '../../components/dropdown/dropdown.js'
// import {HomePageContainer} from './Homepage.styles.js'
// import {usePosition} from '../../effects/usePosition.effect.js'
import {GetUrlForSearchResults} from '../../util/api-processing.js'
import {SpaceResultsItem} from '../../components/SpaceResultsItem/SpaceResultsItem.js'

const SearchResults = (props) => {

    console.log('Search results props: ');
    console.log(GetUrlForSearchResults(props));

    const [SearchResults, setSearchResults] = useState([]);
    useEffect(() => {
        fetch("/api/SearchResults?" + GetUrlForSearchResults(props))
        .then(res => res.json())
        .then((result) => {setSearchResults(result);
            console.log(SearchResults);})
    }, []);


    console.log(SearchResults);


    return (
            <div className="collections-overview">
            {
                SearchResults.map(({ id, name }) => (
                    <SpaceResultsItem key={id} name={name} />
                ))
            }
        </div>
    );
}


export default SearchResults;
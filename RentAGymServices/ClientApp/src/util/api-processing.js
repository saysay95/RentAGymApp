// Transform the JSON result to the type of input expected by react-select component
export const TransformSpaceTypeJSON = (res) => {
    res = res.map(item => {
        item.value = item.type;
        delete item.type;
        delete item.spaceWithTypes;
        return item;
    });
};

// Transform the JSON result to the type of input expected by react-select component
export const TransformCityJSON = (res) => {
    res = res.map(item => {
        item.label = item.name;
        delete item.name;
        item.value = item.cityId;
        delete item.cityId;
        return item;
    });
};

export const GetUrlForSearchResults = (props) => {
    var url = '';
    if(props.location.state.selectedCity &&
        props.location.state.selectedgymTypes)
        {
            props.location.state.selectedgymTypes.forEach(element => {
                url += 'spacetypes=' + element.value + '&'
            });
            url += 'city=' + props.location.state.selectedCity.label;
        }
    return url;
};

export const onCityChange = event =>  {console.log(event);
    fetch("/api/cities/name="+event.target.value)
    .then(res => res.json())
    .then((result) => {TransformCityJSON(result); console.log(result);})
  };
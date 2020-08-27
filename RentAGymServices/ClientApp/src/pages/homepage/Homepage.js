import React, { Component } from 'react'
// import { Dropdown } from '../../components/dropdown/dropdown.js'
import CustomButton, {button} from '../../components/button/CustomButton.js'
import { TransformSpaceTypeJSON, TransformCityJSON } from '../../util/api-processing.js'
import Select from 'react-select';

class Homepage extends Component {

    constructor() {
        super();
        this.state = {
            gymTypes: [],
            city: [{label: '', value:''}],
            selectedgymTypes: [],
            selectedCity: []
        };

        // this.onCityChange = this.onCityChange.bind(this);
    }

    componentDidMount() {
        //   fetch("api/cities/?name=a")
        //     .then(res => res.json())
        //     .then((result) => {
        //         TransformCityJSON(result);
        //         this.setState({ city: result });
        //     })

            fetch("api/spacetypes")
          .then(res => res.json())
          .then((result) => {
              TransformSpaceTypeJSON(result);
              this.setState({
                gymTypes: result
              });
            }
          )
      }

    onCityChange = event => {
        if (event)
            fetch("api/cities/?name=" + event)
                .then(res => res.json())
                .then((result) => {
                    TransformCityJSON(result);
                    this.setState({ city: result });
                });
    };

    onSpaceTypeInputChange = event => {
        this.setState({
            selectedgymTypes: event
          });
    };

    onCityInputChange = event => {
        this.setState({
            selectedCity: event
          });
    };


    render() {
        // console.log("This is selectedgymTypes: " );
        // console.log(this.state.selectedgymTypes);
        return (
            // <HomePageContainer>
            <div>
                <Select options={this.state.gymTypes} isMulti='true' 
                        onChange={this.onSpaceTypeInputChange}/>
                {/* <Dropdown options={this.state.city} onchange={this.onCityChange}></Dropdown> */}
                <Select
        onInputChange={this.onCityChange}
        onChange={this.onCityInputChange}
        options={this.state.city}
      />
      <CustomButton onClick = {() => {
                                        this.props.history.push('/results', this.state);
                                        // console.log(this.props);
                                    }}>SEARCH</CustomButton>
            </div>
            // </HomePageContainer>
        );
    }
}


export default Homepage;
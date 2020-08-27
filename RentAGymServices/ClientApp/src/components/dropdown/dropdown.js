import React from 'react'
import Select from 'react-select'

export const Dropdown = (props) => (
  <Select  options={props.options} isMulti={props.isMulti} className="basic-multi-select"
  classNamePrefix="select" onChange={props.onCityChange} />
)

// let onCityChange = event =>  {console.log(event);};
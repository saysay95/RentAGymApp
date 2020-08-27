import React from 'react'
// import './SpaceResultsItem.styles.scss'

export const SpaceResultsItem = ({//space: {//imageUrl, price, 
    name}//}
    ) => (
    <div className='cart-item'>
        {/* <img src={imageUrl} alt='spaceItem' /> */}
        <div className='item-details'>
            <div ></div>
            <span className='name'>{name}</span>
            {/* <span className='price'>${price}</span> */}
        </div>
    </div>   
)

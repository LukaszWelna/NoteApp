import React from 'react';
import './Missing.css';
import {
    MDBContainer,
    MDBRow,
    MDBCol
}
    from 'mdb-react-ui-kit'

const Missing = () => {
    return (
        <MDBContainer fluid>
            <MDBRow className='justify-content-center mx-1'>
                <MDBCol sm='8' md='6' lg='5' xl='4' xxl='3' className='missing-div mt-5 rounded-4 center-text'>
                    <h1 className="not-found pt-3 pb-2">404 - Not Found</h1>
                </MDBCol>
            </MDBRow>
        </MDBContainer>
    )
}

export default Missing;
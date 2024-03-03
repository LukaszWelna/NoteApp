import React from 'react';
import './Registered.css';
import { Link } from 'react-router-dom';
import {
    MDBContainer,
    MDBRow,
    MDBCol
}
    from 'mdb-react-ui-kit'

const Registered = () => {
    return (
        <MDBContainer fluid>
            <MDBRow className='justify-content-center mx-1'>
                <MDBCol sm='8' md='6' lg='5' xl='4' xxl='3' className='registered-div mt-5 rounded-4 center-text'>
                    <h1 className="register-success pt-3">Success!</h1>
                    <p>You can now <Link to='/'>log in</Link>.</p>
                </MDBCol>
            </MDBRow>
        </MDBContainer>
    )
}

export default Registered;
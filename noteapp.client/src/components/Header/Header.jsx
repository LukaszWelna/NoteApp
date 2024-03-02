import React from 'react'
import './Header.css'

import {
    MDBNavbar,
    MDBContainer,
    MDBNavbarBrand,
    MDBBtn
} from 'mdb-react-ui-kit';

export default function Header() {
    return (
        <>
            <MDBNavbar light className="custom-navbar py-3">
                <MDBContainer fluid>
                    <MDBNavbarBrand tag="span" className='mb-0 h1 ms-3 brand-name'>Note app</MDBNavbarBrand>
                    <MDBBtn color="dark" className='me-3' type='button'>
                        Logout
                    </MDBBtn>
                </MDBContainer>
            </MDBNavbar>
        </>
    );
}
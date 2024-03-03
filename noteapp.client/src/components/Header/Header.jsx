import React from 'react';
import './Header.css';
import {
    MDBNavbar,
    MDBContainer,
    MDBNavbarBrand,
    MDBBtn
} from 'mdb-react-ui-kit';
import useAuth from '../../hooks/UseAuth';
import { Link } from 'react-router-dom';

const Header = () => {

    const { auth, setAuth } = useAuth();

    // remove token
    const handleLogout = () => {
        setAuth({});
    }

    return (
        <>
            <MDBNavbar light className="custom-navbar py-3">
                <MDBContainer fluid>
                    <MDBNavbarBrand tag="span" className='mb-0 h1 ms-3 brand-name'>Note app</MDBNavbarBrand>
                    {auth?.accessToken ? 
                        <Link to='/'>
                            <MDBBtn color="dark" className='me-3' type='button' onClick={handleLogout}>
                                Logout
                            </MDBBtn>
                        </Link>
                        : null
                    }
                </MDBContainer>
            </MDBNavbar>
        </>
    );
}

export default Header;
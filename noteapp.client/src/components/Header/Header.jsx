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
import LogoutIcon from '@mui/icons-material/Logout';

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
                            <button className='button-logout px-3' onClick={handleLogout}>
                                <LogoutIcon />
                            </button>
                        </Link>
                        : null
                    }
                </MDBContainer>
            </MDBNavbar>
        </>
    );
}

export default Header;
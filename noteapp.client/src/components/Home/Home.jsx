import React, { useState, useEffect } from 'react';
import './Home.css';
import axios from '../../api/axios';
import useAuth from '../../hooks/UseAuth'
import {  useNavigate } from 'react-router-dom';

import {
    MDBBtn,
    MDBContainer,
    MDBRow,
    MDBCol,
    MDBInput,
    MDBModal,
    MDBModalDialog,
    MDBModalContent,
    MDBModalHeader,
    MDBModalBody,
    MDBModalFooter
}
    from 'mdb-react-ui-kit'
    
const Home = () => {

    const { setAuth } = useAuth();
    const navigate = useNavigate();
    
    // login useState hooks
    const [loginEmail, setLoginEmail] = useState('');
    const [loginPassword, setLoginPassword] = useState('');

    // register useState hooks
    const [registerFirstName, setRegisterFirstName] = useState('');
    const [registerLastName, setRegisterLastName] = useState('');
    const [registerEmail, setRegisterEmail] = useState('');
    const [registerPassword, setRegisterPassword] = useState('');
    const [registerConfirmPassword, setRegisterConfirmPassword] = useState('');
    const [registerValidPassword, setRegisterValidPassword] = useState(false);
    const [registerMatchPassword, setRegisterMatchPassword] = useState(false);
    const [registerValidEmail, setRegisterValidEmail] = useState(false);

    // Messages useState hooks
    const [registerErrorMessage, setRegisterErrorMessage] = useState([]);
    const [loginErrorMessage, setLoginErrorMessage] = useState();

    // useEffect hooks
    // Check if password is valid
    useEffect(() => {
        const isValid = (registerPassword.length >= 8) ? true : false;
        setRegisterValidPassword(isValid);
        const isMatch = registerPassword === registerConfirmPassword;
        setRegisterMatchPassword(isMatch);
    }, [registerPassword, registerConfirmPassword]);

    const EMAIL_REGEX = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;

    // Check if email is valid
    useEffect(() => {
        const result = EMAIL_REGEX.test(registerEmail);
        setRegisterValidEmail(result);
    }, [registerEmail]);

    // Clear errors
    useEffect(() => {
        setRegisterErrorMessage([]);
    }, [registerFirstName, registerLastName, registerEmail, registerPassword, registerConfirmPassword]);

    useEffect(() => {
        setLoginErrorMessage('');
    }, [loginEmail, loginPassword]);

    // handlers
    async function handleLoginSubmit(event) {
        event.preventDefault();
        try {
            const response = await axios.post('/api/accounts/login',
                JSON.stringify({ Email: loginEmail, Password: loginPassword }),
                {
                    headers: { 'Content-Type': 'application/json' }
                });
            const accessToken = response?.data;
            setAuth({ accessToken });
            setLoginEmail('');
            setLoginPassword('');
            navigate('/dashboard');
        } catch (e) {
            if (!e?.response) {
                setLoginErrorMessage(['No Server Response']);
            } else if (e.response.status === 400) {
                setLoginErrorMessage('Invalid email address or password');
            } else {
                setLoginErrorMessage(['Login failed']);
            }
        }
    }

    async function handleRegisterSubmit(event) {
        event.preventDefault();
        try {
            const response = await axios.post('/api/accounts/register',
                JSON.stringify({
                    FirstName: registerFirstName, LastName: registerLastName,
                    Email: registerEmail, Password: registerPassword, ConfirmPassword: registerConfirmPassword
                }),
                {
                    headers: { 'Content-Type': 'application/json' }
                });
            setRegisterFirstName('');
            setRegisterLastName('');
            setRegisterEmail('');
            setRegisterPassword('');
            setRegisterConfirmPassword('');
            navigate('/registered');
        } catch (e) {
            if (!e?.response) {
                setRegisterErrorMessage(['No Server Response']);
            } else if (e.response.status === 400) {
                const errorsData = e.response.data.errors;
                const errors = [];
                Object.keys(errorsData).forEach((key) => {
                    errors.push(errorsData[key][0])
                }) 
                setRegisterErrorMessage(errors);
            } else {
                setRegisterErrorMessage(['Registration failed']);
            }
        }
    }

    // Modal useState hook
    const [signUpModal, setSignUpModal] = useState(false);
    const toggleSignUpModal = () => setSignUpModal(!signUpModal);

    
    return (
        <MDBContainer fluid>
            {/* Login */}
            <MDBRow className='justify-content-center mx-1'>
                <MDBCol sm='8' md='6' lg='5' xl='4' xxl='3' className='login-div mt-5 rounded-4'>
                    <div className='d-flex flex-column justify-content-center h-custom-2 w-100 pt-4 pb-1 px-3 center-text'>
                        <h3 className="fw-normal mb-3 pb-3" style={{ letterSpacing: '1px' }}>Sign in</h3>
                        <div className={(loginErrorMessage) ? 'login-errors' : 'not-display'}>
                            {loginErrorMessage}
                        </div>

                        <form onSubmit={handleLoginSubmit}>
                            <MDBInput wrapperClass='mb-4 w-100' label='Email address' id='formLoginEmail'
                                type='email' size="lg" onChange={(e) => setLoginEmail(e.target.value)} value={loginEmail} required />
                            <MDBInput wrapperClass='mb-4 w-100' label='Password' id='formLoginPassword'
                                type='password' size="lg" onChange={(e) => setLoginPassword(e.target.value)} value={loginPassword} required />
                            <MDBBtn className="mb-4 px-5 w-100 login-button" type='submit' onClick={(e) => e.target.blur()} size='lg'>Login</MDBBtn>
                            <p className=''>Don&#39;t have an account? <a href="#!" className="link-info" onClick={toggleSignUpModal}>Register here</a></p>
                        </form>
                    </div>
                </MDBCol>
            </MDBRow>

            {/* Register */}
            <MDBModal open={signUpModal} setOpen={setSignUpModal} tabIndex='-1'>
                <MDBModalDialog>
                    <MDBModalContent>
                        <form onSubmit={handleRegisterSubmit}>
                            <MDBModalHeader>
                                <MDBBtn className='btn-close' color='none' type='button' onClick={toggleSignUpModal}></MDBBtn>
                            </MDBModalHeader>
                            <MDBModalBody>
                                <MDBRow className='justify-content-center mx-0'>
                                    <MDBCol className='login-div rounded-4'>
                                        <div className='d-flex flex-column justify-content-center h-custom-2 w-100 pt-4 pb-1 px-3 center-text'>
                                            <h3 className='fw-normal mb-3 pb-3' style={{ letterSpacing: '1px' }}>Sign up</h3>
                                            <div className={(registerErrorMessage.length > 0) ? 'register-errors' : 'not-display'}>
                                                <ul>
                                                    {registerErrorMessage.map((error, index) => 
                                                        <li key={index}>{error}</li>
                                                    )}
                                                </ul>
                                            </div>
                                            <MDBInput wrapperClass='mb-4 w-100' label='First name' id='formSignupFirstName' type='text' size="lg"
                                                onChange={(e) => setRegisterFirstName(e.target.value)} value={registerFirstName} required />
                                            <MDBInput wrapperClass='mb-4 w-100' label='Last name' id='formSignupLastName' type='text' size="lg"
                                                onChange={(e) => setRegisterLastName(e.target.value)} value={registerLastName} required />
                                            <MDBInput wrapperClass='mb-4 w-100' label='Email address' id='formSignupEmail' type='email' size="lg"
                                                onChange={(e) => setRegisterEmail(e.target.value)} value={registerEmail} required />
                                            <p className={(!registerValidEmail && registerEmail) ? 'validation-errors' : 'not-display'}>Email address is not valid.</p>
                                            <MDBInput wrapperClass='mb-4 w-100' label='Password' id='formSignupPassword' type='password' size="lg"
                                                onChange={(e) => setRegisterPassword(e.target.value)} value={registerPassword} required />
                                            <p className={(!registerValidPassword && registerPassword) ? 'validation-errors' : 'not-display'}>The Password must contain at least 8 characters.</p>
                                            <MDBInput wrapperClass='mb-4 w-100' label='Confirm password' id='formSignupConfirmPassword' type='password' size="lg"
                                                onChange={(e) => setRegisterConfirmPassword(e.target.value)} value={registerConfirmPassword} required />
                                            <p className={(!registerMatchPassword && registerConfirmPassword) ? 'validation-errors' : 'not-display'}>The Confirm password must match the Password.</p>
                                        </div>
                                    </MDBCol>
                                </MDBRow>
                            </MDBModalBody>

                            <MDBModalFooter>
                                <MDBBtn className='signup-button active' type='submit' onClick={(e) => e.target.blur()}
                                    disabled={!registerValidEmail || !registerValidPassword || !registerMatchPassword ||
                                        !registerFirstName || !registerLastName ? true : false}>
                                    Create account
                                </MDBBtn>
                                </MDBModalFooter>
                        </form>
                    </MDBModalContent>
                </MDBModalDialog>
            </MDBModal>
        </MDBContainer>
    );
}

export default Home;
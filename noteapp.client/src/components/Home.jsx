import React, { useState } from 'react'

import {
    MDBBtn,
    MDBContainer,
    MDBRow,
    MDBCol,
    MDBIcon,
    MDBInput,
    MDBModal,
    MDBModalDialog,
    MDBModalContent,
    MDBModalHeader,
    MDBModalTitle,
    MDBModalBody,
    MDBModalFooter,

}
    from 'mdb-react-ui-kit'
    

export default function Login() {

    const [signUpModal, setSignUpModal] = useState(false);

    const toggleSignUpModal = () => setSignUpModal(!signUpModal);

    return (
        <MDBContainer fluid>
            <MDBRow className='justify-content-center mx-1'>

                <MDBCol sm='8' md='6' lg='5' xl='4' xxl='3' className='login-div mt-5 rounded-4'>

                    <div className='d-flex flex-column justify-content-center h-custom-2 w-100 pt-4 pb-1 px-3 center-text'>

                        <h3 className="fw-normal mb-3 pb-3" style={{ letterSpacing: '1px' }}>Log in</h3>

                        <MDBInput wrapperClass='mb-4 w-100' label='Email address' id='formLoginEmail' type='email' size="lg" />
                        <MDBInput wrapperClass='mb-4 w-100' label='Password' id='formLoginPassword' type='password' size="lg" />

                        <MDBBtn className="mb-4 px-5 w-100 login-button" size='lg'>Login</MDBBtn>
                        <p className=''>Don&#39;t have an account? <a href="#!" className="link-info" onClick={toggleSignUpModal}>Register here</a></p>
                    </div>

                </MDBCol>

            </MDBRow>

            <MDBModal open={signUpModal} setOpen={setSignUpModal} tabIndex='-1'>
                <MDBModalDialog>
                    <MDBModalContent>
                        <MDBModalHeader>
                            <MDBBtn className='btn-close' color='none' onClick={toggleSignUpModal}></MDBBtn>
                        </MDBModalHeader>
                        <MDBModalBody>
                            <MDBRow className='justify-content-center mx-0'>

                                <MDBCol className='login-div rounded-4'>

                                    <div className='d-flex flex-column justify-content-center h-custom-2 w-100 pt-4 pb-1 px-3 center-text'>

                                        <h3 className="fw-normal mb-3 pb-3" style={{ letterSpacing: '1px' }}>Sign up</h3>

                                        <MDBInput wrapperClass='mb-4 w-100' label='First name' id='formSignupFirstName' type='text' size="lg" />
                                        <MDBInput wrapperClass='mb-4 w-100' label='Last name' id='formSignupLastName' type='text' size="lg" />
                                        <MDBInput wrapperClass='mb-4 w-100' label='Email address' id='formSignupEmail' type='email' size="lg" />
                                        <MDBInput wrapperClass='mb-4 w-100' label='Password' id='formSignupPassword' type='password' size="lg" />
                                        <MDBInput wrapperClass='mb-4 w-100' label='Confirm password' id='formSignupConfirmPassword' type='password' size="lg" />
                                        
                                    </div>

                                </MDBCol>

                            </MDBRow>
                        </MDBModalBody>

                        <MDBModalFooter>
                            <MDBBtn className='signup-button'>Create account</MDBBtn>
                        </MDBModalFooter>
                    </MDBModalContent>
                </MDBModalDialog>
            </MDBModal>

        </MDBContainer>


    );
}
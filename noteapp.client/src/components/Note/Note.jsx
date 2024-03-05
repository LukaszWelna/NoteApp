import React from "react";
import './Note.css';
import PropTypes from 'prop-types';
import {
    MDBCard,
    MDBCardBody,
    MDBCardTitle,
    MDBCardText,
    MDBBtn,
    MDBCol
} from 'mdb-react-ui-kit';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';


function Note(props) {

    /*function handleDelete() {
        props.onDelete(props.id);
    }
    */

    return (
        <MDBCol className='px-2'>
            <MDBCard className='h-100'>
                <MDBCardBody className='p-3'>
                <form className='form-note'>
                    <MDBCardTitle>
                            {props.title }
                        </MDBCardTitle>
                        <MDBCardText className='mb-4'>
                            {props.content }
                        </MDBCardText>
                        <MDBCol className='d-flex justify-content-end note-icons'>
                            <button className='button-edit me-2 p-0'><EditIcon /></button>
                            <button className='button-delete p-0'><DeleteIcon /></button>
                        </MDBCol>
                 </form>        
                </MDBCardBody>
            </MDBCard>
        </MDBCol> 
    );
}

Note.propTypes = {
    title: PropTypes.string.isRequired,
    content: PropTypes.string.isRequired
};

export default Note;
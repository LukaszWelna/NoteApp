import React from "react";
import './Note.css';
import PropTypes from 'prop-types';
import {
    MDBCard,
    MDBCardBody,
    MDBCardTitle,
    MDBCol,
    MDBCardText
} from 'mdb-react-ui-kit';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';


function Note(props) {

    const handleDelete = () => {
        props.handleDelete(props.id);
    }

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
                            <button className='button-delete p-0' type='button' onClick={handleDelete}><DeleteIcon /></button>
                        </MDBCol>
                 </form>        
                </MDBCardBody>
            </MDBCard>
        </MDBCol> 
    );
}

Note.propTypes = {
    id: PropTypes.number.isRequired,
    title: PropTypes.string.isRequired,
    content: PropTypes.string.isRequired,
    handleDelete: PropTypes.func.isRequired
};

export default Note;
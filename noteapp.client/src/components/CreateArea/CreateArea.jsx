import React, { useState } from "react";
import './CreateArea.css';
import PropTypes from 'prop-types';
import {
    MDBCard,
    MDBCardBody,
    MDBCardTitle,
    MDBCardText,
    MDBCol,
} from 'mdb-react-ui-kit';
import AddIcon from '@mui/icons-material/Add';
import Fab from '@mui/material/Fab';
import Zoom from '@mui/material/Zoom';
import { useClickAway } from '@uidotdev/usehooks';

function CreateArea(props) {

    // useState hooks
    const [title, setTitle] = useState('');
    const [content, setContent] = useState('');
    const [expand, setExpand] = useState(false);

    const ref = useClickAway(() => {
        setExpand(false);
    });

    // Handlers
    const handleAddNote = (event) => {
        event.preventDefault();
        props.handleAddNote(title, content);
        setTitle('');
        setContent('');
    }

    return (
        <MDBCol className='px-2 col-create-area' sm='8' md='6' xl='4' ref={ref}>
            <MDBCard>
                <MDBCardBody className='p-3'>
                    <form className='create-note' onSubmit={handleAddNote}>
                        <MDBCardTitle>
                            <input placeholder='Title' id='form-create-title'
                                onChange={(e) => setTitle(e.target.value)} value={title} type={expand ? '' : 'hidden'}
                                maxLength='50' autoComplete='off' required />
                        </MDBCardTitle>
                        <MDBCardText>
                            <textarea placeholder='Add a note...' id='form-create-content' rows={expand ? 3 : 1}
                                onChange={(e) => setContent(e.target.value)} value={content} onClick={() => setExpand(true)}
                                maxLength='400' />
                        </MDBCardText>
                        <MDBCol className='d-flex justify-content-end'>
                            <Zoom in={expand ? true : false}>
                                <Fab type='submit'><AddIcon /></Fab>
                            </Zoom>      
                        </MDBCol>
                    </form>
                </MDBCardBody>
            </MDBCard>
        </MDBCol>
    );
}

CreateArea.propTypes = {
    handleAddNote: PropTypes.func.isRequired
};

export default CreateArea;
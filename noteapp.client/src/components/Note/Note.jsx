import React, { useState } from "react";
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
import { useClickAway } from '@uidotdev/usehooks';
import DoneIcon from '@mui/icons-material/Done';

function Note(props) {

    // useState hooks
    const [title, setTitle] = useState(props.title);
    const [content, setContent] = useState(props.content);
    const [modify, setModify] = useState(false);

    const ref = useClickAway(() => {
        setModify(false);
        setTitle(props.title);
        setContent(props.content);
    });

    // Handlers
    const handleModify = (event) => {
        event.preventDefault();
        setModify(true);
    }

    const handleDelete = (event) => {
        event.preventDefault();
        props.handleDelete(props.id);
    }

    const handleEdit = (event) => {
        event.preventDefault();
        setModify(false);
        props.handleEdit(props.id, title, content)
    }

    // CSS properties
    const modifyStyle = {
        backgroundColor: '#FFFDE7'
    }

    const normalStyle = {
        backgroundColor: 'white'
    }

    return (
        <MDBCol className='px-2'>
            <MDBCard className='h-100' style={modify ? modifyStyle : normalStyle}>
                <MDBCardBody className='p-3'>
                    <form className='form-note' ref={ref} onSubmit={handleEdit}>
                    <MDBCardTitle>
                            <input placeholder='Title' id='form-modify-title' 
                                onChange={(e) => setTitle(e.target.value)} value={title}
                                maxLength='50' autoComplete='off' required disabled={modify ? false : true}
                                style={modify ? modifyStyle : normalStyle} />
                        </MDBCardTitle>
                        <MDBCardText className='mb-4'>
                            <textarea placeholder='Content' id='form-modify-content' rows='3'
                                onChange={(e) => setContent(e.target.value)} value={content}
                                maxLength='400' disabled={modify ? false : true}
                                style={modify ? modifyStyle : normalStyle} />
                        </MDBCardText>
                        <MDBCol className='d-flex justify-content-end note-icons'>
                            {!modify ?
                                <div>
                                    <button className='button-edit me-2 p-0' type='button' onClick={handleModify}>
                                        <EditIcon />
                                    </button>

                                    <button className='button-delete p-0' type='button' onClick={handleDelete}>
                                        <DeleteIcon />
                                    </button>
                                </div>
                                :
                                <button className='button-done p-0'>
                                    <DoneIcon fontSize='large' />
                                </button> }
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
    handleDelete: PropTypes.func.isRequired,
    handleEdit: PropTypes.func.isRequired
};

export default Note;
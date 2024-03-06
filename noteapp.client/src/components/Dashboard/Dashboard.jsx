import React, { useState, useEffect } from 'react';
import './Dashboard.css';
import axios from '../../api/axios';
import useAuth from '../../hooks/UseAuth';
import Note from '../Note/Note';
import CreateArea from '../CreateArea/CreateArea';
import {
    MDBRow,
    MDBCol
} from 'mdb-react-ui-kit';

const Dashboard = () => {

    const { auth } = useAuth();

    // useState hooks
    const [notes, setNotes] = useState([]);
    const [error, setError] = useState('');
    const [trigger, setTrigger] = useState(false);

    // Get all notes from db
    useEffect(() => {
        let isMounted = true;

        const getNotes = async () => {
            try {
                const response = await axios.get('api/notes', {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${auth.accessToken}`
                    }
                });
                if (isMounted) {
                    setNotes(response?.data);
                    setError('');
                }
                
            } catch (e) {
                if (isMounted) {
                    if (!e?.response) {
                        setError(['No server response.']);
                    } else if (e.response.status === 401) {
                        setError('Unathorized to get data from the database.')
                    } else {
                        setError(['Failed to fetch data from the database.']);
                    }
                }
            }
        }

        getNotes();

        return () => {
            isMounted = false;
        }

    }, [trigger]);

    // Add note to db 
    const handleAddNote = (title, content) => {
        let isMounted = true;

        const addNote = async () => {
            try {
                await axios.post('/api/notes',
                    JSON.stringify({ Title: title, Content: content }),
                    {
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${auth.accessToken}`
                        }
                    });

                if (isMounted) {
                    setTrigger(!trigger);
                }

            } catch (e) {
                if (isMounted) {
                    if (!e?.response) {
                        setError(['No server response.']);
                    } else if (e.response.status === 400) {
                        setError('Bad request.');
                    } else if (e.response.status === 401) {
                        setError('Unauthorized to add data to the database.')
                    } else {
                        setError(['Failed to save data in the database.']);
                    }
                } 
            }
        }

        addNote();

        return () => {
            isMounted = false;
        }
    }

    // Delete note by id 
    const handleDelete = (id) => {
        let isMounted = true;

        const deleteNote = async () => {

            try {
                await axios.delete(`/api/notes/${id}`, {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${auth.accessToken}`
                    }
                });

                if (isMounted) {
                    setTrigger(!trigger);
                }

            } catch (e) {
                if (isMounted) {
                    if (!e?.response) {
                        setError(['No server response.']);
                    } else if (e.response.status === 401) {
                        setError('Unathorized to delete data from the database.')
                    } else if (e.response.status === 403) {
                        setError('Access forbidden.')
                    } else if (e.response.status === 404) {
                        setError('Note not found in the database.')
                    } else {
                        setError(['Failed to delete data from the database.']);
                    }
                }
            }
        }

        deleteNote();

        return () => {
            isMounted = false;
        }
    }

    // Edit note by id
    const handleEdit = (id, title, content) => {
        let isMounted = true;

        const editNote = async () => {
            try {
                await axios.put(`/api/notes/${id}`,
                    JSON.stringify({ Title: title, Content: content }),
                    {
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${auth.accessToken}`
                        }
                    });

                if (isMounted) {
                    setTrigger(!trigger);
                }

            } catch (e) {
                if (isMounted) {
                    if (!e?.response) {
                        setError(['No server response.']);
                    } else if (e.response.status === 400) {
                        setError('Bad request.');
                    } else if (e.response.status === 401) {
                        setError('Unauthorized to edit data in the database.')
                    } else {
                        setError(['Failed to edit data in the database.']);
                    }
                }
            }
        }

        editNote();

        return () => {
            isMounted = false;
        }
    }

    return (
        <div>
            {error ?
                <MDBRow className='justify-content-center m-3'>
                    <MDBCol className='div-error p-3 rounded-4' sm='8' lg='4'>
                        {error}
                    </MDBCol>
                </MDBRow>
                :
                <div>
                    <MDBRow className='mx-2 mb-5 mt-3 row-cols-1 g-4 justify-content-center'>
                        <CreateArea handleAddNote={handleAddNote} />
                    </MDBRow>
                    <MDBRow className='mx-2 row-cols-1 row-cols-sm-2 row-cols-lg-3 row-cols-xl-4 g-4'>
                        {notes.map((note) =>
                            <Note key={note.id} id={note.id} title={note.title} content={note.content}
                                handleDelete={handleDelete} handleEdit={handleEdit} />
                        )}
                     </MDBRow>
                </div>
            }
        </div>
    );
}

export default Dashboard;
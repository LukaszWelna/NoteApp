import React from 'react'
import './Footer.css'

const Footer = () => {

    const currentYear = new Date().getFullYear();

    return (
        <div className="container">
            <footer className="py-4">
                <p className="text-center m-0">Copyright &#169; {currentYear}</p>
            </footer>
        </div>
    );
}

export default Footer
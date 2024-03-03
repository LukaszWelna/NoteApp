import React from 'react'
import './Footer.css'

const Footer = () => {

    const currentYear = new Date().getFullYear();

    return (
        <div className="container fixed-bottom">
            <footer className="py-3">
                <p className="text-center">Copyright &#169; {currentYear}</p>
            </footer>
        </div>
    );
}

export default Footer
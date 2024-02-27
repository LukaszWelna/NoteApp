import 'mdb-react-ui-kit/dist/css/mdb.min.css';
import { useEffect, useState } from 'react'
import './App.css'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import Header from './components/Header'
import Footer from './components/Footer'
import Dashboard from './components/Dashboard'
import Home from './components/Home'


function App() {

    /*const [token, setToken] = useState();

    if (!token) {
        return <Login setToken={setToken} />
    }*/
    
    return (
        <div className="wrapper">
            <Header />
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<Home/>} />
                    <Route path="/home" element={<Home/>} />
                    <Route path="/dashboard" element={<Dashboard/>} />
                </Routes>
            </BrowserRouter>
            <Footer />
        </div>
    );    
}

export default App;
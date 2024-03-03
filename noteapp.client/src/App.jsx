import 'mdb-react-ui-kit/dist/css/mdb.min.css';
import './App.css';
import Dashboard from './components/Dashboard/Dashboard';
import Home from './components/Home/Home';
import Layout from './components/Layout/Layout';
import Missing from './components/Missing/Missing';
import RequireAuth from './components/RequireAuth/RequireAuth';
import Registered from './components/Registered/Registered';
import { Route, Routes } from 'react-router-dom';

const App = () => {
    
    return (
        <div className="wrapper">
            <Routes>
                <Route path="/" element={<Layout />}>
                    {/* Public routes */}
                    <Route path="/" element={<Home />} />
                    <Route path="/home" element={<Home />} />
                    <Route path="/registered" element={<Registered />} />
                    {/* Protected routes */}
                    <Route element={<RequireAuth />}>
                        <Route path="/dashboard" element={<Dashboard />} />
                    </Route>
                    {/* Catch all 404 */}
                    <Route path="/*" element={<Missing />} />
                </Route>
            </Routes>
        </div>
    );    
}

export default App;
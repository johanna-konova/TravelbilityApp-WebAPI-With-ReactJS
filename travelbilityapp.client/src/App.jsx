import { Route, Routes } from "react-router-dom";

import AuthContextProvider from "./contexts/Auth-Context";

import Topbar from "./components/topbar/Topbar";
import Navbar from "./components/Navbar";
import Home from "./components/home/Home";
import RegisterForm from "./components/auth-forms/Register-Form";
import LoginForm from "./components/auth-forms/Login-Form";

function App() {
    return (
        <AuthContextProvider>
            <Topbar />

            <Navbar />

            <main>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/login" element={<LoginForm />} />
                    <Route path="/register" element={<RegisterForm />} />
                </Routes>
            </main>
        </AuthContextProvider>
    );
}

export default App;
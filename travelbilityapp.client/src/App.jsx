import { Route, Routes } from "react-router-dom";
import { Toaster } from "react-hot-toast";

import AuthGuard from "./components/common/Auth-Guard";

import AuthContextProvider from "./contexts/Auth-Context";

import Topbar from "./components/topbar/Topbar";
import Navbar from "./components/Navbar";
import Home from "./components/home/Home";
import RegisterForm from "./components/auth-forms/Register-Form";
import LoginForm from "./components/auth-forms/Login-Form";
import PropertyCreateEditForm from "./components/property/property-create-edit/Property-Create-Edit-Form";

function App() {
    return (
        <AuthContextProvider>
            <Toaster position="buttom-left" />

            <Topbar />

            <Navbar />

            <main>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/login" element={<LoginForm />} />
                    <Route path="/register" element={<RegisterForm />} />

                    <Route element={<AuthGuard />}>
                        <Route path="/list" element={<PropertyCreateEditForm />} />
                    </Route>
                </Routes>
            </main>
        </AuthContextProvider>
    );
}

export default App;
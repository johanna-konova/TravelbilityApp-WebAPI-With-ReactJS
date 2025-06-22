import { Route, Routes } from "react-router-dom";
import { Toaster } from "react-hot-toast";

import AuthGuard from "./components/common/Auth-Guard";
import PublisherGuard from "./components/common/Publisher-Guard";

import AuthContextProvider from "./contexts/Auth-Context";
import PropertyContextProvider from "./contexts/Property-Context";

import Topbar from "./components/topbar/Topbar";
import Navbar from "./components/Navbar";
import Home from "./components/home/Home";
import RegisterForm from "./components/auth-forms/Register-Form";
import LoginForm from "./components/auth-forms/Login-Form";
import PropertyDetailsContainer from "./components/property/property-details/Property-Details-Container";
import UserProperties from "./components/properties/user-properties/User-Properties";
import PropertyCreateEditForm from "./components/property/property-create-edit/Property-Create-Edit-Form";
import NotFound from "./components/not-found/Not-Found";

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

                    <Route path="/properties/:propertyId" element={
                        <PropertyContextProvider>
                            <PropertyDetailsContainer />
                        </PropertyContextProvider>
                    } />

                    <Route element={<AuthGuard />}>
                        <Route path="/list" element={<PropertyCreateEditForm />} />

                        <Route path="/edit/:propertyId" element={
                            <PropertyContextProvider>
                                <PublisherGuard>
                                    <PropertyCreateEditForm />
                                </PublisherGuard>
                            </PropertyContextProvider>
                        } />

                        <Route path="/my-properties" element={<UserProperties />} />
                    </Route>

                    <Route path="*" element={<NotFound />} />
                </Routes>
            </main>
        </AuthContextProvider >
  )
}

export default App

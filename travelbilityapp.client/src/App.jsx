import { Route, Routes } from "react-router-dom";
import { Toaster } from "react-hot-toast";

import AdminGuard from "./components/guards/AdminGuard";
import AuthGuard from "./components/guards/Auth-Guard";
import PublisherGuard from "./components/guards/Publisher-Guard";

import AuthContextProvider from "./contexts/Auth-Context";
import PropertyContextProvider from "./contexts/Property-Context";
import UserPropertyContextProvider from "./contexts/User-Property-Context";
import PropertyForEditContextProvider from "./contexts/Property-For-Edit-Context";

import PublicLayout from "./layouts/PublicLayout";
import AdminLayout from "./layouts/AdminLayout";

import Home from "./components/home/Home";
import RegisterForm from "./components/auth-forms/Register-Form";
import LoginForm from "./components/auth-forms/Login-Form";
import AllProperties from "./components/properties/all-properties/All-Properties";
import Contact from "./components/contact/Contact";
import OurMission from "./components/our-mission/Our-Mission";
import PropertyDetailsContainer from "./components/property/property-details/Property-Details-Container";
import UserProperties from "./components/properties/user-properties/User-Properties";
import UserProperty from "./components/property/user-property/User-Property";
import PropertyCreateEditForm from "./components/create-edit-forms/property-create-edit/Property-Create-Edit-Form";
import NotFound from "./components/not-found/Not-Found";
import Properties from "./components/admin/properties/Properties";

function App() {
    return (
        <AuthContextProvider>
            <Toaster position="buttom-left" />

            <Routes>
                <Route element={<PublicLayout />}>
                    <Route path="/" element={<Home />} />
                    <Route path="/login" element={<LoginForm />} />
                    <Route path="/register" element={<RegisterForm />} />

                    <Route path="/properties" element={<AllProperties />} />
                    <Route path="/our-mission" element={<OurMission />} />

                    <Route path="/properties/:propertyId" element={
                        <PropertyContextProvider>
                            <PropertyDetailsContainer />
                        </PropertyContextProvider>
                    } />

                    <Route element={<AuthGuard />}>
                        <Route path="/list" element={<PropertyCreateEditForm />} />

                        <Route path="/edit/:propertyId" element={
                            <PropertyForEditContextProvider>
                                <PublisherGuard>
                                    <PropertyCreateEditForm />
                                </PublisherGuard>
                            </PropertyForEditContextProvider>
                        } />

                        <Route path="/my-properties" element={<UserProperties />} />

                        <Route path="/my-properties/:propertyId" element={
                            <UserPropertyContextProvider>
                                <PublisherGuard>
                                    <UserProperty />
                                </PublisherGuard>
                            </UserPropertyContextProvider>
                        } />
                    </Route>

                    <Route path="/contact" element={<Contact />} />
                    <Route path="*" element={<NotFound />} />
                </Route>

                <Route path="/admin" element={
                    <AdminGuard>
                        <AdminLayout />
                    </AdminGuard>
                }>
                    <Route index element={<Properties />} />
                </Route>
            </Routes>
        </AuthContextProvider >
    )
}

export default App

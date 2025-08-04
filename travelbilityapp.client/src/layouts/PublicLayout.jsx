import { Outlet } from "react-router-dom";

import Topbar from "../components/topbar/Topbar";
import Navbar from "../components/Navbar";
import Footer from "../components/Footer";
import BackToTop from "../components/Back-To-Top";

export default function PublicLayout() {
    return (
        <>
            <Topbar />

            <Navbar />

            <main>
                <Outlet />
            </main>

            <Footer />

            <BackToTop />
        </>
    );
}

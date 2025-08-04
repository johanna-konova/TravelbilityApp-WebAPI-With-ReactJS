import { Outlet } from "react-router-dom";
import { Row } from "react-bootstrap";

import Topbar from "../components/admin/topbar/Topbar";
import Sidebar from "../components/admin/sidebar/Sidebar";

export default function AdminLayout() {
    return (
        <div className="admin-layout">
            <Topbar />

            <Row className="mt-4 ml-4">
                <Sidebar />

                <main>
                    <Outlet />
                </main>
            </Row>
        </div>
    );
}
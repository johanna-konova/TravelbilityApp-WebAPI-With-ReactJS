import { Link } from "react-router-dom";

import styles from "./Sidebar.module.css";

export default function Sidebar() {
    return (
        <nav className={`${styles["sidebar"]} ${styles["sidebar-offcanvas"]}`} id="sidebar">
            <ul className={styles["sidebar-nav"]}>
                <li className="sidebar-nav-item">
                    <Link className="sidebar-nav-link" to="/admin">
                        <i className="fas fa-hotel"></i> <span className="menu-title">Properties</span>
                    </Link>
                </li>
            </ul>
        </nav>
    );
}

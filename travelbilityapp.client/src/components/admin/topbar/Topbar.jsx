import { Link } from "react-router-dom";

import styles from "./Topbar.module.css";

export default function Topbar() {
    return (
        <nav className={styles["navbar"]}>
            <Link to="/" className="navbar-brand">
                <h3 className="m-0 text-primary">
                    <span className="text-dark">Travel</span>bility
                </h3>
            </Link>

            <Link to="/" type="button" className="btn btn-link">Back to the user preview <i className="fas fa-share"></i></Link>
        </nav>
    );
}
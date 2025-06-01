import { Link } from "react-router-dom";

export default function Navbar() {
    return (
        <>
            <div className="container-fluid position-relative nav-bar p-0">
                <div
                    className="container-lg position-relative p-0 px-lg-3"
                    style={{ zIndex: 9 }}
                >
                    <nav className="navbar navbar-expand-lg bg-light navbar-light shadow-lg py-3 py-lg-0 pl-3 pl-lg-5">
                        <Link to="/" className="navbar-brand">
                            <h1 className="m-0 text-primary">
                                <span className="text-dark">Travel</span>bility
                            </h1>
                        </Link>
                        <button
                            type="button"
                            className="navbar-toggler"
                            data-toggle="collapse"
                            data-target="#navbarCollapse"
                        >
                            <span className="navbar-toggler-icon" />
                        </button>
                        <div
                            className="collapse navbar-collapse justify-content-between px-3"
                            id="navbarCollapse"
                        >
                            <div className="navbar-nav ml-auto py-0">
                                <Link to="/properties" className="nav-item nav-link">
                                    Properties
                                </Link>
                                <Link to="/our-mission" className="nav-item nav-link">
                                    Our mission
                                </Link>
                                <Link to="/contact" className="nav-item nav-link">
                                    Contact
                                </Link>
                            </div>
                        </div>
                    </nav>
                </div>
            </div>
        </>
    )
};

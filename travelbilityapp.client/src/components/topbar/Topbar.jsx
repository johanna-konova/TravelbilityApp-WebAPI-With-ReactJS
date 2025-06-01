import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Button, Spinner } from "react-bootstrap";

import { useAuthContext } from "../../contexts/Auth-Context";

import { useLogout } from "../../hooks/use-auth";

import styles from './Topbar.module.css';

export default function Topbar() {
    const { isAuthenticated } = useAuthContext();
    const [isPending, setIsPending] = useState(false);
    const { logoutHandler } = useLogout();
    const navigate = useNavigate();

    async function logout() {
        setIsPending(true);

        await logoutHandler();
        
        setIsPending(false);
        navigate('/');
    }

    return (
        <>
            <div className="container-fluid bg-light pt-3 d-none d-lg-block">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-6 text-center text-lg-left mb-2 mb-lg-0">
                            <div className="d-inline-flex align-items-center">
                                {isAuthenticated
                                    ? <>
                                        {!isPending &&
                                            <Link to="/my-properties" type="button" className="btn btn-outline-success" >Manage my properties</Link>
                                        }

                                        <Button className={`${styles["logout-btn"]} btn-outline-success`} disabled={isPending} onClick={logout}>
                                        {isPending
                                            ? <>
                                                <Spinner
                                                    as="span"
                                                    animation="border"
                                                    size="sm"
                                                    role="status"
                                                    aria-hidden="true"
                                                />
                                                Singing out...
                                            </>
                                            : <span>Sing out</span>
                                        }</Button>
                                    </>
                                    : <>
                                        <Link to="/register" type="button" className="btn btn-outline-success">Sign up</Link>
                                        <Link to="/login" type="Link" className="btn btn-outline-success">Log in</Link>
                                    </>
                                }
                            </div>
                        </div>
                        <div className="col-lg-6 text-center text-lg-right">
                            <div className="d-inline-flex align-items-center">
                                {isPending
                                    ? <span className="btn btn-link">List your property</span>
                                    : <Link to="/list" type="button" className="btn btn-link">List your property</Link>
                                }
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
};
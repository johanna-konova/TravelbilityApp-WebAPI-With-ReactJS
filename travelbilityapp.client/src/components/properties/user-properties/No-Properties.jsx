import { Row } from 'react-bootstrap';
import { Link } from 'react-router-dom';

import styles from './User-Properties.module.css';

export default function NoProperties() {
    return (
        <>
            <div className={styles["no-properties"]}>
                <h1 className="text-center">Make your business accessible to
                    <span className="text-primary"> everyone</span>.
                </h1>

                <Row className="d-flex justify-content-center">
                    <Link to="/list" type="button" className="btn btn-primary py-3">
                        List your property now
                    </Link>
                </Row>
            </div>
        </>
    )
};

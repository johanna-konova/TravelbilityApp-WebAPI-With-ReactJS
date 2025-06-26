import { Col, Container, Row } from "react-bootstrap";

import styles from './Contact.module.css';

export default function Contact() {
    return (
        <>
            <Container fluid className="mt-5">
                <Container className={styles["contact-container"]}>
                    <h2 className="text-primary text-center">
                        Contact <span className="text-dark">us</span>
                    </h2>

                    <Row className="mt-3 justify-content-center">
                        <div className={styles["contacts-container"]}>
                            <Col sm={3} md={3} lg={3}>
                                <i className="fas fa-phone-alt"></i>
                                <span>+359 896 201253</span>
                            </Col>
                            <Col sm={3} md={3} lg={3}>
                                <i className="fas fa-at"></i>
                                <span>contact@travelbility.com</span>
                            </Col>
                            <Col sm={3} md={3} lg={3}>
                                <i className="fas fa-map-marker-alt"></i>
                                <span>45 Tsar Simeon St., Sofia 1000, Bulgaria</span>
                            </Col>
                        </div>
                    </Row>
                </Container>
            </Container>
        </>
    )
};

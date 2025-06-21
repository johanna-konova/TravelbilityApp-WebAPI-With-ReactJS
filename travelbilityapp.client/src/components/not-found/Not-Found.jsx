import React from 'react';
import { Link } from 'react-router-dom';
import { Container, Row, Col, Image } from 'react-bootstrap';

import styles from './Not-Found.module.css';

export default function NotFound() {
    return (
        <Container className="pt-5">
            <Row>
                <Col lg={5} md={5} sm={5}>
                    <Image
                        src="img/404-wheelchair.png"
                        style={{ objectFit: 'cover' }}
                        alt="About"
                        fluid
                    />
                </Col>
                <Col lg={7} md={7} sm={7} className="d-flex align-items-center">
                    <Row className={styles["not-found-text"]}>
                        <Image
                            src="img/404-text.png"
                            style={{ objectFit: 'cover', width: '40%', marginBottom: "1rem" }}
                            alt="About"
                            fluid
                        />
                        <div>
                            <span>Looks like the page is on vacation. Maybe you want to follow it?</span>
                        </div>

                        <Link to="/properties" type="button" className="btn btn-primary mt-4 py-2">
                            Explore properties now
                        </Link>
                    </Row>
                </Col>
            </Row>
        </Container>
    )
};

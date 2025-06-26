import { Container, Row, Col, Image, Card } from "react-bootstrap";
import { Link } from "react-router-dom";

import FeaturesContainer from "./features/Features-Container";

export default function OurMission() {
    return (
        <>
            <Container fluid className="mt-5">
                <Container>
                    <Row>
                        <Col lg={6} style={{ minHeight: 500 }}>
                            <div className="position-relative h-100">
                                <Image
                                    src="img/main.jpg"
                                    className="position-absolute w-100 h-100"
                                    style={{ objectFit: "cover" }}
                                    alt="About"
                                    fluid
                                />
                            </div>
                        </Col>
                        <Col lg={6} className="pt-5 pb-lg-5">
                            <Card className="about-text bg-white p-4 p-lg-5 my-lg-5">
                                <Card.Body>
                                    <h6 className="text-primary text-uppercase" style={{ letterSpacing: 5 }}>
                                        Our mission
                                    </h6>
                                    <Card.Title className="mb-3">
                                        We Provide Best Properties For Your Needs
                                    </Card.Title>
                                    <Card.Text>
                                        <div className="mb-3">
                                            The travel is so much more than just moving from point A to point B – it is a feeling of freedom, new horizons, and moments that fill the heart with joy. Everyone deserves the right to immerse themselves in the beauty of nature, feel the sea breeze, or find inspiration in new and unfamiliar places.
                                        </div>

                                        <div className="mb-3">
                                            But for thousands of people with disabilities, travel is often accompanied by worries. Will the hotel truly be accessible? Will it have the necessary amenities? Will I be able to reach the beach, the restaurant, or the sights I so long to visit?
                                        </div>

                                        <div>
                                            We are here to put an end to these concerns and make travel not only possible but also seamless.
                                        </div>

                                    </Card.Text>

                                    <Link to="/properties" type="button" className="btn btn-primary mt-2 py-2">
                                        Explore now
                                    </Link>
                                </Card.Body>
                            </Card>
                        </Col>
                    </Row>
                </Container>
            </Container>

            <FeaturesContainer />
        </>
    )
};

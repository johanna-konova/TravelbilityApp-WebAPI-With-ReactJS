import { Container, Row, Col, Image, Button, Card } from "react-bootstrap";
import SingleFeatureContainer from "./Single-Feature-Container";

export default function FeaturesContainer() {
    return (
        <Container fluid className="pb-5">
            <Container className="pb-5">
                <Row className="pt-5" style={{ fontSize: "0.9rem" }}>
                    <SingleFeatureContainer imageName="beliefs" title="Our beliefs" text="No one should choose between their travel dreams and the challenges they face." />
                    <SingleFeatureContainer imageName="platform" title="Our platform" text="You will find only properties that are verified and meet the highest accessibility standards." />
                    <SingleFeatureContainer imageName="vission" title="Our vision" text="Life is yours to live without limitations. We are here to make every journey possible." />
                </Row>
            </Container>
        </Container>
    )
};

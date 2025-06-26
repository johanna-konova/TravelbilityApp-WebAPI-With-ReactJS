import { Col, Image } from "react-bootstrap";

import styles from './../Our-Mission.module.css';

export default function SingleFeatureContainer({
    imageName,
    title,
    text
}) {
    return (
        <Col md={4} className="mb-4">
            <div className="d-flex">
                <div className={styles["our-mission-img-container"]}>
                <Image
                    src={`img/${imageName}.png`}
                    alt="About"
                    fluid
                />
                </div>
                <div className="d-flex flex-column">
                    <h5>{title}</h5>
                    <p className="m-0">{text}</p>
                </div>
            </div>
        </Col>
    )
};

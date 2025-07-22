import { useRef, useState } from "react";
import { Col, Image, Row } from "react-bootstrap";

import { useAuthContext } from "../../../contexts/Auth-Context";

import { generateStarIcons } from "../../../utils/property-utils";

import styles from './User-Property.module.css';
import ScrollToButton from "./Scroll-To-Button";

export default function UserPropertyDetails({ propertyData }) {
    const { id } = useAuthContext();

    const detailsRef = useRef(null);
    const [isDescriptionExpanded, setIsDescriptionExpanded] = useState(false);

    const descriptionMaxLengthToShow = 225;
    const shouldTruncate = propertyData.description.length > descriptionMaxLengthToShow;

    const descriptionToShow = isDescriptionExpanded
        ? propertyData.description
        : propertyData.description.slice(0, descriptionMaxLengthToShow) + (shouldTruncate ? '...' : '');

    const commonFacilityNames = [];
    const accessibilityNames = [];

    propertyData.facilities
        .forEach(f => f.isForAccessibility
            ? accessibilityNames.push(f.name)
            : commonFacilityNames.push(f.name));

    return (
        <>
            <ScrollToButton propertyName={propertyData.name} targetRef={detailsRef} />

            <div ref={detailsRef} className={styles["property-details-container"]}>
            <div className={`${styles["property-photos"]} ${styles["scrollbox"]}`}>
                <div className="">
                    <Image
                        src={propertyData.photoUrls[0]}
                        alt={`${propertyData.name}'s main photo`}
                        className={styles["first-image"]}
                    />
                </div>

                <div className={styles["no-first-images-container"]}>
                    {propertyData.photoUrls.map((photo, index) => (
                        <img
                            key={index}
                            src={photo}
                            alt={`${propertyData.name}'s photo`}
                            className={styles["no-first-image"]}
                        />
                    ))}
                </div>
            </div>

            <div className={`${styles["property-details"]} ${styles["scrollbox"]}`}>
                <div className="d-flex">
                    <p className={styles["property-type-name"]}>{propertyData.name}</p>
                    <span className={styles["stars"]}>{generateStarIcons(Number(propertyData.starsCount ?? 0))}</span>
                </div>

                <Row className="mb-1">
                    <Col sm={4} md={4} lg={4}>
                        <span className="text-primary">Type:</span> {propertyData.type.name}
                    </Col>

                    <Col sm={4} md={4} lg={4}>
                        <span className="text-primary">Check-in:</span> {propertyData.checkIn} h.
                    </Col>

                    <Col sm={4} md={4} lg={4}>
                        <span className="text-primary">Check-out:</span> {propertyData.checkOut} h.
                    </Col>
                </Row>

                <Row className="mt-3">
                    <Col>
                        <p className="mb-0 text-primary">Address:</p>
                        <span>{propertyData.address}</span>
                    </Col>
                </Row>

                <Row className="mt-3">
                    <Col>
                        <p className="mb-0 text-primary">Description:</p>
                        <span>{descriptionToShow} </span>
                        {shouldTruncate &&
                            <span
                                className={styles["read-all-less"]}
                                onClick={() => setIsDescriptionExpanded(previousIsDescriptionExpandedValue => !previousIsDescriptionExpandedValue)}
                            >
                                [{isDescriptionExpanded ? "Read less" : "Read all"}]
                            </span>
                        }
                    </Col>
                </Row>

                <div className="mt-3">
                    <p className="mb-0 text-primary">Facilities:</p>
                    <span>{commonFacilityNames.join(", ")}.</span>
                </div>

                {accessibilityNames &&
                    <div className="mt-3">
                        <p className="mb-0 text-primary">Accessibility:</p>
                        <span>{accessibilityNames.join(", ")}.</span>
                    </div>
                }
            </div>
        </div>
        </>
    );
};
import { Col, Image, Row } from "react-bootstrap";

import { useBasicGetFetch } from "../../hooks/use-basic-get-fetch";
import { getById } from "../../services/roomsService";

import styles from './Room-Details-Modal.module.css';

export default function RoomDetailsModal({
    roomId,
    propertyId,
    pickRoomHandler,
    closeModalHandler,
}) {
    const { data: roomData } = useBasicGetFetch(() => getById(roomId, propertyId), {});

    return (
        <div className="custom-modal-overlay">
            <div className="custom-modal-content">
                <div className="x-close">
                    <span onClick={closeModalHandler}>X</span>
                </div>

                <div className={styles["room-details-container"]}>
                    <div className={`${styles["room-photos"]} ${styles["scrollbox"]}`}>
                        <div className="">
                            {roomData?.photoUrls?.length > 0 &&
                                <Image
                                    src={roomData?.photoUrls[0]}
                                    alt={`Room photo`}
                                    className={styles["first-image"]}
                                />
                            }
                        </div>

                        <div className={styles["no-first-images-container"]}>
                            {roomData?.photoUrls?.map((url, index) => (
                                <img
                                    key={index}
                                    src={url}
                                    alt={`Thumbnail ${index + 1}`}
                                    className={styles["no-first-image"]}
                                />
                            ))}
                        </div>
                    </div>

                    <div className={`${styles["room-details"]} ${styles["scrollbox"]}`}>
                        <div className={styles["room-type-name"]}>
                            <span>{roomData?.roomTypeName}</span>
                        </div>

                        <Row className="mb-1">
                            <Col sm={6} md={6} lg={6}>
                                <span className="text-primary">Maximum guest count:</span> {roomData?.maxGuests || "-"}
                            </Col>

                            <Col sm={6} md={6} lg={6}>
                                <span className="text-primary">Price per night:</span> {roomData?.pricePerNight || "-"} EUR
                            </Col>
                        </Row>

                        <Row className="mb-1">
                            <Col sm={6} md={6} lg={6}>
                                <span className="text-primary">Main bed type:</span> {roomData?.mainBedTypeName || "-"}
                            </Col>

                            <Col sm={6} md={6} lg={6}>
                                <span className="text-primary">Room size:</span> {roomData?.size || "-"} м²
                            </Col>
                        </Row>

                        {pickRoomHandler !== undefined &&
                            <Row className="mb-1">
                                <Col>
                                    <span className="text-primary">Number of Rooms of this type:</span> {roomData?.numberOfUnits || "-"}
                                </Col>
                            </Row>
                        }

                        <Row className="mt-3">
                            <Col>
                                <p className="mb-0 text-primary">Description:</p>
                                <span>{roomData?.description}</span>
                            </Col>
                        </Row>

                        <div className="mt-3">
                            <p className="mb-0 text-primary">Facilities:</p>
                            {roomData.commonFacilityNames?.map((an, i) =>
                                <span key={i} className="facilities">
                                    <i className="fas fa-check"></i> {an}{i === roomData.commonFacilityNames.length - 1 ? "." : ", "}
                                </span>
                            )}
                        </div>

                        {roomData.accessibilityNames?.length > 0 &&
                            <div className="mt-3">
                                <p className="mb-0 text-primary">Accessibility:</p>
                                {roomData.accessibilityNames.map((an, i) =>
                                    <span key={i} className="facilities">
                                        <i className="fab fa-accessible-icon text-primary"></i> {an}{i === roomData.accessibilityNames.length - 1 ? "." : ", "}
                                    </span>
                                )}
                            </div>
                        }
                    </div>
                </div>
                {pickRoomHandler !== undefined &&
                    <div className="room-user-actions">
                        <span className="edit" title="Edit" onClick={() => pickRoomHandler(roomId, "edit")}> <i className="fas fa-edit"></i></span>
                        <span className="delete" title="Delete" onClick={() => pickRoomHandler(roomId, "delete")}><i className="fas fa-trash"></i></span>
                    </div>
                }
            </div>
        </div>
    );
};
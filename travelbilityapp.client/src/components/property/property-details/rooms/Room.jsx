import { useState } from 'react';
import { Button, Col, Row } from 'react-bootstrap';

import RoomDetailsModal from '../../../common/Room-Details-Modal';

import styles from '../Property-Details.module.css';

export default function Room({ propertyId, roomData }) {
    const [isShowedRoomDetailsModal, setIsShowedRoomDetailsModal] = useState(false);

    return (
        <Row className={styles["room-container"]}>
            <Col sm={6} md={6} lg={6} className={styles["column"]}>
                <div className={styles["room-type-name"]}>{roomData.roomTypeName}</div>

                {roomData.accessibilityNames.length > 0 &&
                    <>
                        <div>
                            {roomData.accessibilityNames.map((an, i) =>
                                <span key={i} className="facilities">
                                    <i className="fab fa-accessible-icon text-primary"></i> {an}{i === roomData.accessibilityNames.length - 1 ? "." : ", "}
                                </span>
                            )}
                        </div>

                        <div className={styles["line"]}></div>
                    </>
                }

                <div>
                    {roomData.commonFacilityNames.map((cfn, i) =>
                        <span key={i} className="facilities">
                            <i className="fas fa-check"></i> {cfn}{i === roomData.commonFacilityNames.length - 1 ? "." : ", "}
                        </span>
                    )}
                </div>

                <div className="d-flex justify-content-center mt-3">
                    <Button className={styles["view-details-btn"]}>
                        <span onClick={() => setIsShowedRoomDetailsModal(true)}>View details</span>
                    </Button>
                </div>
            </Col>

            <Col sm={3} md={3} lg={3} className={styles["basic-info-container"]}>
                <div>
                    <div>
                        <span>Maximum guest count:</span> {roomData.maxGuests}
                    </div>

                    <div>
                        <span>Main bed type:</span> {roomData.mainBedTypeName}
                    </div>

                    <div>
                        <span>Size:</span> {roomData.size} m²
                    </div>
                </div>
            </Col>

            <Col sm={3} md={3} lg={3} className={styles["price-container"]}>
                <div>
                    <span>{roomData.pricePerNight} EUR</span>
                    <span>/ per a night</span>
                </div>
            </Col>

            {isShowedRoomDetailsModal &&
                <RoomDetailsModal
                    roomId={roomData.id}
                    propertyId={propertyId}
                    closeModalHandler={() => setIsShowedRoomDetailsModal(false)}
                />
            }
        </Row>
    );
};
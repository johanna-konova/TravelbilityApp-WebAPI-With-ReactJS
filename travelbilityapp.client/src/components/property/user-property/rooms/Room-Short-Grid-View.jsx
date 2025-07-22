import { Col, Image, Row } from 'react-bootstrap';

import styles from '../User-Property.module.css';

export default function RoomShortGridView({
    roomData,
    pickRoomHandler
}) {
    return (
        <div className={`${styles["room-preview"]}`}>
            <div className={`${styles["room-photo-preview"]}`}>
                <div className={styles["room-type-label"]}>
                    <i className="fas fa-door-open"></i> {roomData.roomTypeName}
                </div>
                <div className={styles["room-photo-thumbnail"]}>
                    <Image
                        src={roomData.mainPhotoUrl}
                        alt={`${roomData.roomTypeName}'s main photo`}
                    />
                </div>
            </div>
            <div className={styles["room-info-preview"]}>
                <Row>
                    <Col lg={6} className="pl-4 pr-0">
                        <i className="fas fa-bed text-primary"></i> {roomData.mainBedTypeName}
                    </Col>
                    <Col lg={6} className="pl-4 pr-0">
                        <i className="fas fa-hand-holding-usd text-primary"></i> {roomData.pricePerNight} EUR
                    </Col>
                </Row>
                <div className={styles["line"]}></div>
            </div>
            <div className={styles["room-user-actions"]}>
                <span className={styles["view"]} title="View details" onClick={() => pickRoomHandler(roomData.id, "view")}><i className="fas fa-eye"></i></span>
                <span className={styles["edit"]} title="Edit" onClick={() => pickRoomHandler(roomData.id, "edit")}> <i className="fas fa-edit"></i></span>
                <span className={styles["delete"]} title="Delete" onClick={() => pickRoomHandler(roomData.id, "delete")}><i className="fas fa-trash"></i></span>
            </div>
        </div>
    )
};

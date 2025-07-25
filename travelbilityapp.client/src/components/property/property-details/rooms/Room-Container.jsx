import Room from './Room';

import styles from '../Property-Details.module.css';

export default function RoomContainer({
    areAccessibleRooms,
    roomsData,
    propertyId
}) {
    const labelName = `${areAccessibleRooms === false ? "Non-a" : "A"}ccessible Rooms`;
    const className = areAccessibleRooms ? "accessible-room" : "non-accessible-room";

    return (
        <div className={styles[className]}>
            <p className={styles["label"]}>{labelName} ({roomsData.length})</p>

            {roomsData.map((rd, i) => <Room key={i} propertyId={propertyId} roomData={rd} />)}
        </div>
    );
}
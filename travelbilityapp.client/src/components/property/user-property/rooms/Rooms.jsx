import { useEffect, useState } from 'react';
import { Button, Row, Spinner } from 'react-bootstrap';
import toast from "react-hot-toast";

import { useBasicGetFetch } from '../../../../hooks/use-basic-get-fetch';
import { getAll as getRooms } from '../../../../services/roomsService';
import { sendForApproval } from '../../../../services/propertiesService';

import RoomShortGridView from './Room-Short-Grid-View';
import AddRoomModal from '../../../create-edit-forms/room-create-edit/Room-Create-Edit-Form-Modal';
import RoomDetailsModal from '../../../common/Room-Details-Modal';
import DeleteRoomModal from '../../../modals/Delete-Room-Modal';

import styles from '../User-Property.module.css';

export default function Rooms({ propertyId, inputPropertyStatus }) {
    const {
        data: roomsData,
        updateData: updateRoomsDataHandler,
        removeDataElement: deleteRoomByIdHandler
    } = useBasicGetFetch(() => getRooms(propertyId));

    const [propertyStatus, setPropertyStatus] = useState(inputPropertyStatus);
    const [isAddRoomModalShowed, setIsAddRoomModalShowed] = useState(false);
    const [pickedRoomInfo, setPickedRoomInfo] = useState(undefined);
    const [hasAccessibleRoom, setHasAccessibleRoom] = useState(true);
    const [isSendingForApproval, setIsSendingForApproval] = useState(false);

    useEffect(() => {
        setHasAccessibleRoom(false);

        if (roomsData.some(rd => rd.isAccessibleRoom === true)) {
            setHasAccessibleRoom(true);
        }
    }, [roomsData]);

    const hideAddRoomModalHandler = () => {
        updatePickedRoomData();
        setIsAddRoomModalShowed(false);
    }

    const updatePickedRoomData = (id, status) =>
        setPickedRoomInfo(id !== undefined ? { id, status } : undefined);

    const addHandler = (roomData) => {
        updateRoomsDataHandler(roomData);
        hideAddRoomModalHandler();
        _updatePropertyStatusAndSetToastError(roomData.id);
    }

    const deleteHandler = (id) => {
        deleteRoomByIdHandler(id);
        _updatePropertyStatusAndSetToastError(id);
    };

    const sendForApprovalHandler = async (id) => {
        setIsSendingForApproval(true);

        try {
            debugger
            await sendForApproval(id);

            setHasAccessibleRoom(true);
            toast.success(`Thank you for wanting to include your property in our cause! It will be reviewed by an administrator very soon.`, { duration: "750", icon: "🎉", position: "buttom-right" });
        } catch (e) {
            toast.error("Please, add at least one Accessible Room so you can send your property for approval.", { position: "buttom-right" });
        } finally {
            setIsSendingForApproval(false);
        }
    }

    const _updatePropertyStatusAndSetToastError = (roomId) => {
        const wasAccessibleRoom = roomsData.some(rd => rd.id === roomId && rd.isAccessibleRoom === true);

        if (propertyStatus === "Published" &&
            wasAccessibleRoom === true &&
            roomsData.filter(rd => rd.id !== roomId).every(rd => rd.isAccessibleRoom === false)) {
            setPropertyStatus("Saved");
            toast.error("Your property was unpublushed. Please, add at least one Accessible Room so you can republish it again.", { duration: "500", position: "buttom-right" });
        }
    };

    return (
        <>
            {hasAccessibleRoom === false &&
                <div className="mb-4 text-center">
                    <p className={styles["add-room-text"]}>Only one step left to send your property for approval. Add at least one Accessible Room now.</p>
                </div>
            }

            <Row className="justify-content-center">
                {roomsData.map(rd => (
                    <RoomShortGridView
                        key={rd.id}
                        roomData={rd}
                        pickRoomHandler={updatePickedRoomData}
                    />
                ))}
                <Row className={styles["upload-room-box"]} onClick={() => setIsAddRoomModalShowed(true)}>
                    <div>
                        <i className={`fas fa-door-open ${styles["door"]}`}></i>
                    </div>
                    <div>
                        <p className={styles["add-first-room"]}>Add a room</p>
                    </div>
                </Row>
            </Row>

            <Row className="mb-5 justify-content-end">
                <Button type="button" variant="primary" disabled={hasAccessibleRoom === false || isSendingForApproval} onClick={() => sendForApprovalHandler(propertyId)}>
                    {isSendingForApproval
                        ? <>
                            <Spinner
                                as="span"
                                animation="border"
                                size="sm"
                                role="status"
                                aria-hidden="true"
                            />
                            Sending for Approval...
                        </>
                        : <span>Send for Approval</span>
                    }
                </Button>
            </Row>

            {(isAddRoomModalShowed || (pickedRoomInfo && pickedRoomInfo.status === "edit")) &&
                <AddRoomModal
                    roomId={pickedRoomInfo?.id}
                    propertyId={propertyId}
                    addRoomHandler={addHandler}
                    closeModalHandler={hideAddRoomModalHandler}
                />
            }

            {pickedRoomInfo && pickedRoomInfo.status === "view" &&
                <RoomDetailsModal
                    propertyId={propertyId}
                    roomId={pickedRoomInfo.id}
                    pickRoomHandler={updatePickedRoomData}
                    closeModalHandler={updatePickedRoomData}
                />
            }

            {pickedRoomInfo && pickedRoomInfo.status === "delete" &&
                <DeleteRoomModal
                    id={pickedRoomInfo.id}
                    typeName={roomsData?.find(rd => rd.id === pickedRoomInfo.id)?.roomTypeName}
                    propertyId={propertyId}
                    isModalShowed={true}
                    viewRoomDetailsHandler={() => updatePickedRoomData(pickedRoomInfo.id, "view")}
                    closeModalHandler={updatePickedRoomData}
                    deleteHandler={deleteHandler}
                />
            }
        </>
    )
};

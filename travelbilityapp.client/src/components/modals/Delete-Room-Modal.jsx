import { useState } from 'react';
import { Button, Modal, Spinner } from 'react-bootstrap';
import toast from "react-hot-toast";

import { deleteById } from '../../services/roomsService';

import styles from './Modals.module.css';

export default function DeleteRoomModal({
    id,
    typeName,
    propertyId,
    isModalShowed,
    viewRoomDetailsHandler,
    closeModalHandler,
    deleteHandler
}) {
    const [isDeleting, setIsDeleting] = useState(false);

    const deleteRoom = async (id, propertyId) => {
        try {
            setIsDeleting(true);
            await deleteById(id, propertyId);
            toast.success("You have successfully deleted the room.");
            deleteHandler(id);
            // eslint-disable-next-line no-unused-vars
        } catch (error) {
            toast.error("An unexpected error occurred. Please try again or contact us.");
        } finally {
            closeModalHandler();
        }
    };

    return (
        <Modal show={isModalShowed} centered>
            <Modal.Header>
                <Button className={styles["x-icon"]} disabled={isDeleting} onClick={closeModalHandler}>
                    <i className="fas fa-times text-primary"></i>
                </Button>
            </Modal.Header>
            <Modal.Body className={styles["modal-body"]}>
                Are you sure you want to delete the 
                <div>
                    {isDeleting
                        ? <span className={styles["deleting"]}>{typeName}</span>
                        : <span
                            className={styles["room-to-delete"]}
                            onClick={viewRoomDetailsHandler}
                        >
                            {typeName}
                        </span>
                    }
                    ?
                </div>

                <div className={styles["warning-message"]}>
                    *This action is irreversible and will permanently remove the room.
                    <div>
                        Please confirm if you're sure.
                    </div>
                </div>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="primary" disabled={isDeleting} onClick={closeModalHandler}>Cancel</Button>
                <Button className={styles["agree-btn"]} variant="danger" disabled={isDeleting} onClick={() => deleteRoom(id, propertyId)}>
                    {isDeleting
                        ? <>
                            <Spinner
                                as="span"
                                animation="border"
                                size="sm"
                                role="status"
                                aria-hidden="true"
                            />
                            Deleting...
                        </>
                        : <span>Delete</span>
                    }
                </Button>
            </Modal.Footer>
        </Modal>
    )
};

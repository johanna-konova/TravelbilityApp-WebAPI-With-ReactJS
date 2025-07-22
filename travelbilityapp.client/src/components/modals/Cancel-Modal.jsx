import { Button, Modal } from 'react-bootstrap';

import styles from './Modals.module.css';

export default function CancelModal({
    isModalShowed,
    closeModalHandler,
    discardChangesHandler,
}) {
    return (
        <Modal show={isModalShowed} centered>
            <Modal.Header>
                <Button className={styles["x-icon"]} onClick={closeModalHandler}>
                    <i className="fas fa-times text-primary"></i>
                </Button>
            </Modal.Header>
            <Modal.Body className={styles["modal-body"]}>
                You may have unsaved changes. Are you sure you want to leave?
            </Modal.Body>
            <Modal.Footer>
                <Button variant="primary" onClick={closeModalHandler}>No, go back</Button>
                <Button className={styles["agree-btn"]} variant="danger" onClick={discardChangesHandler}>Yes, discard changes</Button>
            </Modal.Footer>
        </Modal>
    )
};

import { Button, Modal, Spinner } from 'react-bootstrap';
import { Link } from 'react-router-dom';

import styles from './Modals.module.css';

export default function DeleteModal({
    propertyId,
    propertyName,
    isModalShowed,
    isDeleting,
    closeModalHandler,
    deleteHandler
}) {
    return (
        <Modal show={isModalShowed} centered>
            <Modal.Header>
                <Button className={styles["x-icon"]} disabled={isDeleting} onClick={closeModalHandler}>
                    <i className="fas fa-times text-primary"></i>
                </Button>
            </Modal.Header>
            <Modal.Body className={styles["modal-body"]}>
                Are you sure you want to delete
                <div>
                    {isDeleting
                        ? <span className={styles["deleting"]}>{propertyName}</span>
                        : <Link to={`/properties/${propertyId}`} className="text-dark">{propertyName}</Link>
                    }
                    ?
                </div>

                <div className={styles["warning-message"]}>
                    *This action is irreversible and will permanently remove the item.
                    <div>
                        Please confirm if you're sure.
                    </div>
                </div>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="primary" disabled={isDeleting} onClick={closeModalHandler}>
                    Cancel
                </Button>
                <Button className={styles["agree-btn"]} variant="danger" disabled={isDeleting} onClick={deleteHandler}>
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

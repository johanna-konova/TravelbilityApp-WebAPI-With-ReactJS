import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import toast from "react-hot-toast";

import { usePropertiesContext } from '../../contexts/Properties-Context';

import { deleteById } from '../../services/propertiesService';

import DeleteModal from '../modals/Delete-Modal';

import styles from './User-Actions.module.css';

export default function UserActions({
    _id,
    name,
    hasPaddingBottom
}) {
    const [isDeleteModalShowed, setIsDeleteModalShowed] = useState(false);
    const [isDeleting, setIsDeleting] = useState(false);
    const navigate = useNavigate();
    const { deletePropertyByIdHandler } = usePropertiesContext();

    const hideDeleteModalHandler = () => setIsDeleteModalShowed(false);

    const deleteHandler = async (id) => {
        try {
            setIsDeleting(true);
            /*await Promise.all([deleteById(id), deletePropertyFacilities(id)]);
            deletePropertyByIdHandler === undefined
                ? navigate(-1)
                : deletePropertyByIdHandler(id);
            toast.success("You have successfully deleted the property.");*/
        // eslint-disable-next-line no-unused-vars
        } catch (error) {
            toast.error("An unexpected error occurred. Please try again or contact us.");
        } finally {
            hideDeleteModalHandler();
        }
    }

    return (
        <>
            <div className={`${styles["user-actions-container"]} ${hasPaddingBottom ? "pb-3" : ""}`}>
                <Link to={`/edit/${_id}`} className={styles["edit"]} title="Edit">
                    <i className="fas fa-edit m-1"></i>
                </Link>
                <Button className={styles["delete"]} title="Delete" onClick={() => setIsDeleteModalShowed(true)}>
                    <i className="fas fa-trash m-1"></i>
                </Button>
            </div>

            <DeleteModal
                propertyId={_id}
                propertyName={name}
                isModalShowed={isDeleteModalShowed}
                isDeleting={isDeleting}
                closeModalHandler={hideDeleteModalHandler}
                deleteHandler={() => deleteHandler(_id)}
            />
        </>
    )
};

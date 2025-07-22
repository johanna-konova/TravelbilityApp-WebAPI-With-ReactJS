import { Card } from 'react-bootstrap';
import { Link } from 'react-router-dom';

import { generateStarIcons } from '../../../utils/property-utils';

import UserActions from '../../user-actions/User-Actions';

import styles from './User-Properties.module.css';

export default function PropertyShortGridView({ propertyData }) {
    const isPublished = propertyData.status === "Published";

    return (
        <Card className={styles["property-card"]}>
            <div className="position-relative">
                <div
                    className={styles["status-label"]}
                    style={{ backgroundColor: isPublished ? "#7AB730" : "#c82333" }}
                >
                    {isPublished ? "Published" : "Unpublished"}
                </div>
                <Link to={`/properties/${propertyData.id}`}>
                    <Card.Img variant="top" src={propertyData.mainPhotoUrl} />
                </Link>
            </div>
            <Card.Body className="pb-0">
                <Link to={`/properties/${propertyData.id}`}>
                    <Card.Title className="mb-0">{propertyData.name}</Card.Title>
                </Link>
                <div className={styles["stars"]}>{generateStarIcons(Number(propertyData.starsCount))}</div>
                <Card.Text className="mt-2">
                    <i className="fas fa-map-marker-alt"></i> <span>{propertyData.address}</span>
                </Card.Text>
                <Card.Text>{propertyData.description.slice(0, 150) + '...'}</Card.Text>
            </Card.Body>

            {isPublished === false &&
                <Link className={styles["hasNoAccessibleRoom"]} to={`/my-properties/${propertyData.id}`}>*Add at least one Accessible Room to publish your property.</Link>
            }

            <div className={styles["user-actions"]}>
                <Link to={`/my-properties/${propertyData.id}`}>
                    <span className="ml-3 text-primary">View as publisher</span>
                </Link>
                <UserActions
                    id={propertyData.id}
                    name={propertyData.name}
                />
            </div>
        </Card>
    )
};

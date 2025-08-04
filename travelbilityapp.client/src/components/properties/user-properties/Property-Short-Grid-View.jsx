import { Card } from 'react-bootstrap';
import { Link } from 'react-router-dom';

import { generateStarIcons } from '../../../utils/property-utils';

import UserActions from '../../user-actions/User-Actions';

import styles from './User-Properties.module.css';

const statusColors = {
    Saved: "#17a2b8",
    Pending: "#ffb700",
    Published: "#7AB730",
    Rejected: "#c82333",
};

export default function PropertyShortGridView({ propertyData }) {
    const status = propertyData.status;
    const isPublished = status === "Published";

    return (
        <Card className={styles["property-card"]}>
            <div className="position-relative">
                <div
                    className={styles["status-label"]}
                    style={{ backgroundColor: statusColors[status] }}
                >
                    {status}
                </div>
                {isPublished
                    ? <Link to={`/properties/${propertyData.id}`}>
                        <Card.Img variant="top" src={propertyData.mainPhotoUrl} />
                    </Link>
                    : <Card.Img variant="top" src={propertyData.mainPhotoUrl} />
                }
            </div>
            <Card.Body className="pb-0">
                {isPublished
                    ? <Link to={`/properties/${propertyData.id}`}>
                        <Card.Title className="mb-0">{propertyData.name}</Card.Title>
                    </Link>
                    : <Card.Title className="mb-0">{propertyData.name}</Card.Title>
                }
                <div className={styles["stars"]}>{generateStarIcons(Number(propertyData.starsCount))}</div>
                <Card.Text className="mt-2">
                    <i className="fas fa-map-marker-alt"></i> <span>{propertyData.address}</span>
                </Card.Text>
                <Card.Text>{propertyData.description.slice(0, 150) + '...'}</Card.Text>
            </Card.Body>

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

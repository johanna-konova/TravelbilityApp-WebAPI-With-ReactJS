import { Card } from 'react-bootstrap';
import { Link } from 'react-router-dom';

import { generateStarIcons } from '../../../utils/property-utils';

import UserActions from '../../user-actions/User-Actions';

import styles from './User-Properties.module.css';

export default function PropertyShortGridView({ propertyData }) {
    return (
        <>
            <Card className={styles["property-card"]}>
                <Link to={`/properties/${propertyData.id}`}>
                    <Card.Img variant="top" src={propertyData.mainPhoto} />
                </Link>
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
                
                <UserActions
                    id={propertyData.id}
                    name={propertyData.name}
                    hasPaddingBottom={true}
                />
            </Card>
        </>
    )
};
